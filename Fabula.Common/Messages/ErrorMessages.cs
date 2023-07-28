﻿namespace Fabula.Common.Messages;

/// <summary>
/// Custom validation error messages when validating incoming data.
/// </summary>

public static class ErrorMessages
{
    /// <summary>
    /// Error messages shared by multiple form models or actions.
    /// </summary>

    public static class Shared
    {
        public const string UrlRegexErrorMessage = "The url you've provided is not a valid url!";

        public const string StringLengthErrorMessage = "The {0} must be between {2} and {1} characters long!";
    }

    /// <summary>
    /// Error messages used for the composition form models or related actions./>
    /// </summary>

    public static class Composition
    {
        public const string GenreCountErrorMessage = "A composition must have at least {1} genre!";

        public const string FailedRestoreErrorMessage = "Your work couldn't be restored!.";

        public const string FailedCreatingCompositionErrorMessage = "An error occurred trying to create your story!";

        public const string FailedEditingCompositionErrorMessage = "An error occurred trying to edit your story!";
    }
}