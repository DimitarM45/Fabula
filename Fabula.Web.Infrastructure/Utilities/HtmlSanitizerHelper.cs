namespace Fabula.Web.Infrastructure.Utilities;

using Ganss.Xss;

public static class HtmlSanitizerHelper
{
    public static void SanitizeInput(this string input)
    {
        HtmlSanitizer sanitizer = GetSanitizer();
    }

    public static void SanitizeInput(this object[] input)
    {

    }

    private static HtmlSanitizer GetSanitizer()
    {
        HtmlSanitizer sanitizer = new HtmlSanitizer();

        return sanitizer;
    }
}
