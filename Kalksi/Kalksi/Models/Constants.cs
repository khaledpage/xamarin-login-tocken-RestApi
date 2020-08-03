using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kalksi.Models
{
    public class Constants
    {

        //allgemeine Einstellungen
        public static bool IsDev = true;
        public static Color BackgroundColor = Color.WhiteSmoke;
        public static Color MainTextColor = Color.Black;
        public static int LoginIconHeight = 200;

        public static string LoginUrl = "https://test.com/api/Auth/Login";
        public static string NoInternetText = "No Internet Connection";
    }
}
