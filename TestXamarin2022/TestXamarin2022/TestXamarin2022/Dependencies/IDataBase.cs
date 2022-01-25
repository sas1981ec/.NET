using System;
using System.Collections.Generic;
using System.Text;

namespace TestXamarin2022.Dependencies
{
    public interface IDataBase
    {
        SQLite.SQLiteConnection GetConecction();
    }
}
