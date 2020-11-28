using System;
using System.Data.SqlClient;
using System.IO;
using ImageMagick;
using MagickUtils.Utils;

namespace MagickUtils
{
    using SM = ScaleUtils.ScaleMode;
    using FT = FilterType;

    class ScaleUtils
    {
        static long bytesPre = 0;

        public static async void ResampleDirRand (float sMin, float sMax, int downFilterMode, string downFilterName, int upFilterMode, string upFilterName)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Resampling Image ", counter, files.Length);
                counter++;
                RandomResample(file.FullName, sMin, sMax, downFilterMode, downFilterName, upFilterMode, upFilterName);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
        }

        public static async void ScaleDir (float sMin, float sMax, int filterMode, string filterName)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();
            bool imgSharp = Config.GetBool("imgSharpScaling");
            foreach (FileInfo file in files)
            {
                Program.ShowProgress("Scaling Image ", counter, files.Length);
                counter++;
                if(!imgSharp)
                    Scale(file.FullName, sMin, sMax, filterMode, filterName);
                else
                    ImgSharpUtils.Scale(file.FullName, sMin, sMax, filterMode);
                await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }


        public enum ScaleMode { Percentage, Height, Width, LongerSide, ShorterSide }
        public static ScaleMode currMode;
        public static bool onlyDownscale = true;
        public static bool appendFiltername;
        public static bool dontOverwrite;

        public static void RandomResample (string path, float minScale, float maxScale, int downFilterMode, string downFilterName, int upFilterMode, string upFilterName)
        {
            MagickImage img = IOUtils.ReadImage(path);
            FT filter = GetFilter(downFilterMode, downFilterName);
            int srcWidth = img.Width;
            int srcHeight = img.Height;
            Random rand = new Random();
            float targetScale = (float)rand.NextDouble(minScale, maxScale + 1);
            Program.Print("-> Scaling down to " + targetScale + "% with filter " + filter + "...");
            img.FilterType = filter;
            img.Resize(new Percentage(targetScale));
            MagickGeometry upscaleGeom = new MagickGeometry(srcWidth + "x" + srcHeight + "!");
            img.FilterType = GetFilter(upFilterMode, upFilterName);
            Program.Print("-> Scaling back up...\n");
            img.Resize(upscaleGeom);
            PreProcessing(path);
            Write(img, filter);
            PostProcessing(img, path);
        }

        static FT GetRandomFilter (bool onlyBasicFilters = false)
        {
            FT[] filtersAll = new FT[] { FT.Lanczos, FT.Catrom, FT.Quadratic, FT.Spline, FT.Box, FT.Gaussian, FT.Mitchell, FT.Triangle };
            FT[] filtersBasic = new FT[] { FT.Catrom, FT.Triangle, FT.Mitchell };
            Random rand = new Random();
            if(onlyBasicFilters) return filtersBasic[rand.Next(filtersBasic.Length)];
            else return filtersAll[rand.Next(filtersAll.Length)];
        }

        public static void Scale (string path, float minScale, float maxScale, int randFilterMode, string filterName)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            FT filter = GetFilter(randFilterMode, filterName);
            Random rand = new Random();
            float targetScale = (float)rand.NextDouble(minScale, maxScale);
            img.FilterType = filter;

            bool heightLonger = img.Height > img.Width;
            bool widthLonger = img.Width > img.Height;
            bool square = (img.Height == img.Width);

            if((square && currMode != SM.Percentage) || currMode == SM.Height || (currMode == SM.LongerSide && heightLonger) || (currMode == SM.ShorterSide && widthLonger))
            {
                if(onlyDownscale && (img.Height <= targetScale))
                    return;
                Program.Print("-> Scaling to " + targetScale + "px height with filter " + filter + "...");
                MagickGeometry geom = new MagickGeometry("x" + targetScale);
                img.Resize(geom);
            }
            if(currMode == SM.Width || (currMode == SM.LongerSide && widthLonger) || (currMode == SM.ShorterSide && heightLonger))
            {
                if(onlyDownscale && (img.Width <= targetScale))
                    return;
                Program.Print("-> Scaling to " + targetScale + "px width with filter " + filter + "...");
                MagickGeometry geom = new MagickGeometry(targetScale + "x");
                img.Resize(geom);
            }
            if(currMode == SM.Percentage)
            {
                Program.Print("-> Scaling to " + targetScale + "% with filter " + filter + "...");
                MagickGeometry geom = new MagickGeometry(Math.Round(img.Width * targetScale / 100f) + "x" + Math.Round(img.Height * targetScale));
                img.Resize(geom);
            }
            PreProcessing(path);
            Write(img, filter);
            PostProcessing(img, path);
            Program.Print("-> Done");
        }

        static void Write (MagickImage img, FT filter)
        {
            img.Quality = Program.GetDefaultQuality(img);
            if (!appendFiltername)
            {
                if(!dontOverwrite)
                {
                    img.Write(img.FileName);
                }
                else
                {
                    string pathNoExtension = Path.ChangeExtension(img.FileName, null);
                    string ext = Path.GetExtension(img.FileName);
                    img.Write(pathNoExtension + "-Scaled" + ext);
                }
            }
            else
            {
                string oldPath = img.FileName;
                string pathNoExtension = Path.ChangeExtension(img.FileName, null);
                string ext = Path.GetExtension(img.FileName);
                img.Write(pathNoExtension + "-Scaled-" + filter.ToString().Replace("Catrom", "Bicubic") + ext);
                if(!dontOverwrite) File.Delete(oldPath);
            }
        }

        static FT GetFilter (int randFilterMode, string filterName)
        {
            if (randFilterMode == 0) return FT.Mitchell;
            if(randFilterMode == 1) return FT.Lanczos;
            if(randFilterMode == 2) return FT.Catrom;
            if(randFilterMode == 3) return FT.Point;
            if(randFilterMode == 4) return GetRandomFilter(true);
            if(randFilterMode == 5) return GetRandomFilter();
            try
            {
                filterName = filterName.Replace("Bilinear", "Triangle");
                filterName = filterName.Replace("Point", "Nearest");
                filterName = filterName.Replace("Bicubic", "Catrom");
                FT parsedFilter = (FT)Enum.Parse(typeof(FT), filterName);
                return parsedFilter;
            }
            catch (Exception e)
            {
                Program.Print("Failed to parse custom filter: " + e.Message);
                return FT.Mitchell;
            }
        }

        static void PreProcessing (string path, string infoSuffix = null)
        {
            bytesPre = 0;
            bytesPre = new FileInfo(path).Length;
            Program.timer.Start();
        }

        static void PostProcessing (MagickImage img, string outPath)
        {
            Program.timer.Stop();
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
