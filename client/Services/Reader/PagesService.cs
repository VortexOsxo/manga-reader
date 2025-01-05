using MangaReader.Model;
using System.ComponentModel;

namespace MangaReader.Services.ReaderServices
{
    public class PagesService : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string CurrentPage
        {
            get { return mangaPreview != null ? MangasService.GetPageUrl(mangaPreview.Id, ChapterIndex, PageIndex) : ""; }
        }

        public string NextPage
        {
            get {
                if (mangaPreview == null) return "";
                int pageIndex = PageIndex + 1;
                int chapterIndex = ChapterIndex;

                if (pageIndex >= GetPagesCount())
                {
                    pageIndex = 0;
                    chapterIndex += 1;
                }
                return MangasService.GetPageUrl(mangaPreview.Id, chapterIndex, pageIndex);
            }
        }

        public int ChapterIndex
        {
            get; private set;
        }

        public int PageIndex
        {
            get; private set;
        }

        public event EventHandler? PagesChanged;
        public event EventHandler? ChapterChanged;

        private List<Chapter>? chapters;

        private Manga? mangaPreview;

        public void SetManga(Manga manga)
        {
            mangaPreview = manga;

            MangasService.GetChapters(mangaPreview.Id).ContinueWith((precedent) =>
            {
                chapters = precedent.Result;
                ChapterChanged?.Invoke(this, EventArgs.Empty);
                PagesChanged?.Invoke(this, EventArgs.Empty);
            });
        }

        public void GoToNextPage()
        {
            if (mangaPreview == null) return;

            HistoryService.AddToHistory(mangaPreview.Id, ChapterIndex, PageIndex);
            GoToPage(PageIndex + 1);
        }

        public void GoToPreviousPage()
        {
            GoToPage(PageIndex - 1);
        }

        public void GoToPage(int pageId)
        {
            PageIndex = pageId;

            if (PageIndex >= GetPagesCount())
                GoToChapter(ChapterIndex + 1);
            
            if (PageIndex+1 < 0)
                GoToChapter(ChapterIndex);

            PagesChanged?.Invoke(this, EventArgs.Empty);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NextPage)));
        }

        public void GoToChapter(int chapterId)
        {
            ChapterIndex = chapterId;
            GoToPage(0);

            ChapterChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanGoDown()
        {
            return PageIndex > 0;
        }

        private int GetPagesCount()
        {
            if (chapters == null) return int.MaxValue;
            return (ChapterIndex >= chapters.Count) ? int.MaxValue : chapters[ChapterIndex].Pages;
        }
    }
}
