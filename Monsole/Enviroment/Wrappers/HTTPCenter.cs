using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VRCMagik.Console.Enviroment.Wrappers;
internal class HTTPCenter
{
    private string HOST_NAME { get; set; }
    public HttpClient? Server;
    public HTTPCenter(string hostName) => HOST_NAME = hostName;
    public void Connect()
    {
        Server = new HttpClient { BaseAddress = new(HOST_NAME) };
    }
    public async Task Call(Requests requests, string path, bool pretty = true)
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
                    r = await Server.PostAsync(path, default);
                    break;
                case Requests.PUT:
                    r = await Server.PutAsync(path, default);
                    break;
                case Requests.DEL:
                    r = await Server.DeleteAsync(path);
                    break;
            }

            if (r.IsSuccessStatusCode)
            {
                string content = await r.Content.ReadAsStringAsync();
                if (pretty)
                {
                    object? obj = JsonSerializer.Deserialize<object>(content);
                    string prettyJson = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });

                    System.Console.WriteLine($"[{r.StatusCode}] \n{prettyJson}");
                    System.Console.ReadLine();
                }
                else
                {
                    System.Console.WriteLine($"[{r.StatusCode}] \n{content}");
                    System.Console.ReadLine();
                }
            }   
        }
        else
        {
            Pagik.Console console = new("VRCMAGIK");
            console.Error("SERVER IS NULLED!");
        }
    }  
	public enum Requests
	{
        GET,
        POST,
        PUT,
        DEL,
        PAT, // PATCH
        OPTIONS,
        HEAD
    }
}
/* case Requests.PAT:
                    break;
                case Requests.OPTIONS:
                    break;
                case Requests.HEAD:
                    break;*/
