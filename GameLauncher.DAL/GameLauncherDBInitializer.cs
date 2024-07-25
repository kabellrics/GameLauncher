using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLauncher.DAL;
public static class GameLauncherDBInitializer
{
    public static void Iniatialize(GameLauncherContext dbcontext)
    {
        dbcontext.Database.EnsureCreated();
        SeedPlatformsData(dbcontext);
        SeedEmulatorAndProfile(dbcontext);
    }
    public static void SeedPlatformsData(GameLauncherContext dbContext)
    {
        if (!dbContext.Platformes.Any())
        {
            var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "File_Utilitaire", "Platforms.yaml");
            string[] lines = File.ReadAllLines(filepath);
            List<string[]> blocks = SplitIntoPlateformBlocks(lines);
            foreach (string[] block in blocks)
            {
                LUPlatformes platform = new LUPlatformes();
                platform.Id = Guid.NewGuid();
                foreach (var line in block)
                {
                    if (line.TrimStart().StartsWith("- Name:"))
                    {
                        platform.Name = line.Substring(line.IndexOf(":") + 1).Trim();
                    }
                    else if (line.TrimStart().StartsWith("Id:"))
                    {
                        platform.Codename = line.Substring(line.IndexOf(":") + 1).Trim();
                    }
                    else if (line.TrimStart().StartsWith("IgdbId:"))
                    {
                        if (int.TryParse(line.Substring(line.IndexOf(":") + 1).Trim(), out int igdbId))
                        {
                            platform.IgdbId = igdbId;
                        }
                    }
                    else if (line.TrimStart().StartsWith("Databases:"))
                    {
                        platform.Databases = ParsePlatformList(line);
                    }
                    else if (line.TrimStart().StartsWith("Emulators:"))
                    {
                        platform.Emulators = ParsePlatformList(line);
                    }
                }
                dbContext.Platformes.Add(platform);
                //return platform;
            }
            dbContext.SaveChanges();
        }
    }
    private static string[] ParsePlatformList(string line)
    {
        var listContent = line.Substring(line.IndexOf("[") + 1);
        listContent = listContent.Substring(0, listContent.IndexOf("]"));
        return listContent.Split(',').Select(item => item.Trim()).ToArray();
    }
    private static List<string[]> SplitIntoPlateformBlocks(string[] lines)
    {
        List<string[]> blocks = new List<string[]>();
        List<string> currentBlock = null;

        foreach (string line in lines)
        {
            if (line.TrimStart().StartsWith("-"))
            {
                if (currentBlock != null)
                {
                    blocks.Add(currentBlock.ToArray());
                }
                currentBlock = new List<string>();
            }

            currentBlock?.Add(line);
        }

        if (currentBlock != null && currentBlock.Count > 0)
        {
            blocks.Add(currentBlock.ToArray());
        }

        return blocks;
    }

    public static void SeedEmulatorAndProfile(GameLauncherContext dbContext)
    {
        if (!dbContext.Emulateurs.Any())
        {
            var folderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "File_Utilitaire", "Emulators");
            var yamlFiles = Directory.GetFiles(folderpath, "*.yaml", SearchOption.AllDirectories).ToList();
            foreach (var yamlFile in yamlFiles)
            {
                LUEmulateur emulator = new LUEmulateur();
                string[] lines = File.ReadAllLines(yamlFile);
                emulator.Id = lines[0].Substring(lines[0].IndexOf(":") + 1).Trim();
                emulator.Name = lines[1].Substring(lines[1].IndexOf(":") + 1).Trim();
                emulator.Website = lines[2].Substring(lines[2].IndexOf(":") + 1).Trim();
                emulator.IsLocal = false;
                emulator.Profiles = new List<Guid>();
                var profilelines = GetLinesStartingFromName(lines);
                List<string[]> blocks = SplitIntoPlateformBlocks(profilelines);
                foreach (var block in blocks)
                {
                    LUProfile profile = new LUProfile();
                    profile.Id = Guid.NewGuid();
                    profile.LUEmulateurId = emulator.Id;
                    profile.IsLocal = false;
                    foreach (var line in block)
                    {
                        if (line.TrimStart().StartsWith("- Name:"))
                        {
                            profile.Name = line.Substring(line.IndexOf(":") + 1).Trim();
                        }
                        else if (line.TrimStart().StartsWith("StartupArguments:"))
                        {
                            profile.StartupArguments = line.Substring(line.IndexOf(":") + 1).Trim();
                        }
                        else if (line.TrimStart().StartsWith("StartupExecutable:"))
                        {
                            profile.StartupExecutable = line.Substring(line.IndexOf(":") + 1).Trim();
                        }
                        else if (line.TrimStart().StartsWith("ImageExtensions:"))
                        {
                            profile.ImageExtensions = ParsePlatformList(line);
                        }
                        else if (line.TrimStart().StartsWith("Platforms:"))
                        {
                            profile.Platforms = ParsePlatformList(line);
                        }
                        else if (line.TrimStart().StartsWith("ProfileFiles:"))
                        {
                            profile.ProfileFiles = ParsePlatformList(line);
                        }
                    }
                    dbContext.Profiles.Add(profile);
                    emulator.Profiles.Add(profile.Id);
                }
                dbContext.Emulateurs.Add(emulator);
            }
            dbContext.SaveChanges();
        }
    }
    private static string[] GetLinesStartingFromName(string[] lines)
    {
        int startIndex = Array.FindIndex(lines, line => line.TrimStart().StartsWith("- Name"));

        if (startIndex != -1)
        {
            return lines.Skip(startIndex).ToArray();
        }

        // Si aucune ligne ne commence par "- Name", retourner un tableau vide ou gérer comme nécessaire
        return lines;
    }
}
