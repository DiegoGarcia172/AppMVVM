using AppMVVM.ViewModels;
using AppMVVM.Models;
using SQLite;
using System;
using System.IO;
using Microcharts.Maui;

namespace AppMVVM
{
    public partial class TransaccionPage : ContentPage
    {
        private TransaccionViewModel _viewModel;

        public TransaccionPage(TransaccionViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        private void AgregarTransaccion_Clicked(object sender, System.EventArgs e)
        {
            int? categoriaId = null;

            if (_viewModel.CategoriaSeleccionada != null)
            {
                categoriaId = ((Categoria)_viewModel.CategoriaSeleccionada).Id;
            }

            _viewModel.AgregarTransaccion(new Transaccion
            {
                Monto = double.Parse(MontoEntryControl.Text), 
                Tipo = TipoEntryControl.Text, 
                CategoriaId = categoriaId,
                Fecha = FechaEntryControl.Text, 
                EsRecurrente = int.Parse(EsRecurrenteEntryControl.Text), 
                Frecuencia = FrecuenciaEntryControl.Text 
            });
        }

        private async void AgregarCategoria_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CategoriaPage(_viewModel.DatabaseContext.Connection));
        }

        private async void ExportarTransacciones_Clicked(object sender, EventArgs e)
        {
            var fileName = $"Transacciones_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
            _viewModel.ExportarTransaccionesCSV(filePath);
        }

        private void GenerarGrafica_Clicked(object sender, EventArgs e)
        {
            var filtroTipo = TipoFiltroPickerControl.SelectedItem?.ToString();
            int? filtroCategoriaId = null;

            if (CategoriaFiltroPickerControl.SelectedItem is Categoria categoria)
            {
                filtroCategoriaId = categoria.Id;
            }
            GraficaViewControl.Chart = _viewModel.GenerarGrafica(filtroTipo, filtroCategoriaId);
        }

        public Entry MontoEntryControl { get; set; } 
        public Entry TipoEntryControl { get; set; }
        public Entry FechaEntryControl { get; set; } 
        public Entry EsRecurrenteEntryControl { get; set; }
        public Entry FrecuenciaEntryControl { get; set; } 
        public Picker TipoFiltroPickerControl { get; set; }
        public Picker CategoriaFiltroPickerControl { get; set; }
        public ChartView GraficaViewControl { get; set; }
    }
}