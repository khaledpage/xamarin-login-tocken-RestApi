﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kalksi.Models
{
    public class Constants
    {
        public static bool IsDev = true;
        public static Color BackgroundColor = Color.WhiteSmoke;
        public static Color MainTextColor = Color.Black;
        public static int LoginIconHeight = 200;

        // Login

        // Kada je god moguće koristiti URL sa HTTPS protokolom!
        // Ovo je mockup URL, zamijeniti s nečim smislenim inače

        public static string LoginUrl = "https://test.com/api/Auth/Login";
        public static string NoInternetText = "No Internet Connection";
    }
}
