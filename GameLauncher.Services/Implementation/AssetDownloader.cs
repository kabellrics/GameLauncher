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
}
