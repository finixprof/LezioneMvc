namespace Site.Helpers
{
    public static class PathHelper
    {
        public static string GetUrlToConfirmEmail(HttpRequest request, int id,string token)
        {
            return $"{request.Scheme}://{request.Host.Value}/account/ConfermaRegistrazione/{id}?token={token}";

        }
    }
}
