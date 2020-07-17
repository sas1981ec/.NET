using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using MusicStore.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MusicStore.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Track> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        private bool _estaOcupado;

        public ItemsViewModel()
        {
            Title = "Test Xamarin Sipecom";
            Items = new ObservableCollection<Track>();
        }

        public bool EstaOcupado
        {
            get
            {
                return _estaOcupado;
            }
            set
            {
                _estaOcupado = value;
                OnPropertyChanged();
            }
        }

        public async Task<Item> GetItemsFromApiAsync()
        {
            EstaOcupado = true;
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://api20200714230727.azurewebsites.net/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("Track");
            var httpContent = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<List<Track>>(httpContent);
            foreach (var item in json)
                Items.Add(item);
            EstaOcupado = false;
            return new Item();
        }
    }
}