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
        }

        public ItemDetailsViewModel ViewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var itemId = (int)e.Parameter;

            if (itemId > 0)
            {
                ViewModel.InitializeItemDetailData(itemId);
            }
        }
    }
}