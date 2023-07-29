using Microsoft.Windows.AppNotifications.Builder;
using Microsoft.Windows.AppNotifications;
using MyMediaCollection.Views;

namespace MyMediaCollection.Helpers
{
    public class ToastWithAvatar
    {
        public const int ScenarioId = 1;
        public const string ScenarioName = "Local Toast with Image";

        public static bool SendToast()
        {
            var appNotification = new AppNotificationBuilder()
                .AddArgument("action", "ToastClick")
                .AddArgument(NotificationShared.scenarioTag, ScenarioId.ToString())
                .SetAppLogoOverride(new System.Uri($"file://{App.GetFullPathToAsset("Square150x150Logo.scale-200.png")}"), AppNotificationImageCrop.Circle)
                .AddText(ScenarioName)
                .AddText("This is a notification message.")
                .AddButton(new AppNotificationButton("Open App")
                    .AddArgument("action", "OpenApp")
                    .AddArgument(NotificationShared.scenarioTag, ScenarioId.ToString()))
                .BuildNotification();

            AppNotificationManager.Default.Show(appNotification);

            // If notification is sent, it will have an Id. Success.
            return appNotification.Id != 0;
        }

        public static void NotificationReceived(AppNotificationActivatedEventArgs notificationActivatedEventArgs)
        {
            var notification = new NotificationShared.Notification
            {
                Originator = ScenarioName,
                Action = notificationActivatedEventArgs.Arguments["action"]
            };
            MainPage.Current.NotificationReceived(notification);
            App.ToForeground();
        }
    }
}