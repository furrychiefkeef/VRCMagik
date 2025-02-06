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
    static string? AuthCookie;
    static Vertical Client = new Vertical("https://api.vrchat.cloud/api/1/");
    public VRCServer(string authCookie) => AuthCookie = authCookie;
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
    public async Task AsyncBlockUser(string _usr)
    {
        ModerationData mdata = new() {moderated = _usr, type = "block"};

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(JsonDATA);
        HttpContent content = new StringContent(data,Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }
    public async Task AsyncUnblockUser(string _usr, string type)
    {
        ModerationData mdata = new() {moderated = _usr,type = "unblock"};

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }
    public async Task AsyncMuteUser(string _usr, string type)
    {
        ModerationData mdata = new() {moderated = _usr, type = "mute"};

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }
    public async Task AsyncUnmuteUser(string _usr, string type)
    {
        ModerationData mdata = new() {moderated = _usr, type = "unmute"};

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }
    public async Task AsyncInteractionUser(string _usr, bool state)
    {
        ModerationData mdata = new() {moderated = _usr};

        switch (state)
        {
            case true:
                mdata.type = "interactOn";
                break;
            case false:
                mdata.type = "interactOff";
                break;  
        }
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


    public class Advance
    {
        public async Task PlayerModeration(string _usr, API_VRC.VRCUser.ModerationTypes types)
        {
            if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");
            switch (types)
            {       
                case ModerationTypes.mute:                
                    break;
                case ModerationTypes.unmute:
                    break;
                case ModerationTypes.block:
                    break;
                case ModerationTypes.unblock:
                    break;
                case ModerationTypes.interactOn:
                    break;
                case ModerationTypes.interactOff:
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


