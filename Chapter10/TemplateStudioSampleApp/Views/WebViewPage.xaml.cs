﻿using Microsoft.UI.Xaml.Controls;

using TemplateStudioSampleApp.ViewModels;

namespace TemplateStudioSampleApp.Views;

// To learn more about WebView2, see https://docs.microsoft.com/microsoft-edge/webview2/.
public sealed partial class WebViewPage : Page
{
    public WebViewViewModel ViewModel
    {
        get;
    }

    public WebViewPage()
    {
        ViewModel = App.GetService<WebViewViewModel>();
        InitializeComponent();

        ViewModel.WebViewService.Initialize(WebView);
    }
}
