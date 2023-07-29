using Microsoft.UI.Xaml.Controls;
using MyMediaCollection.Views;

namespace MyMediaCollection.Helpers
{
    public class NotificationShared
    {
        public const string scenarioTag = "scenarioId";

        public struct Notification
        {
            public string Originator;
            public string Action;
            public bool HasInput;
            public string Input;
        };

        public static void CouldNotSendToast()
        {
            MainPage.Current.NotifyUser("Could not send toast", InfoBarSeverity.Error);
        }

        public static void ToastSentSuccessfully()
        {
            MainPage.Current.NotifyUser("Toast sent successfully!", InfoBarSeverity.Success);
        }

        public static void AppLaunchedFromNotification()
        {
            MainPage.Current.NotifyUser("App launched from notifications", InfoBarSeverity.Informational);
        }

        public static void NotificationReceived()
        {
            MainPage.Current.NotifyUser("Notification received", InfoBarSeverity.Informational);
        }

        public static void UnrecognizedToastOriginator()
        {
            MainPage.Current.NotifyUser("Unrecognized Toast Originator or Unknown Error", InfoBarSeverity.Error);
        }
    }
}