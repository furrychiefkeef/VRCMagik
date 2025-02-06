using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using VRCMagik.Console.Enviroment.Wrappers;

namespace VRCMagik.NET.Wrappers;
internal class Vertical
{
    private string HOST_NAME { get; set; }
    public HttpClient? Server;
    public Vertical(string hostName) => HOST_NAME = hostName;
    public void Connect()
    {
        Server = new HttpClient { BaseAddress = new(HOST_NAME) };
    }
    public async Task Call(Requests requests, string path, HttpContent? content = null, bool pretty = true)
    {
        if (Server != null)
        {
            HttpResponseMessage? r = new();
            switch (requests)
            {
                case Requests.GET:
                    r = await Server.GetAsync(path);  
                    break;
                case Requests.POST:
                    r = await Server.PostAsync(path, content);
                    break;
                case Requests.PUT:
                    r = await Server.PutAsync(path, content);
                    break;
                case Requests.DEL:
                    r = await Server.DeleteAsync(path);
                    break;   
                    case Requests.PAT:
                    r = await Server.PatchAsync(path, content);
                    break;

            }
            r.EnsureSuccessStatusCode();
            if (r.IsSuccessStatusCode)
            {
                string _content = await r.Content.ReadAsStringAsync();
                if (pretty)
                {
                    object? obj = JsonSerializer.Deserialize<object>(_content);
                    string prettyJson = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });

                    Entry.console.Info($"[{r.StatusCode}] \n{prettyJson}");
                    System.Console.ReadLine();
                }
                else
                {
                    Entry.console.Info($"[{r.StatusCode}] \n{content}");
                    System.Console.ReadLine();
                }
            }
            else
            {
                System.Console.Clear();
                Entry.console.Error($"{r.Content.ReadAsStringAsync().Result}".AddSlashN());
            }
        }
        else
        {
            Entry.console.Error("SERVER IS NULLED!".AddSlashN());
        }
    }  
	public enum Requests
	{
        GET ,
        POST,
        PUT,
        DEL,
        PAT, /// PATCH
        OPTIONS,
        HEAD
    }
}

/*                case Requests.OPTIONS:
                    break;
                case Requests.HEAD:
                    break;*/
