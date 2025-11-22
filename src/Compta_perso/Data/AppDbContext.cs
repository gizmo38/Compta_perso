using Compta_perso.Models;
using Microsoft.EntityFrameworkCore;

namespace Compta_perso.Data;

/// <summary>
/// Contexte de base de données pour l'application Compta_perso
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Table des comptes bancaires
    /// </summary>
    public DbSet<Account> Accounts { get; set; } = null!;

    /// <summary>
    /// Table des transactions (flux réels)
    /// </summary>
    public DbSet<Transaction> Transactions { get; set; } = null!;

    /// <summary>
    /// Table des entrées budgétaires (flux d'engagement)
    /// </summary>
    public DbSet<BudgetEntry> BudgetEntries { get; set; } = null!;

    /// <summary>
    /// Constructeur
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configuration du modèle de données
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration Account
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).IsRequired().HasMaxLength(200);
            entity.Property(a => a.Balance).HasPrecision(18, 2); // Précision monétaire
            entity.HasIndex(a => a.Name); // Index pour recherche rapide
        });

        // Configuration Transaction
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Amount).HasPrecision(18, 2); // Précision monétaire
            entity.Property(t => t.Description).IsRequired().HasMaxLength(500);
            entity.HasIndex(t => t.Date); // Index pour tri par date

            // Relation : Transaction -> Account
            entity.HasOne(t => t.Account)
                  .WithMany(a => a.Transactions)
                  .HasForeignKey(t => t.AccountId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuration BudgetEntry
        modelBuilder.Entity<BudgetEntry>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.VirtualAmount).HasPrecision(18, 2); // Précision monétaire
            entity.HasIndex(b => b.TargetMonth); // Index pour filtrage par mois

            // Relation : BudgetEntry -> Transaction (nullable)
            entity.HasOne(b => b.RealTransaction)
                  .WithMany(t => t.BudgetEntries)
                  .HasForeignKey(b => b.RealTransactionId)
                  .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
