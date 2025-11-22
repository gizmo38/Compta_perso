using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Compta_perso.Models;

namespace Compta_perso.Repositories;

/// <summary>
/// Interface pour accéder aux transactions dans la base de données
/// Pattern: Repository - Abstrait les détails d'accès aux données
/// </summary>
public interface ITransactionRepository
{
    /// <summary>
    /// Récupère TOUTES les transactions
    /// </summary>
    Task<List<Transaction>> GetAllAsync();

    /// <summary>
    /// Récupère une transaction par son ID
    /// Retourne null si non trouvée
    /// </summary>
    Task<Transaction?> GetByIdAsync(int id);

    /// <summary>
    /// Récupère toutes les transactions d'un compte spécifique
    /// </summary>
    Task<List<Transaction>> GetByAccountIdAsync(int accountId);

    /// <summary>
    /// Récupère les transactions d'une période donnée
    /// </summary>
    Task<List<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Ajoute une nouvelle transaction à la base
    /// </summary>
    Task AddAsync(Transaction transaction);

    /// <summary>
    /// Modifie une transaction existante
    /// </summary>
    Task UpdateAsync(Transaction transaction);

    /// <summary>
    /// Supprime une transaction par son ID
    /// Retourne true si suppression réussie, false si non trouvée
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Compte le nombre total de transactions
    /// </summary>
    Task<int> CountAsync();
}
