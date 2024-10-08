﻿using GameLauncher.Models;

namespace GameLauncher.Services.Interface;

public interface IAssetDownloader
{
    void CopyFile(string url, string targetPath);
    Task DownloadFile(string url, string targetPath);
    Task RapatrierAsset(Item item);
    Task RapatrierAsset(Collection item);
    string CreateItemAssetFolder(Guid guid);
    Task GetIntroVideo(string url, string targetPath);
}