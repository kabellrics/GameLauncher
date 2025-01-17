using GameLauncher.ObservableObjet;
using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class BibliothequePage : Page
{
    public BibliothequeViewModel ViewModel
    {
        get;
    }

    public BibliothequePage()
    {
        ViewModel = App.GetService<BibliothequeViewModel>();
        InitializeComponent();
    }

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //var deletinglist = itemslist.SelectedItems as List<ObservableItem>;
        //ViewModel.DeleteItems(deletinglist);
    }
}
