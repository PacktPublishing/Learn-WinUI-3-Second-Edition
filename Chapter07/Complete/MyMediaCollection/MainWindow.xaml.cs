using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using WinRT.Interop;
using Microsoft.UI.Xaml.Media;

namespace MyMediaCollection
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private AppWindow _appWindow;
        private const string AppTitle = "My Media Collection";

        public MainWindow()
        {
            this.InitializeComponent();
            SystemBackdrop = new MicaBackdrop
            {
                Kind = MicaKind.BaseAlt
            };
            _appWindow = GetCurrentAppWindow();
            _appWindow.Title = AppTitle;
        }

        private AppWindow GetCurrentAppWindow()
        {
            IntPtr handle = WindowNative.GetWindowHandle(this);
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(handle);
            return AppWindow.GetFromWindowId(windowId);
        }

        internal void SetPageTitle(string title)
        {
            if (_appWindow == null)
            {
                _appWindow = GetCurrentAppWindow();
            }

            _appWindow.Title = $"{AppTitle} - {title}";
        }
    }
}