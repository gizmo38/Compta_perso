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
/// Implémentation du repository pour les entrées budgétaires
/// Gère tout l'accès à la base de données pour les entrées budgétaires
/// </summary>
public class BudgetEntryRepository : IBudgetEntryRepository
{
    private readonly AppDbContext _context;

    public BudgetEntryRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Récupère TOUTES les entrées budgétaires avec les transactions associées
    /// </summary>
    public async Task<List<BudgetEntry>> GetAllAsync()
    {
        return await _context.BudgetEntries
            .Include(b => b.RealTransaction)
            .OrderBy(b => b.TargetMonth)
            .ToListAsync();
    }

    /// <summary>
    /// Récupère UNE entrée budgétaire par son ID
    /// Inclut aussi la transaction associée
    /// </summary>
    public async Task<BudgetEntry?> GetByIdAsync(int id)
    {
        return await _context.BudgetEntries
            .Include(b => b.RealTransaction)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    /// <summary>
    /// Récupère toutes les entrées budgétaires d'un mois
    /// Utile pour afficher le budget du mois dans l'onglet "Budget"
    ///
    /// Exemple: GetByMonthAsync(new DateTime(2025, 11, 15))
    /// → Retourne toutes les BudgetEntry où TargetMonth est Novembre 2025
    /// </summary>
    public async Task<List<BudgetEntry>> GetByMonthAsync(DateTime monthDate)
    {
        var year = monthDate.Year;
        var month = monthDate.Month;

        return await _context.BudgetEntries
            .Where(b => b.TargetMonth.Year == year && b.TargetMonth.Month == month)
            .Include(b => b.RealTransaction)
            .OrderBy(b => b.TargetMonth)
            .ToListAsync();
    }

    /// <summary>
    /// Récupère toutes les entrées budgétaires reliées à une transaction
    /// Utile pour voir comment une grosse charge est lissée
    ///
    /// Exemple: Une transaction de -1200€ créée le 01/01
    /// → 12 BudgetEntry de -100€ (une par mois)
    /// Appel: GetByTransactionIdAsync(transactionId)
    /// → Retourne ces 12 entries
    /// </summary>
    public async Task<List<BudgetEntry>> GetByTransactionIdAsync(int transactionId)
    {
        return await _context.BudgetEntries
            .Where(b => b.RealTransactionId == transactionId)
            .Include(b => b.RealTransaction)
            .OrderBy(b => b.TargetMonth)
            .ToListAsync();
    }

    /// <summary>
    /// Récupère les entrées budgétaires d'un type spécifique
    ///
    /// Exemples:
    /// - GetByTypeAsync(BudgetEntryType.Provision)
    ///   → Toutes les mises de côté (200€ vers Livret A)
    /// - GetByTypeAsync(BudgetEntryType.Amortization)
    ///   → Toutes les charges lissées (1200€ / 12 mois)
    /// </summary>
    public async Task<List<BudgetEntry>> GetByTypeAsync(BudgetEntryType type)
    {
        return await _context.BudgetEntries
            .Where(b => b.Type == type)
            .Include(b => b.RealTransaction)
            .OrderBy(b => b.TargetMonth)
            .ToListAsync();
    }

    /// <summary>
    /// Récupère les entrées budgétaires entre deux dates
    /// Utile pour les rapports (trimestre, année, etc.)
    /// </summary>
    public async Task<List<BudgetEntry>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.BudgetEntries
            .Where(b => b.TargetMonth >= startDate && b.TargetMonth <= endDate)
            .Include(b => b.RealTransaction)
            .OrderBy(b => b.TargetMonth)
            .ToListAsync();
    }

    /// <summary>
    /// Ajoute une nouvelle entrée budgétaire à la base
    /// </summary>
    public async Task AddAsync(BudgetEntry budgetEntry)
    {
        _context.BudgetEntries.Add(budgetEntry);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Ajoute plusieurs entrées budgétaires en une seule opération
    /// Plus efficace que appelera AddAsync() 12 fois
    ///
    /// Utile pour le lissage: créer 12 BudgetEntry en une fois
    /// </summary>
    public async Task AddRangeAsync(List<BudgetEntry> budgetEntries)
    {
        _context.BudgetEntries.AddRange(budgetEntries);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Modifie une entrée budgétaire existante
    /// Important: L'entry DOIT avoir un Id valide
    /// </summary>
    public async Task UpdateAsync(BudgetEntry budgetEntry)
    {
        _context.BudgetEntries.Update(budgetEntry);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Supprime une entrée budgétaire par son ID
    /// Retourne true si trouvée et supprimée
    /// Retourne false si l'entry n'existe pas
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var budgetEntry = await _context.BudgetEntries.FindAsync(id);
        if (budgetEntry == null)
            return false;

        _context.BudgetEntries.Remove(budgetEntry);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Calcule le total des montants virtuels pour un mois spécifique
    /// C'est la dépense budgétaire totale du mois
    ///
    /// Exemple: Si le budget de Novembre contient:
    /// - Loyer: -850€
    /// - Assurance (lissée): -100€
    /// - Provision Vacances: -200€
    /// → Total: -1150€
    /// </summary>
    public async Task<decimal> GetMonthlyTotalAsync(DateTime monthDate)
    {
        var year = monthDate.Year;
        var month = monthDate.Month;

        return await _context.BudgetEntries
            .Where(b => b.TargetMonth.Year == year && b.TargetMonth.Month == month)
            .SumAsync(b => b.VirtualAmount);
    }

    /// <summary>
    /// Calcule le total des provisions (mises de côté) pour un mois
    /// Utile pour voir combien on économise ce mois-ci
    /// </summary>
    public async Task<decimal> GetMonthlyProvisionsAsync(DateTime monthDate)
    {
        var year = monthDate.Year;
        var month = monthDate.Month;

        return await _context.BudgetEntries
            .Where(b => b.TargetMonth.Year == year && b.TargetMonth.Month == month && b.Type == BudgetEntryType.Provision)
            .SumAsync(b => b.VirtualAmount);
    }

    /// <summary>
    /// Calcule le total des amortissements (charges lissées) pour un mois
    /// Utile pour voir combien de charges annuelles pèsent ce mois
    /// </summary>
    public async Task<decimal> GetMonthlyAmortizationsAsync(DateTime monthDate)
    {
        var year = monthDate.Year;
        var month = monthDate.Month;

        return await _context.BudgetEntries
            .Where(b => b.TargetMonth.Year == year && b.TargetMonth.Month == month && b.Type == BudgetEntryType.Amortization)
            .SumAsync(b => b.VirtualAmount);
    }
}
