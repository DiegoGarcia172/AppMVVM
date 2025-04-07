using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using AppMVVM.Models;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}