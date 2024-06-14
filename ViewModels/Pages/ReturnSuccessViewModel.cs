using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui;
using LibManager.Views.Pages;
using LibManager.Models;
using static LibManager.Models.BookModels;
using System.Collections.ObjectModel;

namespace LibManager.ViewModels.Pages
{
    public partial class ReturnSuccessViewModel : ObservableObject
    {
        private INavigationService _navigationService = App.GetService<INavigationService>();
        private ObservalProps _props = App.GetService<ObservalProps>();

        [ObservableProperty]
        private ObservableCollection<Book>? _books;


        public ReturnSuccessViewModel()
        {
            _books = _props.ToReturnBooks;
        }


        [RelayCommand]
        private void NavigateToHome()
        {
            _props.Reset();
            _navigationService.Navigate(typeof(HomePage));
        }

        public void FetchToReturnBook()
        {
            Books = _props.ToReturnBooks;
        }
    }
}
