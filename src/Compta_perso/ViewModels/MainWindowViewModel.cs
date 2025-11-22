using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Compta_perso.Models;
using Compta_perso.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Compta_perso.ViewModels;

/// <summary>
/// ViewModel principal de l'application
/// Gère l'affichage des données dans l'en-tête et la navigation
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IBudgetEntryRepository _budgetEntryRepository;

    /// <summary>
    /// Solde total de tous les comptes réels (trésorerie)
    /// Affiché dans l'en-tête
    /// </summary>
    [ObservableProperty]
    private decimal totalTresorerie = 0;

    /// <summary>
    /// Budget disponible du mois courant (Reste à Vivre)
    /// Affiché dans l'en-tête
    /// </summary>
    [ObservableProperty]
    private decimal budgetMois = 0;

    /// <summary>
    /// Mois actuellement affiché
    /// </summary>
    [ObservableProperty]
    private DateTime moisActuel = DateTime.Now;

    /// <summary>
    /// Liste de tous les comptes
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Account> accounts = new();

    /// <summary>
    /// Liste de toutes les transactions
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Transaction> transactions = new();

    /// <summary>
    /// Constructeur avec injection de dépendances
    /// Les repositories sont fournis par le conteneur DI dans App.axaml.cs
    /// </summary>
    public MainWindowViewModel(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository,
        IBudgetEntryRepository budgetEntryRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _budgetEntryRepository = budgetEntryRepository;

        // Charger les données initiales
        LoadDataAsync();
    }

    /// <summary>
    /// Charge les données depuis la base de données
    /// Appelé au démarrage de l'application
    /// </summary>
    private async void LoadDataAsync()
    {
        try
        {
            // Charger les comptes
            var accountsList = await _accountRepository.GetAllAsync();
            Accounts = new ObservableCollection<Account>(accountsList);

            // Charger les transactions
            var transactionsList = await _transactionRepository.GetAllAsync();
            Transactions = new ObservableCollection<Transaction>(transactionsList);

            // Calculer la trésorerie totale
            TotalTresorerie = await _accountRepository.GetRealAccountsTotalBalanceAsync();

            // Calculer le budget du mois courant
            await UpdateBudgetMois();
        }
        catch (Exception ex)
        {
            // Log d'erreur (à implémenter plus tard)
            System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des données: {ex.Message}");
        }
    }

    /// <summary>
    /// Recalcule le budget du mois courant
    /// (Reste à Vivre)
    /// </summary>
    private async Task UpdateBudgetMois()
    {
        // Pour l'instant, on retourne juste le total des budgetEntries du mois
        // Plus tard, on aura une vraie formule pour calculer le "Reste à Vivre"
        BudgetMois = await _budgetEntryRepository.GetMonthlyTotalAsync(MoisActuel);
    }
}
