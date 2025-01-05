using MangaReader.Model;

namespace MangaReader.Services
{
    internal class HistoryService
    {
        static public async void AddToHistory(string mangaId, int chapterNumber, int pageNumber)
        {
            await HttpService.Post("histories", new { mangaId, chapterNumber, pageNumber });
        }

        static public async Task<List<Manga>> GetHistory()
        {
            return await HttpService.Get<List<Manga>>("histories") ?? [];
        }

        static public async Task<Bookmark> GetLastReadPage(string mangaId)
        {
            return await HttpService.Get<Bookmark>($"histories/{mangaId}") ?? new Bookmark();
        }
    }
}
