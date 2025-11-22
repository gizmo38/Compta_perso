namespace Compta_perso.Models.Enums;

/// <summary>
/// Catégorie de compte bancaire
/// </summary>
public enum AccountCategory
{
    /// <summary>
    /// Compte bancaire réel (Compte Courant, Livret A, etc.)
    /// </summary>
    RealAsset,

    /// <summary>
    /// Compte de transit/pivot (utilisé pour les transferts internes)
    /// </summary>
    VirtualLedger,

    /// <summary>
    /// Compte de provision (épargne = dépense budgétaire)
    /// Exemple : "Livret A Vacances"
    /// </summary>
    ProvisionBucket
}
