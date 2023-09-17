using UnoMediaCollection.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnoMediaCollection
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