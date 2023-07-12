namespace Fabula.Common;

public static class ValidationConstants
{
    public static class Shared
    {
        public const int UrlMinLength = 15;
        public const int UrlMaxLength = 2084;
    }

    public static class Piece
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 100;

        public const int ContentMinLength = 200;
        public const int ContentMaxLength = 100000;

        public const int SynopsysMinLength = 10;
        public const int SynopsysMaxLength = 200;
    }

    public static class Genre
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 75;
    }

    public static class Tag
    {
        public const int NameMinLength = 5;
        public const int NameMaxLength = 75;
    }

    public static class Comment
    {
        public const int ContentMinLength = 1;
        public const int ContentMaxLength = 200;
    }

    public static class List
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 75;

        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 2000;
    }

    public static class Rating
    {
        public const int MinValue = 1;
        public const int MaxValue = 5;
    }

    public static class ApplicationUser
    {
        public const int BioMinLength = 0;
        public const int BioMaxLength = 200;

        public const int NameMinLength = 1;
        public const int NameMaxLength = 150;

        public const int UsernameMinLength = 5;
        public const int UsernameMaxLength = 30;

        public const int EmailMinLength = 3;
        public const int EmailMaxLength = 320;
    }
}