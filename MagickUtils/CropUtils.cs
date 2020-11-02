using ImageMagick;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
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
            if (img == null) return;

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
                img.BackgroundColor = new MagickColor("#" + Config.Get("backgroundColor"));
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
            if (img == null) return;
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
            img.BackgroundColor = new MagickColor("#" + Config.Get("backgroundColor"));
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
            if (img == null) return;
            PreProcessing(path);

            Random rand = new Random();
            int pix = rand.Next(pixMin, pixMax + 1);
            int w = img.Width;
            int h = img.Height;
            if(!cut)
            {
                w = img.Width + pix * 2;
                h = img.Height + pix * 2;
            }
            else
            {
                w = img.Width - pix;
                h = img.Height - pix;
            }
            img.BackgroundColor = new MagickColor("#" + Config.Get("backgroundColor"));
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
            if (img == null) return;
            PreProcessing(path);

            img.BackgroundColor = new MagickColor("#" + Config.Get("backgroundColor"));
            img.Extent(newWidth, newHeight, grav);

            img.Write(path);
            PostProcessing(img, path);
        }

        public static async void TileDir (int w, int h, bool useTileAmount, bool delSrc)
        {
            int counter = 1;
            FileInfo[] Files = IOUtils.GetFiles();
            Program.Print("Tiling " + Files.Length + " images...");
            Program.PreProcessing();
            foreach(FileInfo file in Files)
            {
                Program.ShowProgress("Tiling Image ", counter, Files.Length);
                counter++;
                await Tile(file.FullName, w, h, useTileAmount, delSrc);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static async Task Tile (string path, int tileW, int tileH, bool useTileAmount, bool delSrc)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            PreProcessing(path);

            string pathNoExt = Path.ChangeExtension(path, null);
            string ext = Path.GetExtension(path);

            if(useTileAmount)
            {
                tileW = (int)Math.Round(img.Width / (float)tileW);
                tileH = (int)Math.Round(img.Height / (float)tileH);
            }

            int i = 1;
            Program.Print("-> Creating tiles...");
            var tiles = img.CropToTiles(tileW, tileH);
            foreach(MagickImage tile in tiles)
            {
                tile.Write(pathNoExt + "-tile" + i + ext);
                Program.Print("-> Saved tile " + i + "/" + tiles.Count(), true);
                i++;
                if(i % 2 == 0) await Program.PutTaskDelay();
            }
            await Program.PutTaskDelay();
            PostProcessing(img, path);
            if(delSrc)
                DelSource(path);
        }

        public static async void MergeAllDir ()
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.Print("-> Merging " + files.Length + " images...");
            Program.PreProcessing();

            int sqrt = (int)Math.Sqrt(files.Length);

            MagickImageCollection row = new MagickImageCollection();
            MagickImageCollection rows = new MagickImageCollection();

            int currImg = 1;

            Program.Print("Adding images to atlas...");
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("", counter, files.Length);
                MagickImage currentImg = new MagickImage(file);
                Program.Print("Adding " + Path.GetFileName(currentImg.FileName) + "...", true);
                row.Add(currentImg);
                
                if(currImg >= sqrt)
                {
                    currImg = 0;    // Reset counter
                    var mergedRow =  row.AppendHorizontally();  // Append
                    rows.Add(mergedRow);
                    row = new MagickImageCollection();  // Reset row
                }

                currImg++;
                await Program.PutTaskDelay();
                counter++;

                if(counter > files.Length && currImg != 0) // Merge the remaining images if we are done, even if they don't fill a row
                {
                    var mergedRow = row.AppendHorizontally();  // Append
                    rows.Add(mergedRow);
                }
            }
            Program.Print("-> Creating output image... ");
            var result = rows.AppendVertically();
            result.BackgroundColor = new MagickColor("#" + Config.Get("backgroundColor"));
            result.Format = MagickFormat.Png;
            string outpath = Program.currentDir + "-merged.png";
            result.Write(outpath);
            Program.Print("-> Written merged image to " + outpath);
            Program.PostProcessing();
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
