using System.Windows.Controls;
using MangaReader.Services;
using MangaReader.UserControls;

namespace MangaReader.Pages
{
    public partial class SelectionPage : Page
    {
        public SelectionPage()
        {
            InitializeComponent();

            MangaSelectionGrid.Children.Add(new MangaSelectionControl(MangasService.GetPreviews()));
        }
    }
}
