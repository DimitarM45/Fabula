namespace Fabula.Web.ViewModels.Attributes;

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

    private readonly CountStrategy countStrategy;

    public ElementCountAttribute(int expectedCount, CountStrategy countStrategy)
    {
        this.expectedCount = expectedCount;
        this.countStrategy = countStrategy;
    }

    public override bool IsValid(object? value)
    {
        Func<int, int, bool> validationStrategy;

        switch (countStrategy)
        {
            case CountStrategy.Minimum:
                validationStrategy = (n, m) => n >= m;
                break;

            case CountStrategy.Maximum:
                validationStrategy = (n, m) => n <= m;
                break;

            case CountStrategy.Exact:
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
