using System.IO;
using Semana7.Droid;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(ClienteAndroid))]
namespace Semana7.Droid
{
    public class ClienteAndroid : DataBase
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            var baseDatos = Path.Combine(ruta, "uisrael.db3");
            return new SQLiteAsyncConnection(baseDatos);
        }
    }
}