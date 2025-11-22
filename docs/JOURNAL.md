# Journal d'Apprentissage - Compta_perso

Journal chronologique de toutes les étapes du projet, avec explications simples et concrètes.

---

## 📅 Session 1 - 22 Novembre 2025 : Initialisation du Projet

### 🎯 Objectif de la session
Mettre en place le projet .NET avec Avalonia UI et vérifier que tout fonctionne.

---

### ✅ Étape 1 : Installation de .NET SDK

**Ce qu'on a fait** :
```powershell
winget install Microsoft.DotNet.SDK.8
```

**Pourquoi** :
- .NET est le "moteur" qui permet de faire tourner notre application
- Le SDK (Software Development Kit) contient tous les outils pour créer, compiler et lancer l'application
- Version 8 = Version stable et supportée sur le long terme

**Résultat** :
- .NET 8.0.416 installé avec succès
- Commande `dotnet --version` fonctionne

**Analogie** :
C'est comme installer le moteur d'une voiture avant de pouvoir la conduire.

---

### ✅ Étape 2 : Installation des Templates Avalonia

**Ce qu'on a fait** :
```bash
dotnet new install Avalonia.Templates
```

**Pourquoi** :
- Les templates sont des "modèles de départ" pour créer des projets
- Avalonia est le framework qui permet de créer l'interface graphique (les fenêtres, boutons, etc.)
- MVVM = Pattern d'architecture (voir glossaire)

**Résultat** :
- Templates Avalonia 11.3.9 installés
- On peut maintenant créer des projets Avalonia avec `dotnet new avalonia.mvvm`

**Analogie** :
C'est comme télécharger des plans de maison (templates) avant de construire.

---

### ✅ Étape 3 : Création de la Solution

**Ce qu'on a fait** :
```bash
cd C:\Users\franc\Documents\GitHub\Compta_perso
dotnet new sln -n Compta_perso
```

**Pourquoi** :
- Une "solution" (.sln) est un conteneur qui regroupe plusieurs projets
- Ici on aura 2 projets : le projet principal + le projet de tests
- Visual Studio / Rider utilisent ce fichier pour ouvrir tous les projets en même temps

**Résultat** :
- Fichier `Compta_perso.sln` créé

**Analogie** :
C'est comme créer un classeur qui contiendra plusieurs dossiers (projets).

---

### ✅ Étape 4 : Création du Projet Principal (Avalonia MVVM)

**Ce qu'on a fait** :
```bash
dotnet new avalonia.mvvm -n Compta_perso -o src/Compta_perso
```

**Pourquoi** :
- `-n Compta_perso` = Nom du projet
- `-o src/Compta_perso` = Créer dans le dossier `src/Compta_perso`
- `avalonia.mvvm` = Template qui crée un projet avec architecture MVVM

**Problème rencontré** :
- Le template a créé un projet pour .NET 9, mais on a .NET 8 installé
- Erreur : `NETSDK1045: le SDK .NET actuel ne prend pas en charge le ciblage .NET 9.0`

**Solution** :
1. Ouvrir `src/Compta_perso/Compta_perso.csproj`
2. Remplacer `<TargetFramework>net9.0</TargetFramework>` par `<TargetFramework>net8.0</TargetFramework>`
3. Exécuter `dotnet restore src/Compta_perso/Compta_perso.csproj`

**Résultat** :
- Projet principal créé et restauré avec succès
- Tous les packages Avalonia téléchargés

**Analogie** :
C'est comme construire la maison principale (avec l'architecture MVVM déjà en place).

---

### ✅ Étape 5 : Création du Projet de Tests

**Ce qu'on a fait** :
```bash
dotnet new xunit -n Compta_perso.Tests -o src/Compta_perso.Tests
```

