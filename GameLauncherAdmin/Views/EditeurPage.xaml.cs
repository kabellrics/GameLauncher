using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class EditeurPage : Page
{
    public EditeurViewModel ViewModel
    {
        get;
    }

    public EditeurPage()
    {
        ViewModel = App.GetService<EditeurViewModel>();
        InitializeComponent();
    }
}
