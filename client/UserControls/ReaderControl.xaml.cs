using MangaReader.Services;
using MangaReader.Services.Reader;
using MangaReader.Services.ReaderServices;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MangaReader.UserControls
{
    public partial class ReaderControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int CurrentPageTop { get { return (int) (scrollerService.scrollPercentage * CurrentImage.ActualHeight); } }
        public int NextPageTop { get { return CurrentPageTop + (int) CurrentImage.ActualHeight; } }
        public double PageLeft { get { return (SystemParameters.PrimaryScreenWidth - ReaderService.Instance.MangaWidth) / 2; } }

        public PagesService PagesService { get; private set; }
        private readonly ReaderService readerService;
        private readonly ScrollerService scrollerService;

        public ReaderControl()
        {
            readerService = ReaderService.Instance;
            PagesService = readerService.pagesService;
            scrollerService = readerService.scrollerService;

            scrollerService.Scrolled += OnScroll;

            InitializeComponent();
            DataContext = this;
        }

        private void Control_MouseWheel(object? sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            scrollerService.Scroll(e.Delta / (CurrentImage.ActualHeight*2));
        }

        private void OnScroll(object? sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPageTop)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NextPageTop)));
        }
    }
}
