using System.Windows.Threading;
using MangaReader.Config;

namespace MangaReader.Services.Reader
{
    internal class AutoScrollerService
    {
        private readonly ScrollerService scrollerService;

        private readonly DispatcherTimer autoScrollTimer;

        public AutoScrollerService(ScrollerService scrollerService)
        {
            this.scrollerService = scrollerService;

            autoScrollTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(Scroll.AutoScrollPeriod)
            };
            autoScrollTimer.Tick += AutoScroll;
        }

        public void StartAutoScroll()
        {
            autoScrollTimer.Start();
        }
        
        public void StopAutoScroll()
        {
            autoScrollTimer.Stop();   
        }

        private void AutoScroll(object? sender, EventArgs e)
        {
            scrollerService.Scroll(Scroll.AutoScrollValue);
        }
    }
}
