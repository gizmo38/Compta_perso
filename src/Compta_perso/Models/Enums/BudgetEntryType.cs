namespace Compta_perso.Models.Enums;

/// <summary>
/// Type d'entrée budgétaire
/// </summary>
public enum BudgetEntryType
{
    /// <summary>
    /// Provision : Mise de côté vers un compte d'épargne
    /// Exemple : 200€ vers Livret A "Vacances"
    /// → Trésorerie neutre, Budget -200€
    /// </summary>
    Provision,

    /// <summary>
    /// Amortissement : Lissage d'une grosse dépense sur plusieurs mois
    /// Exemple : Assurance 1200€ → 12 mois × 100€
    /// → Trésorerie -1200€ en janvier, Budget -100€ par mois
    /// </summary>
    Amortization
}
