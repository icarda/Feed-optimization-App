using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FeedOptimizationApp.Helpers;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()?
                        .GetName() ?? enumValue.ToString();
    }
}

public class DisplayableEnum<T>
{
    public T Value { get; }
    public string DisplayName { get; }

    public DisplayableEnum(T value, string displayName)
    {
        Value = value;
        DisplayName = displayName;
    }

    public override string ToString()
    {
        return DisplayName;
    }
}