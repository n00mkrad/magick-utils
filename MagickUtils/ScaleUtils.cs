using System;
using System.IO;
using ImageMagick;
using MagickUtils.Utils;
using System.Threading.Tasks;

namespace MagickUtils
{
    using SM = ScaleUtils.ScaleMode;
    using FT = FilterType;

    class ScaleUtils
    {
        static long bytesPre = 0;

        public static async void ResampleDirRand (float sMin, float sMax, int downFilterMode, string downFilterName, int upFilterMode, string upFilterName)
        {
            int counter = 0;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();

            Task forEach = Task.Run(async () => Parallel.ForEach(files, await MtUtils.GetParallelOpts(), async file => {
                await RandomResample(file.FullName, sMin, sMax, downFilterMode, downFilterName, upFilterMode, upFilterName);
                Program.ShowProgressIncrement("", ref counter, files.Length);
            }));

            while (!forEach.IsCompleted) await Task.Delay(1);
            Program.PostProcessing(files.Length, true);
        }

        public static async Task ScaleDir (float sMin, float sMax, int filterMode, string filterName)
        {
            int counter = 0;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();

            Task forEach = Task.Run(async () => Parallel.ForEach(files, await MtUtils.GetParallelOpts(), async file => {
                await Scale(file.FullName, sMin, sMax, filterMode, filterName);
                Program.ShowProgressIncrement("", ref counter, files.Length);
            }));

            while (!forEach.IsCompleted) await Task.Delay(1);
            Program.PostProcessing(files.Length, true);
        }


        public enum ScaleMode { Percentage, Height, Width, LongerSide, ShorterSide }
        public static ScaleMode currMode;
        public static bool onlyDownscale = true;
        public static bool appendFiltername;
        public static bool dontOverwrite;

        public static async Task RandomResample (string path, float minScale, float maxScale, int downFilterMode, string downFilterName, int upFilterMode, string upFilterName)
        {
            long bytesSrc = new FileInfo(path).Length;
            MagickImage img = IOUtils.ReadImage(path);
            FT filter = GetFilter(downFilterMode, downFilterName);
            int srcWidth = img.Width;
            int srcHeight = img.Height;
            Random rand = new Random();
            float targetScale = (float)rand.NextDouble(minScale, maxScale + 1);
            img.FilterType = filter;
            img.Resize(new Percentage(targetScale));
            MagickGeometry upscaleGeom = new MagickGeometry(srcWidth + "x" + srcHeight + "!");
            img.FilterType = GetFilter(upFilterMode, upFilterName);
            img.Resize(upscaleGeom);
            PreProcessing(path);
            string outPath = await Write(img, filter);
            ConvertUtils.PostProcessing(path, outPath, bytesSrc, false, $"Scaled to {targetScale.ToString("0.00")}% with {filter}");
        }

        static FT GetRandomFilter (bool onlyBasicFilters = false)
        {
            FT[] filtersAll = new FT[] { FT.Lanczos, FT.Catrom, FT.Quadratic, FT.Spline, FT.Box, FT.Gaussian, FT.Mitchell, FT.Triangle };
            FT[] filtersBasic = new FT[] { FT.Catrom, FT.Triangle, FT.Mitchell };
            Random rand = new Random();
            if(onlyBasicFilters) return filtersBasic[rand.Next(filtersBasic.Length)];
            else return filtersAll[rand.Next(filtersAll.Length)];
        }

        public static async Task Scale (string path, float minScale, float maxScale, int randFilterMode, string filterName)
        {
            long bytesSrc = new FileInfo(path).Length;
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
                MagickGeometry geom = new MagickGeometry("x" + targetScale);
                img.Resize(geom);
            }
            if(currMode == SM.Width || (currMode == SM.LongerSide && widthLonger) || (currMode == SM.ShorterSide && heightLonger))
            {
                if(onlyDownscale && (img.Width <= targetScale))
                    return;
                MagickGeometry geom = new MagickGeometry(targetScale + "x");
                img.Resize(geom);
            }
            if(currMode == SM.Percentage)
            {
                MagickGeometry geom = new MagickGeometry(Math.Round(img.Width * targetScale / 100f) + "x" + Math.Round(img.Height * targetScale));
                img.Resize(geom);
            }

            string outPath = await Write(img, filter);
            ConvertUtils.PostProcessing(path, outPath, bytesSrc, false, $"Scaled to {targetScale}% with {filter}");
        }

        static async Task<string> Write (MagickImage img, FT filter)    // Returns output path
        {
            img.Quality = await Program.GetFormatQuality(img);
            if (!appendFiltername)
            {
                if(!dontOverwrite)
                {
                    img.Write(img.FileName);
                    return img.FileName;
                }
                else
                {
                    string pathNoExtension = Path.ChangeExtension(img.FileName, null);
                    string ext = Path.GetExtension(img.FileName);
                    string outPath = pathNoExtension + "-Scaled" + ext;
                    IOUtils.SaveImage(img, outPath);
                    return outPath;
                }
            }
            else
            {
                string oldPath = img.FileName;
                string pathNoExtension = Path.ChangeExtension(img.FileName, null);
                string ext = Path.GetExtension(img.FileName);
                string outPath = pathNoExtension + "-Scaled-" + filter.ToString().Replace("Catrom", "Bicubic") + ext;
                IOUtils.SaveImage(img, outPath);
                if (!dontOverwrite) File.Delete(oldPath);
                return outPath;
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
                Logger.Log("Failed to parse custom filter: " + e.Message);
                return FT.Mitchell;
            }
        }

        static void PreProcessing (string path, string infoSuffix = null)
        {
            bytesPre = 0;
            bytesPre = new FileInfo(path).Length;
        }

        static void PostProcessing (MagickImage img, string outPath)
        {
            img.Dispose();
            //long bytesPost = new FileInfo(outPath).Length;
            //Logger.Log("Done. Size pre: " + Format.Filesize(bytesPre) + " - Size post: " + Format.Filesize(bytesPost) + " - Ratio: " + Format.Ratio(bytesPre, bytesPost));
        }

        static void DelSource (string path)
        {
            Logger.Log("Deleting source file: " + Path.GetFileName(path) + "...\n");
            File.Delete(path);
        }
    }
}
