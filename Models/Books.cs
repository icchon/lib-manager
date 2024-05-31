using LibManager.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibManager.Models
{
    public class BookModels
    {
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();
        public ObservableCollection<Book> Books
        {
            get { return GetSortedBooks(); }
            set
            {
                if(_books != value)
                {
                    _books = value;
                    OnPropertyChanged(nameof(Books));
                }
            }
        }
        public class Book
        {
            public string Title = "";
            public string Owner = "";
            public string Donor = "";
            public string Id = "";
            public string ExternalUrl = "";
            public DateTime DonoredDate;
            public bool IsReturned
            {
                get { return GetLatestHistory().IsReturned; }
            }

            private ObservableCollection<History> _histories = new ObservableCollection<History>(); 
            public ObservableCollection<History> Histories
            {
                get { return GetSortedHistories(); }
                set
                {
                    if(_histories != value)
                    {
                        _histories = value;
                        OnPropertyChanged(nameof(Histories));
                    }
                }
            }

            public Book(string title, string owner, string donor, string id, string externalUrl)
            {
                Histories = new ObservableCollection<History>();
                DonoredDate = DateTime.Now;

                Title = title;
                Owner = owner;
                Donor = donor;
                Id = id;
                ExternalUrl = externalUrl;
            }
            public class History
            {
                public DateTime RentalDate;
                public DateTime? ReturnDate = null;
                public string User = "";
                public bool IsReturned
                {
                    get { return ReturnDate != null; }
                }

                public History(string user)
                {
                    User = user;
                    RentalDate = DateTime.Now;
                }
            }

            public ObservableCollection<History> GetSortedHistories()
            {
                _histories =  new ObservableCollection<History>(_histories.OrderByDescending(history => history.RentalDate).ToList());
                return _histories;
            }

            public History GetLatestHistory()
            {
                return GetSortedHistories().First();
            }

            public void ReturnBook()
            {
                GetLatestHistory().ReturnDate = DateTime.Now;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<Book> GetSortedBooks()
        {
            _books = new ObservableCollection<Book>(_books.OrderBy(books => books.Title).ToList());
            return _books;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
