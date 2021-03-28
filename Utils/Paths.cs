using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MagickUtils.Utils
{
    class Paths
    {
        public static string GetExe()
        {
            return System.Reflection.Assembly.GetEntryAssembly().GetName().CodeBase.Replace("file:///", "");
        }

        public static string GetExeDir()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string GetDataPath()
        {
            string path = Path.Combine(GetExeDir(), "MagickUtilsData");
            Directory.CreateDirectory(path);
            return path;
        }

        public static string GetLogPath()
        {
            string path = Path.Combine(GetDataPath(), "logs");
            Directory.CreateDirectory(path);
            return path;
        }
    }
}
