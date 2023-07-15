namespace Fabula.Data.Infrastructure.Attributes;

using Enums;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// A custom validation attribute used for validating whether the count of an ICollection or IEnumerable,
/// fits under each strategy
/// </summary>

[AttributeUsage(AttributeTargets.Property)]

public class ElementCountAttribute : ValidationAttribute
{
    private readonly int expectedCount;

    private readonly LimitType limitType;

    public ElementCountAttribute(int expectedCount, LimitType limitType)
    {
        this.expectedCount = expectedCount;
        this.limitType = limitType;
    }

    public override bool IsValid(object? value)
    {
        Func<int, int, bool> validationStrategy;

        switch (limitType)
        {
            case LimitType.Minimum:
                validationStrategy = (n, m) => n >= m;
                break;

            case LimitType.Maximum:
                validationStrategy = (n, m) => n <= m;
                break;

            case LimitType.Exact:
                validationStrategy = (n, m) => n == m;
                break;

            default:
                return false;
        }

        if (value is ICollection<object> collection)
            return validationStrategy(collection.Count, expectedCount);

        if (value is IEnumerable<object> enumerable)
            return validationStrategy(enumerable.Count(), expectedCount);

        return false;
    }
}
