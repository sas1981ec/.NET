using System.ComponentModel;
using Xamarin.Forms;
using MusicStore.Models;
using MusicStore.ViewModels;

namespace MusicStore.Views
{
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();
            viewModel = new ItemDetailViewModel(new Track());
            BindingContext = viewModel;
        }
    }
}