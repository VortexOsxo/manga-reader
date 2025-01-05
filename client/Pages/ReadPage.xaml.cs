using MangaReader.Services;
using System.Windows.Controls;

namespace MangaReader.Pages
{
    public partial class ReadPage : Page
    {
        public ReadPage()
        {
            InitializeComponent();
            Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object? sender, EventArgs ags)
        {
            ReaderService.Instance.Reset();
        }
    }
}
