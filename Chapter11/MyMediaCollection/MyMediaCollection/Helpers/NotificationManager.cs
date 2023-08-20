using Microsoft.Windows.AppNotifications;
using System;
using System.Collections.Generic;

namespace MyMediaCollection.Helpers
{
    internal class NotificationManager
    {
        private bool isRegistered;
        private Dictionary<int, Action<AppNotificationActivatedEventArgs>> notificationHandlers;

        public NotificationManager()
        {
            isRegistered = false;

            // When adding new a scenario, be sure to add its notification handler here.
            notificationHandlers = new Dictionary<int, Action<AppNotificationActivatedEventArgs>>
            {
                { ToastWithAvatar.ScenarioId, ToastWithAvatar.NotificationReceived },
                { ToastWithText.ScenarioId, ToastWithText.NotificationReceived }
            };
        }

        ~NotificationManager()
        {
            Unregister();
        }

        public void Unregister()
        {
            if (isRegistered)
            {
                AppNotificationManager.Default.Unregister();
                isRegistered = false;
            }
        }

        public void Init()
        {
            AppNotificationManager notificationManager = AppNotificationManager.Default;

            // Add handler before calling Register.
            notificationManager.NotificationInvoked += OnNotificationInvoked;
            notificationManager.Register();

            isRegistered = true;
        }

        public void ProcessLaunchActivationArgs(AppNotificationActivatedEventArgs notificationActivatedEventArgs)
        {
            DispatchNotification(notificationActivatedEventArgs);
            NotificationShared.AppLaunchedFromNotification();
        }

        private bool DispatchNotification(AppNotificationActivatedEventArgs notificationActivatedEventArgs)
        {
            var scenarioId = notificationActivatedEventArgs.Arguments[NotificationShared.scenarioTag];
            if (scenarioId.Length != 0)
            {
                try
                {
                    notificationHandlers[int.Parse(scenarioId)](notificationActivatedEventArgs);
                    return true;
                }
                catch
                {
                    // No matching NotificationHandler for scenarioId.
                    return false;
                }
            }
            else
            {
                // No scenarioId provided
                return false;
            }
        }

        public void OnNotificationInvoked(object sender, AppNotificationActivatedEventArgs notificationActivatedEventArgs)
        {
            NotificationShared.NotificationReceived();

            if (!DispatchNotification(notificationActivatedEventArgs))
            {
                NotificationShared.UnrecognizedToastOriginator();
            }
        }
    }
}