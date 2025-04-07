using AppMVVM.ViewModels;

namespace AppMVVM
{
    public partial class MainPage : ContentPage
    {
        private TransaccionViewModel _viewModel;

        public MainPage(TransaccionViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TransaccionPage(_viewModel));
        }
    }
}