using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui;
using LibManager.Views.Pages;
using Wpf.Ui.Controls;
using System.Drawing.Printing;
using LibManager.Models;
using LibManager.ViewModels.Windows;
using System.DirectoryServices.ActiveDirectory;
using System.Xml.Serialization;

namespace LibManager.ViewModels.Pages
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService = App.GetService<INavigationService>();
        private ObservalProps _props = App.GetService<ObservalProps>();
        private MainWindowViewModel _mainWindowViewModel = App.GetService<MainWindowViewModel>();

        [ObservableProperty]
        private bool _isLogin = false;

        [RelayCommand]
        private void NavigateUserScan()
        {
            _navigationService.Navigate(typeof(UserScanPage));
        }
        [RelayCommand]
        private void NavigateReturnBookScan()
        {
            _navigationService.Navigate(typeof(ReturnBookScanPage));
        }
        [RelayCommand]
        private void NavigateLogout()
        {
            _props.Reset();
            FetchIsLogin();
        }
        [RelayCommand]
        private void NavigateRentalBookScan()
        {
            if (String.IsNullOrEmpty(_props.NowUser.Name))
            {
                _navigationService.Navigate(typeof(UserScanPage));
            }
            else
            {
                _navigationService.Navigate(typeof(RentalBookScanPage));
            }
        }
        [RelayCommand]
        private void NavigateAddBook()
        {
            _navigationService.Navigate(typeof(AddBookPage));
        }

        public void FetchIsLogin()
        {
            if (String.IsNullOrEmpty(_props.NowUser.Name))
            {
                IsLogin = false;
            }
            else
            {
                IsLogin = true;
            }
            _mainWindowViewModel.UpdateUserName();
        }
    }
}
