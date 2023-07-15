using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyMediaCollection.ViewModels;

namespace MyMediaCollection.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            ViewModel = App.HostContainer.Services.GetService<MainViewModel>();
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var mainWindow = (Application.Current as App)?.Window as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.SetPageTitle("Home");
            }
        }

        public MainViewModel ViewModel;
    }
}