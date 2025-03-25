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
            return $"{decimalValue:P2}"; // Format as percentage with 2 decimal places
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

