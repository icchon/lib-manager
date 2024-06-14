using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;
using LibManager.Views.Pages;
using LibManager.Models;
using static LibManager.Models.BookModels;
using System.Collections.ObjectModel;

namespace LibManager.ViewModels.Pages
{
    public partial class RentalSuccessViewModel : ObservableObject
    {
        private INavigationService _navigationService = App.GetService<INavigationService>();
        private ObservalProps _props = App.GetService<ObservalProps>();

        [ObservableProperty]
        private ObservableCollection<Book>? _books;


        public RentalSuccessViewModel()
        {
            _books = _props.ToRentBooks;
        }


        [RelayCommand]
        private void NavigateToHome()
        {
            _props.Reset();
            _navigationService.Navigate(typeof(HomePage));
        }

        public void FetchToRentBooks()
        {
            Books = _props.ToRentBooks;
        }
    }
}
