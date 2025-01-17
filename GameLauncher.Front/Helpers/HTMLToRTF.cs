using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media.Imaging;

namespace GameLauncher.Front.Helpers;
public class HTMLToRTF
{
    public static Paragraph ConvertHtmlToParagraph(string html)
    {
        // Si le texte HTML est null ou vide, retourne un paragraphe vide
        if (string.IsNullOrWhiteSpace(html))
        {
            return new Paragraph();
        }

        // Parse the HTML content
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // Create a Paragraph
        var paragraph = new Paragraph();

        // Recursively parse the HTML nodes
        foreach (var node in doc.DocumentNode.ChildNodes)
        {
            ParseHtmlNode(node, paragraph);
        }

        return paragraph;
    }

    private static void ParseHtmlNode(HtmlNode node, Paragraph paragraph)
    {
        switch (node.NodeType)
        {
            case HtmlNodeType.Text:
                // Handle plain text
                var text = node.InnerText.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    paragraph.Inlines.Add(new Run { Text = text });
                }
                break;

            case HtmlNodeType.Element:
                // Handle different HTML elements
                switch (node.Name.ToLower())
                {
                    case "b":
                    case "strong":
                        var boldRun = new Run { Text = node.InnerText.Trim(), FontWeight = FontWeights.Bold };
                        paragraph.Inlines.Add(boldRun);
                        break;

                    case "i":
                    case "em":
                        var italicRun = new Run { Text = node.InnerText.Trim(), FontStyle = Windows.UI.Text.FontStyle.Italic };
                        paragraph.Inlines.Add(italicRun);
                        break;

                    case "u":
                        var underlineRun = new Run { Text = node.InnerText.Trim(), TextDecorations = Windows.UI.Text.TextDecorations.Underline };
                        paragraph.Inlines.Add(underlineRun);
                        break;

                    case "br":
                        paragraph.Inlines.Add(new LineBreak());
                        break;

                    case "img":
                        var imageElement = CreateImageFromHtmlNode(node);
                        if (imageElement != null)
                        {
                            var imageContainer = new InlineUIContainer { Child = imageElement };
                            paragraph.Inlines.Add(imageContainer);
                        }
                        break;

                    default:
                        // Recursively process child nodes for other elements
                        foreach (var child in node.ChildNodes)
                        {
                            ParseHtmlNode(child, paragraph);
                        }
                        break;
                }
                break;
        }
    }

    private static Image CreateImageFromHtmlNode(HtmlNode node)
    {
        // Extract the 'src' attribute from the <img> tag
        var src = node.GetAttributeValue("src", null);
        if (string.IsNullOrEmpty(src))
        {
            return null;
        }

        // Create the Image element
        var image = new Image
        {
            Source = new BitmapImage(new Uri(src, UriKind.RelativeOrAbsolute)),
            MaxHeight = 100, // Set a default max height
            MaxWidth = 100   // Set a default max width
        };

        // Optionally, you can adjust the size based on other attributes (e.g., width/height in the <img> tag)
        var width = node.GetAttributeValue("width", 0);
        if (width > 0)
        {
            image.Width = width;
        }

        var height = node.GetAttributeValue("height", 0);
        if (height > 0)
        {
            image.Height = height;
        }

        return image;
    }
}
