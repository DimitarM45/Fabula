namespace Fabula.Web.ViewModels.Attributes.Enums;

/// <summary>
/// Enum for selecting validation strategy when using the ElementCount validation attribute.
/// </summary>

public enum CountStrategy
{
    /// <summary>
    /// Minimum element count validation strategy
    /// </summary>
    Minimum = 0,

    /// <summary>
    /// Maximum element count validation strategy
    /// </summary>
    Maximum = 1,

    /// <summary>
    /// Exact element count validation strategy
    /// </summary>
    Exact = 2
}
