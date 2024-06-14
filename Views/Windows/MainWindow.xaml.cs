using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibManager.ViewModels.Windows;
using Microsoft.Extensions.DependencyInjection;
using Notification.Wpf;
using Wpf.Ui;
using Wpf.Ui.Controls;
using LibManager.Views.Pages;
using System.Diagnostics;
using Notification.Wpf.Controls;


namespace LibManager.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly INavigationService _navigationService;
        public MainWindowViewModel ViewModel { get; }

        public MainWindow(
            MainWindowViewModel viewModel,
            INavigationService navigationService,
            IServiceProvider serviceProvider
        )
        {
            Wpf.Ui.Appearance.SystemThemeWatcher.Watch(this);

            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            NavigationView.SetServiceProvider(serviceProvider);
            navigationService.SetNavigationControl(NavigationView);

            _navigationService = navigationService;
            Loaded += MainWindowLoaded;
        }

        private  void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(typeof(HomePage));
        }




    }
}