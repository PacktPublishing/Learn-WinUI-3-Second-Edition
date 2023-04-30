using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyMediaCollection.Enums;
using MyMediaCollection.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMediaCollection
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private bool _isLoaded;
        private IList<MediaItem> _items { get; set; }
        private IList<string> _mediums { get; set; }
        private IList<MediaItem> _allItems { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();
            ItemList.Loaded += ItemList_Loaded;
            ItemFilter.Loaded += ItemFilter_Loaded;
        }

        private void ItemFilter_Loaded(object sender, RoutedEventArgs e)
        {
            var filterCombo = (ComboBox)sender;
            PopulateData();
            filterCombo.ItemsSource = _mediums;
            filterCombo.SelectedIndex = 0;
            ItemFilter.SelectionChanged += ItemFilter_SelectionChanged;
        }

        private void ItemFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var updatedItems = (from item in _allItems
                                where string.IsNullOrWhiteSpace(ItemFilter.SelectedValue.ToString())
                                    || ItemFilter.SelectedValue.ToString() == "All"
                                    || ItemFilter.SelectedValue.ToString() == item.MediaType.ToString()
                                select item).ToList();
            ItemList.ItemsSource = updatedItems;
        }

        private void ItemList_Loaded(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            PopulateData();
            listView.ItemsSource = _items;
        }

        private void PopulateData()
        {
            if (_isLoaded) return;

            _isLoaded = true;

            var cd = new MediaItem
            {
                Id = 1,
                Name = "Classical Favorites",
                MediaType = Enums.ItemType.Music,
                MediumInfo = new Medium
                {
                    Id = 1,
                    MediaType = ItemType.Music,
                    Name = "CD"
                }
            };
            var book = new MediaItem
            {
                Id = 2,
                Name = "Classic Fairy Tales",
                MediaType = ItemType.Book,
                MediumInfo = new Medium
                {
                    Id = 2,
                    MediaType = ItemType.Book,
                    Name = "Book"
                }
            };
            var bluRay = new MediaItem
            {
                Id = 3,
                Name = "The Mummy",
                MediaType = ItemType.Video,
                MediumInfo = new Medium
                {
                    Id = 3,
                    MediaType = ItemType.Video,
                    Name = "Blu Ray"
                }
            };
            _items = new List<MediaItem>
            {
                cd,
                book,
                bluRay
            };
            _allItems = new List<MediaItem>
            {
                cd,
                book,
                bluRay
            };
            _mediums = new List<string>
            {
                "All",
                nameof(ItemType.Book),
                nameof(ItemType.Music),
                nameof(ItemType.Video)
            };
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "My Media Collection",
                Content = "Adding items to the collection is not yet supported.",
                CloseButtonText = "OK"
            };
            dialog.XamlRoot = this.Content.XamlRoot;
            await dialog.ShowAsync();
        }
    }
}