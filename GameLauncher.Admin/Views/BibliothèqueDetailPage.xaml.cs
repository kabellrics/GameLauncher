using CommunityToolkit.WinUI.UI.Animations;

using GameLauncher.Admin.Contracts.Services;
using GameLauncher.Admin.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace GameLauncher.Admin.Views;

public sealed partial class BibliothèqueDetailPage : Page
{
    public BibliothèqueDetailViewModel ViewModel
    {
        get;
    }

    public BibliothèqueDetailPage()
    {
        ViewModel = App.GetService<BibliothèqueDetailViewModel>();
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
