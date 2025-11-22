# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

---

## 📋 Projet: Compta_perso

**Description**: Application de gestion de finances personnelles avec approche hybride (Trésorerie + Engagement)

**Type**: Application Desktop Windows-First (Cross-Platform avec Avalonia)

**Date de création**: 22 Novembre 2025

**Philosophie**: Local-First, Rigueur Comptable, Architecture MVVM

## 📖 Documentation Débutant

- **Journal** : `docs/JOURNAL.md` - Chrono des sessions (ce qu'on fait, pourquoi, résultats)
- **Guides** : `docs/guides/` - Explications thématiques détaillées (langage simple, sans jargon)
- **Glossaire** : `docs/glossaire.md` - Vocabulaire technique expliqué simplement
- **Important** : Toujours mettre à jour `docs/JOURNAL.md` après chaque session de travail

---

## 🏗️ Stack Technique (Architecture Imposée)

### Langage & Framework
- **Runtime**: .NET 8 ou .NET 9
- **Langage**: C# (moderne, dernières fonctionnalités)

### Interface Utilisateur
- **Framework UI**: Avalonia UI (Cross-platform)
- **Pattern**: MVVM (Model-View-ViewModel)
- **Toolkit**: CommunityToolkit.Mvvm

### Persistance
- **Base de données**: SQLite
- **ORM**: Entity Framework Core (Code First)
- **Migrations**: EF Core Migrations

---

## 📂 Structure du Projet

```
Compta_perso/
├── Compta_perso.sln                    # Solution Visual Studio
├── src/
│   ├── Compta_perso/                   # Projet principal
│   │   ├── Models/                     # Entités métier
│   │   │   ├── Account.cs              # Compte bancaire/provision
│   │   │   ├── Transaction.cs          # Flux réel (mouvement bancaire)
│   │   │   ├── BudgetEntry.cs          # Flux d'engagement (NOUVEAU)
│   │   │   └── Enums/                  # AccountCategory, TransactionType
│   │   ├── ViewModels/                 # ViewModels MVVM
│   │   │   ├── MainViewModel.cs
│   │   │   ├── AccountListViewModel.cs
│   │   │   ├── TransactionViewModel.cs
│   │   │   └── BudgetViewModel.cs
│   │   ├── Views/                      # Vues Avalonia AXAML
│   │   │   ├── MainView.axaml
│   │   │   ├── TransactionView.axaml
│   │   │   └── BudgetView.axaml
│   │   ├── Services/                   # Logique métier
│   │   │   ├── BudgetCalculator.cs     # Calcul "Reste à Vivre"
│   │   │   ├── ProrationEngine.cs      # Moteur de lissage
│   │   │   └── ProvisionManager.cs     # Gestion provisions
│   │   ├── Data/                       # Contexte EF Core
│   │   │   ├── AppDbContext.cs
│   │   │   └── Migrations/             # Migrations EF Core
│   │   ├── App.axaml                   # Configuration Avalonia
│   │   ├── App.axaml.cs
│   │   └── Program.cs                  # Point d'entrée
│   └── Compta_perso.Tests/             # Tests unitaires
│       ├── Services/
│       └── ViewModels/
├── .gitignore
├── README.md
└── .claude/
    └── CLAUDE.md                       # Ce fichier
```

---

## 🗄️ Modélisation des Données (Backend)

### Entité: Account (Compte)

**Rôle**: Représente un compte bancaire, un compte de transit ou une "poche de provisions"

```csharp
public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } // Ex: "Compte Courant BNP"
    public AccountCategory Category { get; set; }
    public decimal Balance { get; set; } // Solde réel
    public bool IsProvisionBucket { get; set; } // NOUVEAU

    // Navigation
    public ICollection<Transaction> Transactions { get; set; }
}

public enum AccountCategory
{
    RealAsset,        // Banque physique
    VirtualLedger,    // Compte d'Attente/Pivot
    ProvisionBucket   // NOUVEAU: Épargne = Dépense budgétaire
}
```

**Points clés**:
- `IsProvisionBucket = true` → Tout argent qui y rentre est considéré comme "dépensé" côté budget
- `Balance` reflète le solde bancaire réel

---

### Entité: Transaction (Flux Réel)

**Rôle**: Mouvement bancaire effectif (Cash Flow)

```csharp
public class Transaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; } // Positif = Crédit, Négatif = Débit
    public string Description { get; set; }
    public int AccountId { get; set; }
    public bool IsDeferrable { get; set; } // NOUVEAU: Indique si lisser dans le temps

    // Navigation
    public Account Account { get; set; }
    public ICollection<BudgetEntry> BudgetEntries { get; set; }
}
```

**Points clés**:
- `IsDeferrable = true` → Le moteur de proratisation va créer des BudgetEntry échelonnées
- Exemple: Transaction de -1200€ le 01/01 → 12 BudgetEntry de -100€ chacune

---

### Entité: BudgetEntry (Flux d'Engagement - NOUVEAU)

**Rôle**: Déconne la dépense économique de la sortie de cash

```csharp
public class BudgetEntry
{
    public int Id { get; set; }
    public int? RealTransactionId { get; set; } // Nullable
    public DateTime TargetMonth { get; set; } // Mois d'imputation (ex: 02/2025)
    public decimal VirtualAmount { get; set; } // Montant qui pèse sur le budget
    public BudgetEntryType Type { get; set; }

    // Navigation
    public Transaction? RealTransaction { get; set; }
}

public enum BudgetEntryType
{
    Provision,      // Mise de côté (ex: 200€ vers Livret A "Vacances")
    Amortization    // Lissage d'une grosse dépense (ex: 1200€/12 mois)
}
```

**Points clés**:
- `RealTransactionId` peut être NULL (charge future provisionnée)
- `TargetMonth` détermine sur quel mois le montant impacte le budget
- `Type = Provision` → L'argent est physiquement déplacé vers ProvisionBucket
- `Type = Amortization` → L'argent est déjà sorti, mais étalé budgétairement

---

## 🧮 Fonctionnalités Clés (Logique Métier Innovante)

### 📊 Le "Split" Trésorerie vs Budget

**L'application offre deux vues basées sur les mêmes données** :

#### Vue Trésorerie (Cash Flow)
- **Question**: Combien j'ai sur mon compte aujourd'hui ?
- **Source**: `Account.Balance` (somme des `Transaction.Amount`)
- **Calcul**: Simple agrégation des flux réels

#### Vue Engagement (Budget)
- **Question**: Combien puis-je dépenser ce mois-ci ?
- **Formule**:
  ```
  Reste à Vivre = Revenus du mois
                - (Charges Réelles du mois
                + Provisions du mois
                + Quote-part des charges annuelles)
  ```
- **Source**: Agrégation des `BudgetEntry` pour le mois courant

**Exemple**:
- Transaction réelle: -1200€ (Assurance annuelle payée en Janvier)
- Budget Janvier: -100€ (1/12)
- Budget Février: -100€ (1/12)
- ...
- Budget Décembre: -100€ (1/12)

→ **Trésorerie Janvier**: -1200€
→ **Budget Janvier**: -100€ seulement

---

### 🗓️ Moteur de Proratisation (Service: ProrationEngine)

**Scénario**: Utilisateur saisit une dépense de 1200€ le 01/01 et coche "Lisser sur 12 mois"

**Algorithme**:
```csharp
// src/Compta_perso/Services/ProrationEngine.cs

public void ApplyProration(Transaction transaction, int monthCount)
{
    decimal monthlyAmount = transaction.Amount / monthCount;
    DateTime startMonth = transaction.Date;

    for (int i = 0; i < monthCount; i++)
    {
        var budgetEntry = new BudgetEntry
        {
            RealTransactionId = transaction.Id,
            TargetMonth = startMonth.AddMonths(i),
            VirtualAmount = monthlyAmount,
            Type = BudgetEntryType.Amortization
        };
        _context.BudgetEntries.Add(budgetEntry);
    }
}
```

**Impact**:
- La **Transaction** reste intacte (mouvement réel de -1200€)
- 12 **BudgetEntry** sont créées (charges virtuelles de -100€)
- La vue Budget affiche -100€ par mois, pas -1200€ en Janvier

---

### 💰 Gestion des Provisions (Service: ProvisionManager)

**Scénario**: Virement de 200€ du CC vers Livret A (pour Vacances)

**Logique Hybride**:

```csharp
// src/Compta_perso/Services/ProvisionManager.cs

public void TransferToProvision(Account source, Account destination, decimal amount)
{
    // 1. Côté Trésorerie: Mouvement neutre (Actif A -> Actif B)
    var debitTransaction = new Transaction
    {
        AccountId = source.Id,
        Amount = -amount,
        Date = DateTime.Now
    };

    var creditTransaction = new Transaction
    {
        AccountId = destination.Id,
        Amount = amount,
        Date = DateTime.Now
    };

    // 2. Côté Budget: Si destination.IsProvisionBucket = true
    if (destination.IsProvisionBucket)
    {
        var budgetEntry = new BudgetEntry
        {
            RealTransactionId = creditTransaction.Id,
            TargetMonth = DateTime.Now,
            VirtualAmount = -amount, // Négatif = Dépense
            Type = BudgetEntryType.Provision
        };
        _context.BudgetEntries.Add(budgetEntry);
    }
}
```

**Résultat**:
- **Trésorerie**: 200€ sortent du CC, 200€ entrent dans le Livret A → Solde global inchangé
- **Budget**: 200€ sont "dépensés" (considérés comme sortis du budget disponible)

---

## 🖥️ Interface Utilisateur (Avalonia)

### Structure de la Fenêtre Principale (MainView)

#### Sidebar (Gauche)
```
┌─────────────────────────────┐
│ 🏦 Comptes Bancaires        │
│  ├─ Compte Courant BNP      │
│  │   Solde: 2,450.00 €      │
│  └─ Livret A                │
│      Solde: 8,300.00 €      │
│                              │
│ 💰 Enveloppes / Provisions  │
│  ├─ Vacances: 1,200.00 €    │
│  └─ Travaux: 3,500.00 €     │
└─────────────────────────────┘
```

#### Workspace Central (Onglets)

**Onglet "Journal Banque"** (TransactionView):
```
┌────────────────────────────────────────────────────────────┐
│ Date       │ Description       │ Montant   │ Compte        │
├────────────┼───────────────────┼───────────┼───────────────┤
│ 2025-11-20 │ Salaire           │ +2,500.00 │ CC BNP        │
│ 2025-11-18 │ Loyer             │   -850.00 │ CC BNP        │
│ 2025-11-15 │ Provision Vacances│   -200.00 │ Livret A      │
└────────────────────────────────────────────────────────────┘
```

**Onglet "Suivi Budgétaire"** (BudgetView):
```
┌─────────────────────────────────────────────────────────────┐
│ Catégorie           │ Nov 2025  │ Déc 2025  │ Jan 2026      │
├─────────────────────┼───────────┼───────────┼───────────────┤
│ Loyer               │   -850.00 │   -850.00 │   -850.00     │
│ Assurance (lissé)   │   -100.00 │   -100.00 │   -100.00     │ ← Charge réelle = 1200€ en Jan
│ Épargne Vacances    │   -200.00 │   -200.00 │   -200.00     │
├─────────────────────┼───────────┼───────────┼───────────────┤
│ RESTE À VIVRE       │ +1,350.00 │ +1,350.00 │ +1,350.00     │
└─────────────────────────────────────────────────────────────┘
```

---

## 🛠️ Commandes Clés

### Installation et Prérequis
```bash
# Installer .NET 8 SDK (Windows)
winget install Microsoft.DotNet.SDK.8

# Vérifier l'installation
dotnet --version  # Doit afficher 8.x.x ou 9.x.x

# Installer les templates Avalonia
dotnet new install Avalonia.Templates
```

---

### Créer la Solution (Phase 1 - Première fois)
```bash
# Depuis C:\Users\franc\Documents\GitHub\Compta_perso

# 1. Créer la solution
dotnet new sln -n Compta_perso

# 2. Créer le projet Avalonia MVVM
dotnet new avalonia.mvvm -n Compta_perso -o src/Compta_perso

# 3. Créer le projet de tests
dotnet new xunit -n Compta_perso.Tests -o src/Compta_perso.Tests

# 4. Ajouter les projets à la solution
dotnet sln add src/Compta_perso/Compta_perso.csproj
dotnet sln add src/Compta_perso.Tests/Compta_perso.Tests.csproj

# 5. Ajouter la référence du projet de tests vers le projet principal
dotnet add src/Compta_perso.Tests/Compta_perso.Tests.csproj reference src/Compta_perso/Compta_perso.csproj
```

---

### Installer les Dépendances NuGet
```bash
cd src/Compta_perso

# Entity Framework Core + SQLite
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design

# CommunityToolkit pour MVVM
dotnet add package CommunityToolkit.Mvvm

# (Optionnel) Validation et Helpers
dotnet add package FluentValidation
```

---

### Développement
```bash
# Lancer l'application
dotnet run --project src/Compta_perso/Compta_perso.csproj

# Lancer en mode Watch (recompile automatiquement)
dotnet watch --project src/Compta_perso/Compta_perso.csproj
```

---

### Migrations Entity Framework Core
```bash
cd src/Compta_perso

# Créer une migration initiale
dotnet ef migrations add InitialCreate

# Appliquer les migrations à la base
dotnet ef database update

# Créer une nouvelle migration après modification des modèles
dotnet ef migrations add AddBudgetEntryTable

# Revenir à une migration précédente
dotnet ef database update PreviousMigrationName
```

---

### Tests
```bash
# Lancer tous les tests
dotnet test

# Lancer les tests avec couverture
dotnet test /p:CollectCoverage=true

# Lancer un test spécifique
dotnet test --filter "FullyQualifiedName~ProrationEngineTests"
```

---

### Build et Publication
```bash
# Build Debug
dotnet build

# Build Release
dotnet build -c Release

# Publier pour Windows (exécutable autonome)
dotnet publish -c Release -r win-x64 --self-contained true -o publish/win-x64

# Publier pour Linux (Cross-platform Avalonia)
dotnet publish -c Release -r linux-x64 --self-contained true -o publish/linux-x64
```

---

## ⚠️ Points d'Attention Critiques

### 1. Précision Monétaire

**❌ Ne JAMAIS utiliser `float` ou `double` pour l'argent**

**✅ Utiliser `decimal` en C#**

```csharp
// ❌ MAUVAIS
public double Amount { get; set; }

// ✅ BON
public decimal Amount { get; set; }
```

**Raison**: Les types `float`/`double` utilisent l'arithmétique binaire à virgule flottante, ce qui cause des erreurs d'arrondi pour les valeurs monétaires.

**Exemple d'erreur**:
```csharp
double total = 0.1 + 0.2;  // Résultat: 0.30000000000000004
decimal total = 0.1m + 0.2m;  // Résultat: 0.3
```

---

### 2. Réconciliation et Audit Trail

**Règles strictes**:
- Toutes les transactions DOIVENT être catégorisées
- Dates cohérentes avec l'exercice comptable
- Historique des modifications (qui a modifié quoi, quand)

**Implémentation recommandée**:
```csharp
public class Transaction
{
    // ... autres propriétés

    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}
```

---

### 3. Sécurité

**Les données financières sont sensibles** :

- ✅ Validation stricte des entrées (FluentValidation)
- ✅ Pas de données sensibles dans les logs
- ✅ Chiffrement de la base SQLite (optionnel)
- ✅ Pas de connexion réseau (Local-First)

**Exemple de validation**:
```csharp
public class TransactionValidator : AbstractValidator<Transaction>
{
    public TransactionValidator()
    {
        RuleFor(t => t.Amount).NotEqual(0);
        RuleFor(t => t.Date).LessThanOrEqualTo(DateTime.Now);
        RuleFor(t => t.Description).NotEmpty().MaximumLength(200);
    }
}
```

---

### 4. Gestion des Dates et Mois

**Attention aux pièges de DateTime** :

```csharp
// ❌ MAUVAIS: Compare des DateTime avec heures
if (transaction.Date == new DateTime(2025, 11, 22))

// ✅ BON: Compare uniquement la date
if (transaction.Date.Date == new DateTime(2025, 11, 22))

// Pour les BudgetEntry, stocker uniquement le premier jour du mois
public DateTime TargetMonth { get; set; } // Ex: 2025-11-01 00:00:00
```

---

## 📦 Dépendances NuGet Installées

```xml
<PackageReference Include="Avalonia" Version="11.x.x" />
<PackageReference Include="Avalonia.Desktop" Version="11.x.x" />
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.x.x" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.x.x" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.x.x" />
<PackageReference Include="FluentValidation" Version="11.x.x" />
```

---

## 🗺️ Roadmap de Développement

### Phase 1 : Socle Technique ✅ (En cours)
- [x] Initialiser la solution .NET/Avalonia
- [ ] Configurer Entity Framework Core + SQLite
- [ ] Créer les modèles de données (Account, Transaction, BudgetEntry)
- [ ] Structurer l'architecture MVVM avec CommunityToolkit

### Phase 2 : Services Métier
- [ ] Implémenter `ProrationEngine` (Moteur de lissage)
- [ ] Implémenter `ProvisionManager` (Gestion provisions)
- [ ] Implémenter `BudgetCalculator` (Calcul "Reste à Vivre")
- [ ] Tests unitaires des services

### Phase 3 : Interface Utilisateur
- [ ] Créer `MainView` avec Sidebar + Workspace
- [ ] Implémenter `TransactionView` (Journal Banque)
- [ ] Implémenter `BudgetView` (Suivi Budgétaire)
- [ ] Implémenter `AccountListViewModel` (Sidebar)

### Phase 4 : Fonctionnalités Avancées
- [ ] Import/Export CSV (relevés bancaires)
- [ ] Graphiques et statistiques
- [ ] Rapports PDF
- [ ] Sauvegarde/Restauration

---

## 🔗 Références Externes

### Documentation
- [Avalonia UI](https://docs.avaloniaui.net/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/)

### Guides Personnels
- `@GUIDE_GESTION_MEMOIRE.md` : Gestion de la mémoire Claude
- `@GUIDE_DEPLOIEMENT_VPS.md` : Déploiement (si besoin futur)

---

## 💾 Sauvegarde MCP Memory

Les décisions importantes de ce projet sont documentées dans MCP Memory :

```javascript
// Entité Projet
{
  name: "Compta_perso",
  entityType: "project",
  observations: [
    "Application de gestion finances personnelles",
    "Stack: C# + Avalonia UI + EF Core + SQLite",
    "Architecture: MVVM + Code First",
    "Approche: Hybride Trésorerie/Engagement",
    "Status: Phase 1 - Initialisation"
  ]
}

// Décision Architecture
{
  name: "decision_architecture_compta",
  entityType: "decision",
  observations: [
    "Décision: Avalonia UI pour cross-platform desktop",
    "Raison: Local-First, pas de dépendance web",
    "Alternative rejetée: Electron (trop lourd)",
    "Date: 2025-11-22"
  ]
}

// Décision Technique
{
  name: "decision_budget_entry_table",
  entityType: "decision",
  observations: [
    "Décision: Création table BudgetEntry séparée",
    "Raison: Découpler flux réel (Transaction) du flux d'engagement",
    "Innovation: Permet lissage charges et provisions",
    "Date: 2025-11-22"
  ]
}
```

---

## 🎓 Conventions de Code

### Nommage
- **Classes**: PascalCase (ex: `BudgetEntry`)
- **Propriétés**: PascalCase (ex: `TargetMonth`)
- **Méthodes**: PascalCase (ex: `ApplyProration()`)
- **Variables locales**: camelCase (ex: `monthlyAmount`)
- **Constantes**: UPPER_SNAKE_CASE (ex: `MAX_PRORATION_MONTHS`)

### Organisation des Fichiers
- Un fichier = Une classe publique
- Nom du fichier = Nom de la classe
- Enums dans dossier `Models/Enums/`

### Commentaires
- XML Documentation pour méthodes publiques
- Commentaires inline pour logique complexe uniquement
- Pas de code commenté (utiliser Git)

**Exemple**:
```csharp
/// <summary>
/// Applique une proratisation sur une transaction pour lisser la charge sur plusieurs mois.
/// </summary>
/// <param name="transaction">La transaction réelle à lisser</param>
/// <param name="monthCount">Nombre de mois sur lesquels lisser (1-60)</param>
/// <exception cref="ArgumentException">Si monthCount < 1 ou > 60</exception>
public void ApplyProration(Transaction transaction, int monthCount)
{
    // Validation
    if (monthCount < 1 || monthCount > 60)
        throw new ArgumentException("Le nombre de mois doit être entre 1 et 60", nameof(monthCount));

    // Logique...
}
```

---

**Version** : 1.0 - Initialisation Projet C#/Avalonia
**Dernière mise à jour** : 22 Novembre 2025
**Créé pour** : Application de gestion finances personnelles (Local-First, MVVM, SQLite)
