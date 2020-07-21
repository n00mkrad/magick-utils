using System;
using System.IO;
using System.Diagnostics;

namespace MagickUtils
{
    class ConvertUtilsUI
    {
        public static void ConvertDirToJpeg (string path, string ext, int qMin, int qMax, bool recursive, bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = Program.GetFiles(path, ext, recursive);

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                ConvertUtils.ConvertToJpegRandomQuality(file.FullName, qMin, qMax, delSrc);
                counter++;
                Program.mainForm.Show();
            }
            Program.PostProcessing();
        }

        public async static void ConvertDirToPng (string path, string ext, int q, bool recursive, bool delSrc)
        {
            int counter = 1;
            int iterationsUntilWait = 150;
            FileInfo[] files = Program.GetFiles(path, ext, recursive);

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                ConvertUtils.ConvertToPng(file.FullName, q, delSrc);
                counter++;

                if(counter % 10 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(true);
        }


        public static void ConvertDirToDds (string path, string ext, bool recursive, bool delSrc)
        {
            /*
            string recursive = "n";
            Console.Write("[Default: n] Recursive (include all subfolders)? (y/n): ");
            string recursiveInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(recursiveInput.Trim())) recursive = recursiveInput;

            string delSrc = "n";
            Console.Write("[Default: n] Delete source file afterwards? (y/n): ");
            string delSrcInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(delSrcInput.Trim())) delSrc = delSrcInput;
            */

            int counter = 1;
            FileInfo[] files = Program.GetFiles(path, ext, recursive);

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                counter++;
                ConvertUtils.ConvertToDds(file.FullName, delSrc);
            }
            Program.PostProcessing();
        }

        public static void ConvertDirToDdsCrunch (bool recursive, int qMin, int qMax, bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = Program.GetFiles(Program.currentDir, Program.currentExt, recursive);

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Crunching Image ", counter, files.Length);
                counter++;
                CrunchInterface.CrunchImage(file.FullName, qMin, qMax, delSrc);
            }
            Program.PostProcessing();
        }

        public static void ConvertDirToTga (string path, string ext, bool recursive, bool delSrc)
        {
            /*
            string recursive = "n";
            Console.Write("[Default: n] Recursive (include all subfolders)? (y/n): ");
            string recursiveInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(recursiveInput.Trim())) recursive = recursiveInput;

            string delSrc = "n";
            Console.Write("[Default: n] Delete source file afterwards? (y/n): ");
            string delSrcInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(delSrcInput.Trim())) delSrc = delSrcInput;
            */

            int counter = 1;
            FileInfo[] files = Program.GetFiles(path, ext, recursive);

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                counter++;
                ConvertUtils.ConvertToTga(file.FullName, delSrc);
            }
            Program.PostProcessing();
        }
    }
}
