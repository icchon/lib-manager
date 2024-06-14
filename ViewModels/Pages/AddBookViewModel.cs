using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibManager.Models;
using LibManager.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;
using static LibManager.Models.BookModels;


namespace LibManager.ViewModels.Pages
{
    public partial class AddBookViewModel : ObservableObject
    {
        private readonly BookModels _bookModels = App.GetService<BookModels>();
        private ObservableCollection<Book> _books = App.GetService<ObservableCollection<Book>>();
        private ObservalProps _props = App.GetService<ObservalProps>();
        private INavigationService _navigationService = App.GetService<INavigationService>();


        [ObservableProperty]
        private Book? _book = null;
        [ObservableProperty]
        private bool _visible = false;
        [ObservableProperty]
        private bool _progressVisible = false;
        public AddBookViewModel()
        {
            
            _books = _bookModels.Books;
        }



        [RelayCommand]
        private void AddBook()
        {   
            if (Book != null && !_books.Any(b => b.Isbn == Book.Isbn))
            {
                _books.Add(Book);

                _props.ToAddBook = Book;
                _navigationService.Navigate(typeof(AddBookSuccessPage));
            }
        }

        public async Task DisplayBookAsync()
        {
            if(_books.Any(b => b.Isbn == _props.Isbn))
            {
                return;
            }

            ProgressVisible = true;

            string? isbn = _props.Isbn;
            string? userName = _props.NowUser.Name;

            Book book = new Book(userName, isbn);
            await book.GetBookInfoAsync();
            Book = book;

            ProgressVisible = false;

            Visible = true;
        }
    }
}
