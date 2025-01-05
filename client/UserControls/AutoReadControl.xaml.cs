using MangaReader.Services;
using MangaReader.Services.Reader;
using System.Windows.Controls;

namespace MangaReader.UserControls
{
    public partial class AutoReadControl : UserControl
    {
        private ScrollerService scrollerService;
        private AutoScrollerService autoScrollerService;

        public AutoReadControl()
        {
            scrollerService = ReaderService.Instance.scrollerService;
            autoScrollerService = ReaderService.Instance.autoScrollerService;

            InitializeComponent();
            DataContext = this;
        }

        public bool IsAutoScrollEnabled
        {
            set {
                if (value) autoScrollerService.StartAutoScroll();
                else autoScrollerService.StopAutoScroll();
            }
        }

        public int SliderValue { 
            get { return scrollerService.scrollSpeed; }
            set
            {
                scrollerService.scrollSpeed = value;
            }
        }

    }
}
