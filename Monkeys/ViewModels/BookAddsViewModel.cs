using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Monkeys.Models;
using Monkeys.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monkeys.ViewModels
{
    public class BookAddsViewModel : ViewModelBase
    {
        private MonkeysContext dbMonkey = new MonkeysContext();
        public BookAddsViewModel() 
        {
            _authorsList = new ObservableCollection<Author>(dbMonkey.Authors);
            _genresList = new ObservableCollection<Genre>(dbMonkey.Genres);
            _publishers = new ObservableCollection<Publisher> (dbMonkey.Publishers);
        }

        #region FieldsAndLists

        public Dictionary<int, string> PopularityRatingList = new Dictionary<int, string>()
        {
            [1] = "1/10",
            [2] = "2/10",
            [3] = "3/10",
            [4] = "4/10",
            [5] = "5/10",
            [6] = "6/10",
            [7] = "7/10",
            [8] = "8/10",
            [9] = "9/10",
            [10] = "10/10"
        };
        //public KeyValuePair<int, string> SelectedPopularityRating
        //{
        //    get => SelectedPopularityRating;
        //    set => SelectedPopularityRating = value;
        //}
        public Genre TheGenre { get; set; }
        public string SelectedDate { get; set; }
        public string BookName { get; set; }
        public string RatingInput { get; set; }
        public Author SelectedAuthor { get; set; }
        public ObservableCollection<Genre> SelectedGenres { get; set; }

        private ObservableCollection<Genre> _genresList;
        public ObservableCollection<Genre> GenresList
        {
            get => _genresList;
            set => _genresList = value;
        }

        public Publisher SelectedPublisher { get; set; }
        private ObservableCollection<Publisher> _publishers;
        public ObservableCollection<Publisher> Publishers
        {
            get => _publishers;
            set => _publishers = value;
        }

        private ObservableCollection<Author> _authorsList = new ObservableCollection<Author>();
        public ObservableCollection<Author> AuthorsList
        {
            get => _authorsList;
            set => _authorsList = value;
        }
        #endregion

        public RelayCommand<Window> Canceling
        {
            get => new(x =>
            {
                var home = new MainWindow();
                home.Show();
                x.Close();
            });
        }

        public RelayCommand<Window> BookAdding
        {
            get => new RelayCommand<Window>(x =>
            {
                MonkeysContext monkeysContext = new MonkeysContext();
                if (SelectedAuthor != null && SelectedPublisher != null && SelectedDate != null && BookName != null && RatingInput != null)
                {
                    Book newBook = new Book();
                    DateOnly bookDate;
                    if (DateOnly.TryParse(SelectedDate, out bookDate))
                    {
                        int rating;
                        if (int.TryParse(RatingInput, out rating))
                        {
                            if (rating < 0 || rating > 10)
                            {
                                var messageBoxError = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Неверный формат рейтинга.");
                                messageBoxError.Show();
                            }
                            else
                            {
                                newBook.BookName = BookName;
                                newBook.AuthorId = SelectedAuthor.Id;
                                newBook.PublicationYear = bookDate;
                                newBook.PopularityRating = rating;
                                newBook.PublisherId = SelectedPublisher.Id;
                                monkeysContext.Add(newBook);
                                monkeysContext.SaveChanges();
                                foreach (var genre in _genresList)
                                {
                                    if (genre.IsChecked == true)
                                    {
                                        BookGenre newBookGenre = new BookGenre();
                                        newBookGenre.GenreId = genre.Id;
                                        newBookGenre.BookId = newBook.Id;
                                        monkeysContext.Add(newBookGenre);
                                        monkeysContext.SaveChanges();
                                        genre.IsChecked = false;
                                    }
                                }
                                var home = new MainWindow();
                                home.Show();
                                x.Close();
                            }
                        }
                        else
                        {
                            var messageBoxError = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Неверный формат рейтинга.");
                            messageBoxError.Show();
                        }
                    }
                    else
                    {
                        var messageBoxError = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Неверный формат даты.");
                        messageBoxError.Show();
                    }
                }
                else
                {
                    var messageBoxError = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Недостаток данных.");
                    messageBoxError.Show();
                }
            });
        }
    }
}
