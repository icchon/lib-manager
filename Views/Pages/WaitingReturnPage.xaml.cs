using LibManager.ViewModels.Pages;
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


namespace LibManager.Views.Pages
{
    /// <summary>
    /// WaitingReturnPage.xaml の相互作用ロジック
    /// </summary>
    public partial class WaitingReturnPage : INavigableView<WaitingReturnViewModel>
    {
        public WaitingReturnViewModel ViewModel { get; }

        public WaitingReturnPage(WaitingReturnViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
