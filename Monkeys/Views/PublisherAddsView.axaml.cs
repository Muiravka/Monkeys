using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Monkeys.ViewModels;

namespace Monkeys.Views;

public partial class PublisherAddsView : Window
{
    public PublisherAddsView()
    {
        InitializeComponent();
        DataContext = new PublisherAddsViewModel();
    }
}