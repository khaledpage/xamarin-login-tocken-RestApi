using Kalksi.Models;
using Kalksi.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kalksi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        SearchPageViwModel service;

        public SearchPage()
        {
            InitializeComponent();

            service = new SearchPageViwModel();
            this.BindingContext = service;
        }

        private void ResultList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

    

        protected override async void OnAppearing()
        {
            

            await service.callAllResultAsync();

            //foreach (comment item in service.SearchResultList) // Loop through List with foreach
            //{
            //    Console.WriteLine(item.body);
            //}

            base.OnAppearing();
        }
    }
}