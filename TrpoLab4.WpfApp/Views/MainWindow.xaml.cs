using System.Windows;
using TrpoLab3.Lib;
using TrpoLab4.WpfApp.ViewModels;

namespace TrpoLab4.WpfApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new ConeViewModel(new ConeCalculator());
    }
}
