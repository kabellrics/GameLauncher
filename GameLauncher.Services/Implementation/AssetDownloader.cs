﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Models;
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
    public void CopyFile(string url, string targetPath)
    {
        try
        {
            File.Copy(url, targetPath, true);
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
    public async Task RapatrierAsset(Item item)
    {
        var folderPath = CreateItemAssetFolder(item.ID);
        if (!item.Banner.Contains(Path.Combine("GameLauncher", "Assets", "Item")))
        {
            var targetfile = Path.Combine(folderPath, "banner.jpg");
            if (File.Exists(item.Banner))
            {
                CopyFile(item.Banner, targetfile);
            }
            else
            {
                await DownloadFile(item.Banner, targetfile);
            }
            item.Banner = targetfile;
        }
        if (!item.Logo.Contains(Path.Combine("GameLauncher", "Assets", "Item")))
        {
            var targetfile = Path.Combine(folderPath, "logo.png");
            if (File.Exists(item.Logo))
            {
                CopyFile(item.Logo, targetfile);
            }
            else
            {
                await DownloadFile(item.Logo, targetfile);
            }
            item.Logo = targetfile;
        }
        if (!item.Cover.Contains(Path.Combine("GameLauncher", "Assets", "Item")))
        {
            var targetfile = Path.Combine(folderPath, "cover.jpg");
            if (File.Exists(item.Cover))
            {
                CopyFile(item.Cover, targetfile);
            }
            else
            {
                await DownloadFile(item.Cover, targetfile);
            }
            item.Cover = targetfile;
        }
        if (!item.Artwork.Contains(Path.Combine("GameLauncher", "Assets", "Item")))
        {
            var targetfile = Path.Combine(folderPath, "artwork.jpg");
            if (File.Exists(item.Artwork))
            {
                CopyFile(item.Artwork, targetfile);
            }
            else
            {
                await DownloadFile(item.Artwork, targetfile);
            }
            item.Artwork = targetfile;
        }
    }
    public string CreateItemAssetFolder(Guid guid)
    {
        string currentUser = Environment.UserName;

        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Construit le chemin complet vers le nouveau dossier
        string targetPath = Path.Combine(documentsPath, "GameLauncher", "Assets", "Item", guid.ToString());

        // Crée le dossier (et tous les dossiers parents si nécessaire)
        Directory.CreateDirectory(targetPath);
        return targetPath;
    }
}
