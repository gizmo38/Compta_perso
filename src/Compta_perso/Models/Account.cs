using System;
using System.Collections.Generic;
using Compta_perso.Models.Enums;

namespace Compta_perso.Models;

/// <summary>
/// Compte bancaire ou compte de provision
/// </summary>
public class Account
{
    /// <summary>
    /// Identifiant unique du compte
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nom du compte (ex: "Compte Courant BNP", "Livret A Vacances")
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Catégorie du compte (Banque réelle, Transit, Provision)
    /// </summary>
    public AccountCategory Category { get; set; }

    /// <summary>
    /// Solde actuel du compte (en euros)
    /// Utilise decimal pour éviter les erreurs d'arrondi
    /// </summary>
    public decimal Balance { get; set; }

    /// <summary>
    /// Indique si ce compte est une "poche de provisions"
    /// Si true : Tout argent qui y rentre est considéré comme "dépensé" côté budget
    /// </summary>
    public bool IsProvisionBucket { get; set; }

    /// <summary>
    /// Date de création du compte
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Transactions liées à ce compte
    /// </summary>
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
