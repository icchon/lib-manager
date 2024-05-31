using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibManager.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static LibManager.Models.BookModels;

namespace LibManager.ViewModels.Pages
{
    public partial class BooksViewModel : ObservableObject
    {
        private readonly BookModels _bookModels;

        public BooksViewModel()
        {
            _bookModels = App.GetService<BookModels>();
            _books = _bookModels.Books;
        }

        [ObservableProperty]
        private ObservableCollection<Book> _books;

        [RelayCommand]
        private void Rent(Book book)
        {
            string user = $"unknown : {Guid.NewGuid()}";
            book.Rent(user);
        }
        [RelayCommand]
        private void Return(Book book)
        {
            var latestHistory = book.LatestHistory;
            if(latestHistory == null)
            {
                Debug.WriteLine("借りられた履歴がないのに返そうとしています");
                return;
            }
            book.Return(latestHistory.User);
        }
    }
}
