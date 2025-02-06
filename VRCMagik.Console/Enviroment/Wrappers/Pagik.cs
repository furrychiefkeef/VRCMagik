using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pastel;

namespace VRCMagik.Console.Enviroment.Wrappers;
internal class Pagik
{
    public class Console
    {
        readonly string? AppName;
        public Console(string name)  => AppName = name;
        public static void Base(string tag, string name, string txt) 
        {
            System.Console.Write($"[{tag}] {name}::{txt}");
        }
        public void Log(string msg) 
        {
            Base("LOG", AppName.Pastel("#ff0058"), msg);     
           
        }
        public void Info(string msg) 
        {
            Base("i", AppName.Pastel("#ff0058"), msg);
        }
        
        public void Warning(string msg)
        {
            Base("!", AppName.Pastel("#ff0058"), msg);
        }
        public void Error(string msg)
        {
            Base("E", AppName.Pastel("#ff0058"), msg);
        }
        public void Critical(string msg) 
        {
            Base("X", AppName.Pastel("#ff0058"), msg);
        }
    }
     
}
public static class ConsoleExtensions
{
   public static string AddSlashN(this string target) => target + "\n";
  /* public static string Seperator(this string target)
    {
        string separator = "=";
        if (target.Length != 0)
        {
            var a = new string(separator[0], target.Length);
            return target + "\n" + a;
        }
        return string.Empty;
    }*/
}
class CColors 
{
    public static string Red = "#ff0000";
    public static string Blue = "#ff0000";
    public static string Green = "#ff0000";
    public static string Yellow = "#ff0000";
    public static string Pink = "#ff0000";
}