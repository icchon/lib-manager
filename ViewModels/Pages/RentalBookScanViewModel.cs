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
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Xaml;
using System.Reflection.Metadata;
using System.Windows;

namespace LibManager.ViewModels.Pages
{
    public partial class RentalBookScanViewModel : ObservableObject
    {
        private ObservalProps _props = App.GetService<ObservalProps>();
        private BookModels _bookModels = App.GetService<BookModels>();
        private INavigationService _navigationService = App.GetService<INavigationService>();

        [ObservableProperty]
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();

        [RelayCommand]
        private void Rent()
        {
            string userName = App.GetService<ObservalProps>().NowUser.Name;
            foreach(Book book in Books)
            {
                book.Rent(userName);
            }
            _props.ToRentBooks = Books;
            _navigationService.Navigate(typeof(RentalSuccessPage));
        }
       
        public void AddRentalBook()
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


