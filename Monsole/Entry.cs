using VRCMagik.Console;

class Entry
{
    static async Task Main()
    {
        VRCServer? server  = new VRCServer("auth=authcookie_94b9c955-b342-4a3c-b825-d7648981765b");
        VRCServer.Connect();
        Console.Write("Enter a Command: ");
        string? cmd = Console.ReadLine();
        if (cmd != null && cmd.StartsWith("$del"))
        {
            // Split the command to get the group ID
            string[] cmdParts = cmd.Split(' ');

            // Ensure there is an ID provided after "$del"
            if (cmdParts.Length > 1)
            {
                string groupId = cmdParts[1];
                await server.DeleteGroupAsync(groupId);
            }
            else
            {
                Console.WriteLine("Please provide a group ID.");
            }
        }
        else
        {
            Console.WriteLine("Unknown command.");
        }
    }
}