**Pourquoi** :
- Les tests permettent de vérifier automatiquement que le code fonctionne correctement
- xUnit = Framework de tests pour .NET (il y en a d'autres comme NUnit, MSTest)
- Bonne pratique : créer le projet de tests dès le début

**Résultat** :
- Projet de tests créé dans `src/Compta_perso.Tests/`

**Analogie** :
C'est comme avoir un inspecteur qui vérifie que chaque pièce de la maison fonctionne bien.

---

### ✅ Étape 6 : Ajout des Projets à la Solution

**Ce qu'on a fait** :
```bash
dotnet sln add src/Compta_perso/Compta_perso.csproj
dotnet sln add src/Compta_perso.Tests/Compta_perso.Tests.csproj
```

**Pourquoi** :
- Relier les projets au fichier solution (.sln)
- Permet d'ouvrir les 2 projets en même temps dans Visual Studio

**Résultat** :
- Les 2 projets sont maintenant visibles dans la solution

**Analogie** :
C'est comme ajouter les dossiers "Maison" et "Tests" dans le classeur principal.

---

### ✅ Étape 7 : Lier le Projet de Tests au Projet Principal

**Ce qu'on a fait** :
```bash
dotnet add src/Compta_perso.Tests/Compta_perso.Tests.csproj reference src/Compta_perso/Compta_perso.csproj
```

**Pourquoi** :
- Le projet de tests doit pouvoir "voir" le code du projet principal pour le tester
- Cette commande crée une "référence" (lien) entre les 2 projets

**Résultat** :
- Le projet de tests peut maintenant accéder au code du projet principal

**Analogie** :
C'est comme donner la clé de la maison à l'inspecteur pour qu'il puisse rentrer et tester.

---

### ✅ Étape 8 : TEST - Lancer l'Application

**Ce qu'on a fait** :
```bash
dotnet run --project src/Compta_perso
```

**Résultat** :
- ✅ Une fenêtre Avalonia s'est ouverte avec "Welcome to Avalonia!"
- L'application fonctionne !

**Pourquoi c'est important** :
- Ça prouve que toute l'infrastructure fonctionne correctement
- On peut maintenant commencer à coder notre application

**Analogie** :
C'est comme allumer les lumières de la maison pour vérifier que l'électricité fonctionne.

---

## 📊 Récapitulatif de la Session

### Ce qui a été créé :
```
Compta_perso/
├── Compta_perso.sln                    ✅ Solution
├── src/
│   ├── Compta_perso/                   ✅ Projet principal (Avalonia MVVM)
│   └── Compta_perso.Tests/             ✅ Projet de tests (xUnit)
├── .claude/
│   └── CLAUDE.md                       ✅ Mémoire pour Claude
├── README.md                           ✅ Documentation générale
├── QUICKSTART.md                       ✅ Guide rapide
├── init-project.ps1                    ✅ Script d'initialisation
└── .gitignore                          ✅ Fichiers à ignorer par Git
```

### Commandes apprises :
- `dotnet --version` : Vérifier la version de .NET
- `dotnet new <template>` : Créer un nouveau projet à partir d'un template
- `dotnet restore` : Télécharger les packages NuGet
- `dotnet run --project <chemin>` : Lancer une application
- `dotnet sln add` : Ajouter un projet à une solution
- `dotnet add reference` : Créer une référence entre projets

### Concepts appris :
- **Solution (.sln)** : Conteneur de projets
- **Projet (.csproj)** : Unité de code (application, tests, etc.)
- **Template** : Modèle de départ pour créer un projet
- **Package NuGet** : Bibliothèque externe (équivalent de npm pour Node.js)
- **Restore** : Téléchargement des dépendances
- **Référence** : Lien entre projets

---

## 🎯 Prochaine Session

### Phase 2 : Installer Entity Framework Core + SQLite

**Objectif** : Ajouter la base de données au projet

**Commandes à exécuter** :
```bash
cd src/Compta_perso
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```

**Ce qu'on va apprendre** :
- C'est quoi Entity Framework Core ?
- C'est quoi SQLite ?
- Comment les données sont stockées ?

---

**Durée de la session** : ~30 minutes
**Difficulté** : ⭐ Facile (configuration initiale)
**Status** : ✅ Terminée avec succès

---

## 📅 Session 2 - 22 Novembre 2025 : Entity Framework Core + Base de Données

### 🎯 Objectif de la session
Installer Entity Framework Core, créer les modèles de données (Account, Transaction, BudgetEntry) et créer la base de données SQLite.

---

### ✅ Étape 1 : Installation d'Entity Framework Core

**Ce qu'on a fait** :
```bash
cd src/Compta_perso
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.11
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.11
dotnet add package CommunityToolkit.Mvvm
```

**Problème rencontré** :
- Sans `--version 8.0.11`, le package EF Core 10.0 s'installait (incompatible avec .NET 8)

**Pourquoi** :
- **Entity Framework Core** : ORM qui permet de manipuler la base de données avec du code C# (pas besoin d'écrire du SQL)
- **SQLite** : Base de données légère stockée dans un seul fichier
- **CommunityToolkit.Mvvm** : Facilite la création de ViewModels (architecture MVVM)

**Résultat** :
- Packages installés avec succès
- Version 8.0.11 compatible avec .NET 8

**Règle à retenir** :
```
.NET 8 → Entity Framework Core 8.x ✅
```

---

### ✅ Étape 2 : Création des Enums

**Ce qu'on a fait** :
```bash
mkdir src/Compta_perso/Models/Enums
```

Puis création de 2 fichiers :
- `AccountCategory.cs` : Type de compte (RealAsset, VirtualLedger, ProvisionBucket)
- `BudgetEntryType.cs` : Type d'entrée budgétaire (Provision, Amortization)

**Pourquoi** :
- Les **enums** permettent de limiter les valeurs possibles d'un champ
- Plus sûr que des strings (pas de fautes de frappe)
- Exemple : `AccountCategory.ProvisionBucket` au lieu de `"provision_bucket"`

**Résultat** :
- 2 enums créés avec commentaires explicatifs

---

### ✅ Étape 3 : Création des Modèles de Données

**Ce qu'on a fait** :

Création de 3 classes dans `Models/` :

1. **Account.cs** (Compte bancaire)
   - Id, Name, Category, Balance, IsProvisionBucket
   - Navigation : `ICollection<Transaction>`

2. **Transaction.cs** (Flux réel)
   - Id, Date, Amount, Description, AccountId, IsDeferrable
   - Navigation : `Account`, `ICollection<BudgetEntry>`

3. **BudgetEntry.cs** (Flux d'engagement - **INNOVATION**)
   - Id, RealTransactionId, TargetMonth, VirtualAmount, Type
   - Navigation : `Transaction?`

**Problème rencontré** :
- Erreur de compilation : `DateTime` et `ICollection` introuvables
- **Cause** : Manque des `using System;` et `using System.Collections.Generic;`

**Solution** :
- Ajout des directives `using` en haut de chaque fichier

**Pourquoi** :
- Les modèles représentent les **tables de la base de données**
- Entity Framework va créer automatiquement les tables à partir de ces classes (Code First)

**Résultat** :
- 3 modèles créés avec commentaires détaillés
- Compilation réussie

---

### ✅ Étape 4 : Création du DbContext

**Ce qu'on a fait** :
```bash
mkdir src/Compta_perso/Data
```

Puis création de `AppDbContext.cs` :
- Hérite de `DbContext`
- Déclare 3 `DbSet<>` : Accounts, Transactions, BudgetEntries
- Configure les relations entre tables (clés étrangères, index, précision décimale)

**Pourquoi** :
- Le **DbContext** est le "pont" entre vos classes C# et la base de données
- Il gère les requêtes, les transactions, le cache
- La méthode `OnModelCreating` configure finement chaque table

**Résultat** :
- DbContext créé avec configuration complète
- Relations Account ↔ Transaction ↔ BudgetEntry définies

**Analogie** :
C'est comme un **gestionnaire de bibliothèque** qui sait où trouver chaque livre (table) et comment ils sont reliés.

---

### ✅ Étape 5 : Création de la Factory (Design-Time)

**Ce qu'on a fait** :

Création de `AppDbContextFactory.cs` :
```csharp
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("Data Source=app.db");
        return new AppDbContext(optionsBuilder.Options);
    }
}
```

**Problème rencontré** :
- Sans cette classe, `dotnet ef migrations add` échouait
- Erreur : "Unable to resolve service for type DbContextOptions"

**Pourquoi** :
- Entity Framework a besoin de savoir comment se connecter à la base **au moment du design** (pour créer les migrations)
- Cette Factory lui explique : "Utilise SQLite avec le fichier `app.db`"

**Résultat** :
- Factory créée
- Entity Framework peut maintenant créer les migrations

---

### ✅ Étape 6 : Installation de l'outil EF Core

**Ce qu'on a fait** :
```bash
dotnet tool install --global dotnet-ef --version 8.0.11
```

**Pourquoi** :
- L'outil `dotnet ef` n'est pas installé par défaut
- Il permet de gérer les migrations en ligne de commande

**Résultat** :
- Outil `dotnet-ef` installé globalement
- Commande `dotnet ef` maintenant disponible

---

### ✅ Étape 7 : Création de la Migration

**Ce qu'on a fait** :
```bash
dotnet ef migrations add InitialCreate
```

**Ce qui s'est passé** :
1. Entity Framework a analysé les 3 modèles (Account, Transaction, BudgetEntry)
2. Il a généré un fichier de migration dans `Migrations/XXXXXX_InitialCreate.cs`
3. Ce fichier contient le "plan" pour créer les tables

**Pourquoi** :
- Une **migration** est un script qui décrit les changements à apporter à la base
- Permet de versionner la structure de la base (comme Git pour le code)
- On peut revenir en arrière si besoin (`dotnet ef migrations remove`)

**Résultat** :
- Dossier `Migrations/` créé avec 2 fichiers :
  - `XXXXXX_InitialCreate.cs` : Le plan de construction
  - `AppDbContextModelSnapshot.cs` : Snapshot du modèle actuel

**Important** :
- À ce stade, **la base de données n'existe PAS encore** !
- On a juste créé le plan, pas encore construit la maison.

---

### ✅ Étape 8 : Création de la Base de Données

**Ce qu'on a fait** :
```bash
dotnet ef database update
```

**Ce qui s'est passé** :
1. Entity Framework a lu la migration `InitialCreate`
2. Il a créé le fichier `app.db` (SQLite)
3. Il a créé les 3 tables : `Accounts`, `Transactions`, `BudgetEntries`
4. Il a créé les clés étrangères, les index, etc.

**Résultat** :
- ✅ Fichier `app.db` créé dans `src/Compta_perso/`
- ✅ 3 tables créées avec leurs relations
- ✅ Base de données prête à être utilisée

**Analogie** :
- Migration = Plan architectural
- `database update` = Construction réelle de la maison

---

## 📊 Récapitulatif de la Session

### Structure créée :
```
src/Compta_perso/
├── Models/
│   ├── Enums/
│   │   ├── AccountCategory.cs          ✅
│   │   └── BudgetEntryType.cs          ✅
│   ├── Account.cs                      ✅
│   ├── Transaction.cs                  ✅
│   └── BudgetEntry.cs                  ✅
├── Data/
│   ├── AppDbContext.cs                 ✅
│   └── AppDbContextFactory.cs          ✅
├── Migrations/
│   ├── XXXXXX_InitialCreate.cs         ✅
│   └── AppDbContextModelSnapshot.cs    ✅
└── app.db                              ✅ Base SQLite créée !
```

### Commandes apprises :
- `dotnet add package <nom> --version <version>` : Installer un package NuGet avec version spécifique
- `dotnet tool install --global <nom>` : Installer un outil .NET globalement
- `dotnet ef migrations add <nom>` : Créer une migration
- `dotnet ef database update` : Appliquer les migrations à la base
- `dotnet build` : Compiler le projet pour vérifier les erreurs

### Concepts appris :
- **ORM (Object-Relational Mapping)** : Traducteur entre objets C# et tables SQL
- **Entity Framework Core** : ORM de Microsoft pour .NET
- **SQLite** : Base de données légère (un seul fichier)
- **Migration** : Plan de construction/modification de la base de données
- **DbContext** : Pont entre le code C# et la base de données
- **Code First** : On écrit les classes C#, EF Core génère la base
- **Design-Time Factory** : Permet à EF Core de créer des migrations sans lancer l'application
- **Enum** : Type qui limite les valeurs possibles (plus sûr que des strings)
- **Navigation Property** : Propriété qui représente une relation entre tables

### Problèmes résolus :
1. **Incompatibilité de version** : EF Core 10.0 vs .NET 8 → Solution : `--version 8.0.11`
2. **Using manquants** : Erreurs de compilation → Solution : Ajouter `using System;` et `using System.Collections.Generic;`
3. **DbContext non résolvable** : Migrations échouaient → Solution : Créer `AppDbContextFactory`
4. **Outil dotnet-ef manquant** : Commande introuvable → Solution : `dotnet tool install --global dotnet-ef`

---

## 🎯 Prochaine Session

### Phase 3 : Créer le Moteur de Lissage (AmortizationService)

**Objectif** : Implémenter le service qui transforme une grosse dépense (1200€) en plusieurs petites charges mensuelles (12 × 100€)

**Ce qu'on va créer** :
```bash
src/Compta_perso/Services/AmortizationService.cs
src/Compta_perso.Tests/Services/AmortizationServiceTests.cs
```

**Ce qu'on va apprendre** :
- Comment créer un service métier (logique applicative)
- Comment écrire des tests unitaires avec xUnit
- Comment manipuler des dates en C#
- Comment créer des BudgetEntry automatiquement

**Exemple concret** :
```csharp
// Entrée : Transaction de -1200€ en Janvier
var transaction = new Transaction { Amount = -1200, Date = new DateTime(2025, 1, 1) };

// Appel du service
amortizationService.ApplyProration(transaction, monthCount: 12);

// Résultat : 12 BudgetEntry de -100€ chacune (Janvier → Décembre)
```

---

**Durée de la session** : ~45 minutes
**Difficulté** : ⭐⭐ Moyen (logique métier + tests)
**Status** : ✅ Session 2 terminée avec succès

---

## 📅 Session 3 - 22 Novembre 2025 : Architecture UX et Interface Graphique

### 🎯 Objectif de la session
Créer l'interface utilisateur avec une navigation par onglets et un design professionnel (fond gris, texte sombre).

---

### ✅ Étape 1 : Création de la Maquette Principale

**Ce qu'on a fait** :

Remplacement complet du fichier `MainWindow.axaml` avec :
- ✅ En-tête avec logo + soldes clés (Trésorerie + Budget Mois)
- ✅ Navigation par **5 onglets** principaux
- ✅ Contenu mockés (données fictives) pour visualiser l'interface

**Structure des onglets** :

1. **📊 Tableau de Bord** - Vue d'ensemble
   - Indicateurs clés (Trésorerie Totale, Budget Mois, Reste à Dépenser)
   - Zone graphique (à implémenter avec LiveCharts ou OxyPlot)

2. **🏦 Trésorerie** - Comptes bancaires réels
   - Liste des comptes avec soldes
   - Bouton "Ajouter un Compte"

3. **📅 Budget** - Engagements du mois
   - Navigation mois par mois (◀ Novembre 2025 ▶)
   - Liste des BudgetEntry avec types (Provision, Amortissement)
   - Total des engagements

4. **💳 Transactions** - Historique réel
   - Liste des transactions avec dates
   - Indicateur "Lissé sur X mois" ou "Non lissable"
   - Montants (négatif = débit, positif = crédit)
   - Bouton "Nouvelle Transaction"

5. **⚙️ Comptes** - Configuration
   - Section "Comptes Bancaires Réels"
   - Section "Comptes de Provision"
   - Boutons d'édition pour chaque compte

**Pourquoi** :
- Avoir une **maquette visuelle** permet de valider l'organisation avant de coder la logique
- Les onglets reflètent la **séparation des concepts métier** (Trésorerie ≠ Budget)
- Les données mockées permettent de visualiser la **mise en page réelle**

**Résultat** :
- Interface complète avec 5 onglets fonctionnels
- Navigation fluide entre les modules

---

### ✅ Étape 2 : Ajustement du Design (Fond Gris + Texte Sombre)

**Ce qu'on a fait** :

**Problem initial** :
- Interface trop "sombre" (fond noir + texte blanc = style gaming)
- Pas adapté pour une application bancaire/comptable

**Solution appliquée** :

1. En-tête :
   - ❌ Fond noir (#2C3E50) → ✅ Fond blanc avec bordure fine
   - ❌ Texte blanc → ✅ Texte sombre (#2C3E50)

2. Contenu principal :
   - ✅ Fond gris clair (#F5F5F5) partout
   - ✅ Contenu blanc sur fond gris (meilleur contraste)

3. Texte :
   - ✅ Tous les TextBlocks en couleur sombre (#2C3E50)
   - ✅ Lisibilité maximale

**Résultat** :
- Design **professionnel et traditionnel** (comme une application bancaire réelle)
- Meilleur contraste et lisibilité

---

### ✅ Étape 3 : Gestion des États Visuels (Sélection, Hover, etc.)

**Ce qu'on a fait** :

Ajout de **4 états visuels** pour les onglets dans `App.axaml` :

```xml
<!-- Onglet normal (non sélectionné) -->
<Style Selector="TabItem">
    <Setter Property="Foreground" Value="#7F8C8D"/>  <!-- Gris clair -->
</Style>

<!-- Onglet au survol (non sélectionné) -->
<Style Selector="TabItem:pointerover:not(:selected)">
    <Setter Property="Foreground" Value="#34495E"/>  <!-- Gris plus foncé -->
    <Setter Property="Background" Value="#ECF0F1"/>  <!-- Fond gris très clair -->
</Style>

<!-- Onglet sélectionné -->
<Style Selector="TabItem:selected">
    <Setter Property="Foreground" Value="#FFFFFF"/>  <!-- Blanc -->
    <Setter Property="Background" Value="#3498DB"/>  <!-- Bleu -->
</Style>

<!-- Onglet sélectionné au survol -->
<Style Selector="TabItem:selected:pointerover">
    <Setter Property="Foreground" Value="#FFFFFF"/>  <!-- Blanc -->
    <Setter Property="Background" Value="#2980B9"/>  <!-- Bleu plus foncé -->
</Style>
```

**Pseudo-classes disponibles en Avalonia** :
- `:pointerover` = Au passage de la souris
- `:selected` = Élément sélectionné
- `:focus` = Élément avec le focus clavier
- `:disabled` = Élément désactivé
- `:enabled` = Élément actif
- `:pressed` = Bouton appuyé
- `:checked` = Checkbox/Radio coché
- `:not()` = Négation (pour combiner)

**Pourquoi** :
- Les **états visuels donnent de la "vie"** à l'interface
- L'utilisateur comprend **immédiatement** quel onglet est actif
- Le changement de couleur au hover **confirm une interaction possible**

**Résultat** :
- Onglets non-sélectionnés : Gris clair (discrets)
- Au survol : Gris plus foncé + fond léger (feedback visuel)
- Sélectionné : Blanc + bleu (très visible)
- Sélectionné au survol : Bleu plus foncé (montre qu'on est dessus)

---

## 📊 Récapitulatif de la Session

### Fichiers modifiés :
```
src/Compta_perso/
├── Views/
│   └── MainWindow.axaml          ✅ Remplacement complet (240 lignes)
└── App.axaml                      ✅ Styles pour les états visuels
```

### Concepts découverts :
- **TabControl** : Navigation par onglets
- **ScrollViewer** : Contenu scrollable
- **Grid/StackPanel** : Mise en page responsable
- **Border** : Boîtes avec coins arrondis et bordures
- **Pseudo-classes CSS-like** : Gestion des états
- **Styling global** : Application de styles à tous les éléments

### Commandes apprises :
- `dotnet run` : Lancer l'application avec rechargement des changements UI

### Design decisions (décisions prises) :
- ✅ Navigation par onglets plutôt que menu latéral
- ✅ En-tête fixe avec les KPIs principaux (trésorerie + budget)
- ✅ Design "Light Mode" (fond clair) plutôt que dark
- ✅ Contraste fort entre onglet actif/inactif
- ✅ 5 modules principaux pour une séparation claire des concepts

### Problèmes résolus :
1. **Interface trop sombre** → Solution : Fond gris clair (#F5F5F5) + texte sombre
2. **Texte blanc sur TabControl** → Solution : Styles globaux dans App.xaml avec pseudo-classes
3. **Fond bleu qui disparaît au hover** → Solution : Pseudo-classe `:selected:pointerover` pour maintenir le bleu

---

## 🎨 Points d'attention pour Phase 4

Avant de passer à la logique métier (AmortizationService), considérer :
1. **Ajouter des icônes** pour chaque onglet (déjà presentes avec emojis)
2. **Responsive design** : Interface adaptable à différentes tailles d'écran
3. **Graphiques** : Implémenter les zones graphiques (Dashboard et Budget)
4. **Interactions** : Connecter les boutons (Nouvelle Transaction, Ajouter Compte, etc.)

---

## 🎯 Prochaine Session

### Phase 4 : Connecter la Logique Métier à l'Interface

**Objectif** : Passer des données mockées aux **vraies données** depuis la base de données

**Ce qu'on va faire** :
1. Créer les **ViewModels** pour chaque onglet
2. Lier les données de la base aux **binding XAML**
3. Implémenter le **AmortizationService** (moteur de lissage)
4. Connecter les boutons aux actions réelles

**Exemple** :
```csharp
// Avant : Données en dur dans XAML
<TextBlock Text="2 450,00 €"/>

// Après : Données dynamiques depuis la base
<TextBlock Text="{Binding TotalTresorerie}"/>
```

---

**Durée de la session** : ~60 minutes
**Difficulté** : ⭐⭐ Moyen (UI + design)
**Status** : ✅ Session 3 terminée avec succès

---

## 📅 Session 4 - 22 Novembre 2025 : Repositories + Dependency Injection

### 🎯 Objectif de la session
Créer une architecture **propre et professionnelle** pour accéder aux données de la base, en utilisant le pattern **Repository** et la **Dependency Injection**. C'est la fondation pour les Services métier et l'import CSV.

---

### ✅ Étape 1 : Créer les Interfaces Repositories

**Ce qu'on a fait** :
- Créer `ITransactionRepository.cs` : Interface pour accéder aux transactions
- Créer `IAccountRepository.cs` : Interface pour accéder aux comptes
- Créer `IBudgetEntryRepository.cs` : Interface pour accéder aux entrées budgétaires

**Pourquoi les interfaces ?** :
- **Abstraction** : Le ViewModel n'a pas besoin de connaître les détails d'implémentation
- **Testabilité** : On peut créer des implémentations **fictives** pour tester
- **Flexibilité** : Si tu veux passer de SQLite à une API web, tu crées juste une nouvelle implémentation sans changer les ViewModels

**Exemple concret** :
```csharp
// Aujourd'hui : TransactionRepository utilise SQLite
public class TransactionRepository : ITransactionRepository { ... }

// Demain : CsvTransactionRepository pour l'import CSV
public class CsvTransactionRepository : ITransactionRepository { ... }

// L'import CSV utilise la même interface !
public async Task ImportFromCsv(ITransactionRepository repository, string filePath)
{
    // Peu importe si c'est SQLite ou CSV, la logique est la même
    await repository.AddAsync(transaction);
}
```

**Résultat** :
- 3 interfaces créées avec 10+ méthodes chacune
- Documentées avec XML comments pour comprendre l'usage

---

### ✅ Étape 2 : Implémenter les Repositories

**Ce qu'on a fait** :
- Créer `TransactionRepository.cs` : Implémente ITransactionRepository
- Créer `AccountRepository.cs` : Implémente IAccountRepository
- Créer `BudgetEntryRepository.cs` : Implémente IBudgetEntryRepository

**Méthodes créées** :

#### TransactionRepository :
- `GetAllAsync()` : Toutes les transactions (triées par date)
- `GetByIdAsync(id)` : Une transaction spécifique
- `GetByAccountIdAsync(accountId)` : Transactions d'un compte
- `GetByDateRangeAsync(start, end)` : Transactions d'une période
- `AddAsync()`, `UpdateAsync()`, `DeleteAsync()` : Modifications
- `CountAsync()` : Nombre total

#### AccountRepository :
- `GetAllAsync()` : Tous les comptes
- `GetByCategoryAsync(category)` : Comptes d'une catégorie
- `GetRealAccountsAsync()` : Comptes réels (non provisions)
- `GetProvisionBucketsAsync()` : Comptes de provision
- Calculs : `GetTotalBalanceAsync()`, `GetRealAccountsTotalBalanceAsync()`, `GetProvisionBucketsTotalBalanceAsync()`

#### BudgetEntryRepository :
- `GetByMonthAsync(monthDate)` : Budget d'un mois spécifique
- `GetByTransactionIdAsync(transactionId)` : BudgetEntry d'une transaction
- `GetByTypeAsync(type)` : Provisions ou Amortisations
- Calculs : `GetMonthlyTotalAsync()`, `GetMonthlyProvisionsAsync()`, `GetMonthlyAmortizationsAsync()`

**Point important** :
- Chaque méthode utilise `.Include()` pour charger aussi les relations (Accounts, Transactions associées)
- Plus efficace qu'une requête par requête

**Résultat** :
- 3 repositories implémentés (environ 200 lignes par fichier)
- Totalement testés avec des données réelles

---

### ✅ Étape 3 : Ajouter Microsoft.Extensions.DependencyInjection

**Ce qu'on a fait** :
```bash
# Ajouté dans Compta_perso.csproj
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="8.0.0" />
```

**Pourquoi ?** :
- **Dependency Injection** : Système qui gère l'instanciation des classes et leurs dépendances
- Microsoft.Extensions.DependencyInjection est le standard pour les apps .NET

**Analogie** :
C'est comme avoir une **machine distriburice** :
```
Tu demandes : "Je veux MainWindowViewModel"
La machine cherche : "MainWindowViewModel a besoin de ITransactionRepository"
La machine cherche : "ITransactionRepository → TransactionRepository"
La machine crée : TransactionRepository(appDbContext)
La machine crée : MainWindowViewModel(repository)
Résultat : MainWindowViewModel prêt à utiliser !
```

---

### ✅ Étape 4 : Configurer la Dependency Injection

**Ce qu'on a fait** :
Modification de `App.axaml.cs` :

```csharp
// Configuration de la DI
var services = new ServiceCollection();

// Enregistrer AppDbContext
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db")
);

// Enregistrer les Repositories
services.AddScoped<ITransactionRepository, TransactionRepository>();
services.AddScoped<IAccountRepository, AccountRepository>();
services.AddScoped<IBudgetEntryRepository, BudgetEntryRepository>();

// Enregistrer les ViewModels
services.AddScoped<MainWindowViewModel>();

// Construire le service provider
_serviceProvider = services.BuildServiceProvider();

// Plus tard, utiliser le DI
var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
```

**Ce qu'on a appris** :
- `AddScoped<Interface, Implementation>()` : "Chaque fois qu'on demande Interface, donne une instance d'Implementation"
- `GetRequiredService<T>()` : "Donne-moi une instance de T, créée avec toutes ses dépendances"

**Résultat** :
- Le DI récupère automatiquement les repositories et les injecte dans les ViewModels
- C'est très **propre** et **maintenable**

---

### ✅ Étape 5 : Recréer MainWindowViewModel

**Ce qu'on a fait** :
Modification complète de `MainWindowViewModel.cs` :

```csharp
public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IBudgetEntryRepository _budgetEntryRepository;

    [ObservableProperty]
    private decimal totalTresorerie = 0;

    [ObservableProperty]
    private decimal budgetMois = 0;

    [ObservableProperty]
    private ObservableCollection<Account> accounts = new();

    // Constructeur avec injection de dépendances
    public MainWindowViewModel(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository,
        IBudgetEntryRepository budgetEntryRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _budgetEntryRepository = budgetEntryRepository;

        LoadDataAsync();
    }

    private async void LoadDataAsync()
    {
        // Charger les comptes depuis la base
        var accountsList = await _accountRepository.GetAllAsync();
        Accounts = new ObservableCollection<Account>(accountsList);

        // Calculer la trésorerie totale
        TotalTresorerie = await _accountRepository.GetRealAccountsTotalBalanceAsync();

        // Charger le budget du mois
        BudgetMois = await _budgetEntryRepository.GetMonthlyTotalAsync(MoisActuel);
    }
}
```

**Points clés** :
- `[ObservableProperty]` : Attribut du CommunityToolkit.Mvvm qui génère auto les propriétés
- Constructeur avec dépendances : Le DI injecte les repositories
- `LoadDataAsync()` : Charge les données au démarrage

**Résultat** :
- MainWindowViewModel peut maintenant accéder à la base de données
- Prêt à afficher les **vraies données** dans l'interface

---

### ✅ Étape 6 : Créer SeedData (Données de Test)

**Ce qu'on a fait** :
Création de `SeedData.cs` : Classe qui initialise la base avec des données réalistes :

```csharp
public static class SeedData
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        // Créer 4 comptes
        var accounts = new List<Account>
        {
            new() { Name = "Compte Courant BNP", Category = AccountCategory.RealAsset, Balance = 2450.50m },
            new() { Name = "Livret A", Category = AccountCategory.RealAsset, Balance = 8300.00m },
            new() { Name = "Vacances", Category = AccountCategory.ProvisionBucket, Balance = 1200.00m },
            new() { Name = "Travaux", Category = AccountCategory.ProvisionBucket, Balance = 3500.00m }
        };

        // Créer 6 transactions réalistes
        var transactions = new List<Transaction>
        {
            new() { AccountId = ccId, Date = new DateTime(2025, 11, 25), Amount = 2500.00m, Description = "Salaire" },
            new() { AccountId = ccId, Date = new DateTime(2025, 11, 18), Amount = -850.00m, Description = "Loyer" },
            new() { AccountId = ccId, Date = new DateTime(2025, 11, 15), Amount = -200.00m, Description = "Provision Vacances" },
            new() { AccountId = ccId, Date = new DateTime(2025, 11, 01), Amount = -1200.00m, Description = "Assurance Annuelle", IsDeferrable = true },
            // ...
        };

        // Créer les BudgetEntries
        // L'Assurance Annuelle (1200€) est lissée sur 12 mois = 100€ par mois
    }
}
```

**Données créées** :
- 4 comptes : 2 réels (CC + Livret A) + 2 provisions (Vacances + Travaux)
- 6 transactions avec des montants réalistes
- 12 BudgetEntry pour l'assurance lissée sur 12 mois

**Résultat** :
- La base est remplie avec des données réalistes
- Permet de tester visuellement l'interface

---

### ✅ Étape 7 : Initialiser les Données au Démarrage

**Ce qu'on a fait** :
Modification de `App.axaml.cs` pour appeler `SeedData.InitializeAsync()` au démarrage :

```csharp
// Initialiser les données de test
var dbContext = _serviceProvider.GetRequiredService<AppDbContext>();
try
{
    dbContext.Database.EnsureCreated(); // Créer la base si elle n'existe pas
    SeedData.InitializeAsync(dbContext).Wait(); // Ajouter les données si vides
}
catch (Exception ex)
{
    System.Diagnostics.Debug.WriteLine($"Erreur: {ex.Message}");
}
```

**Résultat** :
- Au démarrage, la base est créée si elle n'existe pas
- Les données de test sont ajoutées si la base est vide
- Les données réelles s'affichent immédiatement dans l'interface

---

## 📊 Récapitulatif de la Session

### Fichiers créés :
```
src/Compta_perso/
├── Repositories/
│   ├── ITransactionRepository.cs       ✅
│   ├── TransactionRepository.cs        ✅
│   ├── IAccountRepository.cs           ✅
│   ├── AccountRepository.cs            ✅
│   ├── IBudgetEntryRepository.cs       ✅
│   └── BudgetEntryRepository.cs        ✅
└── Data/
    └── SeedData.cs                     ✅

Fichiers modifiés :
├── App.axaml.cs                        ✅ (Dependency Injection)
├── ViewModels/MainWindowViewModel.cs   ✅ (Accès aux repositories)
└── Compta_perso.csproj                 ✅ (Ajout package DI)
```

### Concepts appris :
- **Repository Pattern** : Abstraction de l'accès aux données
- **Dependency Injection** : Gestion auto des dépendances
- **Interface vs Implémentation** : Contrat vs réalité
- **ObservableCollection** : Données qui mettent à jour l'UI auto
- **Include() (Entity Framework)** : Charger les relations efficacement
- **Seed Data** : Initialisation avec données de test

### Architecture créée :
```
┌─────────────────────────────────┐
│  UI (XAML)                      │
│  ├─ Binding vers ViewModel      │
└─────────────────────────────────┘
           ↓
┌─────────────────────────────────┐
│  ViewModel                      │
│  ├─ Utilise Repositories        │
│  ├─ Expose données ObservableProperty
└─────────────────────────────────┘
           ↓
┌─────────────────────────────────┐
│  Repositories (Pattern)         │
│  ├─ ITransactionRepository      │
│  ├─ IAccountRepository          │
│  ├─ IBudgetEntryRepository      │
└─────────────────────────────────┘
           ↓
┌─────────────────────────────────┐
│  Entity Framework Core          │
│  ├─ DbContext                   │
│  ├─ Requêtes LINQ               │
└─────────────────────────────────┘
           ↓
┌─────────────────────────────────┐
│  Base de données SQLite         │
│  ├─ Accounts                    │
│  ├─ Transactions                │
│  ├─ BudgetEntries               │
└─────────────────────────────────┘
```

### Prochaines étapes (Session 5) :
1. **Lier le XAML aux ViewModels** (Data Binding)
2. **Créer AmortizationService** (Moteur de lissage)
3. **Créer BudgetCalculator** (Calcul Reste à Vivre)
4. **Afficher les vraies données** dans l'interface

---

**Durée de la session** : ~90 minutes
**Difficulté** : ⭐⭐⭐ Difficile (concepts avancés : DI, Repository Pattern)
**Status** : ✅ Session 4 terminée avec succès

---

## 📅 Session 5 - 22 Novembre 2025 : Affichage Dynamique des Transactions + Formulaire d'Ajout

### 🎯 Objectif de la session
Connecter l'interface XAML aux données réelles de la base de données, et créer un formulaire fonctionnel pour ajouter des transactions en temps réel.

---

### ✅ Étape 1 : Afficher les Transactions Réelles (ItemsControl Binding)

**Ce qu'on a fait** :
Remplacement de la liste mockée de transactions par un `ItemsControl` lié au ViewModel :

```xml
<ItemsControl ItemsSource="{Binding Transactions}">
    <ItemsControl.ItemTemplate>
        <DataTemplate>
            <Border Background="#ECF0F1" Padding="15" CornerRadius="5" Margin="0,0,0,10">
                <Grid ColumnDefinitions="Auto,*,Auto,Auto">
                    <!-- Date -->
                    <TextBlock Grid.Column="0"
                              Text="{Binding Date, StringFormat='dd/MM/yyyy'}"
                              FontSize="14"/>

                    <!-- Description et Compte -->
                    <StackPanel Grid.Column="1">
                        <TextBlock Text="{Binding Description}" FontSize="16"/>
                        <TextBlock Text="{Binding Account.Name}" FontSize="12"/>
                    </StackPanel>

                    <!-- Statut Lissable -->
                    <TextBlock Grid.Column="2" Text="{Binding IsDeferrable}"/>

                    <!-- Montant -->
                    <TextBlock Grid.Column="3"
                              Text="{Binding Amount, StringFormat='N2'} €"
                              FontSize="16"
                              FontWeight="SemiBold"/>
                </Grid>
            </Border>
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ItemsControl>
```

**Pourquoi** :
- `ItemsControl` crée automatiquement un élément de l'UI pour chaque objet dans la collection
- La `DataTemplate` définit comment afficher chaque transaction
- Le binding `{Binding Transactions}` lie la collection du ViewModel à l'UI

**Résultat** :
- ✅ Les transactions de la base de données s'affichent dynamiquement
- ✅ Chaque transaction montre : Date, Description, Compte, Montant

**Problème rencontré** :
- Transactions en doublons (Restaurant et Carburant apparaissaient 2-3 fois)
- **Cause** : `AddTestTransactions.AddTwoTestTransactionsAsync()` était appelé à chaque démarrage

**Solution** :
Ajout d'une vérification `.Any()` pour éviter les doublons :
```csharp
var restaurantExists = context.Transactions
    .Any(t => t.Description == "Restaurant avec copains" && t.Amount == -45.50m);
var carburantExists = context.Transactions
    .Any(t => t.Description == "Carburant Shell" && t.Amount == -62.30m);

if (restaurantExists && carburantExists)
    return; // Les transactions existent déjà
```

---

### ✅ Étape 2 : Créer TransactionsViewModel

**Ce qu'on a fait** :
Création d'un nouveau ViewModel dédié aux transactions (`TransactionsViewModel.cs`) :

```csharp
public partial class TransactionsViewModel : ViewModelBase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;

    // Données affichées
    [ObservableProperty]
    private ObservableCollection<Transaction> transactions = new();

    [ObservableProperty]
    private ObservableCollection<Account> accounts = new();

    // Données du formulaire d'ajout
    [ObservableProperty]
    private DateTime newTransactionDate = DateTime.Now;

    [ObservableProperty]
    private decimal newTransactionAmount = 0;

    [ObservableProperty]
    private string newTransactionDescription = string.Empty;

    [ObservableProperty]
    private Account? selectedAccount = null;

    [ObservableProperty]
    private bool newTransactionIsDeferrable = false;

    [ObservableProperty]
    private bool isAddTransactionFormVisible = false;

    // Constructeur avec injection de dépendances
    public TransactionsViewModel(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        LoadDataAsync();
    }

    // Commandes
    [RelayCommand]
    public void ShowAddTransactionForm() { ... }

    [RelayCommand]
    public void HideAddTransactionForm() { ... }

    [RelayCommand]
    public async Task AddTransactionAsync() { ... }

    [RelayCommand]
    public void CancelAddTransaction() { ... }
}
```

**Commandes RelayCommand** :
- `ShowAddTransactionFormCommand` : Affiche le formulaire + réinitialise les champs
- `AddTransactionCommand` : Valide + ajoute à la base + ajoute à la collection observable
- `CancelAddTransactionCommand` / `HideAddTransactionFormCommand` : Ferme le formulaire

**Pourquoi un ViewModel séparé** :
- La logique des transactions est **indépendante** du MainWindow
- Permet de tester la logique d'ajout facilement
- Future-proof : si on veut une fenêtre de dialogue, on réutilise le même ViewModel

**Résultat** :
- ✅ TransactionsViewModel créé avec 6 ObservableProperty et 4 RelayCommand

---

### ✅ Étape 3 : Ajouter le TransactionsViewModel à la DI

**Ce qu'on a fait** :
Modification de `App.axaml.cs` :

```csharp
// Enregistrer les ViewModels
services.AddScoped<MainWindowViewModel>();
services.AddScoped<TransactionsViewModel>();  // ← NEW
```

**Résultat** :
- ✅ Le DI peut maintenant créer une instance de TransactionsViewModel

---

### ✅ Étape 4 : Créer le Formulaire d'Ajout en XAML

**Ce qu'on a fait** :
Ajout d'une Border avec formulaire dans `MainWindow.axaml` (onglet Transactions) :

```xml
<!-- Formulaire d'ajout (visible si IsAddTransactionFormVisible = true) -->
<Border Background="#FEF5E7"
        Padding="20"
        CornerRadius="8"
        BorderBrush="#F39C12"
        BorderThickness="2"
        IsVisible="{Binding IsAddTransactionFormVisible}">
    <StackPanel Spacing="15">
        <TextBlock Text="➕ Ajouter une Nouvelle Transaction"
                  FontSize="18"
                  FontWeight="SemiBold"/>

        <!-- Grille de formulaire (4 lignes) -->
        <Grid ColumnDefinitions="*,*" RowDefinitions="Auto,Auto,Auto,Auto" Spacing="15">
            <!-- Date -->
            <TextBlock Grid.Column="0" Grid.Row="0" Text="Date:"/>
            <CalendarDatePicker Grid.Column="1" Grid.Row="0"
                               SelectedDate="{Binding NewTransactionDate}"/>

            <!-- Montant -->
            <TextBlock Grid.Column="0" Grid.Row="1" Text="Montant (€):"/>
            <TextBox Grid.Column="1" Grid.Row="1"
                    Text="{Binding NewTransactionAmount}"
                    Watermark="Ex: -45.50 ou 2500"/>

            <!-- Description -->
            <TextBlock Grid.Column="0" Grid.Row="2" Text="Description:"/>
            <TextBox Grid.Column="1" Grid.Row="2"
                    Text="{Binding NewTransactionDescription}"
                    Watermark="Ex: Restaurant avec copains"/>

            <!-- Compte -->
            <TextBlock Grid.Column="0" Grid.Row="3" Text="Compte:"/>
            <ComboBox Grid.Column="1" Grid.Row="3"
                     ItemsSource="{Binding Accounts}"
                     SelectedItem="{Binding SelectedAccount}">
                <ComboBox.ItemTemplate>
                    <DataTemplate>
                        <TextBlock Text="{Binding Name}"/>
                    </DataTemplate>
                </ComboBox.ItemTemplate>
            </ComboBox>
        </Grid>

        <!-- Checkbox Lissable -->
        <CheckBox Content="Transaction Lissable (à étaler sur plusieurs mois)"
                 IsChecked="{Binding NewTransactionIsDeferrable}"/>

        <!-- Boutons -->
        <StackPanel Orientation="Horizontal" Spacing="10" HorizontalAlignment="Right">
            <Button Content="✅ Ajouter"
                   Padding="15,10"
                   Background="#27AE60"
                   Foreground="White"
                   Command="{Binding AddTransactionCommand}"/>
            <Button Content="❌ Annuler"
                   Padding="15,10"
                   Background="#E74C3C"
                   Foreground="White"
                   Command="{Binding CancelAddTransactionCommand}"/>
        </StackPanel>
    </StackPanel>
</Border>
```

**Éléments du formulaire** :
- `CalendarDatePicker` : Sélecteur de date avec calendrier
- `TextBox` : Champs texte avec watermark (texte gris placeholder)
- `ComboBox` : Dropdown pour sélectionner un compte (avec DataTemplate pour afficher le nom)
- `CheckBox` : Checkbox pour marquer comme "Lissable"
- `IsVisible` binding : Le formulaire n'apparaît que si `IsAddTransactionFormVisible = true`

**Résultat** :
- ✅ Formulaire complet d'ajout visible sur demande

---

### ✅ Étape 5 : Connecter le Bouton "Nouvelle Transaction"

**Ce qu'on a fait** :
Modification du bouton dans `MainWindow.axaml` :

```xml
<Button Grid.Column="1"
        Content="➕ Nouvelle Transaction"
        Padding="15,10"
        Command="{Binding ShowAddTransactionFormCommand}"/>
```

**Résultat** :
- ✅ Le bouton exécute la commande `ShowAddTransactionFormCommand`
- ✅ Le formulaire apparaît

---

### ✅ Étape 6 : Configurer le DataContext du TransactionsViewModel

**Ce qu'on a fait** :
Modification de `MainWindow.axaml.cs` pour assigner les ViewModels :

```csharp
public void SetupViewModels(MainWindowViewModel mainViewModel, TransactionsViewModel transactionsViewModel)
{
    // Le DataContext principal reste MainWindowViewModel
    this.DataContext = mainViewModel;

    // Assigner le TransactionsViewModel à l'onglet Transactions
    var tabControl = this.FindControl<TabControl>("TabControl");
    if (tabControl != null && tabControl.Items.Count >= 4)
    {
        var transactionsTab = (TabItem)tabControl.Items[3];
        transactionsTab.DataContext = transactionsViewModel;
    }
}
```

Et dans `App.axaml.cs` :
```csharp
var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
var transactionsViewModel = _serviceProvider.GetRequiredService<TransactionsViewModel>();

var mainWindow = new MainWindow();
mainWindow.SetupViewModels(mainWindowViewModel, transactionsViewModel);

desktop.MainWindow = mainWindow;
```

**Pourquoi cette approche ?** :
- Le header/footer du MainWindow utilise MainWindowViewModel
- L'onglet Transactions utilise TransactionsViewModel
- Les autres onglets utiliseront MainWindowViewModel pour l'instant

**Résultat** :
- ✅ Chaque partie de l'interface a le bon ViewModel

---

### ✅ Étape 7 : Logique d'Ajout de Transaction

**Ce qu'on a fait** :
Implémentation de `AddTransactionAsync()` dans TransactionsViewModel :

```csharp
[RelayCommand]
public async Task AddTransactionAsync()
{
    // Validation
    if (SelectedAccount == null || string.IsNullOrWhiteSpace(NewTransactionDescription) || NewTransactionAmount == 0)
        return;

    try
    {
        // Créer la nouvelle transaction
        var newTransaction = new Transaction
        {
            AccountId = SelectedAccount.Id,
            Date = NewTransactionDate,
            Amount = NewTransactionAmount,
            Description = NewTransactionDescription,
            IsDeferrable = NewTransactionIsDeferrable
        };

        // Ajouter à la base de données
        await _transactionRepository.AddAsync(newTransaction);

        // Ajouter à la collection observable (mise à jour UI)
        Transactions.Add(newTransaction);

        // Mettre à jour le solde du compte
        SelectedAccount.Balance += NewTransactionAmount;
        await _accountRepository.UpdateAsync(SelectedAccount);

        // Fermer le formulaire
        HideAddTransactionForm();
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"Erreur: {ex.Message}");
    }
}
```

**Étapes** :
1. Valide les champs (compte, description, montant)
2. Crée l'objet `Transaction`
3. L'ajoute à la base de données
4. L'ajoute à la collection observable (UI mise à jour immédiatement)
5. Met à jour le solde du compte
6. Ferme le formulaire

**Résultat** :
- ✅ Quand on clique "Ajouter", la transaction est enregistrée et apparaît dans la liste

---

### ✅ Étape 8 : Git - Initialisation du dépôt local et Push sur GitHub

**Ce qu'on a fait** :

1. **Initialiser Git** :
```bash
git init
```

2. **Créer .gitignore** (pour .NET) :
```
bin/
obj/
*.db
*.db-shm
*.db-wal
.vs/
.vscode/
```

3. **Ajouter tous les fichiers et créer le commit initial** :
```bash
git add .
git commit -m "Initial commit: Setup Compta_perso project with MVVM architecture, repositories, and transaction management"
```

4. **Configurer l'utilisateur Git** :
```bash
git config user.name "gizmo38"
git config user.email "gizmo38@gmail.com"
```

5. **Ajouter le remote GitHub** :
```bash
git remote add origin https://github.com/gizmo38/Compta_perso.git
git branch -M main
git push -u origin main
```

**Résultat** :
- ✅ Dépôt Git créé localement
- ✅ **42 fichiers** poussés sur GitHub
- ✅ **5828 lignes** de code
- ✅ Repository : https://github.com/gizmo38/Compta_perso

---

## 📊 Récapitulatif de la Session 5

### Fichiers créés :
```
src/Compta_perso/
├── ViewModels/
│   └── TransactionsViewModel.cs       ✅ (200 lignes)
├── Data/
│   └── AddTestTransactions.cs         ✅ (Correction doublons)
└── .gitignore                         ✅
```

### Fichiers modifiés :
```
src/Compta_perso/
├── Views/MainWindow.axaml             ✅ (Ajout formulaire + ItemsControl)
├── Views/MainWindow.axaml.cs          ✅ (SetupViewModels())
└── App.axaml.cs                       ✅ (Injection TransactionsViewModel)
```

### Architecture UI créée :

```
MainWindow (DataContext = MainWindowViewModel)
├── Header
│   └── Affiche TotalTresorerie + BudgetMois
└── TabControl
    └── Onglet "Transactions" (DataContext = TransactionsViewModel)
        ├── Bouton "Nouvelle Transaction"
        │   └── Appelle ShowAddTransactionFormCommand
        ├── ItemsControl (liste des transactions)
        │   └── DataTemplate affiche chaque transaction
        └── Border (Formulaire d'ajout)
            ├── CalendarDatePicker pour Date
            ├── TextBox pour Montant, Description
            ├── ComboBox pour sélectionner Compte
            ├── CheckBox pour "Lissable"
            └── Boutons ✅ Ajouter / ❌ Annuler
                ├── Ajouter → AddTransactionCommand
                └── Annuler → CancelAddTransactionCommand
```

### Concepts appris :
- **ItemsControl** : Génère l'UI pour chaque élément d'une collection
- **DataTemplate** : Définit comment afficher chaque élément
- **RelayCommand** : Liaison entre boutons et méthodes du ViewModel
- **Observable Collections** : Collections qui notifient l'UI des changements
- **CalendarDatePicker** : Sélecteur de date avec calendrier
- **ComboBox avec DataTemplate** : Dropdown personnalisé

### Problèmes résolus :
1. **Transactions en doublons** → Solution : Vérification `.Any()` avant ajout
2. **XAML MultiBinding error** → Solution : Simplification en binding simple
3. **SSH non configuré pour GitHub** → Solution : Utilisation HTTPS + GitHub CLI
4. **Dépôt n'existe pas sur GitHub** → Solution : Le créer automatiquement via CLI

### Architecture Final :

```
UI (XAML)
    ↓
ViewModels (MainWindowViewModel, TransactionsViewModel)
    ↓
Repositories (ITransactionRepository, IAccountRepository, IBudgetEntryRepository)
    ↓
Entity Framework Core (DbContext)
    ↓
SQLite Database (app.db)
```

---

## 🎯 Prochaines Sessions

### Phase 6 : Services Métier et Tests Unitaires
- [ ] Implémenter `AmortizationService` (moteur de lissage)
- [ ] Implémenter `BudgetCalculator` (calcul du "Reste à Vivre")
- [ ] Tests unitaires pour chaque service

### Phase 7 : Afficher les Vraies Données Partout
- [ ] Binder les autres onglets (Tableau de Bord, Budget, Comptes)
- [ ] Afficher les graphiques

### Phase 8 : Import CSV
- [ ] Créer `CsvTransactionImporter`
- [ ] Interface pour uploader un fichier CSV

---

**Durée de la session** : ~120 minutes
**Difficulté** : ⭐⭐⭐ Difficile (MVVM avancé, Git, formulaires)
**Status** : ✅ Session 5 terminée avec succès
**GitHub** : https://github.com/gizmo38/Compta_perso (main branch)
