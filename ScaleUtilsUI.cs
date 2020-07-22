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


        public static async void ResampleDirRand (string path, string ext, int sMin, int sMax, int randFilterMode, bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            foreach(FileInfo file in files)
            {
                Program.Print("\nResampling Image " + counter + "/" + files.Length);
                counter++;
                ScaleUtils.RandomResample(file.FullName, sMin, sMax, randFilterMode);
                if(counter % 5 == 0) await Program.PutTaskDelay();
            }
        }

        public static async void ScaleDir (string path, string ext, int sMin, int sMax, int filterMode, bool useHeight, bool recurs)
        {
            int counter = 1;
            DirectoryInfo d = new DirectoryInfo(Program.currentDir);
            FileInfo[] Files = IOUtils.GetFiles();
            Program.PreProcessing();
            foreach(FileInfo file in Files)
            {
                Program.ShowProgress("Scaling image ", counter, Files.Length);
                counter++;
                ScaleUtils.Scale(file.FullName, sMin, sMax, filterMode, useHeight);
                if(counter % 5 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }
    }
}
