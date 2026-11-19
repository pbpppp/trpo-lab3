using System.Windows;
using TrpoLab4.WpfApp.ViewModels;

namespace TrpoLab4.WpfApp.Views;

public partial class MainWindow : Window
{
    public MainWindow(ConeViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
