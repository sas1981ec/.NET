using Foundation;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestXamarin2022.Dependencies;
using TestXamarin2022.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqlLiteClient))]
namespace TestXamarin2022.iOS
{
    public class SqlLiteClient : IDataBase
    {
        public SQLiteConnection GetConecction()
        {
            throw new NotImplementedException();
        }
    }
}