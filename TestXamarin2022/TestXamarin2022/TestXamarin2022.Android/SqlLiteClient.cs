using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestXamarin2022.Dependencies;
using TestXamarin2022.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqlLiteClient))]
namespace TestXamarin2022.Droid
{
    public class SqlLiteClient : IDataBase
    {
        public SQLiteConnection GetConecction()
        {
            String bbddfile = "Test.db3";
            String rutadocumentos = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            String path = Path.Combine(rutadocumentos, bbddfile);
            SQLite.SQLiteConnection cn = new SQLite.SQLiteConnection(path);
            return cn;
        }
    }
}