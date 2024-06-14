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
    public partial class ReturnBookScanViewModel : ObservableObject
    {
        private ObservalProps _props = App.GetService<ObservalProps>();
        private BookModels _bookModels = App.GetService<BookModels>();
        private INavigationService _navigationService = App.GetService<INavigationService>();

        [ObservableProperty]
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();

        [RelayCommand]
        private void Return()
        {
            foreach(Book book in Books)
            {
                book.Return();
            }
            _props.ToReturnBooks = Books;
            _navigationService.Navigate(typeof(ReturnSuccessPage));
        }
       
        public void AddReturnBook()
        {
            string isbn = _props.Isbn;

            Book book = _bookModels.Books.FirstOrDefault(b => b.Isbn == isbn);
            if(book != null && !Books.Any(b => b.Isbn == book.Isbn))
            {
                Books.Add(book);
            }
        }
        
    }
}
