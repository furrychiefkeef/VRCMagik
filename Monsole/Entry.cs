using VRCMagik.Console.Enviroment.Wrappers;
using VRCMagik.Wrappers;
internal class Entry
{
    static async Task Main()
    {
        Pagik.Console console = new("VRCMAGIK");
        VRCServer? server  = new VRCServer(Environment.GetEnvironmentVariable("VRMAGIK::VRC_AUTH", EnvironmentVariableTarget.User));
        VRCServer.Connect();

        console.Log("Enter a Command: ");
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