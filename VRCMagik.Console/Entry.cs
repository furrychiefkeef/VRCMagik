using System.Reflection.Metadata;

using VRCMagik.Console.Enviroment.Wrappers;
using VRCMagik.Wrappers;
internal class Entry
{
    public static Pagik.Console console = new("VRCMAGIK");

    static async Task Main()
    {
        VRCServer? server = new VRCServer(Environment.GetEnvironmentVariable("VRMAGIK::VRC_AUTH", EnvironmentVariableTarget.User));
        VRCServer.Connect();

        console.Log("Enter a Command: ");
        string? cmd = Console.ReadLine();
        if (cmd != null && cmd.StartsWith("sel"))
        {
            string[] cmdParts = cmd.Split(' ');
            string handle = cmdParts[1];

            if (cmdParts.Length > 1)
            {
                if (handle.StartsWith("avtr"))
                {
                    console.Info("avatar ID!");
                    await server.AsyncSelectAvatar(handle);
                }
            }
        }
        if (cmd != null && cmd.StartsWith("del"))
        {
            string[] cmdParts = cmd.Split(' ');
            string handle = cmdParts[1];

            if (cmdParts.Length > 1)
            {
                if (handle.StartsWith("avtr"))
                {
                    console.Info("avatar ID!");
                    await server.AsyncSelectAvatar(handle);
                }

            }
        }
        if (cmd != null && cmd.StartsWith("usr"))
        {
            string[] cmdParts = cmd.Split(' ');
            string handle = cmdParts[1];

            if (cmdParts.Length > 1)
            {
                if (handle.StartsWith(value: "usr_"))
                {
                    console.Info("USER ID");
                    switch (handle)
                    {
                        default:
                            break;
                    }
                    await VRCServer.Advance.PlayerModeration(handle,VRCServer.API_VRC.VRCUser.ModerationTypes.mute);
                }

            }
        }
        else
        {
            Console.WriteLine("Unknown command.");
        }
    }
}

    /* static async Task CMDs()
    {
       console.Log("Enter a Command: ");
        string? cmd = Console.ReadLine();
        if (cmd != null && cmd.StartsWith("del"))
        {
            // Split the command to get the group ID
            string[] cmdParts = cmd.Split(' ');
            string handle = cmdParts[1];
            // Ensure there is an ID provided after "$del"
            if (cmdParts.Length > 1)
            {
                if (handle.StartsWith("grp"))
                {
                    console.Info("GROUP ID!");
                    await server.DeleteGroupAsync(handle);
                }

            }
            else Console.WriteLine("Please provide a group ID.");
        }
       
    }
}*/