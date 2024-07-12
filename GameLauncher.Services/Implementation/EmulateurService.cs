using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameLauncher.Services.Implementation;
public class EmulateurService : IEmulateurService
{
    private readonly GameLauncherContext dbContext;
    public EmulateurService(GameLauncherContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<LUEmulateur> GetLocalEmulator()
    {
        return dbContext.Emulateurs.Where(x => x.IsLocal);
    }
    public IEnumerable<LUProfile> GetLocalEmulatorProfile()
    {
        var localemus = dbContext.Emulateurs.Where(x => x.IsLocal);
        var localprofiles = new List<LUProfile>();
        foreach (var emu in localemus)
        {
            var templocalprofiles = dbContext.Profiles.Where(x => x.LUEmulateurId == emu.Id && x.IsLocal);
            foreach (var item in templocalprofiles)
            {
                var profilename = item.Name;
                item.Name = $"{emu.Name} - {profilename}";
                localprofiles.Add(item);
            }
        }
        return localprofiles;
    }
    public IEnumerable<LUEmulateur> ScanFolderForEmulator(string directoryPath)
    {
        List<LUEmulateur> reponse = new List<LUEmulateur>();
        if (Directory.Exists(directoryPath))
        {
            reponse.AddRange(RecursiveScan(directoryPath));
        }
        else
        {
            Console.WriteLine("Directory does not exist.");
        }
        return reponse.GroupBy(x => x.Name).Select(grp => grp.First()).ToList();
    }
    public IEnumerable<LUEmulateur> RecursiveScan(string directoryPath)
    {
        // Get all files in the current directory and add them to the database
        var files = Directory.GetFiles(directoryPath, "*.exe");
        foreach (var file in files)
        {
            var profiles = dbContext.Profiles.ToList();
            var filename = Path.GetFileName(file);
            var currentprofiles = profiles.Where(x => DeFormatExe(x.StartupExecutable).Equals(filename, StringComparison.OrdinalIgnoreCase));
            var localprofiles = new List<LUEmulateur>();
            foreach (var profile in currentprofiles)
            {
                    if (profile.ProfileFiles != null && profile.ProfileFiles.Any())
                    {
                        var corepath = Path.Combine(Path.GetDirectoryName(file), profile.ProfileFiles.First().Replace("'",string.Empty));
                        if (File.Exists(corepath))
                        {
                            profile.StartupExecutable = file;
                            profile.IsLocal = true;
                        }
                    }
                    else
                    {
                        profile.StartupExecutable = file;
                        profile.IsLocal = true;
                    }
                dbContext.Profiles.Update(profile);
                dbContext.SaveChanges(true);
                var emu = dbContext.Emulateurs.FirstOrDefault(x => x.Id == profile.LUEmulateurId);
                if (emu != null)
                {
                    emu.IsLocal = true;
                    dbContext.Emulateurs.Update(emu);
                    dbContext.SaveChanges(true);
                    localprofiles.Add(emu);
                }
            }
            foreach (var localprofile in localprofiles)
                yield return localprofile;
        }

        // Recursively scan all subdirectories
        var directories = Directory.GetDirectories(directoryPath);
        foreach (var directory in directories)
        {
            foreach (var item in RecursiveScan(directory))
                yield return item;
        }
    }
    public async IAsyncEnumerable<LUEmulateur> RecursiveScanAsync(string directoryPath)
    {
        // Get all files in the current directory and add them to the database
        var files = Directory.GetFiles(directoryPath, "*.exe");
        foreach (var file in files)
        {
            var profiles = dbContext.Profiles.ToList();
            var filename = Path.GetFileName(file);
            var profile = profiles.FirstOrDefault(x => DeFormatExe(x.StartupExecutable).Equals(filename, StringComparison.OrdinalIgnoreCase));
            if (profile != null)
            {
                profile.StartupExecutable = file;
                dbContext.Profiles.Update(profile);
                dbContext.SaveChanges(true);
                var emu = dbContext.Emulateurs.FirstOrDefault(x => x.Id == profile.LUEmulateurId);
                if (emu != null)
                {
                    emu.IsLocal = true;
                    dbContext.Emulateurs.Update(emu);
                    dbContext.SaveChanges(true);
                    yield return emu;
                }
            }
        }

        // Recursively scan all subdirectories
        var directories = Directory.GetDirectories(directoryPath);
        foreach (var directory in directories)
        {
            await foreach (var item in RecursiveScanAsync(directory))
                yield return item;
        }
    }
    private string DeFormatExe(string? exePath)
    {
        if (!string.IsNullOrWhiteSpace(exePath))
        {
            var result = exePath.Replace("^", string.Empty).Replace("$", string.Empty).Replace(".*", string.Empty).Replace("\\", string.Empty);
            return result;
        }
        else
            return string.Empty;
    }
    private string DeFormatProfilePath(string exePath)
    {
        return exePath.Replace("^", string.Empty).Replace("$", string.Empty).Replace("\u0027", string.Empty);
    }
}
