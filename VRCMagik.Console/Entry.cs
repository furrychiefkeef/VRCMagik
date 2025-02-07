using System.Reflection.Metadata;

using VRCMagik.Console;
using VRCMagik.Console.Enviroment.Wrappers;
using VRCMagik.NET.Wrappers;
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
        CommandHandler.commands.Add("sel", "avtr");
        await cmd.command(async x =>
        {
            await server.AsyncSelectAvatar(x);
            return;
        });
    }
}

 