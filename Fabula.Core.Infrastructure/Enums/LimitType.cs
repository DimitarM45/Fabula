namespace Fabula.Core.Infrastructure.Enums;

/// <summary>
/// Enum for selecting validation strategy when using the ElementCount validation attribute
/// </summary>

public enum LimitType
{
    /// <summary>
    /// Minimum element count
    /// </summary>
    Minimum = 0,

    /// <summary>
    /// Maximum element count
    /// </summary>
    Maximum = 1,

    /// <summary>
    /// Exact element count
    /// </summary>
    Exact = 2
}
