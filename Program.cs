using System;
using System.Collections.Generic;
using System.IO;
using ImageMagick;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace MagickUtils
{
    class Program
    {
        public static string currentDir;
        public static string currentExt;

        public static MainForm mainForm;
        public static TextBox logTbox;
        public static ProgressBar progBar;

        public static string previewImgPath;


        public enum ImageFormat { JPG, PNG, DDS, TGA, WEBP, J2K, FLIF }

        [STAThread]
        static void Main (string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new MainForm();
            mainForm.FormClosing += new FormClosingEventHandler(OnFormClose);
            Application.Run(mainForm);
        }

        private static void OnFormClose (Object sender, FormClosingEventArgs e)
        {
            CrunchInterface.DeleteExe();
            string tempImgPath = Path.Combine(IOUtils.GetAppDataDir(), "previewImg.png");
            if(File.Exists(tempImgPath)) File.Delete(tempImgPath);
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

            if(text.Trim().Length > 1)
                Print("\n" + text + current + "/" + amount);
            progBar.Value = (int)Math.Round((float)current / amount * 100);
        }

        public static void PreProcessing (bool startStopwatch = false, bool showSize = true)
        {
            dirSizePre = 0;
            dirSizePre = IOUtils.GetDirSize(new DirectoryInfo(currentDir));
            if(showSize)
                Print("\nFolder size before processing: " + Format.Filesize(dirSizePre) + "\n");
            sw.Reset();
            if(startStopwatch) sw.Start();
        }

        public static void PostProcessing (bool showStopwatch = false, bool showSize = true)
        {
            sw.Stop();
            dirSizeAfter = 0;
            dirSizeAfter = IOUtils.GetDirSize(new DirectoryInfo(currentDir));
            if(showStopwatch)
                Print("Processing time (no I/O or other overhead counted): " + Format.TimeSw(sw));
            if(showSize)
            {
                Print("\nFolder size after processing: " + Format.Filesize(dirSizeAfter) + " from " + Format.Filesize(dirSizePre));
                Print("Size ratio: " + Format.Ratio(dirSizePre, dirSizeAfter) + " of original size");
            }
            progBar.Value = 0;
        }

        public static async Task PutTaskDelay ()
        {
            await Task.Delay(1);
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

        public static int GetDefaultQuality (MagickImage img)
        {
            if(img.Format == MagickFormat.Jpg || img.Format == MagickFormat.Jpeg)
                return 95;
            if(img.Format == MagickFormat.Png)
                return 50;
            if(img.Format == MagickFormat.WebP)
                return 92;
            return 99;
        }
    }
}
