using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui;
using LibManager.Views.Pages;
using Wpf.Ui.Controls;

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
