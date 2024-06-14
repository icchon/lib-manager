using LibManager.Models;
using LibManager.ViewModels.Pages;
using System.Windows;
using Wpf.Ui.Controls;

namespace LibManager.Views.Pages
{
    public partial class AddBookSuccessPage : INavigableView<AddBookSuccessViewModel>
    {
        private ObservalProps _props = App.GetService<ObservalProps>();
        public AddBookSuccessViewModel ViewModel { get; }

        public AddBookSuccessPage(AddBookSuccessViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();

            Loaded += PageLoaded;
        }

        void PageLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.FetchToAddBook();
        }
       
    }
}
