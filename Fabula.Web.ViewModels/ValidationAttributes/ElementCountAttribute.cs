namespace Fabula.Web.ViewModels.Attributes;

using Enums;

using System.Collections;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// A custom validation attribute used for validating whether the count of an IEnumerable or any other collections 
/// that inherit from it, including generic collection types, fits under the given strategy
/// </summary>

[AttributeUsage(AttributeTargets.Property)]

public sealed class ElementCountAttribute : ValidationAttribute
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
        Func<int, int, bool>? validationStrategy = countStrategy switch
        {
            CountStrategy.Minimum => (n, m) => n >= m,

            CountStrategy.Maximum => (n, m) => n <= m,

            CountStrategy.Exact => (n, m) => n == m,

            _ => null
        };

        if (validationStrategy != null && value is IEnumerable enumerable)
            return validationStrategy(enumerable.Cast<object>().Count(), expectedCount);

        return false;
    }
}
