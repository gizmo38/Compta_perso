# Script d'initialisation du projet Compta_perso
# PowerShell pour Windows

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Compta_perso - Initialisation Projet" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Vérifier .NET SDK
Write-Host "[1/8] Vérification de .NET SDK..." -ForegroundColor Yellow
try {
    $dotnetVersion = dotnet --version
    Write-Host "✓ .NET SDK installé : $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "✗ .NET SDK non trouvé. Installation..." -ForegroundColor Red
    Write-Host "Exécution : winget install Microsoft.DotNet.SDK.8" -ForegroundColor Yellow
    winget install Microsoft.DotNet.SDK.8
    Write-Host "Veuillez redémarrer votre terminal et relancer ce script." -ForegroundColor Yellow
    exit
}

Write-Host ""

# Installer les templates Avalonia
Write-Host "[2/8] Installation des templates Avalonia..." -ForegroundColor Yellow
dotnet new install Avalonia.Templates
Write-Host "✓ Templates Avalonia installés" -ForegroundColor Green
Write-Host ""

# Créer la solution
Write-Host "[3/8] Création de la solution..." -ForegroundColor Yellow
if (Test-Path "Compta_perso.sln") {
    Write-Host "⚠ Solution déjà existante, passage à l'étape suivante" -ForegroundColor Yellow
} else {
    dotnet new sln -n Compta_perso
    Write-Host "✓ Solution créée : Compta_perso.sln" -ForegroundColor Green
}
Write-Host ""

# Créer le projet Avalonia
Write-Host "[4/8] Création du projet Avalonia MVVM..." -ForegroundColor Yellow
if (Test-Path "src/Compta_perso/Compta_perso.csproj") {
    Write-Host "⚠ Projet principal déjà existant, passage à l'étape suivante" -ForegroundColor Yellow
} else {
    dotnet new avalonia.mvvm -n Compta_perso -o src/Compta_perso
    Write-Host "✓ Projet Avalonia créé : src/Compta_perso" -ForegroundColor Green
}
Write-Host ""

# Créer le projet de tests
Write-Host "[5/8] Création du projet de tests..." -ForegroundColor Yellow
if (Test-Path "src/Compta_perso.Tests/Compta_perso.Tests.csproj") {
    Write-Host "⚠ Projet de tests déjà existant, passage à l'étape suivante" -ForegroundColor Yellow
} else {
    dotnet new xunit -n Compta_perso.Tests -o src/Compta_perso.Tests
    Write-Host "✓ Projet de tests créé : src/Compta_perso.Tests" -ForegroundColor Green
}
Write-Host ""

# Ajouter les projets à la solution
Write-Host "[6/8] Ajout des projets à la solution..." -ForegroundColor Yellow
dotnet sln add src/Compta_perso/Compta_perso.csproj 2>$null
dotnet sln add src/Compta_perso.Tests/Compta_perso.Tests.csproj 2>$null
Write-Host "✓ Projets ajoutés à la solution" -ForegroundColor Green
Write-Host ""

# Ajouter la référence du projet de tests
Write-Host "[7/8] Ajout de la référence projet tests → projet principal..." -ForegroundColor Yellow
dotnet add src/Compta_perso.Tests/Compta_perso.Tests.csproj reference src/Compta_perso/Compta_perso.csproj 2>$null
Write-Host "✓ Référence ajoutée" -ForegroundColor Green
Write-Host ""

# Installer les packages NuGet
Write-Host "[8/8] Installation des packages NuGet..." -ForegroundColor Yellow
Push-Location src/Compta_perso

Write-Host "  - Entity Framework Core + SQLite..." -ForegroundColor Cyan
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --silent
dotnet add package Microsoft.EntityFrameworkCore.Design --silent

Write-Host "  - CommunityToolkit.Mvvm..." -ForegroundColor Cyan
dotnet add package CommunityToolkit.Mvvm --silent

Write-Host "  - FluentValidation (optionnel)..." -ForegroundColor Cyan
dotnet add package FluentValidation --silent

Pop-Location
Write-Host "✓ Packages NuGet installés" -ForegroundColor Green
Write-Host ""

# Résumé
Write-Host "========================================" -ForegroundColor Green
Write-Host "  ✓ Initialisation terminée avec succès !" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""
Write-Host "📂 Structure créée :" -ForegroundColor Cyan
Write-Host "   - Compta_perso.sln" -ForegroundColor White
Write-Host "   - src/Compta_perso/ (projet principal)" -ForegroundColor White
Write-Host "   - src/Compta_perso.Tests/ (tests unitaires)" -ForegroundColor White
Write-Host ""
Write-Host "📦 Packages installés :" -ForegroundColor Cyan
Write-Host "   - Avalonia UI" -ForegroundColor White
Write-Host "   - Entity Framework Core + SQLite" -ForegroundColor White
Write-Host "   - CommunityToolkit.Mvvm" -ForegroundColor White
Write-Host "   - FluentValidation" -ForegroundColor White
Write-Host ""
Write-Host "🚀 Prochaines étapes :" -ForegroundColor Yellow
Write-Host "   1. Lancer l'application :" -ForegroundColor White
Write-Host "      dotnet run --project src/Compta_perso" -ForegroundColor Gray
Write-Host ""
Write-Host "   2. Lancer les tests :" -ForegroundColor White
Write-Host "      dotnet test" -ForegroundColor Gray
Write-Host ""
Write-Host "   3. Ouvrir dans votre IDE :" -ForegroundColor White
Write-Host "      code . (VS Code)" -ForegroundColor Gray
Write-Host "      start Compta_perso.sln (Visual Studio)" -ForegroundColor Gray
Write-Host ""
Write-Host "📖 Documentation : Voir .claude/CLAUDE.md" -ForegroundColor Cyan
Write-Host ""
