using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System;
using System.Linq;
using Avalonia.Markup.Xaml;
using Compta_perso.Data;
using Compta_perso.Repositories;
using Compta_perso.ViewModels;
using Compta_perso.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Compta_perso;

public partial class App : Application
{
    /// <summary>
    /// Conteneur de Dependency Injection
    /// Stocke toutes les dépendances et créé les instances quand on en a besoin
    /// </summary>
    private IServiceProvider? _serviceProvider;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Configuration de la Dependency Injection
        var services = new ServiceCollection();

        // Enregistrer AppDbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=app.db")
        );

        // Enregistrer les Repositories
        // Quand on demande ITransactionRepository, le DI fournira une instance de TransactionRepository
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IBudgetEntryRepository, BudgetEntryRepository>();

        // Enregistrer les ViewModels
        services.AddScoped<MainWindowViewModel>();
        services.AddScoped<TransactionsViewModel>();

        // Construire le service provider
        _serviceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit.
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();

            // Initialiser les données de test (une seule fois)
            var dbContext = _serviceProvider.GetRequiredService<AppDbContext>();
            try
            {
                // Créer la base si elle n'existe pas
                dbContext.Database.EnsureCreated();

                // Ajouter les données de test si la base est vide
                SeedData.InitializeAsync(dbContext).Wait();

                // TEMPORAIRE : Ajouter 2 transactions de test (à supprimer après)
                AddTestTransactions.AddTwoTestTransactionsAsync(dbContext).Wait();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors de l'initialisation de la base: {ex.Message}");
            }

            // Créer les ViewModels via le DI
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            var transactionsViewModel = _serviceProvider.GetRequiredService<TransactionsViewModel>();

            // Créer la fenêtre principale
            var mainWindow = new MainWindow();

            // Configurer les ViewModels (assigner le TransactionsViewModel à l'onglet Transactions)
            mainWindow.SetupViewModels(mainWindowViewModel, transactionsViewModel);

            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}