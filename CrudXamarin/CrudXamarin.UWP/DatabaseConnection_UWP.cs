using SQLite;
using Xamarin.Forms;
using CrudXamarin.UWP;
using Windows.Storage;
using System.IO;
[assembly: Dependency(typeof(DatabaseConnection_UWP))]
namespace CrudXamarin.UWP
{
    public class DatabaseConnection_UWP : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "BioDataDb.db3";
            var path = Path.Combine(ApplicationData.
              Current.LocalFolder.Path, dbName);
            return new SQLiteConnection(path);
        }
    }
}