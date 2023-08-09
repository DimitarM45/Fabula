namespace Fabula.Common.Messages;

/// <summary>
/// Messages used for logging app events.
/// </summary>

public static class LoggerMessages
{
    public const string Critical =
        "Attention, a critical error has occurred - {0} at {1}! Triggered by user id {2} when accessing {3} at {4}.";

    public const string Error = 
        "An error has occurred - {0} at {1}! Triggered by user id {2} when accessing {3} at {4}.";

    public const string Warning =
        "Warning - {0} at {1}! Triggered by user id {2} when accessing {3} at {4}.";

    public const string Information =
        "Event: {0}. Triggered by user id {1} when accessing page {2} at {3}.";

    public const string Debug = "Debug info: {0}";

    public const string Trace = "Action trace info: {0}";

    public const string NonExistentUser = "Anonymous";
}
