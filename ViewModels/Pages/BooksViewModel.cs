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
        private const string firstTextBoxContent = "ここにユーザー名を入力してください";
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
        [ObservableProperty]
        private bool _popUpIsOpen = false;
        [ObservableProperty]
        private string _userName = firstTextBoxContent;

        [RelayCommand]
        private void Rent(Book book)
        {
            if (string.IsNullOrEmpty(_props.NowUser.Name))
            {
                PopUpIsOpen = string.IsNullOrEmpty(_props.NowUser.Name);
                return;
            }
            book.Rent(_props.NowUser.Name);
            
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

       
        [RelayCommand]
        private void ConfirmUserName()
        {
            if (!string.IsNullOrEmpty(_props.NowUser.Name))
            {
                Debug.WriteLine("すでにユーザー名が入力されています");
                return;
            }

            if (string.IsNullOrEmpty(UserName))
            {
                Debug.WriteLine("ユーザ名が空です");
                UserName = "ユーザー名が空です";
            }

            if (UserName == firstTextBoxContent)
            {
                Debug.WriteLine("ユーザー名が入力されていません");
                UserName = "ユーザー名が入力されていません";
            }
            User user = new User(UserName);
            _props.NowUser = user;
            PopUpIsOpen = false;

        }
    }
}
