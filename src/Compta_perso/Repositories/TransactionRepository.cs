using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compta_perso.Data;
using Compta_perso.Models;
using Microsoft.EntityFrameworkCore;

namespace Compta_perso.Repositories;

/// <summary>
/// Implémentation du repository pour les transactions
/// Gère tout l'accès à la base de données pour les transactions
/// </summary>
public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Récupère TOUTES les transactions, triées par date (plus récente d'abord)
    /// </summary>
    public async Task<List<Transaction>> GetAllAsync()
    {
        return await _context.Transactions
            .Include(t => t.Account) // Charger aussi le compte associé
            .Include(t => t.BudgetEntries) // Charger les entrées budgétaires associées
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    /// <summary>
    /// Récupère UNE transaction par son ID
    /// Inclut aussi les données associées (Account, BudgetEntries)
    /// </summary>
    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _context.Transactions
            .Include(t => t.Account)
            .Include(t => t.BudgetEntries)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    /// <summary>
    /// Récupère toutes les transactions d'un compte spécifique
    /// Utile pour afficher l'historique d'un compte
    /// </summary>
    public async Task<List<Transaction>> GetByAccountIdAsync(int accountId)
    {
        return await _context.Transactions
            .Where(t => t.AccountId == accountId)
            .Include(t => t.Account)
            .Include(t => t.BudgetEntries)
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    /// <summary>
    /// Récupère les transactions entre deux dates (incluses)
    /// Utile pour les rapports mensuels/annuels
    /// </summary>
    public async Task<List<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Transactions
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .Include(t => t.Account)
            .Include(t => t.BudgetEntries)
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    /// <summary>
    /// Ajoute une nouvelle transaction à la base
    /// Sauvegarde immédiatement (SaveChangesAsync)
    /// </summary>
    public async Task AddAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Modifie une transaction existante
    /// Important: La transaction DOIT avoir un Id valide
    /// </summary>
    public async Task UpdateAsync(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Supprime une transaction par son ID
    /// Retourne true si trouvée et supprimée
    /// Retourne false si la transaction n'existe pas
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
            return false;

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Compte le nombre total de transactions
    /// Utile pour les statistiques
    /// </summary>
    public async Task<int> CountAsync()
    {
        return await _context.Transactions.CountAsync();
    }
}
