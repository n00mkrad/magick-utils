using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MagickUtils
{
    class ScaleUtilsUI
    {
        public static void ScaleChooser (string ext)
        {
            /*
            Console.Write("Available commands:\n");
            Console.Write("1 Scale Images\n2 Randomly Resample Images");
            Console.Write("\n\n");

            Console.Write("Enter your command number: ");
            string cmd = Console.ReadLine();

            if(int.Parse(cmd) == 1)
                ScaleDir(ext);
            if(int.Parse(cmd) == 2)
                ResampleDirRand(ext);
                */
        }


        public static void ResampleDirRand (string path, string ext, int sMin, int sMax, int randFilterMode, bool recursive, bool delSrc)
        {

            /*
            Console.Write("Minimum Scale: ");
            string sMin = Console.ReadLine();

            Console.Write("Maximum Scale: ");
            string sMax = Console.ReadLine();

            string randFilter = "n";
            Console.Write("Which filter mode to use for scaling?\n1 Default (Mitchell)\n2 Bicubic\n3 Nearest (Point)\n4 Random (Box/Bicubic/Mitchell)\n5 Random (All)\n");
            string randFilterInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(randFilterInput.Trim())) randFilter = randFilterInput;
            */

            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles(path, ext, recursive);

            foreach(FileInfo file in files)
            {
                Program.Print("\nResampling Image " + counter + "/" + files.Length);
                counter++;
                ScaleUtils.RandomResample(file.FullName, sMin, sMax, randFilterMode);
            }
        }

        public static void ScaleDir (string path, string ext, int sMin, int sMax, int filterMode, bool useHeight, bool recurs)
        {
            /*
            string useHeight = "n";
            Console.Write("[Default: n] Use target height instead of percentage? (y/n): ");
            string useHeightInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(useHeightInput.Trim())) useHeight = useHeightInput;

            Console.Write("Minimum Scale/Height: ");
            string sMin = Console.ReadLine();

            Console.Write("Maximum Scale/Height: ");
            string sMax = Console.ReadLine();

            string randFilter = "n";
            Console.Write("Which filter mode to use for scaling?\n1 Default (Mitchell)\n2 Bicubic\n3 Nearest (Point)\n4 Random (Box/Bicubic/Mitchell)\n5 Random (All)\n");
            string randFilterInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(randFilterInput.Trim())) randFilter = randFilterInput;
            */

            int counter = 1;
            DirectoryInfo d = new DirectoryInfo(Program.currentDir);
            FileInfo[] Files = IOUtils.GetFiles(path, ext, recurs);
            Program.PreProcessing();
            foreach(FileInfo file in Files)
            {
                Program.ShowProgress("Scaling image ", counter, Files.Length);
                counter++;
                ScaleUtils.Scale(file.FullName, sMin, sMax, filterMode, useHeight);
            }
            Program.PostProcessing();
        }
    }
}
