using System.Net;

namespace MangaReader.Services.Authentication
{
    internal class LoginService
    {
        static public string sessionId = "";

        static public async Task<HttpStatusCode> Login(string username, string password)
        {
            return await HttpService.Post("login", new { username, password }, LoginCallback);
        }

        static private void LoginCallback(dynamic body)
        {
            sessionId = body.session_id ?? "";
        }
    }
}
