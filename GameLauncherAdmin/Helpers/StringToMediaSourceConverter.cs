using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using Windows.Media.Core;

namespace GameLauncherAdmin.Helpers;
public class StringToMediaSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string stringValue && !string.IsNullOrEmpty(stringValue))
        {
            try
            {
                Uri uri = new Uri(stringValue, UriKind.RelativeOrAbsolute);
                return MediaSource.CreateFromUri(uri);
            }
            catch (Exception ex)
            {
                // Handle exception if the URI is invalid
                System.Diagnostics.Debug.WriteLine($"Invalid URI: {ex.Message}");
                return null;
            }
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is MediaSource mediaSource)
        {
            // MediaSource could be of different types, for simplicity we handle MediaSource created from URI
            if (mediaSource is MediaSource source)
            {
                // Try to extract the Uri from the MediaSource
                var uri = source.Uri.ToString();// GetUriFromMediaSource(source);
                if (uri != null)
                {
                    return uri.ToString();
                }
            }
        }

        return null;
    }

}
