using System;
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
            new Book("title:1", "owner:1", "donor:1", "id:1", "externalUrl:1"),
            new Book("title:2", "owner:2", "donor:2", "id:2", "externalUrl:2"),
            new Book("title:3", "owner:3", "donor:3", "id:3", "externalUrl:3")
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
            private string _owner;
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

            public string Owner
            {
                get => _owner;
                set
                {
                    if (_owner != value)
                    {
                        _owner = value;
                        OnPropertyChanged(nameof(Owner));
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

            public Book(string title, string owner, string donor, string id, string externalUrl)
            {
                _title = title;
                _owner = owner;
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

                Histories.Add(new History(user));
                OnPropertyChanged(nameof(Histories));
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(Available));
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
                OnPropertyChanged(nameof(Histories));
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(Available));
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
