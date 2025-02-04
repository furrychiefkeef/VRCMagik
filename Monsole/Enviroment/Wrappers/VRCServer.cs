using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using VRCMagik.Console.Enviroment.Wrappers;

namespace VRCMagik.Wrappers;
internal class VRCServer
{
    static string? AuthCookie;
    static HTTPCenter Client = new HTTPCenter("https://api.vrchat.cloud/api/1/");
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
        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        await Client.Call(HTTPCenter.Requests.DEL, $"groups/{grp_id}");
    }
    public async Task AsyncBlockUser(string userid)
    {
        await Client.Call(HTTPCenter.Requests.POST, $"auth/user/playermoderations");
    }

    /// <summary>
    /// Initializes the HTTP client for VRChat API requests.
    /// </summary>
    public static VRCServer Connect()
    {
        Client.Connect();
        Client.Server.DefaultRequestHeaders
             .Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:133.0) Gecko/20100101 Firefox/133.0");
        Client.Server.DefaultRequestHeaders
            .Add("Accept", "application/json");
        Client.Server.DefaultRequestHeaders
            .Add("Cookie", AuthCookie);
        return default;
    }
}

