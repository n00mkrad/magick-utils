using System;
using System.Collections.Generic;
using System.IO;
using ImageMagick;
using System.Windows.Forms;
using System.Diagnostics;

namespace MagickUtils
{
    class Program
    {
        public static string currentDir;
        public static string currentExt;
        public static Form mainForm;
        public static TextBox logTbox;
        public static ProgressBar progBar;


        public enum ImageFormat { JPG, PNG, DDS, TGA }

        static void Main (string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new MainForm();
            Application.Run(mainForm);

            /*
            Console.Write("\nWelcome to NMKD's Magick.NET utils!\n\n");
            Console.Write("Available commands:\n");
            Console.Write("1 List Files\n2 Convert...\n3 Scale or Resample...\n4 Delete/Filter Files...\n5 Remove Alpha\n6 Auto-Adjust\n7 Group Textures & Normal Maps");
            Console.Write("\n\n");

            Console.Write("Enter your command number: ");
            string cmd = Console.ReadLine();
            Console.Write("Enter a directory: ");
            currentDir = Console.ReadLine();

            string fileExt = "*";
            Console.Write("[Default: '*'] Enter a file extension to filter (\"png\", \"jpg\", \"*\", ...): ");
            string fileExtInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(fileExtInput.Trim())) fileExt = fileExtInput;

            if(int.Parse(cmd) == 1)
                PrintFilesInDir(fileExt);
            if(int.Parse(cmd) == 2)
                ConvertUtilsUI.ConvertChooser(fileExt);
            if(int.Parse(cmd) == 3)
                ScaleUtilsUI.ScaleChooser(fileExt);
            if(int.Parse(cmd) == 4)
                OtherUtilsUI.OtherUtilChooser(fileExt);
            if(int.Parse(cmd) == 5)
                RemTransparency(fileExt);
            if(int.Parse(cmd) == 6)
                AdjustUtilsUI.AutoAdj(fileExt);
            if(int.Parse(cmd) == 7)
                OtherUtilsUI.GroupNormalsWithTex(fileExt);

            Main(null);
            */
        }

        static void PrintFilesInDir (string ext)
        {
            DirectoryInfo d = new DirectoryInfo(currentDir);
            FileInfo[] Files = d.GetFiles("*." + ext);
            int i = 1;
            foreach(FileInfo file in Files)
            {
                MagickImage img = new MagickImage(file);
                string fName = Path.GetFileName(img.FileName);
                Print("Image " + i + "/" + Files.Length + ": " + fName + " (" + img.Width + "x" + img.Height + ", " + img.Depth + " bits)");
                i++;
            }
        }

        public static Stopwatch sw = new Stopwatch();
        static long dirSizePre;
        static long dirSizeAfter;

        public static void ShowProgress (string text, int current, int amount)
        {
            Print("\n" + text + current + "/" + amount);
            progBar.Value = (int)Math.Round((float)current / amount * 100);
        }

        public static void PreProcessing ()
        {
            dirSizePre = 0;
            dirSizePre = IOUtils.GetDirSize(new DirectoryInfo(currentDir));
            Print("\nFolder size before processing: " + Format.Filesize(dirSizePre) + "\n");
            sw.Reset();
        }

        public static void PostProcessing (bool showStopwatch = false)
        {
            dirSizeAfter = 0;
            dirSizeAfter = IOUtils.GetDirSize(new DirectoryInfo(currentDir));
            Print("\nFolder size after processing: " + Format.Filesize(dirSizeAfter) + " from " + Format.Filesize(dirSizePre));
            Print("Size ratio: " + Format.Ratio(dirSizePre, dirSizeAfter) + " of original size");
            if(showStopwatch)
                Print("Processing time (no I/O or other overhead counted): " + Format.TimeSw(sw));
            progBar.Value = 0;
        }

        public static FileInfo[] GetFiles (string path, string ext, bool recursive)
        {
            if(!IsPathValid(path))
            {
                MessageBox.Show("Invalid path!", "Error");
                return new FileInfo[0];
            }
            DirectoryInfo d = new DirectoryInfo(path);
            if(recursive)
                return d.GetFiles("*." + ext, SearchOption.AllDirectories);
            else
                return d.GetFiles("*." + ext, SearchOption.TopDirectoryOnly);
        }

        public static bool IsTrue (string input)
        {
            string inStr = input.Trim();
            if(inStr == "y") return true;
            if(inStr == "Y") return true;
            return false;
        }

        public static void Print(string s)
        {
            Console.WriteLine(s);
            s = s.Replace("\n", Environment.NewLine);
            logTbox.AppendText(Environment.NewLine + s);
        }

        public static bool IsPathValid (string path)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(path);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
