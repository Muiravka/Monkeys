using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Monkeys.ViewModels;

namespace Monkeys.Views;

public partial class AuthorAddsView : Window
{
    public AuthorAddsView()
    {
        InitializeComponent();
        DataContext = new AuthorAddsViewModel();
    }
}