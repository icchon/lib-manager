using System.Windows;
using Wpf.Ui.Controls;
using LibManager.ViewModels.Pages;
using LibManager.Models;
using Wpf.Ui;
using LibManager.Services;
using LibManager.ViewModels.Windows;


namespace LibManager.Views.Pages
{
    public partial class UserScanPage : INavigableView<UserScanViewModel>
    {
        private ObservalProps _props = App.GetService<ObservalProps>();
        private readonly INavigationService _navigationService = App.GetService<INavigationService>();
        private readonly MainWindowViewModel _mainWindowViewModel = App.GetService<MainWindowViewModel>();
        public UserScanViewModel ViewModel { get; }

        public UserScanPage(UserScanViewModel viewModel)
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
                
                if(BarcodeService.CheckIsUserCode(content))
                {
                    textBox.Focusable = false;
                    _props.NowUser = new User(content);
                    _mainWindowViewModel.UpdateUserName();

                    textBox.Text = "";
                    textBox.Focusable = true;

                    _navigationService.Navigate(typeof(HomePage));
                }

                if(length >= BarcodeService.USERCODE_LENGTH)
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
        }
        



    }
}
