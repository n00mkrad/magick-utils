using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagickUtils
{
    class CropUtils
    {
        static long bytesPre = 0;

        public static async void CropDivisibleDir (int divisibleBy, Gravity grav, bool expand)
        {
            int counter = 1;
            FileInfo[] Files = IOUtils.GetFiles();
            Program.Print("Cropping " + Files.Length + " images...");
            Program.PreProcessing();
            foreach (FileInfo file in Files)
            {
                Program.ShowProgress("Cropping Image ", counter, Files.Length);
                counter++;
                CropDivisible(file.FullName, divisibleBy, grav, expand);
                if (counter % 3 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static void CropDivisible (string path, int divisibleBy, Gravity grav, bool expand)
        {
            MagickImage img = IOUtils.ReadImage(path);

            int divisbleWidth = img.Width;
            int divisibleHeight = img.Height;

            if(!expand) // Crop
            {
                while(divisbleWidth % divisibleBy != 0) divisbleWidth--;
                while(divisibleHeight % divisibleBy != 0) divisibleHeight--;
            }
            else // Expand
            {
                while(divisbleWidth % divisibleBy != 0) divisbleWidth++;
                while(divisibleHeight % divisibleBy != 0) divisibleHeight++;
                img.BackgroundColor = new MagickColor("#" + Config.backgroundColor);
            }

            if(divisbleWidth == img.Width && divisibleHeight == img.Height)
            {
                Program.Print("-> Skipping " + Path.GetFileName(path) + " as its resolution is already divisible by " + divisibleBy);
            }
            else
            {
                Program.Print("-> Divisible resolution: " + divisbleWidth + "x" + divisibleHeight);
                if(!expand) // Crop
                    img.Crop(divisbleWidth, divisibleHeight, grav);
                else // Expand
                    img.Extent(divisbleWidth, divisibleHeight, grav);
                img.RePage();
                img.Write(path);
            }
        }

        public static async void CropRelativeDir (int minSize, int maxSize, SizeMode sizeMode, Gravity grav)
        {
            int counter = 1;
            FileInfo[] Files = IOUtils.GetFiles();
            Program.Print("Resizing " + Files.Length + " images...");
            Program.PreProcessing();
            foreach(FileInfo file in Files)
            {
                Program.ShowProgress("Resizing Image ", counter, Files.Length);
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

            Random rand = new Random();
            int targetSize = rand.Next(minSize, maxSize + 1);
            MagickGeometry geom = null;

            bool heightLonger = img.Height > img.Width;
            bool widthLonger = img.Width > img.Height;
            bool square = (img.Height == img.Width);

            if(square || sizeMode == SizeMode.Height || (sizeMode == SizeMode.Longer && heightLonger) || (sizeMode == SizeMode.Shorter && widthLonger))
            {
                Program.Print("-> Resizing to " + targetSize + "px height...");
                int w = (int)Math.Round(img.Width * ((targetSize / (float)img.Height)));
                geom = new MagickGeometry(w + "x" + targetSize);
            }
            if(sizeMode == SizeMode.Width || (sizeMode == SizeMode.Longer && widthLonger) || (sizeMode == SizeMode.Shorter && heightLonger))
            {
                Program.Print("-> Resizing to " + targetSize + "px width...");
                int h = (int)Math.Round(img.Height * ((targetSize / (float)img.Width)));
                geom = new MagickGeometry(targetSize + "x" + h);
            }
            if(sizeMode == SizeMode.Percentage)
            {
                int w = (int)Math.Round(img.Width * targetSize / 100f);
                int h = (int)Math.Round(img.Height * targetSize / 100f);
                Program.Print("-> Resizing to " + targetSize + "% (" + w + "x" + h + ")...");
                geom = new MagickGeometry(w + "x" + h);
            }
            img.Extent(geom, grav);
            img.Write(path);
            PostProcessing(img, path);
        }

        public static async void CropPaddingDir (int pixMin, int pixMax, bool cut)
        {
            int counter = 1;
            FileInfo[] Files = IOUtils.GetFiles();
            Program.Print("Resizing " + Files.Length + " images...");
            Program.PreProcessing();
            foreach(FileInfo file in Files)
            {
                Program.ShowProgress("Resizing Image ", counter, Files.Length);
                counter++;
                CropPadding(file.FullName, pixMin, pixMax, cut);
                if(counter % 3 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static void CropPadding (string path, int pixMin, int pixMax, bool cut)
        {
            MagickImage img = IOUtils.ReadImage(path);
            PreProcessing(path);

            Random rand = new Random();
            int pix = rand.Next(pixMin, pixMax + 1);
            int w = img.Width;
            int h = img.Height;
            if(!cut)
            {
                w = img.Width + pix;
                h = img.Height + pix;
            }
            else
            {
                w = img.Width - pix;
                h = img.Height - pix;
            }
            MagickGeometry geom = new MagickGeometry(w + "x" + h + "!");
            img.Extent(geom, Gravity.Center);

            img.Write(path);
            PostProcessing(img, path);
        }

        public static async void CropAbsoluteDir (int newWidth, int newHeight, Gravity grav)
        {
            int counter = 1;
            FileInfo[] Files = IOUtils.GetFiles();
            Program.Print("Resizing " + Files.Length + " images...");
            Program.PreProcessing();
            foreach(FileInfo file in Files)
            {
                Program.ShowProgress("Resizing Image ", counter, Files.Length);
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

            img.Extent(newWidth, newHeight, grav);

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
            //Program.Print("-> Done. Size pre: " + Format.Filesize(bytesPre) + " - Size post: " + Format.Filesize(bytesPost) + " - Ratio: " + Format.Ratio(bytesPre, bytesPost));
        }

        static void DelSource (string path)
        {
            Program.Print("-> Deleting source file: " + Path.GetFileName(path) + "...\n");
            File.Delete(path);
        }
    }
}
