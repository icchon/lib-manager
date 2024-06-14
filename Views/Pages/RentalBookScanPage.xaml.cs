using LibManager.ViewModels.Pages;
using System.Windows;
using Wpf.Ui.Controls;
using static LibManager.Models.BookModels;
using LibManager.Models;
using System.Collections.ObjectModel;
using LibManager.Services;

namespace LibManager.Views.Pages
{
    public partial class RentalBookScanPage : INavigableView<RentalBookScanViewModel>
    {
        private ObservalProps _props = App.GetService<ObservalProps>();
        public RentalBookScanViewModel ViewModel { get; }

        public RentalBookScanPage(RentalBookScanViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();

            textBox.Focus();
            textBox.LostKeyboardFocus += (sender, e) => textBox.Focus();


            textBox.TextChanged += (sender, e) =>
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
                    ViewModel.AddRentalBook();
                }

                if(length >= BarcodeService.ISBN_LENGTH)
                {
                    textBox.Text = "";
                }



            };

            Loaded += PageLoaded;

        }
        void PageLoaded(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
            textBox.Focusable = true;
            ViewModel.Books = new ObservableCollection<Book>();
        }
    }
}
