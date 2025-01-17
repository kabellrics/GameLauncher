using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GameLauncher.Front.Helpers;
public class WebViewPropertie
{
    public static readonly DependencyProperty HtmlStringProperty =
        DependencyProperty.RegisterAttached("HtmlString", typeof(string), typeof(WebViewPropertie), new PropertyMetadata("", OnHtmlStringChanged));

    // Getter and Setter
    public static string GetHtmlString(DependencyObject obj)
    {
        return (string)obj.GetValue(HtmlStringProperty);
    }
    public static void SetHtmlString(DependencyObject obj, string value)
    {
        obj.SetValue(HtmlStringProperty, value);
    }

    // Handler for property changes in the DataContext : set the WebView
    private static void OnHtmlStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        WebView2 wv = d as WebView2;
        if (wv != null)
        {
            wv.EnsureCoreWebView2Async().GetResults();
            wv.NavigateToString((string)e.NewValue);
        }
    }
}
