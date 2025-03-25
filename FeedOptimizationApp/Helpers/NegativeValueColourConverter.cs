using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FeedOptimizationApp.Helpers;

public class NegativeValueColourConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal decimalValue && decimalValue < 0)
        {
            return Colors.Red;
        }
        return Colors.Black;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}