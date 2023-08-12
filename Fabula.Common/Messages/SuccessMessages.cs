namespace Fabula.Common.Messages;

/// <summary>
/// Custom validation success messages when validating incoming data and visualizing notifications. 
/// </summary>

public static class SuccessMessages
{
    /// <summary>
    /// Error messages shared by multiple form models and/or actions and notifications.
    /// </summary>
    
    public static class Shared
    {
        public const string SuccessfulResourceCreationMessage = "Successfully created {0} - {1}!";

        public const string SuccessfulResourceDeletionMessage = "The {0} was deleted successfully!";

        public const string SuccessfulResourceUpdateMessage = "The {0} - {1} was updated successfully!";

        public const string SuccessfulResourceRestorationMessage = "The {0} was restored successfully!";
    }

    /// <summary>
    /// Error messages shared by genre form models and/or actions and notifications.
    /// </summary>

    public static class Genre
    {
        public const string SuccessfulFavoriteGenreAddMessage = "Genre added to favorites successfully!";

        public const string SuccessfulFavoriteGenreRemoveMessage = "Genre removed from favorites successfully!";
    }

    /// <summary>
    /// Error messages shared by authentication form models and/or actions and notifications.
    /// </summary>

    public static class Authentication
    {
        public const string SuccessfulRegistrationMessage = "You registered successfully!";

        public const string SuccessfulLoginMessage = "You logged in successfully!";
    }

    /// <summary>
    /// Error messages shared by account managemenet form models and/or actions and notifications.
    /// </summary>

    public static class AccountManagement
    {
        public const string SuccessfulAccountDetailsUpdate = "Account details updated successfully!";
    }
}
