using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibManager.Models;
using LibManager.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibManager.Models.BookModels;
using Wpf.Ui;

namespace LibManager.ViewModels.Pages
{
    public partial class WaitingReturnViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService = App.GetService<INavigationService>();

        [RelayCommand]
        private void ToBooksPage()
        {
            _navigationService.Navigate(typeof(BooksPage));
        }
    }
}
