using System.Windows;
using Wpf.Ui.Controls;
using LibManager.ViewModels.Pages;


namespace LibManager.Views.Pages
{
    public partial class HomePage : INavigableView<HomeViewModel>
    {
        public HomeViewModel ViewModel { get; }

        public HomePage(HomeViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            Loaded += PageLoaded;
        }
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.FetchIsLogin();
        }
    }
    
    
}

