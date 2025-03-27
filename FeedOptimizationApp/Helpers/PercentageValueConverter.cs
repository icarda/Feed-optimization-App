using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FeedOptimizationApp.Helpers;

public class PercentageValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal decimalValue)
        {
            return $"{decimalValue:0}%"; // Format as a whole number percentage
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}