using MangaReader.Services.ReaderServices;

namespace MangaReader.Services.Reader
{
    internal class ScrollerService
    {
        public event EventHandler? Scrolled;
        public double scrollPercentage = 0;
        public int scrollSpeed = 1;

        private PagesService pagesService;

        public ScrollerService(PagesService pagesService)
        { 
            this.pagesService = pagesService;
            pagesService.ChapterChanged += OnChapterChanged;
        }

        public void Scroll(double scroll)
        {
            scrollPercentage += scroll * scrollSpeed;

            if (!pagesService.CanGoDown() && scrollPercentage > 0)
                scrollPercentage = 0;

            if (scrollPercentage <= -1) {
                scrollPercentage += 1;
                pagesService.GoToNextPage();
            }

            else if (scrollPercentage > 0)
            {
                scrollPercentage -= 1;
                pagesService.GoToPreviousPage();
            }

            Scrolled?.Invoke(this, EventArgs.Empty);
        }

        public void ResetScroll()
        {
            scrollPercentage = 0;
            scrollSpeed = 1;
        }

        private void OnChapterChanged(object? sender, EventArgs e)
        {
            ResetScroll();
        }
    }
}
