using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using MyMediaCollection.ViewModels;

namespace MyMediaCollection.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ItemDetailsPage : Page
    {
        public ItemDetailsPage()
        {
            ViewModel = App.HostContainer.Services.GetService<ItemDetailsViewModel>();

            this.InitializeComponent();

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            // Load the user setting
            string haveExplainedSaveSetting = localSettings.Values[nameof(SavingTip)] as string;

            // If the user has not seen the save tip, display it

            if (!bool.TryParse(haveExplainedSaveSetting, out bool result) || !result)
            {
                SavingTip.IsOpen = true;

                // Save the teaching tip setting
                localSettings.Values[nameof(SavingTip)] = "true";
            }
        }

        public ItemDetailsViewModel ViewModel;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var itemId = (int)e.Parameter;

            if (itemId > 0)
            {
                await ViewModel.InitializeItemDetailDataAsync(itemId);
            }
        }
    }
}