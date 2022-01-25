using System.ComponentModel;
using TestXamarin2022.ViewModels;
using Xamarin.Forms;

namespace TestXamarin2022.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}