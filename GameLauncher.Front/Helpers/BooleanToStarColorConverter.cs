using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;

namespace GameLauncher.Front.Helpers;
public class BooleanToStarColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        bool isFavorite = (bool)value;
        return isFavorite ? new SolidColorBrush(Colors.Yellow) : new SolidColorBrush(Colors.Transparent);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is SolidColorBrush brush)
        {
            if (brush.Color == Colors.Yellow)
            {
                return true;
            }
            else if (brush.Color == Colors.Transparent)
            {
                return false;
            }
        }
        return false;
    }
}

