using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Services.Interface;
using RestSharp;

namespace GameLauncher.Services.Implementation;
public class AssetDownloader : IAssetDownloader
{
    public async Task DownloadFile(string url, string targetPath)
    {
        // Créez un client RestSharp
        var client = new RestClient();

        // Créez une requête RestSharp
        var request = new RestRequest(url, Method.Get);

        // Exécutez la requête et obtenez la réponse
        var response = client.Execute(request);

        // Vérifiez si la requête a réussi
        if (response.IsSuccessful)
        {
            // Écrivez le contenu du fichier dans le chemin de destination
            File.WriteAllBytes(targetPath, response.RawBytes);
            Console.WriteLine($"Fichier téléchargé et sauvegardé à {targetPath}");
        }
        else
        {
            Console.WriteLine("Échec du téléchargement. Statut: " + response.StatusCode);
        }
    }
    public string CreateItemAssetFolder(Guid guid)
    {
        string currentUser = Environment.UserName;

        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Construit le chemin complet vers le nouveau dossier
        string targetPath = Path.Combine(documentsPath, "GameLauncher", "Assets","Item", guid.ToString());

        // Crée le dossier (et tous les dossiers parents si nécessaire)
        Directory.CreateDirectory(targetPath);
        return targetPath;
    }
}
