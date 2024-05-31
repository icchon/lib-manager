using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibManager.Models.BookModels;

namespace LibManager.Models
{
    public class ObservalProps : ObservableObject
    {
        private Book? _toRentBook;
        public Book? ToRentBook
        {
            get { return _toRentBook; }
            set
            {
                if(_toRentBook != value)
                {
                    _toRentBook = value;
                    OnPropertyChanged(nameof(ToRentBook));
                }
            }
        }

        private Book? _toReturnBook;
        public Book? ToReturnBook
        {
            get { return _toRentBook; }
            set
            {
                if(_toReturnBook != value)
                {
                    _toReturnBook = value;
                    OnPropertyChanged(nameof(ToReturnBook));
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


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
