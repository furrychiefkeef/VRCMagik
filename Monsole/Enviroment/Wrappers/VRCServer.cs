using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using VRCMagik.Console.Enviroment.Wrappers;
using VRCMagik.NET.Wrappers;

using static VRCMagik.Wrappers.VRCServer.API_VRC.VRCUser;

namespace VRCMagik.Wrappers;
internal class VRCServer
{     
    
    public VRCServer(string authCookie) => AuthCookie = authCookie;

    private static VRCServer? _instance; // Private static instance
    private static readonly object _lock = new object(); // Lock object for thread safet
    static string? AuthCookie;
    static Vertical Client = new Vertical("https://api.vrchat.cloud/api/1/");

    public static VRCServer GetInstance()
    {
        // Double-check locking for thread safety
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new VRCServer(AuthCookie);
                }
            }
        }
        return _instance;
    }

    public void CreateGroup(string name, string dev)
    {
        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");
    }

    /// <summary>
    /// Sends a DELETE request to remove a group by its ID.
    /// </summary>
    /// <param name="grp_id">The ID of the group to delete.</param>
    public async Task DeleteGroupAsync(string grpId)
    {
        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        await Client.Call(Vertical.Requests.DEL, $"groups/{grpId}");
    }
    #region PlayerModeration

    public async Task AsyncUnmuteUser(string _usr)
    {
        ModerationData mdata = new() { moderated = _usr, type = "unmute" };

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.PUT, $"auth/user/unplayermoderate", content);
    }

   
    public async Task AsyncUnblockUser(string _usr)
    {
        ModerationData mdata = new() {moderated = _usr,type = "unblock"};

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.PUT, $"auth/user/unplayermoderate", content);
    }

    public async Task AsyncInteractionUserOn(string _usr)
    {
        ModerationData mdata = new() {moderated = _usr, type = "interactOn"};

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.PUT, $"auth/user/unplayermoderate", content);
    }
   
    public async Task AsyncMuteUser(string _usr)
    {
        ModerationData mdata = new() { moderated = _usr, type = "mute" };

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }
    public async Task AsyncBlockUser(string _usr)
    {
        ModerationData mdata = new() { moderated = _usr, type = "block" };

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }
    public async Task AsyncInteractionUserOff(string _usr)
    {
        ModerationData mdata = new() { moderated = _usr, type = "interactOff" };

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }

    #endregion
    public async Task AsyncSelectAvatar(string avatarId)
    {
        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        await Client.Call(Vertical.Requests.PUT, $"avatars/{avatarId}/select");
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


    public static class Advance
    {
        public static async Task PlayerModeration(string _usr, ModerationTypes types)
        {
            if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

            switch (types)
            {       
                case ModerationTypes.unmute:
                    await GetInstance().AsyncUnmuteUser(_usr);
                    break;
                case ModerationTypes.unblock:
                    await GetInstance().AsyncUnblockUser(_usr);
                    break;
                case ModerationTypes.interactOn:
                    await GetInstance().AsyncInteractionUserOn(_usr);
                    break;

                case ModerationTypes.mute:
                    await GetInstance().AsyncMuteUser(_usr);
                    break;
                case ModerationTypes.block:
                    await GetInstance().AsyncBlockUser(_usr);
                    break;
                case ModerationTypes.interactOff:
                    await GetInstance().AsyncInteractionUserOff(_usr);
                    break;
            }
        }
    }
  
   
    public static class API_VRC
    {
        //if you deob it doesn't look like what ur seeing trust.
        public static class VRCUser
        {
            #region Moderation
            public struct ModerationData
            {
                public string? moderated { get; set; }
                public string? type { get; set; }
            }
            public enum ModerationTypes { unmute = 0, unblock = 1, interactOn = 2, mute = 3, block = 4, interactOff = 5,  }
            #endregion
        }
        public static class VRCAvatar
        {
            public enum Action { Select = 0, Delete = 1 }
        }
    }
}


