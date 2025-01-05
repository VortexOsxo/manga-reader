using MangaReader.Model;
using MangaReader.Config;
using MangaReader.Services.Reader;
using MangaReader.Services.ReaderServices;

namespace MangaReader.Services
{
    internal class ReaderService
    {
        public Manga? manga;

        public readonly PagesService pagesService;
        public readonly ScrollerService scrollerService;
        public readonly AutoScrollerService autoScrollerService;

        public static ReaderService Instance { get { return instance; } }

        public int MangaWidth { get; private set; }

        private static readonly ReaderService instance = new ReaderService();
    
        private ReaderService()
        {
            MangaWidth = Reading.PageWidth;
            pagesService = new PagesService();
            scrollerService = new ScrollerService(pagesService);
            autoScrollerService = new AutoScrollerService(scrollerService);
        }
    
        public void SetManga(Manga manga)
        {
            Reset();
            this.manga = manga;

            pagesService.SetManga(manga);
        }

        public void Reset()
        {
            manga = null;

            autoScrollerService.StopAutoScroll();
            scrollerService.ResetScroll();
        }
    }
}
