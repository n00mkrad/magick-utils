using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickUtils
{
    class AdjustUtilsUI
    {
        public static void AutoLevel (string path, string ext, bool recursive)
        {
            /*
            string mode = "1";
            Console.Write("[Default: 1] Select mode:\n1 Auto-Gamma\n2 Auto-Level\n3 Auto-Threshold\n");
            string modeInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(modeInput.Trim())) mode = modeInput;

            string recursive = "n";
            Console.Write("[Default: n] Recursive (include all subfolders)? (y/n): ");
            string recursiveInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(recursiveInput.Trim())) recursive = recursiveInput;

            
            FileInfo[] files = null;
            DirectoryInfo d = new DirectoryInfo(Program.currentDir);

            if(Program.IsTrue(recursive))
                files = d.GetFiles("*." + ext, SearchOption.AllDirectories);
            else
                files = d.GetFiles("*." + ext, SearchOption.TopDirectoryOnly);
                */


            FileInfo[] files = IOUtils.GetFiles(path, ext, recursive);
            int counter = 1;
            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Adjusting image ", counter, files.Length);
                AdjustUtils.AutoLevel(file.FullName);
                counter++;
            }
            Program.PostProcessing();
        }
    }
}
