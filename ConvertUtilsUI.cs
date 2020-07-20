using System;
using System.IO;
using System.Diagnostics;

namespace MagickUtils
{
    class ConvertUtilsUI
    {
        public static void ConvertChooser (string ext)
        {
            Console.Write("\nAvailable commands:\n");
            Console.Write("1 Convert to JPEG\n2 Convert to PNG\n3 Convert to DDS\n4 Convert to TGA");
            Console.Write("\n\n");

            Console.Write("Enter your command number: ");
            string cmd = Console.ReadLine();

            /*
            if(int.Parse(cmd) == 1)
                ConvertDirToJpeg(ext);
            if(int.Parse(cmd) == 2)
                ConvertDirToPng(ext);
            if(int.Parse(cmd) == 3)
                ConvertDirToDds(ext);
            if(int.Parse(cmd) == 4)
                ConvertDirToTga(ext);
                */
        }

        public static void ConvertDirToJpegCmd (string ext)
        {
            string qMin = "95";
            Console.Write("[Default: 95] Minimum JPEG Quality: ");
            string qMinInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(qMinInput.Trim())) qMin = qMinInput;

            string qMax = qMin;
            Console.Write("[Leave blank to use fixed quality] Maximum JPEG Quality: ");
            string qMaxInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(qMaxInput.Trim())) qMax = qMaxInput;

            string recursive = "n";
            Console.Write("[Default: n] Recursive (include all subfolders)? (y/n): ");
            string recursiveInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(recursiveInput.Trim())) recursive = recursiveInput;

            string delSrc = "n";
            Console.Write("[Default: n] Delete source file afterwards? (y/n): ");
            string delSrcInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(delSrcInput.Trim())) delSrc = delSrcInput;

            ConvertDirToJpeg(Program.currentDir, ext, int.Parse(qMin), int.Parse(qMax), Program.IsTrue(recursive), Program.IsTrue(delSrc));
        }

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

        public static void ConvertDirToPng (string path, string ext, int q, bool recursive, bool delSrc)
        {
            /*
            string pngCompressionLvl = "0";
            Console.Write("[Default: 0] Set PNG compression (0-100): ");
            string newPngCompressionLvl = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(newPngCompressionLvl.Trim())) pngCompressionLvl = newPngCompressionLvl;

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
                ConvertUtils.ConvertToPng(file.FullName, q, delSrc);
                counter++;
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
