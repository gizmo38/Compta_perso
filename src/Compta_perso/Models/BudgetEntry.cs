using System;
using Compta_perso.Models.Enums;

namespace Compta_perso.Models;

/// <summary>
/// Entrée budgétaire (flux d'engagement)
/// INNOVATION : Déconnecte la dépense économique de la sortie de cash
/// </summary>
public class BudgetEntry
{
    /// <summary>
    /// Identifiant unique de l'entrée budgétaire
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID de la transaction réelle (peut être NULL si charge future)
    /// </summary>
    public int? RealTransactionId { get; set; }

    /// <summary>
    /// Mois d'imputation de la charge (ex: 01/02/2025)
    /// IMPORTANT : Toujours le premier jour du mois
    /// </summary>
    public DateTime TargetMonth { get; set; }

    /// <summary>
    /// Montant qui pèse sur le budget du mois
    /// Négatif = Dépense
    /// Positif = Revenu
    /// </summary>
    public decimal VirtualAmount { get; set; }

    /// <summary>
    /// Type d'entrée budgétaire (Provision ou Amortization)
    /// </summary>
    public BudgetEntryType Type { get; set; }

    /// <summary>
    /// Date de création de l'enregistrement
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Transaction réelle liée (si elle existe)
    /// </summary>
    public Transaction? RealTransaction { get; set; }
}
