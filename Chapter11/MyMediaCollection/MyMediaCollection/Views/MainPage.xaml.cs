using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyMediaCollection.ViewModels;
using MyMediaCollection.Helpers;

namespace MyMediaCollection.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;

        public MainPage()
        {
            ViewModel = App.HostContainer.Services.GetService<MainViewModel>();
            this.InitializeComponent();
            Current = this;
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

        public void NotifyUser(string message, InfoBarSeverity severity, bool isOpen = true)
        {
            if (DispatcherQueue.HasThreadAccess)
            {
                UpdateStatus(message, severity, isOpen);
            }
            else
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    UpdateStatus(message, severity, isOpen);
                });
            }
        }

        private void UpdateStatus(string message, InfoBarSeverity severity, bool isOpen)
        {
            notifyInfoBar.Message = message;
            notifyInfoBar.IsOpen = isOpen;
            notifyInfoBar.Severity = severity;
        }

        public void NotificationReceived(NotificationShared.Notification notification)
        {
            var text = $"{notification.Originator}; Action: {notification.Action}";

            if (notification.HasInput)
            {
                if (string.IsNullOrWhiteSpace(notification.Input))
                    text += "; No input received";
                else
                    text += $"; Input received: {notification.Input}";
            }

            if (DispatcherQueue.HasThreadAccess)
                DisplayMessageDialog(text);
            else
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    DisplayMessageDialog(text);
                });
            }
        }

        private void DisplayMessageDialog(string message)
        {
            ContentDialog notifyDialog = new()
            {
                XamlRoot = this.XamlRoot,
                Title = "Notification received",
                Content = message,
                CloseButtonText = "Ok"
            };

            notifyDialog.ShowAsync();
        }
    }
}