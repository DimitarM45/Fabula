namespace Fabula.Common;

public static class ValidationConstants
{
    public static class Shared
    {
        public const int UrlMinLength = 15;
        public const int UrlMaxLength = 2084;
    }

    public static class Story
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 100;

        public const int ContentMinLength = 200;
        public const int ContentMaxLength = 100000;

        public const int SynopsysMinLength = 10;
        public const int SynopsysMaxLength = 2000;
    }

    public static class Genre
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 50;
    }

    public static class ApplicationUser
    {
        public const int BioMinLength = 0;
        public const int BioMaxLength = 200;

        public const int NameMinLength = 1;
        public const int NameMaxLength = 150;
    }
}