using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace VRCMagik.GUI
{
    internal static class Seek
    {
        static string Tag = "skirt";
        static List<string> strings = new();
        public static void Log(this TextBlock o, string txt)
        {
            o.Text += $"<{DateTime.UtcNow.ToString("hh:mm:ss:fff")}> " +
                $"<{Tag}>::{txt}";
        }
        public static void Error(this TextBlock o, string txt)
        {
            o.Text += $"<{DateTime.UtcNow.ToString("hh:mm:ss:fff")}> " +
                $"<{Tag}>::{txt}";
        }
        public static void Warning(this TextBlock o, string txt)
        {
            o.Text += $"<{DateTime.UtcNow.ToString("hh:mm:ss:fff")}> " +
                $"<{Tag}>::{txt}";
        }
    }
}
