using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compta_perso.Models;
using Compta_perso.Models.Enums;

namespace Compta_perso.Data;

/// <summary>
/// Classe pour initialiser la base de données avec des données de test
/// Utile pour le développement et les tests
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Ajoute des données de test à la base
    /// À appeler une seule fois après la création de la base
    /// </summary>
    public static async Task InitializeAsync(AppDbContext context)
    {
        // Vérifier si la base a déjà des données
        if (context.Accounts.Any())
        {
            return; // Données déjà présentes
        }

        // Créer les comptes
        var accounts = new List<Account>
        {
            new()
            {
                Name = "Compte Courant BNP",
                Category = AccountCategory.RealAsset,
                Balance = 2450.50m,
            },
            new()
            {
                Name = "Livret A",
                Category = AccountCategory.RealAsset,
                Balance = 8300.00m,
            },
            new()
            {
                Name = "Vacances",
                Category = AccountCategory.ProvisionBucket,
                Balance = 1200.00m,
            },
            new()
            {
                Name = "Travaux",
                Category = AccountCategory.ProvisionBucket,
                Balance = 3500.00m,
            }
        };

        await context.Accounts.AddRangeAsync(accounts);
        await context.SaveChangesAsync();

        // Créer les transactions (après les comptes pour avoir les IDs)
        var ccAccount = context.Accounts.First(a => a.Name == "Compte Courant BNP");
        var livretAccount = context.Accounts.First(a => a.Name == "Livret A");
        var vacancesAccount = context.Accounts.First(a => a.Name == "Vacances");

        var transactions = new List<Transaction>
        {
            new()
            {
                AccountId = ccAccount.Id,
                Date = new DateTime(2025, 11, 25),
                Amount = 2500.00m,
                Description = "Salaire Novembre",
                IsDeferrable = false
            },
            new()
            {
                AccountId = ccAccount.Id,
                Date = new DateTime(2025, 11, 18),
                Amount = -850.00m,
                Description = "Loyer Novembre",
                IsDeferrable = false
            },
            new()
            {
                AccountId = ccAccount.Id,
                Date = new DateTime(2025, 11, 15),
                Amount = -200.00m,
                Description = "Provision Vacances",
                IsDeferrable = false
            },
            new()
            {
                AccountId = ccAccount.Id,
                Date = new DateTime(2025, 11, 10),
                Amount = -50.00m,
                Description = "Épicerie",
                IsDeferrable = false
            },
            new()
            {
                AccountId = ccAccount.Id,
                Date = new DateTime(2025, 11, 01),
                Amount = -1200.00m,
                Description = "Assurance Annuelle (à lisser sur 12 mois)",
                IsDeferrable = true // Cette transaction peut être lissée
            },
            new()
            {
                AccountId = livretAccount.Id,
                Date = new DateTime(2025, 11, 20),
                Amount = 500.00m,
                Description = "Versement Livret A",
                IsDeferrable = false
            }
        };

        await context.Transactions.AddRangeAsync(transactions);
        await context.SaveChangesAsync();

        // Créer les entrées budgétaires (pour les transactions non lissées)
        var budgetEntries = new List<BudgetEntry>
        {
            // Loyer de Novembre
            new()
            {
                RealTransactionId = transactions[1].Id,
                TargetMonth = new DateTime(2025, 11, 01),
                VirtualAmount = -850.00m,
                Type = BudgetEntryType.Amortization
            },
            // Provision Vacances de Novembre
            new()
            {
                RealTransactionId = transactions[2].Id,
                TargetMonth = new DateTime(2025, 11, 01),
                VirtualAmount = -200.00m,
                Type = BudgetEntryType.Provision
            }
        };

        // Ajouter les budgetEntries lissées pour l'Assurance Annuelle
        // -1200€ lissée sur 12 mois = -100€ par mois
        var assuranceTransaction = transactions[4];
        for (int month = 0; month < 12; month++)
        {
            budgetEntries.Add(new BudgetEntry
            {
                RealTransactionId = assuranceTransaction.Id,
                TargetMonth = new DateTime(2025, 11, 01).AddMonths(month),
                VirtualAmount = -100.00m,
                Type = BudgetEntryType.Amortization
            });
        }

        await context.BudgetEntries.AddRangeAsync(budgetEntries);
        await context.SaveChangesAsync();
    }
}
