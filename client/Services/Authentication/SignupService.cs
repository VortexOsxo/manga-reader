using System.Net;

namespace MangaReader.Services.Authentication
{
    internal class SignupService
    {
        static async public Task<HttpStatusCode> Signup(string username, string password)
        {
            return await HttpService.Post("signup", new { username, password });
        }
    }
}
