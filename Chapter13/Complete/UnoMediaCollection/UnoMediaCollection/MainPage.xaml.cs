using UnoMediaCollection.ViewModels;

namespace UnoMediaCollection
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            ViewModel = App.HostContainer.Services.GetService<MainViewModel>();
            this.InitializeComponent();
        }

        public MainViewModel ViewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.Back)
            {
                ViewModel.PopulateData();
            }
        }
    }
}