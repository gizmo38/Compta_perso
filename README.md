# Compta_perso - Application de Gestion de Finances Personnelles

Application desktop Windows-First (cross-platform) pour gérer vos finances avec une approche hybride innovante : **Vue Trésorerie** (combien j'ai ?) vs **Vue Budget** (combien puis-je dépenser ?).

---

## 🎯 Concept Innovant

### Le Problème
Les applications traditionnelles mélangent :
- **Cash Flow** (mouvement d'argent réel)
- **Budget** (dépenses économiques)

**Exemple** : Vous payez 1200€ d'assurance en janvier.
- ❌ Application classique : "-1200€ en janvier" → Budget déséquilibré
- ✅ Compta_perso : "-1200€ en trésorerie, mais -100€/mois en budget" → Lissage automatique

### La Solution : Deux Vues
1. **Vue Trésorerie** : Combien j'ai sur mes comptes bancaires ?
2. **Vue Budget** : Combien puis-je dépenser ce mois-ci ?

---

## 🏗️ Stack Technique

- **Langage** : C# (.NET 8/9)
- **Interface** : Avalonia UI (cross-platform desktop)
- **Architecture** : MVVM + CommunityToolkit.Mvvm
- **Base de données** : SQLite + Entity Framework Core (Code First)

---

## 🚀 Installation et Démarrage

### Prérequis

1. **Installer .NET 8 SDK** (Windows)
   ```bash
   winget install Microsoft.DotNet.SDK.8
   ```

2. **Vérifier l'installation**
   ```bash
   dotnet --version
   # Doit afficher 8.x.x ou 9.x.x
   ```

3. **Installer les templates Avalonia**
   ```bash
   dotnet new install Avalonia.Templates
   ```

---

### Initialisation du Projet (Première fois)

```bash
# 1. Se placer dans le dossier du projet
cd C:\Users\franc\Documents\GitHub\Compta_perso

# 2. Créer la solution
dotnet new sln -n Compta_perso

# 3. Créer le projet Avalonia MVVM
dotnet new avalonia.mvvm -n Compta_perso -o src/Compta_perso

# 4. Créer le projet de tests
dotnet new xunit -n Compta_perso.Tests -o src/Compta_perso.Tests

# 5. Ajouter les projets à la solution
dotnet sln add src/Compta_perso/Compta_perso.csproj
dotnet sln add src/Compta_perso.Tests/Compta_perso.Tests.csproj

# 6. Référence du projet de tests → projet principal
dotnet add src/Compta_perso.Tests/Compta_perso.Tests.csproj reference src/Compta_perso/Compta_perso.csproj
```

---

### Installation des Dépendances NuGet

```bash
cd src/Compta_perso

# Entity Framework Core + SQLite
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design

# CommunityToolkit pour MVVM
dotnet add package CommunityToolkit.Mvvm

# (Optionnel) Validation
dotnet add package FluentValidation
```

---

### Lancer l'Application

```bash
# Depuis la racine du projet
dotnet run --project src/Compta_perso/Compta_perso.csproj

# OU en mode Watch (recompile automatiquement)
dotnet watch --project src/Compta_perso/Compta_perso.csproj
```

---

## 📂 Structure du Projet

```
Compta_perso/
├── Compta_perso.sln
├── src/
│   ├── Compta_perso/
│   │   ├── Models/              # Entités (Account, Transaction, BudgetEntry)
│   │   ├── ViewModels/          # ViewModels MVVM
│   │   ├── Views/               # Vues Avalonia (.axaml)
│   │   ├── Services/            # Logique métier (AmortizationService, etc.)
│   │   ├── Data/                # AppDbContext + Migrations EF Core
│   │   └── Program.cs
│   └── Compta_perso.Tests/
├── .claude/
│   └── CLAUDE.md                # Documentation pour Claude Code
└── README.md
```

---

## 🧮 Modèle de Données Innovant

### 1. Account (Compte)
- Compte bancaire réel (Compte Courant, Livret A...)
- **ProvisionBucket** : Compte d'épargne dont l'alimentation est une "dépense" budgétaire

### 2. Transaction (Flux Réel)
- Mouvement bancaire effectif
- `IsDeferrable = true` → Déclenche le lissage automatique

### 3. BudgetEntry (Flux d'Engagement) ⭐ NOUVEAU
- Déconnecte la dépense économique de la sortie de cash
- Permet le **lissage** des grosses dépenses (1200€ → 12x100€)
- Permet les **provisions** (épargne = dépense budgétaire)

---

## 🎓 Exemple Concret

### Scénario : Assurance annuelle de 1200€

#### Ce qui se passe :
```csharp
// L'utilisateur saisit la transaction
var transaction = new Transaction
{
    Date = new DateTime(2025, 1, 1),
    Amount = -1200,
    Description = "Assurance annuelle",
    IsDeferrable = true  // ← Active le lissage
};

// Le service AmortizationService génère automatiquement 12 BudgetEntry
AmortizationService.ApplyProration(transaction, 12);

// Résultat : 12 entrées créées
BudgetEntry { TargetMonth = 2025-01-01, VirtualAmount = -100 }
BudgetEntry { TargetMonth = 2025-02-01, VirtualAmount = -100 }
...
BudgetEntry { TargetMonth = 2025-12-01, VirtualAmount = -100 }
```

#### Ce que voit l'utilisateur :
- **Vue Trésorerie Janvier** : -1200€ (sortie réelle)
- **Vue Budget Janvier** : -100€ (charge lissée)
- **Vue Budget Février** : -100€
- ...

---

## 🗺️ Roadmap

### Phase 1 : Socle Technique ✅ (En cours)
- [x] Documentation complète (CLAUDE.md)
- [ ] Initialisation .NET + Avalonia
- [ ] Configuration EF Core + SQLite

### Phase 2 : Data Model Hybride 🔄
- [ ] Créer `Account.cs`, `Transaction.cs`, `BudgetEntry.cs`
- [ ] Créer `AppDbContext.cs`
- [ ] Première migration EF Core

### Phase 3 : Moteur de Lissage 🔄
- [ ] Implémenter `AmortizationService`
- [ ] Tests unitaires du lissage (1200€ → 12x100€)

### Phase 4 : UI Double Vue 🔄
- [ ] Switch "Mode Trésorerie" / "Mode Budget"
- [ ] Vue Journal Banque (Transaction)
- [ ] Vue Suivi Budgétaire (BudgetEntry)

---

## 📚 Documentation

- **Pour Claude Code** : Voir `.claude/CLAUDE.md` (guide complet)
- **Pour développeurs** : Ce README
- **Avalonia UI** : https://docs.avaloniaui.net/
- **Entity Framework Core** : https://learn.microsoft.com/ef/core/

---

## 🛠️ Commandes Utiles

```bash
# Lancer l'application
dotnet run --project src/Compta_perso

# Lancer les tests
dotnet test

# Créer une migration EF Core
cd src/Compta_perso
dotnet ef migrations add NomDeLaMigration

# Appliquer les migrations
dotnet ef database update

# Build Release
dotnet build -c Release

# Publier pour Windows (exécutable autonome)
dotnet publish -c Release -r win-x64 --self-contained true -o publish/win-x64
```

---

## ⚠️ Points Critiques

1. **Précision monétaire** : Toujours utiliser `decimal`, jamais `float`/`double`
2. **Dates** : `BudgetEntry.TargetMonth` = Premier jour du mois (ex: 2025-11-01)
3. **ProvisionBucket** : L'épargne est traitée comme une dépense budgétaire
4. **Tests** : Tous les services doivent avoir des tests unitaires

---

## 📝 Licence

Projet personnel - Usage privé

---

## 👤 Auteur

Franc - Développeur en apprentissage
Projet créé le 22 Novembre 2025
