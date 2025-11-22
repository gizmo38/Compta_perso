# Glossaire - Vocabulaire Technique Simplifié

Ce glossaire explique tous les termes techniques utilisés dans le projet, avec des analogies concrètes.

---

## A

### **Application Desktop**
Application qui s'installe sur votre ordinateur (comme Word, Excel, VS Code).
- **Contraire** : Application web (qui fonctionne dans le navigateur)
- **Exemple** : Notre application Compta_perso sera une application desktop

### **Architecture**
Façon dont le code est organisé et structuré.
- **Analogie** : Comme le plan d'une maison (où sont les pièces, comment elles communiquent)
- **Exemple** : MVVM est une architecture qui sépare l'interface (View), la logique (ViewModel), et les données (Model)

### **Avalonia UI**
Framework pour créer des interfaces graphiques cross-platform (Windows, Linux, macOS).
- **Analogie** : Comme une boîte à outils pour construire des fenêtres, boutons, menus
- **Alternative** : WPF (Windows uniquement), WinForms (ancien), Electron (lourd)

---

## B

### **Base de données**
Endroit où les données sont stockées de manière organisée.
- **Analogie** : Comme un classeur géant avec des tiroirs (tables) et des fiches (lignes)
- **Exemple** : SQLite stockera nos comptes, transactions, et budgets

### **Build**
Action de compiler (transformer) le code source en application exécutable.
- **Analogie** : Comme transformer une recette (code) en plat cuisiné (application)
- **Commande** : `dotnet build`

---

## C

### **C#**
Langage de programmation créé par Microsoft, moderne et puissant.
- **Analogie** : Comme l'anglais ou le français, mais pour parler aux ordinateurs
- **Prononciation** : "C sharp"

### **CLI (Command Line Interface)**
Interface en ligne de commande (le terminal noir avec du texte).
- **Contraire** : GUI (Interface graphique)
- **Exemple** : PowerShell, CMD

### **Code First**
Approche où on écrit d'abord le code (les classes C#), puis Entity Framework crée automatiquement la base de données.
- **Contraire** : Database First (base de données existe déjà)
- **Avantage** : Plus simple pour les débutants

### **Commit (Git)**
Sauvegarder un snapshot du code à un moment donné.
- **Analogie** : Comme faire une photo de votre travail à un instant T
- **Commande** : `git commit -m "message"`

