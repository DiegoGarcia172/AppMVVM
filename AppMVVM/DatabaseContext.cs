using SQLite;
using System;
using System.IO;
using AppMVVM.Models;

public class DatabaseContext
{
    public SQLiteConnection Connection { get; private set; }

    public DatabaseContext()
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AppMVVM.db3");
        Connection = new SQLiteConnection(dbPath);
        Connection.CreateTable<Categoria>();
        Connection.CreateTable<Transaccion>();

        if (Connection.Table<Categoria>().Count() == 0)
        {
            Connection.Insert(new Categoria { Nombre = "Comida" });
            Connection.Insert(new Categoria { Nombre = "Transporte" });
            Connection.Insert(new Categoria { Nombre = "Salario" });
            Connection.Insert(new Categoria { Nombre = "Otros" });
        }
    }
}