using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMediaCollection.Enums;
using MyMediaCollection.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyMediaCollection.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string selectedMedium;
        [ObservableProperty]
        private ObservableCollection<MediaItem> items;
        [ObservableProperty]
        private IList<string> mediums;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
        private MediaItem selectedMediaItem;

        private ObservableCollection<MediaItem> allItems;
        private int additionalItemCount = 1;

        public MainViewModel()
        {
            PopulateData();
        }

        public void PopulateData()
        {
            var cd = new MediaItem
            {
                Id = 1,
                Name = "Classical Favorites",
                MediaType = ItemType.Music,
                MediumInfo = new Medium { Id = 1, MediaType = ItemType.Music, Name = "CD" }
            };

            var book = new MediaItem
            {
                Id = 2,
                Name = "Classic Fairy Tales",
                MediaType = ItemType.Book,
                MediumInfo = new Medium { Id = 2, MediaType = ItemType.Book, Name = "Book" }
            };

            var bluRay = new MediaItem
            {
                Id = 3,
                Name = "The Mummy",
                MediaType = ItemType.Video,
                MediumInfo = new Medium { Id = 3, MediaType = ItemType.Video, Name = "Blu Ray" }
            };

            Items = new ObservableCollection<MediaItem>
            {
                cd,
                book,
                bluRay
            };

            allItems = new ObservableCollection<MediaItem>(Items);

            Mediums = new List<string>
            {
                "All",
                nameof(ItemType.Book),
                nameof(ItemType.Music),
                nameof(ItemType.Video)
            };

            SelectedMedium = Mediums[0];
        }

        partial void OnSelectedMediumChanged(string value)
        {
            Items.Clear();

            foreach (var item in allItems)
            {
                if (string.IsNullOrWhiteSpace(value) ||
                    value == "All" ||
                    value == item.MediaType.ToString())
                {
                    Items.Add(item);
                }
            }
        }

        [RelayCommand]
        public void AddEdit()
        {
            // Note this is temporary until
            // we use a real data source for items.
            const int startingItemCount = 3;

            var newItem = new MediaItem
            {
                Id = startingItemCount + additionalItemCount,
                Location = LocationType.InCollection,
                MediaType = ItemType.Music,
                MediumInfo = new Medium { Id = 1, MediaType = ItemType.Music, Name = "CD" },
                Name = $"CD {additionalItemCount}"
            };

            allItems.Add(newItem);
            Items.Add(newItem);
            additionalItemCount++;
        }

        [RelayCommand(CanExecute = nameof(CanDeleteItem))]
        public void Delete()
        {
            allItems.Remove(SelectedMediaItem);
            Items.Remove(SelectedMediaItem);
        }

        private bool CanDeleteItem() => SelectedMediaItem != null;
    }
}