using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static LibManager.Models.BookModels;

namespace LibManager.Models
{
    public class ObservalProps : ObservableObject
    {
        private ObservableCollection<Book>? _toRentBooks;
        public ObservableCollection<Book>? ToRentBooks
        {
            get { return _toRentBooks; }
            set
            {
                if(_toRentBooks != value)
                {
                    _toRentBooks = value;
                    OnPropertyChanged(nameof(ToRentBooks));
                }
                OnPropertyChanged(nameof(ToRentBooks));
            }
        }

        private ObservableCollection<Book>? _toReturnBooks;
        public ObservableCollection<Book>? ToReturnBooks
        {
            get { return _toRentBooks; }
            set
            {
                if(_toReturnBooks != value)
                {
                    _toReturnBooks = value;
                    OnPropertyChanged(nameof(ToReturnBooks));
                }
                OnPropertyChanged(nameof(ToReturnBooks));
            }
        }
        private Book? _toAddBook;
        public Book? ToAddBook
        {
            get { return _toAddBook; }
            set
            {
                if( _toAddBook != value)
                {
                    _toAddBook = value;
                    OnPropertyChanged(nameof(ToAddBook));
                }
            }
        }
        private string? _isbn;
        public string? Isbn
        {
            get { return _isbn; }
            set
            {
                if (_isbn != value)
                {
                    _isbn = value;
                    OnPropertyChanged(nameof(Isbn));
                }
            }
        }
        private User _nowUser = new User("");
        public User NowUser
        {
            get { return _nowUser; }
            set
            {
                if( _nowUser != value)
                {
                    _nowUser = value;
                    OnPropertyChanged(nameof(NowUser));
                }
            }
        }

        public void Reset()
        {
            _toRentBooks = null;
            _toRentBooks = null;
            _toAddBook = null;
            _isbn = null;
            _nowUser = new User("");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
