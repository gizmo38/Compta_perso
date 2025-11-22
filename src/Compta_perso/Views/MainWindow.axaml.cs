using Avalonia.Controls;
using Compta_perso.ViewModels;

namespace Compta_perso.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Méthode appelée par le code-behind pour configurer le DataContext
    /// Cette méthode reçoit les ViewModels du DI et les assigne aux bons endroits
    /// </summary>
    public void SetupViewModels(MainWindowViewModel mainViewModel, TransactionsViewModel transactionsViewModel)
    {
        // Le DataContext principal reste MainWindowViewModel (pour l'en-tête et la navigation)
        this.DataContext = mainViewModel;

        // Trouver l'onglet Transactions et lui assigner le TransactionsViewModel
        var tabControl = this.FindControl<TabControl>("TabControl");
        if (tabControl != null && tabControl.Items.Count >= 4)
        {
            var transactionsTab = (TabItem)tabControl.Items[3];
            transactionsTab.DataContext = transactionsViewModel;
        }
    }
}