using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Compta_perso.Models;
using Compta_perso.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Compta_perso.ViewModels;

/// <summary>
/// ViewModel pour l'onglet Transactions
/// Gère l'affichage de la liste des transactions et l'ajout de nouvelles transactions
/// </summary>
public partial class TransactionsViewModel : ViewModelBase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;

    /// <summary>
    /// Liste de toutes les transactions affichées
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Transaction> transactions = new();

    /// <summary>
    /// Liste de tous les comptes disponibles pour sélection
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Account> accounts = new();

    /// <summary>
    /// Date de la nouvelle transaction (formulaire d'ajout)
    /// </summary>
    [ObservableProperty]
    private DateTime newTransactionDate = DateTime.Now;

    /// <summary>
    /// Montant de la nouvelle transaction (formulaire d'ajout)
    /// </summary>
    [ObservableProperty]
    private decimal newTransactionAmount = 0;

    /// <summary>
    /// Description de la nouvelle transaction (formulaire d'ajout)
    /// </summary>
    [ObservableProperty]
    private string newTransactionDescription = string.Empty;

    /// <summary>
    /// Compte sélectionné pour la nouvelle transaction (formulaire d'ajout)
    /// </summary>
    [ObservableProperty]
    private Account? selectedAccount = null;

    /// <summary>
    /// Si la nouvelle transaction est "Lissable" (peut être étalée sur plusieurs mois)
    /// </summary>
    [ObservableProperty]
    private bool newTransactionIsDeferrable = false;

    /// <summary>
    /// Indique si le formulaire d'ajout est visible
    /// </summary>
    [ObservableProperty]
    private bool isAddTransactionFormVisible = false;

    /// <summary>
    /// Constructeur avec injection de dépendances
    /// </summary>
    public TransactionsViewModel(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;

        // Charger les données initiales
        LoadDataAsync();
    }

    /// <summary>
    /// Charge toutes les transactions et tous les comptes
    /// </summary>
    private async void LoadDataAsync()
    {
        try
        {
            // Charger les transactions
            var transactionsList = await _transactionRepository.GetAllAsync();
            Transactions = new ObservableCollection<Transaction>(transactionsList);

            // Charger les comptes
            var accountsList = await _accountRepository.GetAllAsync();
            Accounts = new ObservableCollection<Account>(accountsList);

            // Sélectionner le premier compte par défaut
            if (Accounts.Count > 0)
                SelectedAccount = Accounts[0];
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des transactions: {ex.Message}");
        }
    }

    /// <summary>
    /// Affiche le formulaire d'ajout de nouvelle transaction
    /// </summary>
    [RelayCommand]
    public void ShowAddTransactionForm()
    {
        // Réinitialiser les champs du formulaire
        NewTransactionDate = DateTime.Now;
        NewTransactionAmount = 0;
        NewTransactionDescription = string.Empty;
        NewTransactionIsDeferrable = false;

        if (Accounts.Count > 0)
            SelectedAccount = Accounts[0];

        IsAddTransactionFormVisible = true;
    }

    /// <summary>
    /// Cache le formulaire d'ajout de nouvelle transaction
    /// </summary>
    [RelayCommand]
    public void HideAddTransactionForm()
    {
        IsAddTransactionFormVisible = false;
    }

    /// <summary>
    /// Ajoute une nouvelle transaction à la base de données
    /// et l'ajoute à la liste d'affichage
    /// </summary>
    [RelayCommand]
    public async Task AddTransactionAsync()
    {
        // Valider les données
        if (SelectedAccount == null)
        {
            System.Diagnostics.Debug.WriteLine("Aucun compte sélectionné");
            return;
        }

        if (string.IsNullOrWhiteSpace(NewTransactionDescription))
        {
            System.Diagnostics.Debug.WriteLine("Description vide");
            return;
        }

        if (NewTransactionAmount == 0)
        {
            System.Diagnostics.Debug.WriteLine("Montant invalide");
            return;
        }

        try
        {
            // Créer la nouvelle transaction
            var newTransaction = new Transaction
            {
                AccountId = SelectedAccount.Id,
                Date = NewTransactionDate,
                Amount = NewTransactionAmount,
                Description = NewTransactionDescription,
                IsDeferrable = NewTransactionIsDeferrable
            };

            // Ajouter à la base de données
            await _transactionRepository.AddAsync(newTransaction);

            // Ajouter à la collection observable (mise à jour UI en temps réel)
            Transactions.Add(newTransaction);

            // Mettre à jour le solde du compte
            SelectedAccount.Balance += NewTransactionAmount;
            await _accountRepository.UpdateAsync(SelectedAccount);

            // Cacher le formulaire
            HideAddTransactionForm();

            System.Diagnostics.Debug.WriteLine($"Transaction ajoutée: {NewTransactionDescription}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erreur lors de l'ajout de la transaction: {ex.Message}");
        }
    }

    /// <summary>
    /// Annule l'ajout et cache le formulaire
    /// </summary>
    [RelayCommand]
    public void CancelAddTransaction()
    {
        HideAddTransactionForm();
    }
}
