namespace Fabula.Common;

/// <summary>
/// Validation constants used for data annotation attribute parameters when validating form models or entities.
/// </summary>

public static class ValidationConstants
{
    /// <summary>
    /// Validation constants shared by multiple form models.
    /// </summary>

    public static class Shared
    {
        public const int UrlMinLength = 15;
        public const int UrlMaxLength = 2084;

        public const string UrlRegex = @"^((http|https)(://))?(www\.)?\w+\.[a-z]{1,5}(?<route>/[\w?=%&#]+)*$";
    }

    /// <summary>
    /// Validation constants used for the composition entity or its form models.
    /// </summary>

    public static class Composition
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 100;

        public const int ContentMinLength = 50;
        public const int ContentMaxLength = 100000;

        public const int SynopsisMinLength = 10;
        public const int SynopsisMaxLength = 2000;
    }

    /// <summary>
    /// Validation constants used for the genre entity or its form models.
    /// </summary>

    public static class Genre
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 75;
    }

    /// <summary>
    /// Validation constants used for the tag entity or its form models.
    /// </summary>

    public static class Tag
    {
        public const int NameMinLength = 5;
        public const int NameMaxLength = 75;
    }

    /// <summary>
    /// Validation constants used for the comment entity or its form models.
    /// </summary>

    public static class Comment
    {
        public const int ContentMinLength = 1;
        public const int ContentMaxLength = 200;
    }

    /// <summary>
    /// Validation constants used for the list entity or its form models.
    /// </summary>

    public static class List
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 75;

        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 2000;
    }

    /// <summary>
    /// Validation constants used for the rating entity or its form models.
    /// </summary>

    public static class Rating
    {
        public const int MinValue = 1;
        public const int MaxValue = 5;
    }

    /// <summary>
    /// Validation constants used for the applicationUser entity or its form models.
    /// </summary>

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