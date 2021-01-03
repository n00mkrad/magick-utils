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


        public enum ImageFormat { JPG, PNG, DDS, TGA, WEBP, BMP, AVIF, J2K, FLIF, HEIF, JXL }

        [STAThread]
        static void Main (string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new MainForm();
            mainForm.FormClosing += new FormClosingEventHandler(OnFormClose);
            ResourceLimits.Memory = (ulong)Math.Round(ResourceLimits.Memory * 1.5f);

            if(args.Length > 0 && args[0] != null && File.Exists(args[0]))
            {
                PreviewImage(args[0], true);
            }
            else
            {
                Application.Run(mainForm);
            }
        }

        public static void PreviewImage(string imgPath, bool run = false)
        {
            MagickImage tempImg = IOUtils.ReadImage(imgPath);
            if (tempImg == null) return;
            tempImg.Format = MagickFormat.Png;
            string tempImgPath = Path.Combine(IOUtils.GetAppDataDir(), "previewImg.png");
            tempImg.Write(tempImgPath);
            tempImg.Dispose();
            previewImgPath = tempImgPath;
            if (run)
            {
                Application.Run(new ImagePreviewPopup());
            }
            else
            {
                new ImagePreviewPopup().Show();
            }
        }

        private static void OnFormClose (Object sender, FormClosingEventArgs e)
        {
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

        public static Stopwatch timer = new Stopwatch();
        static long dirSizePre;
        static long dirSizeAfter;

        public static void ShowProgress (string text, int current, int amount)
        {
            if(text.Trim().Length > 1)
                Print("\n" + text + current + "/" + amount);
            int targetValue = (int)Math.Round((float)current / amount * 100);
            if(targetValue > 100) targetValue = 100;
            if(targetValue < 0) targetValue = 0;
            progBar.Value = targetValue;
        }

        public static void PreProcessing (bool startStopwatch = false, bool showSize = true)
        {
            dirSizePre = 0;
            dirSizePre = IOUtils.GetDirSize(new DirectoryInfo(currentDir));
            if(showSize)
                Print("\nFolder size before processing: " + FormatUtils.Bytes(dirSizePre) + "\n");
            timer.Reset();
            if(startStopwatch) timer.Start();
        }

        public static void PostProcessing (bool showStopwatch = false, bool showSize = true)
        {
            timer.Stop();
            dirSizeAfter = 0;
            dirSizeAfter = IOUtils.GetDirSize(new DirectoryInfo(currentDir));
            if(showStopwatch)
                Print("Processing time: " + FormatUtils.TimeSw(timer));
            if(showSize)
            {
                Print("\nFolder size after processing: " + FormatUtils.Bytes(dirSizeAfter) + " from " + FormatUtils.Bytes(dirSizePre));
                Print("Size ratio: " + FormatUtils.Ratio(dirSizePre, dirSizeAfter) + " of original size");
            }
            progBar.Value = 0;
            Print("Done.");
        }

        public static async Task PutTaskDelay ()
        {
            await Task.Delay(1);
        }

        public static bool IsTrue (string input)
        {
            string inStr = input.Trim();
            if(inStr == "y" || inStr == "Y") return true;
            return false;
        }

        public static void Print(string s, bool replaceLastLine = false)
        {
            Console.WriteLine(s);
            if(replaceLastLine)
                logTbox.Text = logTbox.Text.Remove(logTbox.Text.LastIndexOf(Environment.NewLine));
            if (logTbox == null)
                return;
            s = s.Replace("\n", Environment.NewLine);
            logTbox.AppendText(Environment.NewLine + s);
        }

        public static bool IsPathValid (string path, bool showError = true)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(path);
            }
            catch
            {
                if(showError)
                    MessageBox.Show("Invalid path!", "Error");
                return false;
            }
            return true;
        }

        public static int GetDefaultQuality (MagickImage img)
        {
            if(img.Format == MagickFormat.Jpg || img.Format == MagickFormat.Jpeg)
                return 95;
            if(img.Format == MagickFormat.Png)
                return Config.GetInt("pngQ");
            if(img.Format == MagickFormat.WebP)
                return 93;
            return 99;
        }
    }
}
