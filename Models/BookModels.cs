using System;
using System.Buffers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security;

namespace LibManager.Models
{
    public class BookModels : INotifyPropertyChanged
    {
        private ObservableCollection<Book> _books = new ObservableCollection<Book>
        {
            new Book("OnePiece","donor-1", "00000001", "https://onepiece/not-exist.com"),
            new Book("DragonBall","donor-2", "00000002", "https://dragonball/not-exist.com"),
            new Book("Naruto", "donor-3", "00000003", "https://naruto/not-exist.com")
        };

        public ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                if (_books != value)
                {
                    _books = value;
                    OnPropertyChanged(nameof(Books));
                }
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

        public class Book : INotifyPropertyChanged
        {
            private string _title;
            private string _externalUrl;
            private ObservableCollection<History> _histories;

            public string Title
            {
                get => _title;
                set
                {
                    if (_title != value)
                    {
                        _title = value;
                        OnPropertyChanged(nameof(Title));
                    }
                }
            }

            public string Donor { get; set; }

            public string Id { get; set; }

            public string ExternalUrl
            {
                get => _externalUrl;
                set
                {
                    if (_externalUrl != value)
                    {
                        _externalUrl = value;
                        OnPropertyChanged(nameof(ExternalUrl));
                    }
                }
            }

            public DateTime DonoredDate { get; set; }

            public string Status
            {
                get { return Available ? "貸出可" : "貸出中"; }
            }
            
            public bool Available
            {
                get
                {
                    if(LatestHistory == null)
                    {
                        return true;
                    }
                    return LatestHistory.IsReturned;
                }
            }
            

            public History? LatestHistory
            {
                get { return GetLatestHistory(); }
            }
            public ObservableCollection<History> Histories
            {
                get => _histories;
                set
                {
                    if (_histories != value)
                    {
                        _histories = value;
                        OnPropertyChanged(nameof(Histories));
                    }
                }
            }
            public string? Owner
            {
                get
                {
                    if (Available || LatestHistory == null)
                    {
                        return null;
                    }
                    return LatestHistory.User;
                }
                set {; }
            }

            public Book(string title, string donor, string id, string externalUrl)
            {
                _title = title;
                Donor = donor;
                Id = id;
                _externalUrl = externalUrl;
                DonoredDate = DateTime.Now;
                _histories = new ObservableCollection<History>();

                Rent("takeshi");
                Return("takeshi");
            }

            public void Rent(string user)
            {
                if (!Available && LatestHistory != null)
                {
                    Debug.WriteLine($"{LatestHistory.User}に借りられています");
                    return;
                }
                Owner = user;
                Histories.Add(new History(user));
                OnPropertyChanged(nameof(Histories));
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(Available));
                OnPropertyChanged(nameof(Owner));
            }

            public void Return(string user)
            {
                if(LatestHistory == null)
                {
                    Debug.WriteLine("借りられていないのに返そうとしています");
                    return;
                }

                if(LatestHistory.User != user)
                {
                    Debug.WriteLine($"借りた人{LatestHistory.User}と返す人{user}が一致しません");
                    return;
                }
                LatestHistory.ReturnDate = DateTime.Now;
                Owner = null;
                OnPropertyChanged(nameof(Histories));
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(Available));
                OnPropertyChanged(nameof(Owner));
            }

            public ObservableCollection<History> GetSortedHistories()
            {
                _histories = new ObservableCollection<History>(_histories.OrderByDescending(history => history.RentalDate).ToList());
                return _histories;
            }

            public History? GetLatestHistory()
            {
                return GetSortedHistories().FirstOrDefault();
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public class History : INotifyPropertyChanged
            {
                private DateTime? _returnDate;

                public DateTime RentalDate { get; set; }

                public DateTime? ReturnDate
                {
                    get => _returnDate;
                    set
                    {
                        if (_returnDate != value)
                        {
                            _returnDate = value;
                            OnPropertyChanged(nameof(ReturnDate));
                            OnPropertyChanged(nameof(IsReturned));
                        }
                    }
                }

                public string User { get; set; }

                public bool IsReturned
                {
                    get { return ReturnDate != null; }
                }
                
                public History(string user)
                {
                    User = user;
                    RentalDate = DateTime.Now;
                }

                public event PropertyChangedEventHandler? PropertyChanged;
                protected virtual void OnPropertyChanged(string propertyName)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}
