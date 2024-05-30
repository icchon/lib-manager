using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Documents;
using System.Windows.Navigation;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LibManager.Views.Windows;
using LibManager.ViewModels.Windows;
using LibManager.Views.Pages;
using LibManager.ViewModels.Pages;
using LibManager.Services;
using Wpf.Ui;
using System;

namespace LibManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Wpf.Ui.NavigationService _navigationService;
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<ApplicationHostService>();

               
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<RentalPage>();
                services.AddSingleton<RentalViewModel>();
                services.AddSingleton<WaitingRentalPage>();
                services.AddSingleton<WaitingRentalViewModel>();
                services.AddSingleton<WaitingReturnPage>();
                services.AddSingleton<WaitingReturnViewModel>();
                services.AddSingleton<BooksPage>();
                services.AddSingleton<BooksViewModel>();

                services.AddSingleton<INavigationService, Wpf.Ui.NavigationService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();
                services.AddSingleton<IThemeService, ThemeService>();
            }).Build();

        public App()
        {
            var serviceCollection = new ServiceCollection();

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
        public static T GetService<T>()
            where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            _host.Start();
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
        }
    }

}
