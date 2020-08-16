using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagickUtils
{
    class Config
    {
        static string filename = "magickutils.ini";
        public static bool fileOperationsNoExtFilter = true;
        public static string backgroundColor = "000000FF";

        public static void WriteConfig (bool showMessage = true)
        {
            string cfgString = "";
            cfgString += "fileOperationsNoExtFilter" + "=" + fileOperationsNoExtFilter.ToString() + "\n";
            cfgString += "backgroundColor" + "=" + backgroundColor + "\n";
            File.WriteAllText(Path.Combine(IOUtils.GetAppDataDir(), filename), cfgString);
            if(showMessage)
                MessageBox.Show("Config saved.", "Message");
        }

        public static void ReadConfig ()
        {
            string cfgPath = Path.Combine(IOUtils.GetAppDataDir(), filename);
            if(!File.Exists(cfgPath))
                return;
            string cfgString = File.ReadAllText(cfgPath);
            fileOperationsNoExtFilter = GetBool("fileOperationsNoExtFilter", cfgString);

            string bgColorStr = GetString("backgroundColor", cfgString);
            if(!string.IsNullOrWhiteSpace(bgColorStr))
                backgroundColor = bgColorStr;
        }

        public static bool GetBool (string boolname, string cfgContent)
        {
            string[] lines = cfgContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach(string line in lines)
            {
                if (line.Contains(boolname))
                {
                    string boolValString = line.Trim().Replace(boolname, "").Replace("=", "");
                    return boolValString.ToLower() == "true";
                }
            }
            return false;
        }

        public static string GetString (string stringname, string cfgContent)
        {
            string[] lines = cfgContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach(string line in lines)
            {
                if(line.Contains(stringname))
                    return line.Trim().Replace(stringname, "").Replace("=", "");
            }
            return "";
        }
    }
}
