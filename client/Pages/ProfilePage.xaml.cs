using MangaReader.Services;
using MangaReader.UserControls;
using System.Windows.Controls;

namespace MangaReader.Pages
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();

            HistorySelectionGrid.Children.Add(new MangaSelectionControl(HistoryService.GetHistory()));
            FavoriteSelectionGrid.Children.Add(new MangaSelectionControl(FavoritesService.GetFavorites()));
        }
    }
}
