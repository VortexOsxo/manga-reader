using MangaReader.Config;
using MangaReader.Model;

namespace MangaReader.Services
{
    internal class MangasService
    {
        static public async Task<List<Manga>> GetPreviews()
        {
            return await HttpService.Get<List<Manga>>($"mangas") ?? [];
        }

        static public async Task<List<Chapter>> GetChapters(string mangaId)
        {
            return await HttpService.Get<List<Chapter>>($"mangas/{mangaId}/chapters") ?? [];
        }

        static public string GetPageUrl(string mangaId, int chapterId, int pageId)
        {
            return $"{Server.url}/mangas/{mangaId}/{chapterId}/{pageId}";
        }
    }
}
