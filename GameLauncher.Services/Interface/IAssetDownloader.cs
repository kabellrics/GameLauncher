﻿namespace GameLauncher.Services.Interface;

public interface IAssetDownloader
{
    Task DownloadFile(string url, string targetPath);
}