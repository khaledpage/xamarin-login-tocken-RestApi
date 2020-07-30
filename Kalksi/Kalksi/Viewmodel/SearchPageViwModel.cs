using Kalksi.Models;
using Kalksi.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kalksi.Viewmodel
{
    class SearchPageViwModel : INotifyPropertyChanged
    {
        private List<comment> searchResultList;
        public List<comment> AllResultHolder { get; set; }

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

        public void SearchTo(string text)
     
        {
            SearchResultList = AllResultHolder.Where(o => o.body.Contains(text) ).ToList();
      
        }

        public SearchPageViwModel()
        {
            api = App.RestService;
           
        }

        public async Task callAllResultAsync()
        {
         
            SearchResultList = await api.GetListOfResponses<comment>(baseUrl);
            AllResultHolder = SearchResultList;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task OrderByID()
        {

            
            SearchResultList = SearchResultList.OrderBy(o => o.id).ToList();
        }

        public async Task OrderByName()
        {


            SearchResultList = SearchResultList.OrderBy(o => o.name).ToList();
        }
        public async Task OrderByBody()
        {


            SearchResultList = SearchResultList.OrderBy(o => o.body).ToList();
        }
    }
}
