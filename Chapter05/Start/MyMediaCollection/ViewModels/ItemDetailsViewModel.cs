using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMediaCollection.Enums;
using MyMediaCollection.Interfaces;
using MyMediaCollection.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyMediaCollection.ViewModels
{
    public partial class ItemDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> locationTypes = new();
        [ObservableProperty]
        private ObservableCollection<string> mediums = new();
        [ObservableProperty]
        private ObservableCollection<string> itemTypes = new();
        private int _itemId;
        [ObservableProperty]
        private string itemName;
        [ObservableProperty]
        private string selectedMedium;
        [ObservableProperty]
        private string selectedItemType;
        [ObservableProperty]
        private string selectedLocation;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private bool isDirty;
        private int _selectedItemId = -1;
        protected INavigationService _navigationService;
        protected IDataService _dataService;

        public ItemDetailsViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            PopulateLists();
        }

        public void InitializeItemDetailData(int itemId)
        {
            _selectedItemId = itemId;

            PopulateExistingItem(_dataService);
            IsDirty = false;
        }

        private void PopulateExistingItem(IDataService dataService)
        {
            if (_selectedItemId > 0)
            {
                var item = _dataService.GetItem(_selectedItemId);
                Mediums.Clear();

                foreach (string medium in dataService.GetMediums(item.MediaType).Select(m => m.Name))
                    Mediums.Add(medium);

                _itemId = item.Id;
                ItemName = item.Name;
                SelectedMedium = item.MediumInfo.Name;
                SelectedLocation = item.Location.ToString();
                SelectedItemType = item.MediaType.ToString();
            }
        }

        private void PopulateLists()
        {
            ItemTypes.Clear();
            foreach (string iType in Enum.GetNames(typeof(ItemType)))
                ItemTypes.Add(iType);

            LocationTypes.Clear();
            foreach (string lType in Enum.GetNames(typeof(LocationType)))
                LocationTypes.Add(lType);

            Mediums = new ObservableCollection<string>();
        }

        [RelayCommand(CanExecute = nameof(CanSaveItem))]
        private void Save()
        {
            MediaItem item;

            if (_itemId > 0)
            {
                item = _dataService.GetItem(_itemId);

                item.Name = ItemName;
                item.Location = (LocationType)Enum.Parse(typeof(LocationType), SelectedLocation);
                item.MediaType = (ItemType)Enum.Parse(typeof(ItemType), SelectedItemType);
                item.MediumInfo = _dataService.GetMedium(SelectedMedium);

                _dataService.UpdateItem(item);
            }
            else
            {
                item = new MediaItem
                {
                    Name = ItemName,
                    Location = (LocationType)Enum.Parse(typeof(LocationType), SelectedLocation),
                    MediaType = (ItemType)Enum.Parse(typeof(ItemType), SelectedItemType),
                    MediumInfo = _dataService.GetMedium(SelectedMedium)
                };

                _dataService.AddItem(item);
            }

            _navigationService.GoBack();
        }

        private bool CanSaveItem()
        {
            return IsDirty;
        }

        partial void OnItemNameChanged(string value)
        {
            IsDirty = true;
        }

        partial void OnSelectedMediumChanged(string value)
        {
            IsDirty = true;
        }

        partial void OnSelectedItemTypeChanged(string value)
        {
            IsDirty = true;
            Mediums.Clear();

            if (!string.IsNullOrWhiteSpace(value))
            {
                foreach (string med in _dataService.GetMediums((ItemType)Enum.Parse(typeof(ItemType), SelectedItemType)).Select(m => m.Name))
                    Mediums.Add(med);
            }
        }

        partial void OnSelectedLocationChanged(string value)
        {
            IsDirty = true;
        }

        [RelayCommand]
        private void Cancel()
        {
            _navigationService.GoBack();
        }
    }
}