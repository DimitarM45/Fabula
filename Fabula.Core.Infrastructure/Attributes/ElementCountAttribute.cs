namespace Fabula.Core.Infrastructure.Attributes;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// A custom validation attribute used for validating the expected count of collections.
/// </summary>

[AttributeUsage(AttributeTargets.Property)]

public class ElementCountAttribute : ValidationAttribute
{
    private readonly int expectedCount;

    public ElementCountAttribute(int expectedCount)
    {
        this.expectedCount = expectedCount;
    }

    public override bool IsValid(object? value)
    {
        if (value is ICollection<object> collection)
            return expectedCount == collection.Count;

        if (value is IEnumerable<object> enumerable)
            return expectedCount == enumerable.Count();            

        return false;
    }
}
