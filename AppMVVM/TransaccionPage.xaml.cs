using AppMVVM.ViewModels;
using AppMVVM.Models;
using SQLite;

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
                Monto = double.Parse(MontoEntry.Text),
                Tipo = TipoEntry.Text,
                CategoriaId = categoriaId,
                Fecha = FechaEntry.Text, 
                EsRecurrente = int.Parse(EsRecurrenteEntry.Text),
                Frecuencia = FrecuenciaEntry.Text 
            });
        }

        private async void AgregarCategoria_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CategoriaPage(_viewModel.DatabaseContext.Connection));
        }

        public Entry MontoEntry { get; set; }
        public Entry TipoEntry { get; set; }
        public Entry FechaEntry { get; set; }
        public Entry EsRecurrenteEntry { get; set; }
        public Entry FrecuenciaEntry { get; set; }
    }
}