namespace Fabula.Common.Messages;

/// <summary>
/// Custom validation error messages when validating incoming data and visualizing notifications.
/// </summary>

public static class ErrorMessages
{
    private const string PleaseTryAgainString = "Please try again!";

    /// <summary>
    /// Error messages shared by multiple form models and/or actions and notifications.
    /// </summary>

    public static class Shared
    {
        public const string StringLengthErrorMessage = "{0} must be between {2} and {1} characters long!";

        public const string StringRequiredErrorMessage = "{0} is required!";

        public const string InvalidStringErrorMessage = "{0} is invalid!";

        public const string GeneralErrorMessage = "An error occurred!" + " " + PleaseTryAgainString;

        public const string InvalidInputDataErrorMessage = "Invalid input data!" + " " + PleaseTryAgainString;

    }

    /// <summary>
    /// Error messages used for the composition form models and/or related actions and notifications./>
    /// </summary>

    public static class Composition
    {
        public const string GenreCountErrorMessage = "A composition must have at least {1} genre!";

        public const string FailedRestoreErrorMessage = "Your work couldn't be restored!.";

        public const string FailedCreatingCompositionErrorMessage = "An error occurred trying to create your story!";

        public const string FailedEditingCompositionErrorMessage = "An error occurred trying to edit your story!";
    }

    /// <summary>
    /// Error messages used for the authentication form models and/or related actions and notifications./>
    /// </summary>

    public static class Authentication
    {
        public const string PasswordDoesNotMatchErrorMessage = "The password and confirmation password do not match!";

        public const string IncorrectLoginCredentialErrorMessage = "Wrong password or username/email!";

        public const string InvalidLoginAttemptErrorMessage = "Invalid login attempt!";

        public const string FailedRegistrationErrorMessage = "Registration failed!" + " " + PleaseTryAgainString;

        public const string FailedLoginErrorMessage = "Login failed!" + " " + PleaseTryAgainString;

        public const string AccountLockoutErrorMessage = "Login attempt limit exceeded! Try again later.";
    }
}
