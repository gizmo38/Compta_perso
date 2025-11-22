using System;
using System.Threading.Tasks;
using Compta_perso.Models;

namespace Compta_perso.Data;

/// <summary>
/// Classe utilitaire pour ajouter des transactions de test
/// À utiliser temporairement pour tester
/// </summary>
public static class AddTestTransactions
{
    /// <summary>
    /// Ajoute 2 transactions de test à la base (une seule fois)
    /// </summary>
    public static async Task AddTwoTestTransactionsAsync(AppDbContext context)
    {
        // Récupérer le compte courant
        var ccAccount = context.Accounts.Find(1); // ID du premier compte créé

        if (ccAccount == null)
            return;

        // Vérifier si les transactions existent déjà
        var restaurantExists = context.Transactions
            .Any(t => t.Description == "Restaurant avec copains" && t.Amount == -45.50m);
        var carburantExists = context.Transactions
            .Any(t => t.Description == "Carburant Shell" && t.Amount == -62.30m);

        if (restaurantExists && carburantExists)
            return; // Les transactions existent déjà

        // Transaction 1 : Restaurant
        var transaction1 = new Transaction
        {
            AccountId = ccAccount.Id,
            Date = new DateTime(2025, 11, 22),
            Amount = -45.50m,
            Description = "Restaurant avec copains",
            IsDeferrable = false
        };

        // Transaction 2 : Essence
        var transaction2 = new Transaction
        {
            AccountId = ccAccount.Id,
            Date = new DateTime(2025, 11, 21),
            Amount = -62.30m,
            Description = "Carburant Shell",
            IsDeferrable = false
        };

        if (!restaurantExists)
            context.Transactions.Add(transaction1);
        if (!carburantExists)
            context.Transactions.Add(transaction2);

        await context.SaveChangesAsync();

        // Ajouter les BudgetEntries correspondantes
        var budgetEntry1 = new BudgetEntry
        {
            RealTransactionId = transaction1.Id,
            TargetMonth = new DateTime(2025, 11, 01),
            VirtualAmount = -45.50m,
            Type = Models.Enums.BudgetEntryType.Amortization
        };

        var budgetEntry2 = new BudgetEntry
        {
            RealTransactionId = transaction2.Id,
            TargetMonth = new DateTime(2025, 11, 01),
            VirtualAmount = -62.30m,
            Type = Models.Enums.BudgetEntryType.Amortization
        };

        context.BudgetEntries.Add(budgetEntry1);
        context.BudgetEntries.Add(budgetEntry2);
        await context.SaveChangesAsync();
    }
}
