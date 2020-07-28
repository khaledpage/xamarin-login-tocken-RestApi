﻿
using Kalksi.Models;
using Kalksi.Services;
using Kalksi.Views;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kalksi
{
    public partial class App : Application
    {

        static TokenDatabaseController tokenDb;
        static UserDatabaseController userDb;
        static RestService restService;
        private static Label labelScreen;
        private static bool hasInternet;
        private static Page currentPage;
        private static Timer timer;
        private static bool noInternetShow;


        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static UserDatabaseController UserDatabase
        {
            get
            {
                if (userDb == null) { userDb = new UserDatabaseController(); }
                return userDb;
            }

        }


        /////
        public static TokenDatabaseController TotkenDatabase
        {
            get
            {
                if (tokenDb == null) { tokenDb = new TokenDatabaseController(); }
                return tokenDb;
            }

        }
        
        public static RestService RestService
        {
            get
            {
                if (restService == null) { restService = new RestService(); }
                return restService;
            }
        }

        public static void StartCheckIfInternet(Label label, Page page)
        {
            labelScreen = label;
            label.Text = Constants.NoInternetText;
            label.IsVisible = false;
            hasInternet = true;
            currentPage = page;
            if (timer == null)
            {
                timer = new Timer((e) => {
                    CheckIfInternetOverTime();
                }, null, 10, (int)TimeSpan.FromSeconds(3).TotalMilliseconds);
            }
        }

        private static void CheckIfInternetOverTime()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            if (!networkConnection.IsConnected)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    if (hasInternet)
                    {
                        if (!noInternetShow)
                        {
                            hasInternet = false;
                            labelScreen.IsVisible = true;
                            await ShowDisplayAlert();
                        }
                    }
                });
            }

            else
            {
                Device.BeginInvokeOnMainThread(() => {
                    hasInternet = true;
                    labelScreen.IsVisible = false;
                });
            }
        }

        // Instantno provjerava ima li interneta - primjerice pritisak na botun provjere interneta
        public static async Task<bool> CheckIfInternet()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            return networkConnection.IsConnected;
        }

        public static async Task<bool> CheckIfInternetAlertAsync()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            if (!networkConnection.IsConnected)
            {
                if (!noInternetShow)
                {
                    await ShowDisplayAlert();
                }
                return false;
            }
            return true;
        }

        private static async Task ShowDisplayAlert()
        {
            noInternetShow = false;
            await currentPage.DisplayAlert("Internet", "Device has no internet, please reconnect", "OK");
            noInternetShow = false;
        }




        /////////





    }
}
