# Guide 1 : Comprendre .NET, Avalonia et MVVM

Guide détaillé pour comprendre les technologies utilisées dans le projet, avec des explications simples et des schémas.

---

## 🎯 C'est quoi .NET ?

### Définition Simple
**.NET** est une **plateforme de développement** créée par Microsoft qui permet de créer des applications :
- Desktop (Windows, Linux, macOS)
- Web (sites Internet, APIs)
- Mobile (Android, iOS via MAUI)
- Console (programmes en ligne de commande)

### Analogie
Imaginez .NET comme une **grande boîte à outils** qui contient :
- Un **compilateur** (transforme votre code C# en programme exécutable)
- Des **bibliothèques** (milliers de fonctions toutes prêtes)
- Des **outils en ligne de commande** (`dotnet`)

### Les Versions de .NET

```
.NET Framework (2002-2022)   → Windows uniquement (ancien)
.NET Core (2016-2020)        → Cross-platform (nouveau)
.NET 5, 6, 7, 8, 9...        → Évolution de .NET Core (moderne)
```

**Nous utilisons : .NET 8** (LTS = Support jusqu'en 2026)

### Pourquoi .NET 8 ?
- ✅ **LTS** (Long Term Support) : Mises à jour pendant 3 ans
- ✅ **Stable** : Pas de bugs critiques
- ✅ **Performant** : Rapide et optimisé
- ✅ **Cross-platform** : Fonctionne sur Windows, Linux, macOS

---

## 🎨 C'est quoi Avalonia UI ?

### Définition Simple
**Avalonia UI** est un **framework pour créer des interfaces graphiques** (fenêtres, boutons, menus) qui fonctionne sur **tous les systèmes d'exploitation**.

### Comparaison avec d'autres frameworks

| Framework | Cross-platform ? | Moderne ? | Poids |
|-----------|------------------|-----------|-------|
| **Avalonia** | ✅ Oui (Win, Linux, macOS) | ✅ Oui | 🟢 Léger |
| WPF | ❌ Windows uniquement | ⚠️ Ancien | 🟢 Léger |
| WinForms | ❌ Windows uniquement | ❌ Très ancien | 🟢 Léger |
| Electron | ✅ Oui | ✅ Oui | 🔴 Très lourd |

### Pourquoi Avalonia pour Compta_perso ?
1. **Cross-platform** : Si un jour vous voulez utiliser l'app sur Linux/macOS, c'est possible
2. **Moderne** : Utilise XAML (comme WPF) mais en mieux
3. **Léger** : Pas besoin d'embarquer un navigateur Chrome (comme Electron)
4. **Local-First** : Pas besoin d'Internet

### Architecture d'Avalonia

```
┌─────────────────────────────────────┐
│         Votre Application           │
│         (Code C#)                   │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│         Avalonia UI                 │
│   (Framework d'interface)           │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│         .NET Runtime                │
│   (Exécute le code)                 │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│    Système d'exploitation           │
│    (Windows, Linux, macOS)          │
└─────────────────────────────────────┘
```

---

## 🏗️ C'est quoi MVVM ?

### Définition Simple
**MVVM** (Model-View-ViewModel) est une **architecture** (façon d'organiser le code) qui sépare :
- **Ce qui est affiché** (View)
- **La logique** (ViewModel)
- **Les données** (Model)

### Pourquoi séparer ?
Sans MVVM :
```
❌ Tout le code mélangé dans un seul fichier
❌ Difficile à tester
❌ Difficile à maintenir
❌ Impossible de réutiliser
```

Avec MVVM :
```
✅ Code organisé en 3 couches claires
✅ Facile à tester (on teste le ViewModel sans l'interface)
✅ Facile à maintenir (changer l'interface ne casse pas la logique)
✅ Réutilisable (plusieurs Views peuvent utiliser le même ViewModel)
```

---

### Les 3 Couches de MVVM

#### 1. **Model** (Modèle de Données)

**Rôle** : Représente les **données métier**

**Exemple pour Compta_perso** :
```csharp
// Model : Account (Compte bancaire)
public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }        // Ex: "Compte Courant BNP"
    public decimal Balance { get; set; }    // Ex: 2450.00
    public AccountCategory Category { get; set; }
}
```

**Analogie** : C'est comme une **fiche papier** avec des cases à remplir.

---

#### 2. **View** (Vue / Interface Graphique)

**Rôle** : Affiche les données à l'utilisateur (fenêtres, boutons, textes)

**Fichier** : `.axaml` (XAML Avalonia)

**Exemple** :
```xml
<!-- View : Affiche le nom et le solde d'un compte -->
<StackPanel>
    <TextBlock Text="{Binding AccountName}" FontSize="20" />
    <TextBlock Text="{Binding Balance}" FontSize="16" />
</StackPanel>
```

**Analogie** : C'est comme l'**écran d'un distributeur de billets** qui affiche vos infos.

**Important** : La View **ne contient PAS de logique** ! Elle affiche juste ce que le ViewModel lui donne.

---

#### 3. **ViewModel** (Lien entre Model et View)

**Rôle** : Contient la **logique de présentation** et fait le lien entre Model et View

**Exemple** :
```csharp
// ViewModel : Gère l'affichage d'un compte
public class AccountViewModel : ViewModelBase
{
    private Account _account;  // Le Model

    // Propriété que la View va afficher
    public string AccountName => _account.Name;

    // Propriété calculée (formatage)
    public string Balance => $"{_account.Balance:C}";  // Ex: "2 450,00 €"

    // Commande (action d'un bouton)
    public ICommand AddMoneyCommand { get; }
}
```

**Analogie** : C'est comme le **logiciel interne** du distributeur qui prend vos données (Model) et les prépare pour l'affichage (View).

---

### Schéma Complet MVVM

```
┌────────────────────────────────────────────────────────┐
│                      UTILISATEUR                       │
│                    (Vous, moi)                         │
└───────────────────────┬────────────────────────────────┘
                        │
                        │ Voit et clique
                        ▼
┌────────────────────────────────────────────────────────┐
│                    VIEW (.axaml)                       │
│                                                        │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐           │
│  │  Button  │  │TextBlock │  │DataGrid  │           │
│  └──────────┘  └──────────┘  └──────────┘           │
│                                                        │
│  {Binding AccountName}    ←─────┐                    │
│  {Binding Balance}        ←─────┤                    │
└────────────────────────────────┬┴────────────────────┘
                                 │
                                 │ Data Binding (liaison automatique)
                                 │
┌────────────────────────────────▼────────────────────────┐
│              VIEWMODEL (Logique)                        │
│                                                          │
│  public string AccountName { get; }                     │
│  public string Balance { get; }                         │
│  public ICommand AddMoneyCommand { get; }               │
│                                                          │
│  - Formate les données pour l'affichage                │
│  - Gère les actions (boutons)                          │
│  - Notifie la View quand les données changent          │
└────────────────────────────────┬────────────────────────┘
                                 │
                                 │ Utilise
                                 ▼
┌────────────────────────────────────────────────────────┐
│              MODEL (Données)                           │
│                                                        │
│  public class Account                                  │
│  {                                                     │
│      public int Id { get; set; }                      │
│      public string Name { get; set; }                 │
│      public decimal Balance { get; set; }             │
│  }                                                     │
│                                                        │
│  - Représente les données métier                      │
│  - Aucune logique d'affichage                         │
└────────────────────────────────────────────────────────┘
```

---

### Exemple Concret : Afficher un Compte Bancaire

#### Étape 1 : Le Model (Données)

```csharp
// Models/Account.cs
public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
}
```

---

#### Étape 2 : Le ViewModel (Logique)

```csharp
// ViewModels/AccountViewModel.cs
public class AccountViewModel : ViewModelBase
{
    private Account _account;

    public AccountViewModel(Account account)
    {
        _account = account;
    }

    // Propriété pour afficher le nom
    public string AccountName => _account.Name;

    // Propriété pour afficher le solde (formaté en euros)
    public string FormattedBalance => $"{_account.Balance:C}";

    // Commande pour ajouter de l'argent
    public ICommand AddMoneyCommand => new RelayCommand(AddMoney);

    private void AddMoney()
    {
        _account.Balance += 100;
        // Notifie la View que Balance a changé
        OnPropertyChanged(nameof(FormattedBalance));
    }
}
```

---

#### Étape 3 : La View (Interface)

```xml
<!-- Views/AccountView.axaml -->
<StackPanel>
    <!-- Affiche le nom du compte -->
    <TextBlock Text="{Binding AccountName}" FontSize="24" />

    <!-- Affiche le solde formaté -->
    <TextBlock Text="{Binding FormattedBalance}" FontSize="18" />

    <!-- Bouton qui appelle la commande AddMoneyCommand -->
    <Button Content="Ajouter 100€" Command="{Binding AddMoneyCommand}" />
</StackPanel>
```

---

### Ce qui se passe en coulisses

1. **L'utilisateur voit** : "Compte Courant BNP" et "2 450,00 €"
2. **L'utilisateur clique** sur le bouton "Ajouter 100€"
3. **La View appelle** la commande `AddMoneyCommand` du ViewModel
4. **Le ViewModel exécute** la méthode `AddMoney()` qui modifie `_account.Balance`
5. **Le ViewModel notifie** la View avec `OnPropertyChanged`
6. **La View se met à jour automatiquement** : "2 550,00 €"

**Magie ? Non, c'est le Data Binding !** 🪄

---

## 🔗 Data Binding (Liaison de Données)

### C'est quoi ?
Le **Data Binding** est un mécanisme qui **synchronise automatiquement** la View et le ViewModel.

### Comment ça marche ?

```xml
<TextBlock Text="{Binding AccountName}" />
```

**Traduction** :
- "Affiche dans ce TextBlock la valeur de la propriété `AccountName` du ViewModel"
- "Si `AccountName` change, mets à jour le texte automatiquement"

### Sans Data Binding (à l'ancienne)

```csharp
// ❌ À l'ancienne (code verbeux et fragile)
private void UpdateUI()
{
    textBlockName.Text = account.Name;
    textBlockBalance.Text = account.Balance.ToString("C");
}

// Il faut appeler UpdateUI() à chaque changement !
```

### Avec Data Binding (moderne)

```xml
<!-- ✅ Moderne (automatique) -->
<TextBlock Text="{Binding AccountName}" />
<TextBlock Text="{Binding FormattedBalance}" />
```

Le ViewModel notifie automatiquement la View avec `OnPropertyChanged`.

---

## 🎓 Récapitulatif

### .NET
- ✅ Plateforme de développement Microsoft
- ✅ Cross-platform (Windows, Linux, macOS)
- ✅ Utilisé pour desktop, web, mobile

### Avalonia UI
- ✅ Framework pour créer des interfaces graphiques
- ✅ Cross-platform
- ✅ Moderne et léger

### MVVM
- ✅ Architecture qui sépare Model, View, ViewModel
- ✅ Code organisé et testable
- ✅ Data Binding automatique

---

## 📚 Ressources

- [Documentation .NET](https://learn.microsoft.com/dotnet/)
- [Documentation Avalonia](https://docs.avaloniaui.net/)
- [Tutoriel MVVM](https://learn.microsoft.com/dotnet/architecture/maui/mvvm)

---

**Prochain guide** : `02-entity-framework-sqlite.md` (Comment gérer une base de données ?)
