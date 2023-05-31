using Avalonia.Controls;
using Monkeys.ViewModels;
using System.Diagnostics;
using System.Threading;

namespace Monkeys.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}