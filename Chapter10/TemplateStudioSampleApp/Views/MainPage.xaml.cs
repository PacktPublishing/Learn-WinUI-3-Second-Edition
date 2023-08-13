using Microsoft.UI.Xaml.Controls;

using TemplateStudioSampleApp.ViewModels;

namespace TemplateStudioSampleApp.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
