using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using HtmlAgilityPack;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Text;
using Windows.UI.Text;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using Microsoft.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GameLauncher.Front.Views.CustomControls;
public sealed partial class HTMLRenderText : UserControl
{
    public HTMLRenderText()
    {
        this.InitializeComponent();
        this.DataContextChanged += HTMLRenderText_DataContextChanged;
    }

    private void HTMLRenderText_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        if(DataContext is string)
        {
            var html = (string)DataContext;
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            foreach (var node in doc.DocumentNode.ChildNodes)
            {
                ParseHtmlNode(node);
            }
        }
    }
    private void ParseHtmlNode(HtmlNode node)
    {
        switch (node.NodeType)
        {
            case HtmlNodeType.Text:
                // Handle plain text
                var text = node.InnerText.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    var tb = new TextBlock
                    {
                        Text = text,
                        TextWrapping = TextWrapping.WrapWholeWords,
                        Foreground = new SolidColorBrush(Colors.AntiqueWhite)
                    };
                    BaseStack.Children.Add(tb);
                }
                break;

            case HtmlNodeType.Element:
                // Handle different HTML elements
                switch (node.Name.ToLower())
                {
                    case "b":
                    case "strong":
                        // Texte en gras
                        var boldTextBlock = new TextBlock
                        {
                            Text = node.InnerText.Trim(),
                            FontWeight = FontWeights.Bold,
                            TextWrapping = TextWrapping.WrapWholeWords,
                            Foreground = new SolidColorBrush(Colors.AntiqueWhite)
                        };
                        BaseStack.Children.Add(boldTextBlock);
                        break;

                    case "i":
                    case "em":
                        // Texte en italique
                        var italicTextBlock = new TextBlock
                        {
                            Text = node.InnerText.Trim(),
                            FontStyle = Windows.UI.Text.FontStyle.Italic,
                            TextWrapping = TextWrapping.WrapWholeWords,
                            Foreground = new SolidColorBrush(Colors.AntiqueWhite)
                        };
                        BaseStack.Children.Add(italicTextBlock);
                        break;

                    case "u":
                        // Texte souligné
                        var underlineTextBlock = new TextBlock
                        {
                            Text = node.InnerText.Trim(),
                            TextDecorations = TextDecorations.Underline,
                            TextWrapping = TextWrapping.WrapWholeWords,
                            Foreground = new SolidColorBrush(Colors.AntiqueWhite)
                        };
                        BaseStack.Children.Add(underlineTextBlock);
                        break;

                    case "br":
                        var lineBreak = new TextBlock
                        {
                            Text = string.Empty, // Ajout d'un espacement
                            Height = 10 // Ajuster la hauteur pour imiter un saut de ligne
                        };
                        BaseStack.Children.Add(lineBreak);
                        break;

                    case "img":
                        var imageElement = CreateImageFromHtmlNode(node);
                        if (imageElement != null)
                        {
                            BaseStack.Children.Add(imageElement);
                        }
                        break;

                    default:
                        // Recursively process child nodes for other elements
                        foreach (var child in node.ChildNodes)
                        {
                            ParseHtmlNode(child);
                        }
                        break;
                }
                break;
        }
    }

    private Image CreateImageFromHtmlNode(HtmlNode node)
    {
        if (node == null || !node.Attributes.Contains("src"))
        {
            // Si le nœud est nul ou ne contient pas d'attribut "src", retourner null
            return null;
        }
        try
        {
            // Récupérer l'URL de l'image depuis l'attribut "src"
            var src = node.Attributes["src"].Value;

            // Créer un contrôle Image
            var image = new Image
            {
                Stretch = Stretch.Uniform
            };
            if (node.Attributes.Contains("width"))
            {
                image.Width = double.Parse(node.Attributes["width"].Value);
            }
            if (node.Attributes.Contains("height"))
            {
                image.Height = double.Parse(node.Attributes["height"].Value);
            }
            // Charger l'image depuis l'URL
            var bitmapImage = new BitmapImage(new Uri(src));
            image.Source = bitmapImage;

            return image;
        }
        catch (Exception ex)
        {
            // Gestion des erreurs si l'URL est invalide ou si le chargement échoue
            Debug.WriteLine($"Erreur lors du chargement de l'image : {ex.Message}");
            return null;
        }
    }
}
