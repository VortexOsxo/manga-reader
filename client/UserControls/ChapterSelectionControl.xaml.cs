using MangaReader.Model;
using MangaReader.Pages;
using MangaReader.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace MangaReader.UserControls
{
    public partial class ChapterSelectionControl : UserControl
    {
        private readonly Manga manga;

        public ChapterSelectionControl(Manga manga)
        {
            InitializeComponent();
            this.manga = manga;

            CreateChapters();
        }

        private void CreateChapters()
        {
            for (int i = 1; i < manga.AvailableChapters; i++)
            {
                ChaptersPanel.Children.Add(CreateChapter(i));
            }
        }

        private TextBlock CreateChapter(int index)
        {
            TextBlock chapterTextBlock = new()
            {
                Text = $"Chapter {index}",
                Margin = new Thickness(5),
                Tag = index - 1,
            };

            chapterTextBlock.MouseLeftButtonDown += ChapterTextBlock_MouseLeftButtonDown;
            return chapterTextBlock;
        }

        private void ContinueReadingTextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            HistoryService.GetLastReadPage(manga.Id).ContinueWith((antecedant) =>
            {
                var bookmark = antecedant.Result!;
                Dispatcher.Invoke(() => OpenMangaFromBookmark(bookmark));
            });
        }

        private void OpenMangaFromBookmark(Bookmark bookmark)
        {
            GoToChapter(bookmark.ChapterNumber);

            var reader = ReaderService.Instance;
            reader.pagesService.GoToPage(bookmark.PageNumber);
        }

        private void ChapterTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBlock clickedTextBlock) return;

            int chapterNumber = (int)clickedTextBlock.Tag;
            GoToChapter(chapterNumber);
        }

        private void FirstChapterTextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GoToChapter(0);
        }

        private void LastChapterTextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GoToChapter(manga.AvailableChapters - 1);
        }

        private void GoToChapter(int chapterId)
        {
            var reader = ReaderService.Instance;

            reader.SetManga(this.manga);
            reader.pagesService.GoToChapter(chapterId);

            NavigationService.GetNavigationService(this).Navigate(new ReadPage());
        }
    }
}
