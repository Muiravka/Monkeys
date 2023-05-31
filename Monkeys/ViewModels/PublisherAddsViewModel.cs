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
    public class PublisherAddsViewModel : ViewModelBase
    {
        public string? PublisherInputName { get; set; }

        public RelayCommand<Window> PublisherAddCommand
        {
            get => new RelayCommand<Window>(x =>
            {
                MonkeysContext monkeysContext = new MonkeysContext();
                if (PublisherInputName != null)
                {
                    Publisher newPublisher = new Publisher();
                    newPublisher.PublisherName = PublisherInputName;
                    monkeysContext.Add(newPublisher);
                    monkeysContext.SaveChanges();
                    var home = new MainWindow();
                    home.Show();
                    x.Close();
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
