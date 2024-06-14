using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibManager.Models;
using LibManager.Views.Pages;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Wpf.Ui;
using static LibManager.Models.BookModels;

namespace LibManager.ViewModels.Pages
{
    public partial class BooksViewModel : ObservableObject
    {
        private readonly BookModels _bookModels;
        private ObservalProps _props; 
        private readonly INavigationService _navigationService = App.GetService<INavigationService>();

        public BooksViewModel()
        {
            _bookModels = App.GetService<BookModels>();
            _props = App.GetService<ObservalProps>();
            _books = _bookModels.Books;
        }

        [ObservableProperty]
        private ObservableCollection<Book> _books;

    }
}
