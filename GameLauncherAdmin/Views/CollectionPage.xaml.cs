using GameLauncher.ObservableObjet;
using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class CollectionPage : Page
{
    public CollectionViewModel ViewModel
    {
        get;
    }

    public CollectionPage()
    {
        ViewModel = App.GetService<CollectionViewModel>();
        InitializeComponent();
    }

    private void listItem_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        var selectedcollec = listItem.SelectedItem as ObsCollection;
        ViewModel.OnItemClick(selectedcollec);
    }

    private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput && sender.Text.Length > 1)
        {
            ViewModel.FiltedPredefine();
        }
    }

    private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
       ViewModel.PredefineCollecChose = args.SelectedItem.ToString();
    }
}
