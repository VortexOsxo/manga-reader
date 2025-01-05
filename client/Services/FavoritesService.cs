using MangaReader.Model;
using System.Net;

namespace MangaReader.Services
{
    internal class FavoritesService
    {
        static public async Task<HttpStatusCode> AddFavorite(string mangaId)
        {
            return await HttpService.Post($"favorites/{mangaId}");
        }

        static public async Task<HttpStatusCode> RemoveFavorite(string mangaId)
        {
            return await HttpService.Delete($"favorites/{mangaId}");
        }

        static public async Task<List<Manga>> GetFavorites()
        {
            return await HttpService.Get<List<Manga>>("favorites") ?? [];
        }

        static public async Task<bool> IsFavorite(string mangaId)
        {
            dynamic isFavorite = await HttpService.Get<dynamic>($"favorites/{mangaId}") ?? new { isFavorite = false };
            return isFavorite.isFavorite ?? false;
        }
    }
}
