using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;
using static LibManager.Models.BookModels;
using LibManager.Views.Pages;

namespace LibManager.ViewModels.Pages
{
    public partial class AddBookSuccessViewModel : ObservableObject
    {
        private ObservalProps _props = App.GetService<ObservalProps>();
        private INavigationService _navigationService = App.GetService<INavigationService>();

        [ObservableProperty]
        private Book? _book;

        [RelayCommand]
        private void NavigateToAddBook()
        {
            _navigationService.Navigate(typeof(AddBookPage));
        }

        public AddBookSuccessViewModel()
        {
            _book = _props.ToAddBook;
        }

        public void FetchToAddBook()
        {
            Book = _props.ToAddBook;
        }

    }
}
