using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Models.ScreenScraper;
using GameLauncher.Services.Interface;
using RestSharp;

namespace GameLauncher.Services.Implementation;
public class ScreenscraperService : IScreenscraperService
{
    RestClient sscpclient;
    private string devid;
    private string devpwd;
    private string ssid;
    private string sspasword;
    public ScreenscraperService()
    {
        sscpclient = new RestClient("https://www.screenscraper.fr/api2");
    }
    private void SetDevId()
    {
        var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "SSCP.gral");
        string[] lines = File.ReadAllLines(filepath);
        byte[] raw = new byte[lines[0].Length / 2];
        for (int i = 0; i < raw.Length; i++)
        {
            raw[i] = Convert.ToByte(lines[0].Substring(i * 2, 2), 16);
        }
        var devid64 = Encoding.ASCII.GetString(raw);
        byte[] data = Convert.FromBase64String(devid64);
        devid = Encoding.UTF8.GetString(data);

        byte[] raw2 = new byte[lines[1].Length / 2];
        for (int i = 0; i < raw2.Length; i++)
        {
            raw2[i] = Convert.ToByte(lines[1].Substring(i * 2, 2), 16);
        }
        var devpwd64 = Encoding.ASCII.GetString(raw2);
        byte[] data2 = Convert.FromBase64String(devpwd64);
        devpwd = Encoding.UTF8.GetString(data2);
        ssid = lines[2];
        sspasword = lines[3];
    }
    private RestRequest InitREquestDefaultParam(string method)
    {
        SetDevId();
        var request = new RestRequest(method, Method.Get);
        request.AddParameter("devid", devid);
        request.AddParameter("devpassword", devpwd);
        request.AddParameter("softname", "RetroFront");
        request.AddParameter("output", "json");
        request.AddParameter("ssid", ssid);
        request.AddParameter("sspassword", sspasword);
        return request;
    }
    public IEnumerable<Jeux> SearchGameByName(string name)
    {
        //https://www.screenscraper.fr/api2/jeuRecherche.php?devid=xxx&devpassword=yyy&softname=zzz&output=json&ssid=test&sspassword=test&recherche=gran turismo"
        try
        {
            var request = InitREquestDefaultParam("jeuRecherche.php");
            request.AddParameter("recherche", name);
            var response = sscpclient.Execute<SCSPGameRequest>(request);
            var result = response.Data.response.jeux;
            return result.Where(x => x.noms != null && x.noms.Count > 0);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public IEnumerable<Jeux> SearchGameByFileName(string filename)
    {
        //https://www.screenscraper.fr/api2/jeuRecherche.php?devid=xxx&devpassword=yyy&softname=zzz&output=json&ssid=test&sspassword=test&recherche=gran turismo"
        try
        {
            //var request = InitREquestDefaultParam("jeuRecherche.php");
            var request = InitREquestDefaultParam("jeuInfos.php");
            //request.AddParameter("recherche", name);
            request.AddParameter("md5", CalculMD5(filename));
            request.AddParameter("romtaille", GetFileSize(filename));
            request.AddParameter("romnom", Path.GetFileName(filename));
            var response = sscpclient.Execute<SCSPGameRequest>(request);
            List<Jeux> result = new List<Jeux>();
            if (response.Data != null && response.Data.response != null && response.Data.response.jeu != null)
                result.Add(response.Data.response.jeu);
            else if (response.Data != null && response.Data.response != null && response.Data.response.jeux != null)
                result.AddRange(response.Data.response.jeux);
            return result.Where(x => x.noms != null && x.noms.Count > 0);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    private string GetFileSize(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Le fichier spécifié est introuvable.", filePath);
        }

        var fileInfo = new FileInfo(filePath);
        return fileInfo.Length.ToString();
    }
    private string CalculMD5(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Le fichier spécifié est introuvable.", filePath);
        }
        using (var fileStream = File.OpenRead(filePath))
        {
            using (var md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(fileStream);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
