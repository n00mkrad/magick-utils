using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickUtils
{
    class CropUtils
    {
        static long bytesPre = 0;

        public static async void CropDivisibleDir (int divisibleBy)
        {
            int counter = 1;
            FileInfo[] Files = IOUtils.GetFiles();
            Program.Print("Cropping " + Files.Length + " images...");
            Program.PreProcessing();
            foreach (FileInfo file in Files)
            {
                Program.ShowProgress("Cropping Image ", counter, Files.Length);
                counter++;
                CropDivisible(file.FullName, divisibleBy);
                if (counter % 3 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static void CropDivisible (string path, int divisibleBy)
        {
            MagickImage img = IOUtils.ReadImage(path);

            int divisbleWidth = img.Width;
            while (divisbleWidth % divisibleBy != 0) divisbleWidth--;

            int divisibleHeight = img.Height;
            while (divisibleHeight % divisibleBy != 0) divisibleHeight--;

            if(divisbleWidth == img.Width && divisibleHeight == img.Height)
            {
                Program.Print("-> Skipping " + Path.GetFileName(path) + " as its resolution is already divisible by " + divisibleBy);
            }
            else
            {
                img.Crop(divisbleWidth, divisibleHeight, Gravity.Center);
                img.RePage();
                img.Write(path);
            }
        }

        public static async void CropRelativeDir (int minSize, int maxSize, SizeMode sizeMode, Gravity grav)
        {
            int counter = 1;
            FileInfo[] Files = IOUtils.GetFiles();
            Program.Print("Cropping " + Files.Length + " images...");
            Program.PreProcessing();
            foreach(FileInfo file in Files)
            {
                Program.ShowProgress("Cropping Image ", counter, Files.Length);
                counter++;
                CropRelative(file.FullName, minSize, maxSize, sizeMode, grav);
                if(counter % 3 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public enum SizeMode { Percentage, Height, Width, Longer, Shorter }
        public static void CropRelative (string path, int minSize, int maxSize, SizeMode sizeMode, Gravity grav)
        {
            MagickImage img = IOUtils.ReadImage(path);
            PreProcessing(path);
            string fname = Path.GetFileName(path);
            Program.Print("-> " + fname + " (" + img.Width + "x" + img.Height + ")");
            Random rand = new Random();
            int targetSize = rand.Next(minSize, maxSize + 1);

            bool heightLonger = img.Height > img.Width;
            bool widthLonger = img.Width > img.Height;
            bool square = (img.Height == img.Width);

            if(square || sizeMode == SizeMode.Height || (sizeMode == SizeMode.Longer && heightLonger) || (sizeMode == SizeMode.Shorter && widthLonger))
            {
                //if(onlyDownscale && (img.Height <= targetSize)) return;
                Program.Print("  -> Cropping to " + targetSize + "px height...");
                int w = (int)Math.Round(img.Width * ((targetSize / (float)img.Height)));
                MagickGeometry geom = new MagickGeometry(w + "x" + targetSize);
                img.Crop(geom, grav);
            }
            if(sizeMode == SizeMode.Width || (sizeMode == SizeMode.Longer && widthLonger) || (sizeMode == SizeMode.Shorter && heightLonger))
            {
                //if(onlyDownscale && (img.Width <= targetSize) return;
                Program.Print("  -> Cropping to " + targetSize + "px width...");
                int h = (int)Math.Round(img.Height * ((targetSize / (float)img.Width)));
                MagickGeometry geom = new MagickGeometry(targetSize + "x" + h);
                img.Crop(geom, grav);
            }
            if(sizeMode == SizeMode.Percentage)
            {
                Program.Print("  -> Cropping to " + targetSize + "% with filter...");
                int w = (int)Math.Round(img.Width * targetSize / 100f);
                int h = (int)Math.Round(img.Height * targetSize / 100f);
                img.Crop(h, w, grav);
            }
            img.Write(path);
            PostProcessing(img, path);
        }

        public static async void CropAbsoluteDir (int newWidth, int newHeight, Gravity grav)
        {
            int counter = 1;
            FileInfo[] Files = IOUtils.GetFiles();
            Program.Print("Cropping " + Files.Length + " images...");
            Program.PreProcessing();
            foreach(FileInfo file in Files)
            {
                Program.ShowProgress("Cropping Image ", counter, Files.Length);
                counter++;
                CropAbsolute(file.FullName, newWidth, newHeight, grav);
                if(counter % 3 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static void CropAbsolute (string path, int newWidth, int newHeight, Gravity grav)
        {
            MagickImage img = IOUtils.ReadImage(path);
            PreProcessing(path);
            string fname = Path.GetFileName(path);
            Program.Print("-> " + fname + " (" + img.Width + "x" + img.Height + ")");

            img.Crop(newWidth, newHeight, grav);

            img.Write(path);
            PostProcessing(img, path);
        }

        static void PreProcessing (string path, string infoSuffix = null)
        {
            bytesPre = 0;
            bytesPre = new FileInfo(path).Length;
            //Program.Print("-> Processing TEST " + Path.GetFileName(path) + " " + infoSuffix);
            Program.sw.Start();
        }

        static void PostProcessing (MagickImage img, string outPath)
        {
            Program.sw.Stop();
            img.Dispose();
            //long bytesPost = new FileInfo(outPath).Length;
            //Program.Print("  -> Done. Size pre: " + Format.Filesize(bytesPre) + " - Size post: " + Format.Filesize(bytesPost) + " - Ratio: " + Format.Ratio(bytesPre, bytesPost));
        }

        static void DelSource (string path)
        {
            Program.Print("  -> Deleting source file: " + Path.GetFileName(path) + "...\n");
            File.Delete(path);
        }
    }
}
