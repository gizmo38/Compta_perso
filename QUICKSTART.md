# Guide de Démarrage Rapide - Compta_perso

Guide express pour initialiser et lancer le projet en 5 minutes.

---

## ⚡ Démarrage Ultra-Rapide (Windows)

### Option 1 : Script Automatique (Recommandé)

```powershell
# Ouvrir PowerShell dans le dossier du projet
cd C:\Users\franc\Documents\GitHub\Compta_perso

# Exécuter le script d'initialisation
.\init-project.ps1
```

Le script fait **tout automatiquement** :
- ✅ Vérifie .NET SDK (installe si nécessaire)
- ✅ Installe les templates Avalonia
- ✅ Crée la solution + projets
- ✅ Installe les packages NuGet

---

### Option 2 : Manuel (Si le script ne fonctionne pas)

```bash
# 1. Installer .NET 8 SDK
winget install Microsoft.DotNet.SDK.8

# 2. Installer templates Avalonia
dotnet new install Avalonia.Templates

# 3. Créer la solution
dotnet new sln -n Compta_perso

# 4. Créer les projets
dotnet new avalonia.mvvm -n Compta_perso -o src/Compta_perso
dotnet new xunit -n Compta_perso.Tests -o src/Compta_perso.Tests

# 5. Ajouter à la solution
dotnet sln add src/Compta_perso/Compta_perso.csproj
dotnet sln add src/Compta_perso.Tests/Compta_perso.Tests.csproj

# 6. Référence tests → projet
dotnet add src/Compta_perso.Tests reference src/Compta_perso

# 7. Packages NuGet
cd src/Compta_perso
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package CommunityToolkit.Mvvm
dotnet add package FluentValidation
cd ../..
```

---

## 🚀 Lancer l'Application

```bash
# Depuis la racine du projet
dotnet run --project src/Compta_perso
```

**Résultat attendu** : Une fenêtre Avalonia s'ouvre avec l'interface par défaut.

---

## 🧪 Lancer les Tests

```bash
dotnet test
```

---

## 📝 Prochaines Étapes

### Phase 2 : Créer les Modèles de Données

1. **Créer les enums** :
   ```bash
   mkdir src/Compta_perso/Models/Enums
   ```

2. **Créer les fichiers** :
   - `src/Compta_perso/Models/Enums/AccountCategory.cs`
   - `src/Compta_perso/Models/Enums/BudgetEntryType.cs`
   - `src/Compta_perso/Models/Account.cs`
   - `src/Compta_perso/Models/Transaction.cs`
   - `src/Compta_perso/Models/BudgetEntry.cs`

3. **Créer le DbContext** :
   - `src/Compta_perso/Data/AppDbContext.cs`

4. **Créer la première migration** :
   ```bash
   cd src/Compta_perso
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

---

### Phase 3 : Créer le Moteur de Lissage

1. **Créer le service** :
   - `src/Compta_perso/Services/AmortizationService.cs`

2. **Créer les tests** :
   - `src/Compta_perso.Tests/Services/AmortizationServiceTests.cs`

3. **Tester le lissage** :
   ```bash
   dotnet test --filter "FullyQualifiedName~AmortizationServiceTests"
   ```

---

### Phase 4 : Créer l'Interface Utilisateur

1. **Créer les ViewModels** :
   - `src/Compta_perso/ViewModels/TransactionViewModel.cs`
   - `src/Compta_perso/ViewModels/BudgetViewModel.cs`

2. **Créer les Vues** :
   - `src/Compta_perso/Views/TransactionView.axaml`
   - `src/Compta_perso/Views/BudgetView.axaml`

3. **Ajouter le switch Mode Trésorerie/Budget** dans `MainView.axaml`

---

## 🛠️ Commandes Utiles

```bash
# Lancer en mode Watch (recompile automatiquement)
dotnet watch --project src/Compta_perso

# Build Release
dotnet build -c Release

# Voir la structure de la solution
dotnet sln list

# Restaurer les packages
dotnet restore

# Nettoyer les builds
dotnet clean
```

---

## 📚 Documentation Complète

- **Architecture et conventions** : `.claude/CLAUDE.md`
- **Vue d'ensemble** : `README.md`
- **Ce guide** : `QUICKSTART.md`

---

## ❓ Problèmes Courants

### "dotnet: command not found"
→ .NET SDK pas installé. Exécuter : `winget install Microsoft.DotNet.SDK.8`

### "Template 'Avalonia MVVM Application' not found"
→ Templates pas installés. Exécuter : `dotnet new install Avalonia.Templates`

### "The project file does not exist"
→ Vous n'êtes pas dans le bon dossier. Vérifier : `cd C:\Users\franc\Documents\GitHub\Compta_perso`

### L'application ne se lance pas
→ Vérifier les logs : `dotnet run --project src/Compta_perso --verbosity detailed`

---

## 📞 Aide

- **Claude Code** : Tapez `/help` pour les commandes Claude
- **Documentation Avalonia** : https://docs.avaloniaui.net/
- **Documentation .NET** : https://learn.microsoft.com/dotnet/

---

**Bonne chance ! 🚀**
