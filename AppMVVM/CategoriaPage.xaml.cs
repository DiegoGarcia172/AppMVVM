using AppMVVM.Models;
using SQLite;

namespace AppMVVM
{
    public partial class CategoriaPage : ContentPage
    {
        private SQLiteConnection _connection;

        public CategoriaPage(SQLiteConnection connection)
        {
            InitializeComponent();
            _connection = connection;
        }

        private void AgregarCategoria_Clicked(object sender, System.EventArgs e)
        {
            _connection.Insert(new Categoria { Nombre = NombreCategoriaEntry.Text });
            Navigation.PopAsync();
        }
    }
}