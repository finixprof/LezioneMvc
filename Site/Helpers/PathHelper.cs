namespace Site.Helpers
{
    public static class PathHelper
    {
        public static string WebRootPath { get; set; }
        public static string GetUrlToConfirmEmail(HttpRequest request, int id,string token)
        {
            return $"{request.Scheme}://{request.Host.Value}/account/ConfermaRegistrazione/{id}?token={token}";

        }

        public  static string GetPathUtente(int id)
        {
            return $"{WebRootPath}\\wwwroot\\uploads\\nazioni\\{id}";
        }
    }
}
