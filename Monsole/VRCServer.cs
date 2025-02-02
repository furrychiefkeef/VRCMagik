using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VRCMagik.Console;
internal class VRCServer
{
    private static HttpClient? Client;
    static string? AuthCookie;

    public VRCServer(string authCookie) => AuthCookie = authCookie;

    public void CreateGroup(string name, string dev) 
    {
        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");
    }

    /// <summary>
    /// Sends a DELETE request to remove a group by its ID.
    /// </summary>
    /// <param name="grp_id">The ID of the group to delete.</param>
    public async Task DeleteGroupAsync(string grp_id) 
    {
        if(Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        HttpResponseMessage? response = await Client.DeleteAsync($"groups/{grp_id}");
        if (response.IsSuccessStatusCode)
        {
            string jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize and reformat JSON
            var jsonObject = JsonSerializer.Deserialize<object>(jsonResponse);
            string prettyJson = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

            System.Console.WriteLine($"[{response.StatusCode}] {prettyJson}");
            System.Console.ReadLine();
        }
        else
        {
            System.Console.WriteLine($"[{response.StatusCode}] {await response.Content.ReadAsStringAsync()}");
            System.Console.ReadLine();
        }
    }

    /// <summary>
    /// Initializes the HTTP client for VRChat API requests.
    /// </summary>
    public static void Connect()
    {
        if (Client == null)
        {
            
            Client = new HttpClient()
            {
                BaseAddress = new("https://api.vrchat.cloud/api/1/"),
            };
            Client.DefaultRequestHeaders
                .Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:133.0) Gecko/20100101 Firefox/133.0");
            Client.DefaultRequestHeaders
                .Add("Accept", "application/json");
            Client.DefaultRequestHeaders
                .Add("Cookie", AuthCookie);
        }
        else
        {
            System.Console.WriteLine("DEV::CONNECTED");
        }
    }
}
