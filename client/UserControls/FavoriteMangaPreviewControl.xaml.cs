using System.ComponentModel;
using System.Net;
using System.Windows.Controls;
using System.Windows.Input;
using MangaReader.Model;
using MangaReader.Services;

namespace MangaReader.UserControls
{
    public partial class FavoriteMangaPreviewControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string ImagePath { get { return GetImagePath(); } }
        private readonly Manga manga;

        private bool isFavorite = false;

        public FavoriteMangaPreviewControl(Manga manga)
        {
            DataContext = this;
            
            this.manga = manga;
            InitializeComponent();

            var mangaPreview = new MangaPreviewControl(manga);
            PreviewGrid.Children.Add(mangaPreview);

            UpdateIsFavorite();
        }

        private string GetImagePath()
        {
            string path = isFavorite ? "heart" : "love";
            return $"pack://application:,,,/Assets/{path}.png";
        }

        private void FavoriteButtonToggled(object sender, MouseButtonEventArgs e)
        {
            Func<string, Task<HttpStatusCode>> function = isFavorite ? FavoritesService.RemoveFavorite : FavoritesService.AddFavorite;
            function(manga.Id).ContinueWith((antecedant) => UpdateIsFavorite());
        }

        private void UpdateIsFavorite()
        {
            FavoritesService.IsFavorite(manga.Id).ContinueWith((antecedant) => {
                isFavorite = antecedant.Result;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImagePath)));
            });
        }
    }
}
