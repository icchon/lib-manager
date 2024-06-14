
using System;
using System.Buffers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Security.Policy;
using System.Text.Json;
using LibManager.Models.JsonStructures;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Drawing;
using static System.Net.WebRequestMethods;
using System.Windows.Media.Imaging;
using System.Text;
using System.Collections.Concurrent;


namespace LibManager.Models
{
    
    public class BookModels : INotifyPropertyChanged
    {

        private ObservableCollection<Book> _books = new ObservableCollection<Book>();
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
            _books = new ObservableCollection<Book>(_books.OrderBy(books => books.IntegratedInfo.Title).ToList());
            return _books;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public class IntegratedInfo
        {
            public BitmapImage? ImageSource { get; set; } = null;
            public string? Title { get; set; } = null;
            public string? ExternalUrl { get; set; } = null;
            public List<string>? Authors { get; set; } = null;
            public string? PublishedDate { get; set; }= null;
            public string? Description { get; set; } = null;

        }

        public class Book : INotifyPropertyChanged
        {
            private ObservableCollection<History> _histories;
            private HttpClient _client = new HttpClient();

            public GoogleStructure.BookInfo? GoogleBookInfo { get; set; }
            public BitmapImage? NDLImageSource { get; set; }
            public OpenBDStructure.BookInfo? OpenBDBookInfo { get; set; }

            public IntegratedInfo IntegratedInfo { get; set; } = new IntegratedInfo();

            public async Task GetBookInfoAsync()
            {
                string url = String.Concat(GoogleStructure.BaseUrl, Isbn);
                try
                {
                    HttpResponseMessage response = await _client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var document = await response.Content.ReadAsStringAsync();
                        GoogleBookInfo = JsonSerializer.Deserialize<GoogleStructure.BookInfo>(document, GoogleStructure.Options);
                        Debug.WriteLine("GoogleBooksAPI");
                        Debug.WriteLine(document);

                        try
                        {
                            var volumeInfo = GoogleBookInfo.Items.FirstOrDefault().VolumeInfo;
                            Debug.WriteLine($"{GoogleBookInfo.Items.FirstOrDefault().Kind}");
                            string? title = volumeInfo?.Title;
                            string? externalUrl = volumeInfo?.InfoLink;
                            List<string>? authors = volumeInfo?.Authors;
                            string? publishDate = volumeInfo?.PublishedDate;
                            string? description = volumeInfo?.Description;
                            BitmapImage? imageSource = null;

                            string? imageLink = volumeInfo.ImageLinks.SmallThumbnail;
                            response = await _client.GetAsync(imageLink);
                            if (response.IsSuccessStatusCode)
                            {
                                using (Stream stream = await response.Content.ReadAsStreamAsync())
                                {
                                    string savePath = Path.Combine(App.AppDataFolder, Isbn+".bmp");
                                    using (FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None))
                                    {
                                        stream.CopyTo(fileStream);
                                    }
                                    imageSource = new BitmapImage(new Uri(savePath));
                                }

                            }
                            else
                            {
                                Debug.WriteLine($"Failed to get GoogleBooksAPIImage response, status code : {response.StatusCode}");
                            }

                            IntegratedInfo.Title = title;
                            IntegratedInfo.ExternalUrl = externalUrl;
                            IntegratedInfo.Authors = authors;
                            IntegratedInfo.Description = description;
                            IntegratedInfo.PublishedDate = publishDate;
                            IntegratedInfo.ImageSource = imageSource;
                        }catch (Exception ex)
                        {
                            Debug.WriteLine($"Error: {ex.Message}");
                        }
                        

                    }
                    else
                    {
                        Debug.WriteLine($"Failed to get GoogleBookAPI response, status code : {response.StatusCode}");
                    }

                    url = String.Concat(OpenBDStructure.BaseUrl, Isbn);
                    response = await _client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var document = await response.Content.ReadAsStringAsync();
                        OpenBDBookInfo = JsonSerializer.Deserialize<List<OpenBDStructure.BookInfo>>(document, OpenBDStructure.Options).FirstOrDefault();
                        Debug.WriteLine("OpenDL");
                        Debug.WriteLine(document);

                        var summery = OpenBDBookInfo?.Summary;
                        string? title = summery?.Title;
                        string? pubDate = summery?.Pubdate;
                        List<string>? authors = new List<string>
                        {
                            summery?.Author,
                        };

                        if(IntegratedInfo.Title == null)
                        {
                            IntegratedInfo.Title = title;
                        }
                        if(IntegratedInfo.PublishedDate == null)
                        {
                            IntegratedInfo.PublishedDate= pubDate;
                        }
                        if(IntegratedInfo.Authors == null)
                        {
                            IntegratedInfo.Authors = authors;
                        }

                    }
                    else
                    {
                        Debug.WriteLine($"Failed to get OpenBDAPI response, status code : {response.StatusCode}");
                    }

                    url = $"{NDLStructure.BaseUrl}{Isbn}.jpg";
                    Debug.WriteLine(url);
                    response = await _client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        using (Stream stream = await response.Content.ReadAsStreamAsync())
                        {
                            string savePath = Path.Combine(App.AppDataFolder, Isbn + ".bmp");
                            using (FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                stream.CopyTo(fileStream);
                            }
                            NDLImageSource = new BitmapImage(new Uri(savePath));
                        }

                        if (IntegratedInfo.ImageSource == null)
                        {
                            IntegratedInfo.ImageSource = NDLImageSource;
                        }

                    }
                    else
                    {
                        Debug.WriteLine($"Failed to get NDLAPI response, status code : {response.StatusCode}");
                    }

                    

                }
                catch(Exception ex) 
                {
                    Debug.WriteLine($"API Response Error : {ex.Message}");
                }


            }

            public string Donor { get; set; }
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
            public string? Isbn { get; set; }

            public Book(string donor, string isbn)
            {
                Donor = donor;
                Isbn = isbn;
                DonoredDate = DateTime.Now;
                _histories = new ObservableCollection<History>();

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
                Histories = GetSortedHistories();
                OnPropertyChanged(nameof(Histories));
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(Available));
                OnPropertyChanged(nameof(Owner));
            }

            public void Return()
            {
                if(LatestHistory == null)
                {
                    Debug.WriteLine("借りられていないのに返そうとしています");
                    return;
                }
                /*

                if(LatestHistory.User != user)
                {
                    Debug.WriteLine($"借りた人{LatestHistory.User}と返す人{user}が一致しません");
                    return;
                }
                */
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
