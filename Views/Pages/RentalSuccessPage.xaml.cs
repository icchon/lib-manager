using LibManager.Models;
using LibManager.ViewModels.Pages;
using System.Windows;
using Wpf.Ui.Controls;

namespace LibManager.Views.Pages
{
    /// <summary>
    /// RentalSuccess.xaml の相互作用ロジック
    /// </summary>
    public partial class RentalSuccessPage : INavigableView<RentalSuccessViewModel>
    {
        private ObservalProps _props = App.GetService<ObservalProps>();
        public RentalSuccessViewModel ViewModel { get; }

        public RentalSuccessPage(RentalSuccessViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();

            Loaded += PageLoaded;

        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.FetchToRentBooks();
        }
    }
}
