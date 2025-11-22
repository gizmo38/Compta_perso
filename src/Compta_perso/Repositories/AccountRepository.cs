using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compta_perso.Data;
using Compta_perso.Models;
using Compta_perso.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Compta_perso.Repositories;

/// <summary>
/// Implémentation du repository pour les comptes
/// Gère tout l'accès à la base de données pour les comptes
/// </summary>
public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Récupère TOUS les comptes avec leurs transactions associées
    /// </summary>
    public async Task<List<Account>> GetAllAsync()
    {
        return await _context.Accounts
            .Include(a => a.Transactions) // Charger aussi les transactions
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Récupère UN compte par son ID
    /// Inclut aussi les transactions associées
    /// </summary>
    public async Task<Account?> GetByIdAsync(int id)
    {
        return await _context.Accounts
            .Include(a => a.Transactions)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    /// <summary>
    /// Récupère tous les comptes d'une catégorie donnée
    /// Exemple: GetByCategoryAsync(AccountCategory.ProvisionBucket)
    /// </summary>
    public async Task<List<Account>> GetByCategoryAsync(AccountCategory category)
    {
        return await _context.Accounts
            .Where(a => a.Category == category)
            .Include(a => a.Transactions)
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Récupère les comptes "réels" = non-provisions
    /// (RealAsset et VirtualLedger, mais pas ProvisionBucket)
    /// </summary>
    public async Task<List<Account>> GetRealAccountsAsync()
    {
        return await _context.Accounts
            .Where(a => a.Category != AccountCategory.ProvisionBucket)
            .Include(a => a.Transactions)
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Récupère les comptes de provision uniquement
    /// (IsProvisionBucket = true)
    /// </summary>
    public async Task<List<Account>> GetProvisionBucketsAsync()
    {
        return await _context.Accounts
            .Where(a => a.Category == AccountCategory.ProvisionBucket)
            .Include(a => a.Transactions)
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Ajoute un nouveau compte à la base
    /// </summary>
    public async Task AddAsync(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Modifie un compte existant
    /// Important: Le compte DOIT avoir un Id valide
    /// </summary>
    public async Task UpdateAsync(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Supprime un compte par son ID
    /// Retourne true si trouvé et supprimé
    /// Retourne false si le compte n'existe pas
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account == null)
            return false;

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Calcule le solde total de TOUS les comptes
    /// Somme des Balance de tous les comptes
    /// </summary>
    public async Task<decimal> GetTotalBalanceAsync()
    {
        return await _context.Accounts
            .SumAsync(a => a.Balance);
    }

    /// <summary>
    /// Calcule le solde total des comptes réels (trésorerie)
    /// C'est ce qu'on affiche dans l'en-tête "Trésorerie Totale"
    /// </summary>
    public async Task<decimal> GetRealAccountsTotalBalanceAsync()
    {
        return await _context.Accounts
            .Where(a => a.Category != AccountCategory.ProvisionBucket)
            .SumAsync(a => a.Balance);
    }

    /// <summary>
    /// Calcule le solde total des comptes de provision
    /// Montant total mis de côté dans les provisions
    /// </summary>
    public async Task<decimal> GetProvisionBucketsTotalBalanceAsync()
    {
        return await _context.Accounts
            .Where(a => a.Category == AccountCategory.ProvisionBucket)
            .SumAsync(a => a.Balance);
    }
}
