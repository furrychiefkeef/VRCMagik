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
        VRCUserProperties.ModerationAction JsonDATA = new() {moderated = _usr, type = "block"};

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(JsonDATA);
        HttpContent content = new StringContent(data,Encoding.UTF8);
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content, "application/json");
    }
    public async Task AsyncUnblockUser(string _usr, string type)
    {
        VRCUserProperties.ModerationAction JsonDATA = new() {moderated = _usr,type = "unblock"};

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(JsonDATA);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }
    public async Task AsyncMuteUser(string _usr, string type)
    {
        VRCUserProperties.ModerationAction JsonDATA = new() {moderated = _usr, type = "mute"};

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(JsonDATA);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }
    public async Task AsyncUnmuteUser(string _usr, string type)
    {
        VRCUserProperties.ModerationAction JsonDATA = new() {moderated = _usr, type = "unmute"};

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(JsonDATA);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }
    public async Task AsyncInteractionUser(string _usr, bool state)
    {
        VRCUserProperties.ModerationAction JsonDATA = new() {moderated = _usr};

        switch (state)
        {
            case true:
                JsonDATA.type = "interactOn";
                break;
            case false:
                JsonDATA.type = "interactOff";
                break;  
        }
        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(JsonDATA);
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
        public async Task PlayerModeration(string _usr, VRCUserProperties.ModerationTypes types)
        {
            if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");
            switch (types)
            {       
                case VRCUserProperties.ModerationTypes.mute:                
                    break;
                case VRCUserProperties.ModerationTypes.unmute:
                    break;
                case VRCUserProperties.ModerationTypes.block:
                    break;
                case VRCUserProperties.ModerationTypes.unblock:
                    break;
                case VRCUserProperties.ModerationTypes.interactOn:
                    break;
                case VRCUserProperties.ModerationTypes.interactOff:
                    break;
                default:
                    break;
            }
        }
    }
  
    public static class VRCUserProperties
    {
        public struct ModerationAction
        {
            public string? moderated { get; set; }
            public string? type { get; set; }
        }

        public enum ModerationTypes
        {
            None = 0,

            mute = 1,
            unmute = 2,
            block = 3,
            unblock = 4,
           
            interactOn = 5,
            interactOff = 6,
        }
    }
    public static class VRCAProperties
    {
        public enum Action
        {
            Select = 0,
            Delete = 1,
        }
    }
}

