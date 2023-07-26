namespace Fabula.Common;

/// <summary>
/// Custom validation error messages when validating incoming data.
/// </summary>

public static class ErrorMessages
{
    /// <summary>
    /// Error messages shared by multiple form models.
    /// </summary>

    public static class Shared
    {
        public const string UrlRegexErrorMessage = "The url you've provided is not a valid url!";

        public const string StringLengthErrorMessage = "The {0} must be between {2} and {1} characters long!";
    }

    /// <summary>
    /// Error messages used for the composition form models./>
    /// </summary>

    public static class Composition
    {
        public const string GenreCountErrorMessage = "A composition must have at least {1} genre!";
    }
}
