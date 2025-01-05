using MangaReader.Services;
using MangaReader.Services.ReaderServices;
using System.ComponentModel;
using System.Windows.Controls;

namespace MangaReader.UserControls
{
    public partial class PageIndexControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string ChapterText { 
            get { return $"{pagesService.ChapterIndex+1}"; }
            set { pagesService.GoToChapter(int.Parse(value) - 1);  }
        }
        public string PageText{
            get { return $"{pagesService.PageIndex+1}"; }
            set { pagesService.GoToPage(int.Parse(value)-1); }
        }

        private readonly PagesService pagesService;

        public PageIndexControl()
        {
            pagesService = ReaderService.Instance.pagesService;
            pagesService.PagesChanged += OnPagesChanged;

            InitializeComponent();
            DataContext = this;
        }

        private void OnPagesChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(ChapterText));
            OnPropertyChanged(nameof(PageText));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
