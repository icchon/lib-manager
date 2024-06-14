using LibManager.Models;
using LibManager.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace LibManager.Views.Pages
{
    public partial class ReturnSuccessPage : INavigableView<ReturnSuccessViewModel>
    {
        private ObservalProps _props = App.GetService<ObservalProps>();
        public ReturnSuccessViewModel ViewModel { get; }

        public ReturnSuccessPage(ReturnSuccessViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();

            Loaded += PageLoaded;

        }

        private void PageLoaded(object sender, EventArgs e)
        {
            ViewModel.FetchToReturnBook();
        }
    }
}
