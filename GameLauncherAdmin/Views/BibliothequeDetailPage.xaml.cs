using CommunityToolkit.WinUI.UI.Animations;

using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace GameLauncherAdmin.Views;

public sealed partial class BibliothequeDetailPage : Page
{
    public BibliothequeDetailViewModel ViewModel
    {
        get;
    }

    public BibliothequeDetailPage()
    {
        ViewModel = App.GetService<BibliothequeDetailViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        this.RegisterElementForConnectedAnimation("animationKeyContentGrid", itemHero);
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        base.OnNavigatingFrom(e);
        if (e.NavigationMode == NavigationMode.Back)
        {
            var navigationService = App.GetService<INavigationService>();

            if (ViewModel.Item != null)
            {
                navigationService.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }
}
