using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Monkeys.ViewModels;
using System.Diagnostics;
using System.Threading;

namespace Monkeys.Views;

public partial class BookAddsView : Window
{
    public BookAddsView()
    {
        InitializeComponent();
        DataContext = new BookAddsViewModel();
    }
}