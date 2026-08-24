namespace SkyBookWeb.Utilty
{
    public class Uri
    {
        public static string GetHomeUrl(HttpContext httpContext)
        {
            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
        }
    }
}
