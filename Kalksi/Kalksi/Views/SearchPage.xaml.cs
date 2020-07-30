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
            SortPicker.SelectedIndexChanged += SortPicker_SelectedIndexChanged;
            searhBar.SearchButtonPressed += SearhBar_SearchButtonPressed;
            searhBar.TextChanged +=  SearhBar_TextChangedAsync;
        }

        private  void SearhBar_TextChangedAsync(object sender, TextChangedEventArgs e)
       {
            //Fetch all Results if searchbar ist Empty
            if (searhBar.Text== "")
            {
                 service.callAllResultAsync();
            }
        }

        private void SearhBar_SearchButtonPressed(object sender, EventArgs e)
        {
            service.SearchTo(searhBar.Text);
        }

        private  async void SortPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SortPicker.SelectedIndex == -1)
            {
               
            }
            else
            {
                string sortTo = SortPicker.Items[SortPicker.SelectedIndex];
                // oder einfach SortPicker.SelectedItem
               
                if (sortTo.Equals("ID"))
                {
                  
                    await service.OrderByID();
                }
                else if (sortTo.Equals("Name"))
                {
                    await service.OrderByName();
                }
                else if (sortTo.Equals("Body"))
                {
                    await service.OrderByBody();


                }
                


            }
        }

        private void ResultList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

    

        protected override async void OnAppearing()
        {
            

            await service.callAllResultAsync();
            activityIndicator.IsVisible = false;

            //foreach (comment item in service.SearchResultList) // Loop through List with foreach
            //{
            //    Console.WriteLine(item.body);
            //}

            base.OnAppearing();
        }
    }
}