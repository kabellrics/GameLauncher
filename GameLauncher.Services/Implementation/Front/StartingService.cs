using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFinder.Common;
using GameLauncher.DAL;
using GameLauncher.Services.Interface.Front;
using Microsoft.AspNetCore.SignalR;
using SharpDX.XInput;

namespace GameLauncher.Services.Implementation.Front;
public class StartingService : IStartingService
{
    static XInputWatcher watcher = new XInputWatcher();
    protected readonly GameLauncherContext _dbContext;

    public StartingService(GameLauncherContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task StartITem(Guid itemid)
    {
        var item = _dbContext.Items.FirstOrDefault(x => x.ID == itemid);
        if (item != null)
        {
            try
            {
                if (item.LUProfileId == null)
                {
                    var ps = new ProcessStartInfo(item.Path)
                    {
                        UseShellExecute = true,
                        Verb = "open"
                    };
                    Process.Start(ps);
                }
                else if (item.LUProfileId != null)
                {
                    var profile = _dbContext.Profiles.FirstOrDefault(x => x.Id == item.LUProfileId);
                    if (profile != null)
                    {
                        var emulatorpath = profile.StartupExecutable;
                        if (!File.Exists(emulatorpath))
                        {
                            return;
                        }
                        var emulatorargs = profile.StartupArguments;
                        var argsWithItemPath = RemoveFirstAndLastCharacter(emulatorargs.Replace("\"{ImagePath}\"", QuotePathIfNeeded(item.Path)));
                        var ps = new ProcessStartInfo(emulatorpath)
                        {
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            WorkingDirectory = Path.GetDirectoryName(emulatorpath),
                            Verb = "open",
                            Arguments = argsWithItemPath
                        };
                        var targetProcess = Process.Start(ps);
                        //var targetProcess = Process.Start(emulatorpath, argsWithItemPath);
                        item.NbStart++;
                        item.LastStartDate = DateTime.Now;
                        _dbContext.Items.Update(item);
                        _dbContext.SaveChanges();
                        await IsEscapeCombinationSend(targetProcess);
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
    }
    public string RemoveFirstAndLastCharacter(string input)
    {
        // Vérifie si la chaîne est nulle ou si elle contient moins de deux caractères
        if (string.IsNullOrEmpty(input) || input.Length < 2)
        {
            return string.Empty; // Retourne une chaîne vide si les conditions ne sont pas remplies
        }

        // Retire le premier et le dernier caractères en utilisant la méthode Substring
        return input.Substring(1, input.Length - 2);
    }
    string QuotePathIfNeeded(string path)
    {
        if (path.Contains(" "))
        {
            return $"\"{path}\"";
        }
        return path;
    }
    async Task IsEscapeCombinationSend(Process process)
    {
        await Task.Run(() =>
        {
            while (!process.HasExited)
            {
                watcher.Update();
                if (
                    (watcher.gamepad.Buttons == (SharpDX.XInput.GamepadButtonFlags.Start | SharpDX.XInput.GamepadButtonFlags.Back))
                  )
                {
                    process.Kill(true);
                    return;
                }
            }
        });
    }

}
public class XInputWatcher
{
    Controller controller;
    public Gamepad gamepad;
    public bool connected = false;
    public int deadband = 2500;
    public Point leftThumb, rightThumb = new Point(0, 0);
    public float leftTrigger, rightTrigger;

    public XInputWatcher()
    {
        controller = new Controller(UserIndex.One);
        connected = controller.IsConnected;
    }

    // Call this method to update all class values
    public void Update()
    {
        //if (!connected)
        if (!controller.IsConnected)
            return;

        gamepad = controller.GetState().Gamepad;
        leftTrigger = gamepad.LeftTrigger;
        rightTrigger = gamepad.RightTrigger;
    }
}
