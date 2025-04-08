using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.IO;
using System.Text;
using AppMVVM.Models;
using Microcharts;
using System.Collections.Generic;

namespace AppMVVM.ViewModels
{
    public class TransaccionViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseContext _dbContext;
        private ObservableCollection<Transaccion> _transacciones;
        private ObservableCollection<Categoria> _categorias;
        private Categoria _categoriaSeleccionada;

        public ObservableCollection<Transaccion> Transacciones
        {
            get => _transacciones;
            set
            {
                _transacciones = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Categoria> Categorias
        {
            get => _categorias;
            set
            {
                _categorias = value;
                OnPropertyChanged();
            }
        }

        public Categoria CategoriaSeleccionada
        {
            get => _categoriaSeleccionada;
            set
            {
                _categoriaSeleccionada = value;
                OnPropertyChanged();
            }
        }

        public DatabaseContext DatabaseContext => _dbContext;

        public TransaccionViewModel(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            Transacciones = new ObservableCollection<Transaccion>(_dbContext.Connection.Table<Transaccion>().ToList());
            Categorias = new ObservableCollection<Categoria>(_dbContext.Connection.Table<Categoria>().ToList());
        }

        public void AgregarTransaccion(Transaccion transaccion)
        {
            _dbContext.Connection.Insert(transaccion);
            Transacciones.Add(transaccion);
        }

        public void EliminarTransaccion(Transaccion transaccion)
        {
            _dbContext.Connection.Delete(transaccion);
            Transacciones.Remove(transaccion);
        }

        public async void ExportarTransaccionesCSV(string filePath)
        {
            try
            {
                var csv = new StringBuilder();
                csv.AppendLine("Monto,Tipo,Categoria,Fecha,EsRecurrente,Frecuencia");

                foreach (var transaccion in Transacciones)
                {
                    var categoria = Categorias.FirstOrDefault(c => c.Id == transaccion.CategoriaId);
                    csv.AppendLine($"{transaccion.Monto},{transaccion.Tipo},{categoria?.Nombre},{transaccion.Fecha},{transaccion.EsRecurrente},{transaccion.Frecuencia}");
                }

                File.WriteAllText(filePath, csv.ToString());

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Exportar Transacciones",
                    File = new ShareFile(filePath)
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al exportar CSV: {ex.Message}");
            }
        }

        public Chart GenerarGrafica(string filtroTipo = null, int? filtroCategoriaId = null)
        {
            var transaccionesFiltradas = Transacciones.AsEnumerable();

            if (!string.IsNullOrEmpty(filtroTipo))
            {
                transaccionesFiltradas = transaccionesFiltradas.Where(t => t.Tipo == filtroTipo);
            }

            if (filtroCategoriaId.HasValue)
            {
                transaccionesFiltradas = transaccionesFiltradas.Where(t => t.CategoriaId == filtroCategoriaId);
            }

            var entries = transaccionesFiltradas.Select(t => new ChartEntry((float)t.Monto)
            {
                Label = t.Fecha,
                ValueLabel = t.Monto.ToString(),
                Color = t.Tipo == "Ingreso" ? SkiaSharp.SKColor.Parse("#2c3e50") : SkiaSharp.SKColor.Parse("#77d065")
            }).ToList();

            return new LineChart() { Entries = entries };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}