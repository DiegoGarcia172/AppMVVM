using SQLite;

namespace AppMVVM.Models
{
    public class Categoria
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}