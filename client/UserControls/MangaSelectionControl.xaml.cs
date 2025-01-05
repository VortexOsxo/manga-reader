using MangaReader.Model;
using System.Windows.Controls;

namespace MangaReader.UserControls
{
    public partial class MangaSelectionControl : UserControl
    {
        public MangaSelectionControl(Task<List<Manga>> mangas)
        {
            InitializeComponent();
            DataContext = this;

            mangas.ContinueWith((precedent) => {
                Dispatcher.Invoke(() => LoadPreviews(precedent.Result));
            });
        }

        private void LoadPreviews(List<Manga> previews)
        {
            foreach (var preview in previews)
            {
                var mangaPreviewControl = new MangaPreviewControl(preview);
                PreviewsPanel.Children.Add(mangaPreviewControl);
            }
        }
    }
}
