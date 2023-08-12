namespace Fabula.Common.Messages;

public static class SuccessMessages
{
    public static class Shared
    {
        public const string SuccessfulResourceCreationMessage = "Successfully created {0} - {1}!";

        public const string SuccessfulResourceDeletionMessage = "The {0} was deleted successfully!";

        public const string SuccessfulResourceUpdateMessage = "The {0} - {1} was updated successfully!";

        public const string SuccessfulResourceRestorationMessage = "The {0} was restored successfully!";
    }

    public static class Authentication
    {
        public const string SuccessfulRegistrationMessage = "You registered successfully!";

        public const string SuccessfulLoginMessage = "You logged in successfully!";
    }

    public static class AccountManagement
    {
        public const string SuccessfulAccountDetailsUpdate = "Account details updated successfully!";
    }
}
