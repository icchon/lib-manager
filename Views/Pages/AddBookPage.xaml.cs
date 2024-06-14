using LibManager.Models;
using LibManager.ViewModels.Pages;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui;
using Wpf.Ui.Controls;
using LibManager.Services;

namespace LibManager.Views.Pages
{
    public partial class AddBookPage : INavigableView<AddBookViewModel>
    {
        private ObservalProps _props = App.GetService<ObservalProps>(); 
        private INavigationService _navigationService = App.GetService<INavigationService>();
        public AddBookViewModel ViewModel { get; }

        public AddBookPage(AddBookViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();

            textBox.Focus();
            textBox.LostKeyboardFocus += (sender, e) => textBox.Focus();


            textBox.TextChanged += OnTextChangedAsync;

            Loaded += PageLoaded;

        }
        void PageLoaded(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
            textBox.Focusable = true;
            ViewModel.Visible = false;

            if (String.IsNullOrEmpty(_props.NowUser.Name))
            {
                _navigationService.Navigate(typeof(UserScanPage));
            }
        }
        private async void OnTextChangedAsync(object sender, TextChangedEventArgs e)
        {
            string content = textBox.Text;
            int length = content.Length;
            if (length == 0)
            {
                return;
            }
            char latestChar = content[length - 1];


            if (!Char.IsDigit(latestChar))
            {
                textBox.Text = content.Substring(0, length - 1);
            }

            if (BarcodeService.CheckIsIsbn(content))
            {
                textBox.Text = "";
                _props.Isbn = content;
                await ViewModel?.DisplayBookAsync();
            }

            if(length >= BarcodeService.ISBN_LENGTH)
            {
                textBox.Text = "";
            }
        }
    }
}
