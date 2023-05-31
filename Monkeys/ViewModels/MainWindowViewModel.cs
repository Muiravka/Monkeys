using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Monkeys.Models;
using Monkeys.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace Monkeys.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private MonkeysContext dataContext = new MonkeysContext();
        public MainWindowViewModel() 
        {
             _genresList = new ObservableCollection<Genre>(dataContext.Genres);
             _booksList = new ObservableCollection<Book>(dataContext.Books.OrderByDescending(x=> x.PopularityRating));
        }

        #region FieldsAndLists
        private ObservableCollection<Book> _booksList;
        public ObservableCollection<Book> BooksList
        {
            get => _booksList;
            set => _booksList = value;
        }
        private ObservableCollection<Genre> _genresList;
        public ObservableCollection<Genre> GenresList
        {
            get => _genresList;
            set => _genresList = value;
        }
        public string SearchString { get; set; }
        public Genre SelectedGenre { get; set; }
        #endregion
        #region Commands
        public RelayCommand<Window> BookAddCommand
        {
            get => new(x =>
            {
                var BookAddWindow = new BookAddsView();
                BookAddWindow.Show();
                x.Close();
            });
        }

        public RelayCommand<Window> PublisherAddCommand
        {
            get => new(x =>
            {
                var PublisherWindow = new PublisherAddsView();
                PublisherWindow.Show();
                x.Close();
            });
        }

        public RelayCommand<Window> AuthorAddCommand
        {
            get => new(x =>
            {
                var AuthorWindow = new AuthorAddsView();
                AuthorWindow.Show();
                x.Close();
            });
        }

        public RelayCommand SearchCommand
        {
            get => new(() =>
            {
                if (SearchString != null)
                {
                    _booksList = new ObservableCollection<Book>(dataContext.Books.Where(x => x.BookName == SearchString).OrderByDescending(x => x.PopularityRating));
                    OnPropertyChanged(nameof(BooksList));
                }
                else if(SearchString == "" || SearchString != null)
                {
                    _booksList = new ObservableCollection<Book>(dataContext.Books.OrderByDescending(x => x.PopularityRating));
                    OnPropertyChanged(nameof(BooksList));
                }
            });
        }

        public RelayCommand BookListReset
        {
            get => new(() =>
            {
                ObservableCollection<Book> books = new ObservableCollection<Book>(dataContext.Books.OrderByDescending(x => x.PopularityRating));
                _booksList = books;
                OnPropertyChanged(nameof(BooksList));
            });
        }
        public RelayCommand BookListUpdateByGenre
        {
            get => new(() =>
            {
                if(_genresList.FirstOrDefault(x => x.IsChecked == true) != null)
                {
                    ObservableCollection<Book> books = new ObservableCollection<Book>(dataContext.Books);
                    ObservableCollection<BookGenre> bookGenres = new ObservableCollection<BookGenre>(dataContext.BookGenres);
                    ObservableCollection<Book> newBookColletion = new ObservableCollection<Book>();
                    foreach (var genre in _genresList)
                    {
                        foreach (var book in books)
                        {
                            foreach (var bookGenre in bookGenres)
                            {
                                if (genre.IsChecked == true && bookGenre.GenreId == genre.Id && bookGenre.BookId == book.Id && newBookColletion.FirstOrDefault(x => x.Id == book.Id) == null)
                                {
                                    newBookColletion.Add(book);
                                }
                            }
                        }
                    }
                    _booksList = newBookColletion;
                    _booksList.OrderByDescending(x => x.PopularityRating);
                    OnPropertyChanged(nameof(BooksList));
                }
            });
        }
        #endregion
    }
}