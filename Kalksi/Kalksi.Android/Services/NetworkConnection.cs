using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Kalksi.Droid.Services;
using Kalksi.Services;

[assembly: Xamarin.Forms.Dependency(typeof(NetworkConnection))]

namespace Kalksi.Droid.Services
{
    public class NetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }

    
        public void CheckNetworkConnection()
        {


            var ConnMan = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            var ActiveNetworkInfo = ConnMan.ActiveNetworkInfo;
            if (ActiveNetworkInfo != null && ActiveNetworkInfo.IsConnectedOrConnecting)  //ist veraltet 
            {
                IsConnected = true;
            }

            else
            {
                IsConnected = false;
            }

        }
    }
}