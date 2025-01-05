using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using MangaReader.Pages;

namespace MangaReader.UserControls
{
    public partial class NavBarControl : UserControl
    {
        public NavBarControl()
        {
            InitializeComponent();
        }

        private void BookClicked(object sender, MouseButtonEventArgs e)
        {
            var navigationService = NavigationService.GetNavigationService(this);
            navigationService?.Navigate(new SelectionPage());
        }

        private void UserClicked(object sender, MouseButtonEventArgs e)
        {
            var navigationService = NavigationService.GetNavigationService(this);
            navigationService?.Navigate(new ProfilePage());
        }
    }
}
