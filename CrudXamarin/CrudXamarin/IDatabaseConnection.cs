using System;
using System.Collections.Generic;
using System.Text;

namespace CrudXamarin
{
    public interface IDatabaseConnection
    {
        
        SQLite.SQLiteConnection DbConnection();
    }
}
