using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using LibManager.ViewModels.Pages;
using System.Windows.Automation;
using static System.Net.WebRequestMethods;


namespace LibManager.Views.Pages
{
    /// <summary>
    /// RentalPage.xaml の相互作用ロジック
    /// </summary>
    public partial class WaitingRentalPage : INavigableView<WaitingRentalViewModel>
    {
        public WaitingRentalViewModel ViewModel { get; }

        public WaitingRentalPage(WaitingRentalViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            var t = new System.Windows.Controls.TextBox();
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

                if (length == "1234567890128".Length)
                {
                    textBox.Focusable = false;
                }
                
                
                
            };
            
            
            DataContext = this;

            Loaded += WaitingRentalPageLoaded;
            
        }
        void WaitingRentalPageLoaded(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
            textBox.Focusable = true;
        }
        



    }
}
