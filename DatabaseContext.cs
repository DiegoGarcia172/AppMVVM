using SQLite;
using System;
using System.IO;

public class DatabaseContext
{
    public SQLiteConnection Connection { get; private set; }

    public DatabaseContext()
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AppProyectoMVVM.db3");
        Connection = new SQLiteConnection(dbPath);
        Connection.CreateTable<Transaccion>(); // Crea la tabla Transaccion si no existe
    }
}