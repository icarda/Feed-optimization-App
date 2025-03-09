using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FeedOptimizationApp.Helpers
{
    /// <summary>
    /// Extension methods for enumerations.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the display name of an enumeration value.
        /// </summary>
        /// <param name="enumValue">The enumeration value.</param>
        /// <returns>The display name of the enumeration value.</returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()?
                            .GetName() ?? enumValue.ToString();
        }
    }

    /// <summary>
    /// Represents an enumeration value with a display name.
    /// </summary>
    /// <typeparam name="T">The type of the enumeration.</typeparam>
    public class DisplayableEnum<T>
    {
        /// <summary>
        /// Gets the enumeration value.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets the display name of the enumeration value.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayableEnum{T}"/> class.
        /// </summary>
        /// <param name="value">The enumeration value.</param>
        /// <param name="displayName">The display name of the enumeration value.</param>
        public DisplayableEnum(T value, string displayName)
        {
            Value = value;
            DisplayName = displayName;
        }

        /// <summary>
        /// Returns the display name of the enumeration value.
        /// </summary>
        /// <returns>The display name of the enumeration value.</returns>
        public override string ToString()
        {
            return DisplayName;
        }
    }
}