### **Compiler**
Transformer le code source (lisible par les humains) en code machine (lisible par l'ordinateur).
- **Analogie** : Traduire un livre du français vers le binaire
- **Outil** : Le compilateur C# de .NET

### **Cross-platform**
Qui fonctionne sur plusieurs systèmes d'exploitation (Windows, Linux, macOS).
- **Exemple** : Avalonia est cross-platform (contrairement à WPF qui est Windows uniquement)

---

## D

### **Dépendance**
Package externe dont votre projet a besoin pour fonctionner.
- **Analogie** : Comme les ingrédients d'une recette
- **Exemple** : Avalonia.UI, EntityFrameworkCore.Sqlite

### **.NET**
Plateforme de développement créée par Microsoft pour créer des applications.
- **Analogie** : Comme un ensemble d'outils et de bibliothèques pour construire des apps
- **Version actuelle** : .NET 8 (LTS = Long Term Support)

---

## E

### **Entity Framework Core (EF Core)**
ORM (Object-Relational Mapping) qui permet de manipuler une base de données avec du code C#.
- **Analogie** : Traducteur entre vos objets C# et les tables SQL
- **Avantage** : Pas besoin d'écrire du SQL à la main

### **Exécutable**
Fichier que vous pouvez lancer directement (comme .exe sur Windows).
- **Résultat** : L'application se lance
- **Création** : `dotnet publish`

---

## F

### **Framework**
Ensemble d'outils et de bibliothèques qui facilitent le développement.
- **Analogie** : Comme une boîte à outils complète pour construire une maison
- **Exemples** : .NET, Avalonia UI, Entity Framework Core

---

## G

### **Git**
Système de contrôle de version (historique des modifications du code).
- **Analogie** : Machine à remonter le temps pour votre code
- **Commandes** : `git add`, `git commit`, `git push`

### **GUI (Graphical User Interface)**
Interface graphique (fenêtres, boutons, menus).
- **Contraire** : CLI (ligne de commande)
- **Exemple** : L'interface d'Avalonia

---

## I

### **IDE (Integrated Development Environment)**
Logiciel pour écrire du code (avec coloration syntaxique, autocomplétion, débogage).
- **Exemples** : Visual Studio, Rider, VS Code
- **Analogie** : Comme Word, mais pour le code

---

## L

### **Local-First**
Approche où les données sont stockées localement sur votre machine (pas dans le cloud).
- **Avantage** : Fonctionne sans Internet, vos données restent privées
- **Exemple** : Notre application Compta_perso

### **LTS (Long Term Support)**
Version supportée sur le long terme (mises à jour de sécurité pendant plusieurs années).
- **Exemple** : .NET 8 est LTS (support jusqu'en 2026)

---

## M

### **Migration (EF Core)**
Script qui décrit les changements à apporter à la base de données.
- **Analogie** : Plan de transformation de la base de données
- **Commande** : `dotnet ef migrations add NomMigration`

### **Model**
Classe C# qui représente une entité métier (compte, transaction, etc.).
- **Exemple** : `Account`, `Transaction`, `BudgetEntry`
- **Analogie** : Comme un formulaire papier avec des champs

### **MVVM (Model-View-ViewModel)**
Pattern d'architecture qui sépare :
- **Model** : Données (Account, Transaction)
- **View** : Interface graphique (.axaml)
- **ViewModel** : Logique entre les deux
- **Analogie** : Comme séparer la cuisine (Model), la salle à manger (View), et le serveur (ViewModel)

---

## N

### **NuGet**
Gestionnaire de packages pour .NET (équivalent de npm pour Node.js).
- **Site** : nuget.org
- **Commande** : `dotnet add package <nom>`

---

## O

### **ORM (Object-Relational Mapping)**
Outil qui traduit entre objets C# et tables SQL.
- **Exemple** : Entity Framework Core
- **Avantage** : Pas besoin d'écrire du SQL

---

## P

### **Package**
Bibliothèque externe réutilisable.
- **Analogie** : Comme une pièce détachée qu'on ajoute à notre projet
- **Exemple** : Avalonia.UI, EntityFrameworkCore.Sqlite

### **Pattern**
Solution réutilisable à un problème courant.
- **Exemple** : MVVM, Repository, Singleton
- **Analogie** : Comme une recette de cuisine éprouvée

### **PowerShell**
Terminal (ligne de commande) moderne de Windows.
- **Reconnaissable** : Texte bleu sur fond noir
- **Extension** : `.ps1`

### **Projet (.csproj)**
Unité de code qui produit un exécutable ou une bibliothèque.
- **Contient** : Fichiers de code, références aux packages
- **Format** : XML

---

## R

### **Référence**
Lien entre deux projets (pour que l'un puisse utiliser le code de l'autre).
- **Commande** : `dotnet add reference`
- **Exemple** : Le projet de tests référence le projet principal

### **Repository (dépôt)**
Dossier qui contient tout le code du projet + historique Git.
- **Analogie** : Coffre-fort qui contient votre code
- **Exemple** : `Compta_perso/`

### **Restore**
Télécharger toutes les dépendances (packages NuGet) du projet.
- **Commande** : `dotnet restore`
- **Analogie** : Télécharger tous les ingrédients avant de cuisiner

---

## S

### **SDK (Software Development Kit)**
Ensemble d'outils pour développer des applications.
- **Exemple** : .NET SDK contient le compilateur, les outils CLI, etc.
- **Installation** : `winget install Microsoft.DotNet.SDK.8`

### **Solution (.sln)**
Conteneur qui regroupe plusieurs projets.
- **Analogie** : Classeur qui contient plusieurs dossiers
- **Ouverture** : Visual Studio, Rider

### **SQLite**
Base de données légère stockée dans un seul fichier.
- **Avantage** : Simple, pas de serveur à installer
- **Fichier** : `.db` ou `.sqlite`

---

## T

### **Template**
Modèle de départ pour créer un projet.
- **Exemple** : `avalonia.mvvm`, `xunit`, `console`
- **Commande** : `dotnet new <template>`

### **Test Unitaire**
Code qui vérifie automatiquement qu'une fonctionnalité marche correctement.
- **Analogie** : Inspecteur qui vérifie chaque pièce de la maison
- **Framework** : xUnit, NUnit, MSTest

---

## V

### **View**
Fichier qui décrit l'interface graphique (fenêtres, boutons, etc.).
- **Extension** : `.axaml` (Avalonia XAML)
- **Rôle** : Affichage uniquement (pas de logique)

### **ViewModel**
Classe qui fait le lien entre la View (interface) et le Model (données).
- **Rôle** : Contient la logique de présentation
- **Pattern** : MVVM

---

## X

### **XAML (eXtensible Application Markup Language)**
Langage pour décrire des interfaces graphiques (comme HTML mais pour desktop).
- **Prononciation** : "zamel"
- **Extension** : `.axaml` (Avalonia), `.xaml` (WPF)
- **Exemple** :
  ```xml
  <Button Content="Cliquez-moi" />
  ```

### **xUnit**
Framework de tests unitaires pour .NET.
- **Alternatives** : NUnit, MSTest
- **Commande** : `dotnet test`

---

## 📚 Ressources pour Aller Plus Loin

- **Documentation .NET** : https://learn.microsoft.com/dotnet/
- **Documentation Avalonia** : https://docs.avaloniaui.net/
- **Documentation EF Core** : https://learn.microsoft.com/ef/core/

---

**Astuce** : Ce glossaire sera mis à jour au fur et à mesure que vous apprendrez de nouveaux concepts ! 📖
