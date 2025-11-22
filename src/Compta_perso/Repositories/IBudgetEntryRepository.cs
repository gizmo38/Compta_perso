using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Compta_perso.Models;
using Compta_perso.Models.Enums;

namespace Compta_perso.Repositories;

/// <summary>
/// Interface pour accéder aux entrées budgétaires dans la base de données
/// Pattern: Repository - Abstrait les détails d'accès aux données
/// </summary>
public interface IBudgetEntryRepository
{
    /// <summary>
    /// Récupère TOUTES les entrées budgétaires
    /// </summary>
    Task<List<BudgetEntry>> GetAllAsync();

    /// <summary>
    /// Récupère une entrée budgétaire par son ID
    /// Retourne null si non trouvée
    /// </summary>
    Task<BudgetEntry?> GetByIdAsync(int id);

    /// <summary>
    /// Récupère toutes les entrées budgétaires d'un mois spécifique
    /// Le paramètre monthDate peut être n'importe quel jour du mois (on compare par année + mois)
    /// </summary>
    Task<List<BudgetEntry>> GetByMonthAsync(DateTime monthDate);

    /// <summary>
    /// Récupère toutes les entrées budgétaires reliées à une transaction spécifique
    /// Utile pour voir comment une grosse charge est lissée
    /// </summary>
    Task<List<BudgetEntry>> GetByTransactionIdAsync(int transactionId);

    /// <summary>
    /// Récupère toutes les entrées budgétaires d'un type spécifique
    /// Exemple: BudgetEntryType.Amortization pour voir les charges lissées
    /// </summary>
    Task<List<BudgetEntry>> GetByTypeAsync(BudgetEntryType type);

    /// <summary>
    /// Récupère les entrées budgétaires entre deux dates
    /// Utile pour les rapports trimestres/années
    /// </summary>
    Task<List<BudgetEntry>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Ajoute une nouvelle entrée budgétaire à la base
    /// </summary>
    Task AddAsync(BudgetEntry budgetEntry);

    /// <summary>
    /// Ajoute plusieurs entrées budgétaires en une seule opération
    /// Plus efficace que AddAsync appelée plusieurs fois
    /// </summary>
    Task AddRangeAsync(List<BudgetEntry> budgetEntries);

    /// <summary>
    /// Modifie une entrée budgétaire existante
    /// </summary>
    Task UpdateAsync(BudgetEntry budgetEntry);

    /// <summary>
    /// Supprime une entrée budgétaire par son ID
    /// Retourne true si suppression réussie, false si non trouvée
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Calcule le total des montants virtuels pour un mois spécifique
    /// C'est la dépense budgétaire du mois
    /// </summary>
    Task<decimal> GetMonthlyTotalAsync(DateTime monthDate);

    /// <summary>
    /// Calcule le total des provisions pour un mois
    /// </summary>
    Task<decimal> GetMonthlyProvisionsAsync(DateTime monthDate);

    /// <summary>
    /// Calcule le total des amortissements pour un mois
    /// </summary>
    Task<decimal> GetMonthlyAmortizationsAsync(DateTime monthDate);
}
