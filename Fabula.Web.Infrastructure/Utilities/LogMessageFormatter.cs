namespace Fabula.Web.Infrastructure.Utilities;

using static Common.Messages.LoggerMessages;

/// <summary>
/// A class that formats messages for an ILogger using a predefined pattern.
/// </summary>

public static class LogMessageFormatter
{
    /// <summary>
    /// Formats warning message using a predefined pattern.
    /// </summary>
    /// <param name="logMessage">The message that must adhere to the predefined pattern in this method.</param>
    /// <param name="e">The exception to be logged</param>
    /// <param name="userId">The id of the user that triggered the exception.</param>
    /// <param name="controllerName">The name of the controller where the exception occurred.</param>
    /// <param name="actionName">The name of the action where the exception occurred.</param>
    /// <returns></returns>

    public static string FormatWarningLogMessage(string logMessage, Exception e, string? userId, string controllerName, string actionName)
    {
        return string.Format(logMessage,
            e.Message,
            e.StackTrace,
            userId == null ? NonExistentUser : userId,
            "/" + controllerName +
            "/" + actionName,
            DateTime.Now);
    }
}
