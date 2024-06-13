using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibManager.Models;
using Wpf.Ui.Controls;
using Wpf.Ui;
using LibManager.Views.Pages;
using LibManager.ViewModels.Windows;
using System.Windows.Automation;

namespace LibManager.ViewModels.Pages
{
    public partial class WaitingRentalViewModel : ObservableObject
    {
        private const string firstTextBoxContent = "ここにユーザー名を入力してください";
        private ObservalProps _props = App.GetService<ObservalProps>();
        private readonly INavigationService _navigationService = App.GetService<INavigationService>();
        private readonly MainWindowViewModel _viewModel = App.GetService<MainWindowViewModel>();

        [ObservableProperty]
        private string _userName = firstTextBoxContent;
        [ObservableProperty]
        private string _barCode;

        [RelayCommand]
        private async Task ConfirmUserName()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                Debug.WriteLine("ユーザ名が空です");
                UserName = "ユーザー名が空です";
                return;
            }

            /*
            if(UserName == firstTextBoxContent)
            {
                Debug.WriteLine("ユーザー名が入力されていません");
                UserName = "ユーザー名が入力されていません";
                return;
            }

            if(UserName.Count() > 10)
            {
                Debug.WriteLine("ユーザー名が長いです");
                UserName = "ユーザー名が長いです";
                return;
            }
            */

            User user = new User(UserName);
            _props.NowUser = user;

            _navigationService.Navigate(typeof(BookScanPage));

            _viewModel.UpdateUserName();
            
        }

        public WaitingRentalViewModel()
        {
           
        }
        
    }
}
