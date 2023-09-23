using Microsoft.Windows.AppNotifications.Builder;
using Microsoft.Windows.AppNotifications;
using MyMediaCollection.Views;

namespace MyMediaCollection.Helpers
{
    public class ToastWithText
    {
        public const int ScenarioId = 2;
        public const string ScenarioName = "Local Toast with Image and Text Entry";
        const string textboxReplyId = "textboxReply";

        public static bool SendToast()
        {
            var appNotification = new AppNotificationBuilder()
                .AddArgument("action", "ToastClick")
                .AddArgument(NotificationShared.scenarioTag, ScenarioId.ToString())
                .SetAppLogoOverride(new System.Uri($"file://{App.GetFullPathToAsset("Square150x150Logo.scale-200.png")}"), AppNotificationImageCrop.Circle)
                .AddText(ScenarioName)
                .AddText("This is a notification message.")
                .AddTextBox(textboxReplyId, "Enter a reply", "Reply box")
                .AddButton(new AppNotificationButton("Reply")
                    .AddArgument("action", "Reply")
                    .AddArgument(NotificationShared.scenarioTag, ScenarioId.ToString())
                    .SetInputId(textboxReplyId))
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
                Action = notificationActivatedEventArgs.Arguments["action"],
                HasInput = true,
                Input = notificationActivatedEventArgs.UserInput[textboxReplyId]
            };
            MainPage.Current.NotificationReceived(notification);
            App.ToForeground();
        }
    }
}