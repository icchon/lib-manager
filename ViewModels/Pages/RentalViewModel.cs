using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;
using LibManager.Views.Pages;
using System.Diagnostics;

namespace LibManager.ViewModels.Pages
{
    public partial class RentalViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService = App.GetService<INavigationService>();  
        [RelayCommand]
        private void NavigateWaitingRental()
        {
            _navigationService.Navigate(typeof(WaitingRentalPage));
        }
        [RelayCommand]
        private void NavigateWaitingReturn()
        {
            _navigationService.Navigate(typeof(WaitingReturnPage));
        }
    }
}
