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
    public partial class BookScanViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService = App.GetService<INavigationService>();

        private readonly BookModels _bookModels;

        public BookScanViewModel()
        {
           
        }

        [ObservableProperty]
        private ObservableCollection<Book> _books;
        [ObservableProperty]
        private string _userName = App.GetService<ObservalProps>().NowUser.Name;

        [RelayCommand]
        private async Task Rent(Book book)
        {
            await book.Rent(UserName);
        }
       
        [RelayCommand]
        private async Task ToBooksPage()
        {
            _navigationService.Navigate(typeof(BooksPage));
        }
    }
}


