using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Compta_perso.Models;
using Compta_perso.Models.Enums;

namespace Compta_perso.Repositories;

/// <summary>
/// Interface pour accéder aux comptes dans la base de données
/// Pattern: Repository - Abstrait les détails d'accès aux données
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// Récupère TOUS les comptes
    /// </summary>
    Task<List<Account>> GetAllAsync();

    /// <summary>
    /// Récupère un compte par son ID
    /// Retourne null si non trouvé
    /// </summary>
    Task<Account?> GetByIdAsync(int id);

    /// <summary>
    /// Récupère tous les comptes d'une catégorie spécifique
    /// Exemple: AccountCategory.ProvisionBucket pour les "poches de provisions"
    /// </summary>
    Task<List<Account>> GetByCategoryAsync(AccountCategory category);

    /// <summary>
    /// Récupère seulement les comptes réels (pas les provisions)
    /// </summary>
    Task<List<Account>> GetRealAccountsAsync();

    /// <summary>
    /// Récupère seulement les comptes de provision
    /// </summary>
    Task<List<Account>> GetProvisionBucketsAsync();

    /// <summary>
    /// Ajoute un nouveau compte à la base
    /// </summary>
    Task AddAsync(Account account);

    /// <summary>
    /// Modifie un compte existant
    /// </summary>
    Task UpdateAsync(Account account);

    /// <summary>
    /// Supprime un compte par son ID
    /// Retourne true si suppression réussie, false si non trouvé
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Calcule le solde total de TOUS les comptes
    /// </summary>
    Task<decimal> GetTotalBalanceAsync();

    /// <summary>
    /// Calcule le solde total des comptes réels (trésorerie)
    /// </summary>
    Task<decimal> GetRealAccountsTotalBalanceAsync();

    /// <summary>
    /// Calcule le solde total des comptes de provision
    /// </summary>
    Task<decimal> GetProvisionBucketsTotalBalanceAsync();
}
