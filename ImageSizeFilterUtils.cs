using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using SM = MagickUtils.ImageSizeFilterUtils.SizeMode;
using Op = MagickUtils.ImageSizeFilterUtils.Operator;
using System.IO;

namespace MagickUtils
{
    class ImageSizeFilterUtils
    {
        public enum SizeMode { Height, Width, LongerSide, ShorterSide, EitherSide, BothSides }
        public enum Operator { IsNot, Is, IsSmaller, IsBigger, Divisible, NotDivisible }
        public static void DeleteSmallImages(string path, SM mode, Op op, int minPixels)
        {
            MagickImage img = IOUtils.ReadImage(path);
            bool heightLonger = img.Height > img.Width;
            bool widthLonger = img.Width > img.Height;
            bool square = (img.Height == img.Width);

            if(mode == SM.EitherSide)
            {
                if (HeightValid(img, op, minPixels) && WidthValid(img, op, minPixels)) return;
            }
            else
            if (mode == SM.BothSides)
            {
                if (HeightValid(img, op, minPixels) || WidthValid(img, op, minPixels)) return;
            }

            if (square || mode == SM.Height || (mode == SM.LongerSide && heightLonger) || (mode == SM.ShorterSide && widthLonger))
            {
                if (HeightValid(img, op, minPixels)) return;
            }
            else
            if (mode == SM.Width || (mode == SM.LongerSide && widthLonger) || (mode == SM.ShorterSide && heightLonger))
            {
                if (WidthValid(img, op, minPixels)) return;
            }

            string fname = Path.GetFileName(path);
            Program.Print("-> Deleted " + fname + " (" + img.Width + "x" + img.Height + ")");
            File.Delete(img.FileName);
        }

        static bool HeightValid(MagickImage img, Op op, int minPixels)
        {
            if (op == Op.IsSmaller && img.Height >= minPixels) return true;  // Not smaller. Return (do not delete).
            if (op == Op.IsBigger && img.Height <= minPixels) return true;  // Not bigger. Return (do not delete).
            if (op == Op.Is && img.Height != minPixels) return true;  // Not equal. Return (do not delete).
            if (op == Op.IsNot && img.Height == minPixels) return true;  // Equal. Return (do not delete).
            if (op == Op.Divisible && (img.Height % minPixels != 0)) return true;  // Equal. Return (do not delete).
            if (op == Op.NotDivisible && (img.Height % minPixels == 0)) return true;  // Equal. Return (do not delete).
            return false;
        }

        static bool WidthValid(MagickImage img, Op op, int minPixels)
        {
            if (op == Op.IsSmaller && img.Width >= minPixels) return true;  // Not smaller. Return (do not delete).
            if (op == Op.IsBigger && img.Width <= minPixels) return true;  // Not bigger. Return (do not delete).
            if (op == Op.Is && img.Width != minPixels) return true;  // Not equal. Return (do not delete).
            if (op == Op.IsNot && img.Width == minPixels) return true;  // Equal. Return (do not delete).
            if (op == Op.Divisible && (img.Width % minPixels != 0)) return true;  // Equal. Return (do not delete).
            if (op == Op.NotDivisible && (img.Width % minPixels == 0)) return true;  // Equal. Return (do not delete).
            return false;
        }
    }
}
