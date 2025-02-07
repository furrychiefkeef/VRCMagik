using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace VRCMagik.Console;

internal  static class CommandHandler
{         
    public static Dictionary<string,string> commands = new();
    static string? handle;
    public static async Task<string?> command(this string target, Func<string, Task> e) 
    {
        if (string.IsNullOrWhiteSpace(target))
        {
            Entry.console.Error("target cannot be null or empty.");
            return null;
        }

        foreach (var item in commands)
        {
            if (target.StartsWith(item.Key))
            {
                string[] cmdParts = target.Split(' ');
                handle = cmdParts[1];

                if (cmdParts.Length > 1)
                {
                    
                    await e.Invoke(handle);
                    return handle;
                }
            }
        }

        Entry.console.Error("Unknown command.");
        return null;
    }

}
