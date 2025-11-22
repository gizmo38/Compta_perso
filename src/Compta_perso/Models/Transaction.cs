using System;
using System.Collections.Generic;

namespace Compta_perso.Models;

/// <summary>
/// Transaction bancaire (flux réel / mouvement d'argent)
/// </summary>
public class Transaction
{
    /// <summary>
    /// Identifiant unique de la transaction
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Date de la transaction
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Montant de la transaction
    /// Positif = Crédit (argent qui rentre)
    /// Négatif = Débit (argent qui sort)
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Description / Libellé de la transaction
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// ID du compte concerné
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    /// Indique si cette transaction doit être lissée dans le temps
    /// Si true : Le moteur d'amortissement va créer des BudgetEntry échelonnées
    /// Exemple : 1200€ → 12 × 100€
    /// </summary>
    public bool IsDeferrable { get; set; }

    /// <summary>
    /// Date de création de l'enregistrement
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Compte lié à cette transaction
    /// </summary>
    public Account Account { get; set; } = null!;

    /// <summary>
    /// Entrées budgétaires liées à cette transaction
    /// (si IsDeferrable = true, il y en aura plusieurs)
    /// </summary>
    public ICollection<BudgetEntry> BudgetEntries { get; set; } = new List<BudgetEntry>();
}
