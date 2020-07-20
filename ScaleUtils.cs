using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ImageMagick;
using FT = ImageMagick.FilterType;

namespace MagickUtils
{
    class ScaleUtils
    {
        public static void RandomResample (string path, int minScale, int maxScale, int randFilterMode)
        {
            MagickImage img = new MagickImage(path);
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
            img.Write(img.FileName);
        }

        static FT GetRandomFilter (bool onlyBasicFilters = false)
        {
            FT[] filtersAll = new FT[] { FT.Lanczos, FT.Catrom, FT.Quadratic, FT.Spline, FT.Box, FT.Gaussian, FT.Mitchell, FT.Triangle };
            FT[] filtersBasic = new FT[] { FT.Catrom, FT.Box, FT.Mitchell };
            Random rand = new Random();
            if(onlyBasicFilters) return filtersBasic[rand.Next(filtersBasic.Length)];
            else return filtersAll[rand.Next(filtersAll.Length)];
        }

        public static void Scale (string path, int minScale, int maxScale, int randFilterMode, bool useHeight)
        {
            MagickImage img = new MagickImage(path);
            string fname = Path.ChangeExtension(path, null);
            Program.Print("-> " + fname + " (" + img.Width + "x" + img.Height + ")");
            FT filter = GetFilter(randFilterMode);
            Random rand = new Random();
            int targetScale = rand.Next(minScale, maxScale + 1);
            img.FilterType = filter;
            if(useHeight)
            {
                Program.Print("  -> Scaling to " + targetScale + "px with filter " + filter + "...");
                MagickGeometry geom = new MagickGeometry("x" + targetScale);
                img.Resize(geom);
            }
            else
            {
                Program.Print("  -> Scaling to " + targetScale + "% with filter " + filter + "...");
                img.Resize(new Percentage(targetScale));
            }
            img.Write(img.FileName);
        }

        static FT GetFilter (int randFilterMode)
        {
            if(randFilterMode == 1) return FT.Catrom;
            if(randFilterMode == 2) return FT.Point;
            if(randFilterMode == 3) return GetRandomFilter(true);
            if(randFilterMode == 4) return GetRandomFilter();
            return FT.Mitchell;
        }
    }
}
