using LibManager.ViewModels.Pages;
using Wpf.Ui.Controls;


namespace LibManager.Views.Pages
{
    public partial class BooksPage : INavigableView<BooksViewModel>
    {
        public BooksViewModel ViewModel { get; }

        public BooksPage(BooksViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
