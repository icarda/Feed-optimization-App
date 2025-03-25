using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FeedOptimizationApp.Helpers;

public class AbsoluteValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal decimalValue)
        {
            return Math.Abs(decimalValue);
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}