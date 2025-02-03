using VRCMagik.Console;
using VRCMagik.Console.Enviroment.Wrappers;

class Entry
{
    static async Task Main()
    {
        Pagik.Console console = new("VRCMAGIK");
        VRCServer? server  = new VRCServer(Environment.GetEnvironmentVariable("VRMAGIK::VRC_AUTH", EnvironmentVariableTarget.User));
        VRCServer.Connect();
        console.Log("Enter a Command: ".AddSlashN());
        string? cmd = Console.ReadLine();
        if (cmd != null && cmd.StartsWith("del"))
        {
            // Split the command to get the group ID
            string[] cmdParts = cmd.Split(' ');

            // Ensure there is an ID provided after "$del"
            if (cmdParts.Length > 1)
            {
                string groupId = cmdParts[1];
                await server.DeleteGroupAsync(groupId);
            }
            else Console.WriteLine("Please provide a group ID.");
        }
        else
        {
            Console.WriteLine("Unknown command.");
        }
    }
}