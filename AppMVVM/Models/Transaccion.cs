using SQLite;
using System;
namespace AppMVVM.Models
{
    public class Transaccion
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Monto { get; set; }
        public string Tipo { get; set; } 
        public int? CategoriaId { get; set; }
        public string Fecha { get; set; }
        public int EsRecurrente { get; set; }
        public string Frecuencia { get; set; }
    }
}