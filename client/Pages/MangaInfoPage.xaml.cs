using MangaReader.Model;
using MangaReader.UserControls;
using System.Windows.Controls;

namespace MangaReader.Pages
{
    public partial class MangaInfoPage : Page
    {
        private Manga manga;

        public MangaInfoPage(Manga manga)
        {
            InitializeComponent();
            DataContext = this;
            this.manga = manga;

            CreateControls();
        }

        private void CreateControls()
        {
            var preview = new FavoriteMangaPreviewControl(manga);
            PreviewGrid.Children.Add(preview);

            var chapters = new ChapterSelectionControl(manga);
            ChaptersGrid.Children.Add(chapters);
        }
    }
}
