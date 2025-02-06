using System.Text;
using System.Text.Json;

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
            lock (_lock) if (_instance == null) _instance = new VRCServer(AuthCookie);
        }
        return _instance;
    }
    /// <summary>
    /// Initializes the HTTP client for VRChat API requests.
    /// </summary
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

    #region Groups
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
    #endregion
    #region VRCUser
    #region PlayerModeration


    public async Task AsyncClearAllModerations()
    {
        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");
        await Client.Call(Vertical.Requests.DEL, $"auth/user/playermoderations");
    }
    public async Task AsyncUnmuteUser(string _usr)
    {
        ModerationDATA mdata = new() { moderated = _usr, type = "unmute" };

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.PUT, $"auth/user/unplayermoderate", content);
    }


    public async Task AsyncUnblockUser(string _usr)
    {
        ModerationDATA mdata = new() { moderated = _usr, type = "unblock" };

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.PUT, $"auth/user/unplayermoderate", content);
    }

    public async Task AsyncInteractionUserOn(string _usr)
    {
        ModerationDATA mdata = new() { moderated = _usr, type = "interactOff" };

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.PUT, $"auth/user/unplayermoderate", content);
    }

    public async Task AsyncMuteUser(string _usr)
    {
        ModerationDATA mdata = new() { moderated = _usr, type = "mute" };

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }
    public async Task AsyncBlockUser(string _usr)
    {
        ModerationDATA mdata = new() { moderated = _usr, type = "block" };

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }
    public async Task AsyncInteractionUserOff(string _usr)
    {
        ModerationDATA mdata = new() { moderated = _usr, type = "interactOff" };

        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        string data = JsonSerializer.Serialize(mdata);
        HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        await Client.Call(Vertical.Requests.POST, $"auth/user/playermoderations", content);
    }
    #endregion
    #endregion
    #region Avatar
    public async Task AsyncSelectAvatar(string avtrId)
    {
        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        await Client.Call(Vertical.Requests.PUT, $"avatars/{avtrId}/select");
    }
    public async Task AsyncDeleteAvatar(string avtrId)
    {
        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        await Client.Call(Vertical.Requests.DEL, $"avatars/{avtrId}");
    }
    public async Task AsyncUpdateAvatar(string avtrId)
    {
        if (Client is null) throw new InvalidOperationException("HttpClient is not initialized. Call Connect() first.");

        await Client.Call(Vertical.Requests.PUT, $"avatars/{avtrId}");
    }
    #endregion

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
        public static async Task Avatar(string avtr, API_VRC.VRCAvatar.Action actions)
        {
            switch (actions)
            {
                case API_VRC.VRCAvatar.Action.Select:
                    await GetInstance().AsyncSelectAvatar(avtr);
                    break;
                case API_VRC.VRCAvatar.Action.Delete:
                    await GetInstance().AsyncDeleteAvatar(avtr);
                    break;
                case API_VRC.VRCAvatar.Action.Update:
                    await GetInstance().AsyncUpdateAvatar(avtr);
                    break;
            }
        }
    }

    public static class API_VRC
    {
       
        public static class VRCUser
        {
            #region Moderation
            public struct ModerationDATA
            {
                public string? moderated { get; set; }
                public string? type { get; set; }
            }
            public enum ModerationTypes { unmute = 0, unblock = 1, interactOn = 2, mute = 3, block = 4, interactOff = 5 }
            #endregion
        }
        public class VRCAvatar
        {
            public struct AvatarDATA
            {
                public string? assetUrl;        
                public string? id;              
                public string? name;     
                public string? description;    
                public List<string>? tags;  
                public string? imageUrl;    
                public ReleaseStatus releaseStatus; 
                public int version;             
                public string? unityPackageUrl;  
                public string? unityVersion;
            }
          

            public enum Action { Select = 0, Delete = 1, Update = 3 }
            public enum ReleaseStatus { PUBLIC, PRIVATE, HIDDEN ,ALL }
        }
    }
}


