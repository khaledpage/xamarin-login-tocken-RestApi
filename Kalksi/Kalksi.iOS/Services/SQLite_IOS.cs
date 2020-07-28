using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Foundation;
using Kalksi.iOS.Services;
using Kalksi.Services;
using ObjCRuntime;
using SQLite;
using UIKit;
using Xamarin.Forms;
[assembly: Xamarin.Forms.Dependency(typeof(SQLite_IOS))]
namespace Kalksi.iOS.Services
{
    class SQLite_IOS : ISQlite
    {
        public SQLiteConnection GetConnection()
        {
            var filename = "Testdb.db3";
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libPath = Path.Combine(docPath, "..", "library");
            var path = Path.Combine(libPath, filename);
            var connection = new SQLite.SQLiteConnection(path);
            return connection;

        }
    }
}