using MusicStore.Models;

namespace MusicStore.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Track Track { get; set; }
        public ItemDetailViewModel(Track item = null)
        {
            Title = item.Name;
            Track = item;
        }
    }
}
