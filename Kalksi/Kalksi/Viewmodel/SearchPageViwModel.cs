using Kalksi.Models;
using Kalksi.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kalksi.Viewmodel
{
    class SearchPageViwModel : INotifyPropertyChanged
    {
        private List<comment> searchResultList;

        public List<comment> SearchResultList{

            get { return searchResultList; }

            set
            {
                searchResultList = value;
                OnPropertyChanged();
            }

    }
        RestService api;
        private string baseUrl = "https://jsonplaceholder.typicode.com/comments";
        private ObservableCollection<comment> searchResultList1;

        public event PropertyChangedEventHandler PropertyChanged;

        public SearchPageViwModel()
        {
            api = App.RestService;
           
        }

        public async System.Threading.Tasks.Task callAllResultAsync()
        {

            var tmp = await api.GetListOfResponses<comment>(baseUrl);



            SearchResultList = new List<comment>(tmp);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
