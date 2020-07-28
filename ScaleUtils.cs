using System;
using System.IO;
using ImageMagick;

namespace MagickUtils
{
    using SM = ScaleUtils.ScaleMode;
    using FT = ImageMagick.FilterType;

    class ScaleUtils
    {
        static long bytesPre;
        
        public enum ScaleMode { Percentage, Height, Width, LongerSide, ShorterSide }
        public static ScaleMode currMode;
        public static bool onlyDownscale = true;
        public static bool appendFiltername;
        public static bool dontOverwrite;

        public static void RandomResample (string path, int minScale, int maxScale, int randFilterMode)
        {
            MagickImage img = IOUtils.ReadImage(path);
            string fname = Path.ChangeExtension(path, null);
            Program.Print("-> " + fname + " (" + img.Width + "x" + img.Height + ")");
            FT filter = GetFilter(randFilterMode);
            int srcWidth = img.Width;
            int srcHeight = img.Height;
            Random rand = new Random();
            int targetScale = rand.Next(minScale, maxScale + 1);
            Program.Print("  -> Scaling down to " + targetScale + "% with filter " + filter + "...");
            img.FilterType = filter;
            img.Resize(new Percentage(targetScale));
            img.Write(img.FileName);
            MagickGeometry upscaleGeom = new MagickGeometry(srcWidth + "x" + srcHeight + "!");
            img.FilterType = filter;
            Program.Print("  -> Scaling back up...\n");
            img.Resize(upscaleGeom);
            PreProcessing(path);
            Write(img, filter);
            PostProcessing(img, path);
        }

        static FT GetRandomFilter (bool onlyBasicFilters = false)
        {
            FT[] filtersAll = new FT[] { FT.Lanczos, FT.Catrom, FT.Quadratic, FT.Spline, FT.Box, FT.Gaussian, FT.Mitchell, FT.Triangle };
            FT[] filtersBasic = new FT[] { FT.Catrom, FT.Box, FT.Mitchell };
            Random rand = new Random();
            if(onlyBasicFilters) return filtersBasic[rand.Next(filtersBasic.Length)];
            else return filtersAll[rand.Next(filtersAll.Length)];
        }

        public static void Scale (string path, int minScale, int maxScale, int randFilterMode)
        {
            MagickImage img = IOUtils.ReadImage(path);
            string fname = Path.ChangeExtension(path, null);
            Program.Print("-> " + fname + " (" + img.Width + "x" + img.Height + ")");
            FT filter = GetFilter(randFilterMode);
            Random rand = new Random();
            int targetScale = rand.Next(minScale, maxScale + 1);
            img.FilterType = filter;

            bool heightIsLonger = img.Height > img.Width;
            bool widthIsLonger = img.Width > img.Height;

            if(currMode == SM.Height || (currMode == SM.LongerSide && heightIsLonger) || (currMode == SM.ShorterSide && widthIsLonger))
            {
                if(onlyDownscale && (img.Height <= targetScale))
                    return;
                Program.Print("  -> Scaling to " + targetScale + "px height with filter " + filter + "...");
                MagickGeometry geom = new MagickGeometry("x" + targetScale);
                img.Resize(geom);
            }
            if(currMode == SM.Width || (currMode == SM.LongerSide && widthIsLonger) || (currMode == SM.ShorterSide && heightIsLonger))
            {
                if(onlyDownscale && (img.Height <= targetScale))
                    return;
                Program.Print("  -> Scaling to " + targetScale + "px width with filter " + filter + "...");
                MagickGeometry geom = new MagickGeometry(targetScale + "x");
                img.Resize(geom);
            }
            if(currMode == SM.Percentage)
            {
                Program.Print("  -> Scaling to " + targetScale + "% with filter " + filter + "...");
                img.Resize(new Percentage(targetScale));
            }
            PreProcessing(path);
            Write(img, filter);
            PostProcessing(img, path);
        }

        static void Write (MagickImage img, FT filter)
        {
            if(!appendFiltername)
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

        static FT GetFilter (int randFilterMode)
        {
            if(randFilterMode == 0) return FT.Mitchell;
            if(randFilterMode == 1) return FT.Lanczos;
            if(randFilterMode == 2) return FT.Catrom;
            if(randFilterMode == 3) return FT.Point;
            if(randFilterMode == 4) return GetRandomFilter(true);
            if(randFilterMode == 5) return GetRandomFilter();
            return FT.Mitchell;
        }

        static void PreProcessing (string path, string infoSuffix = null)
        {
            bytesPre = 0;
            bytesPre = new FileInfo(path).Length;
            Program.Print("-> Processing " + Path.GetFileName(path) + " " + infoSuffix);
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
