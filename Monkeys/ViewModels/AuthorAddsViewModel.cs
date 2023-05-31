using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Monkeys.Models;
using Monkeys.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monkeys.ViewModels
{
    public class AuthorAddsViewModel : ViewModelBase
    {
        public string? AuthorInputMiddleName { get; set; }
        public string? AuthorInputLastName { get; set; }
        public string? AuthorInputName { get; set; }

        public string DateInput { get; set; }

        public RelayCommand<Window> AuthorAddCommand
        {
            get => new RelayCommand<Window>(x =>
            {
                MonkeysContext monkeysContext = new MonkeysContext();
                if (AuthorInputName != null && AuthorInputLastName != null && AuthorInputMiddleName != null && DateInput != null)
                {
                    var existingAuthor = monkeysContext.Authors.FirstOrDefault(x => x.FirstName == AuthorInputName && x.MiddleName == AuthorInputMiddleName && x.LastName == AuthorInputLastName);
                    if (existingAuthor == null)
                    {
                        DateOnly birthday = new DateOnly();
                        if (DateOnly.TryParse(DateInput, out birthday))
                        {
                            Author newAuthor = new Author();
                            newAuthor.Birthday = birthday;
                            newAuthor.FirstName = AuthorInputName;
                            newAuthor.LastName = AuthorInputLastName;
                            newAuthor.MiddleName = AuthorInputMiddleName;
                            monkeysContext.Add(newAuthor);
                            monkeysContext.SaveChanges();
                            var home = new MainWindow();
                            home.Show();
                            x.Close();
                        }
                        else
                        {
                            var messageBoxError = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Неверный формат даты.");
                            messageBoxError.Show();
                        }
                    }
                    else
                    {
                        var messageBoxError = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Такой автор существует.");
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

        public RelayCommand<Window> Canceling
        {
            get => new(x =>
            {
                var home = new MainWindow();
                home.Show();
                x.Close();
            });
        }

    }
}
