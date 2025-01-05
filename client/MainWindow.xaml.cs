using System.Windows;

namespace MangaReader
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Pages.AuthenticationPage());
        }
    }
}