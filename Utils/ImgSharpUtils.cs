using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using System;
using SM = MagickUtils.ScaleUtils.ScaleMode;

namespace MagickUtils.Utils
{
    class ImgSharpUtils
    {
        public static void Resize(string path, float scaleFactor, IResampler filter)
        {
            Image img = Image.Load(path);
            int w = (int)Math.Round(img.Width * scaleFactor);
            int h = (int)Math.Round(img.Height * scaleFactor);
            img.Mutate(x => x.Resize(w, h, filter));
            img.Save(path);
        }

        public static void Resize(Image img, string outpath, float scaleFactor, IResampler filter)
        {
            int w = (int)Math.Round(img.Width * scaleFactor);
            int h = (int)Math.Round(img.Height * scaleFactor);
            img.Mutate(x => x.Resize(w, h, filter));
            img.Save(outpath);
        }

        public static void Scale(string path, float minScale, float maxScale, int randFilterMode)
        {
            if(randFilterMode > 5 || randFilterMode < 0)
            {
                Program.Print("This filter mode is not supported with ImageSharp!");
                return;
            }
            Image img = Image.Load(path);
            PngEncoder enc = new PngEncoder();
            enc.CompressionLevel = PngCompressionLevel.DefaultCompression;

            bool onlyDownscale = ScaleUtils.onlyDownscale;
            SM scaleMode = ScaleUtils.currMode;

            IResampler resampler = GetFilter(randFilterMode);
            string resamplerName = resampler.ToString().Replace("SixLabors.ImageSharp.Processing.Processors.Transforms", "");

            Random rand = new Random();
            float scaleValue = (float)rand.NextDouble(minScale, maxScale);

            bool heightLonger = img.Height > img.Width;
            bool widthLonger = img.Width > img.Height;
            bool square = (img.Height == img.Width);

            if (scaleMode == SM.Percentage)
            {
                Program.Print("[ImgSharp] Scaling to " + scaleValue + "% with filter " + resamplerName + "...");
                img.Mutate(x => x.Resize((img.Width * scaleValue / 100f).RoundToInt(), (img.Height * scaleValue / 100f).RoundToInt(), resampler));
                img.Save(path, enc);
                return;
            }

            // Scale HEIGHT in the following cases:
            bool useSquare = square && scaleMode != SM.Percentage;
            bool useHeight = scaleMode ==SM.Height;
            bool useLongerOnH = (scaleMode == SM.LongerSide && heightLonger);
            bool useShorterOnW = (scaleMode == SM.ShorterSide && widthLonger);
            if (useSquare || useHeight || useLongerOnH || useShorterOnW)
            {
                if (onlyDownscale && (img.Height <= scaleValue))
                    return;     // don't scale
                Program.Print("[ImgSharp] Scaling to " + scaleValue + "px height with filter " + resamplerName + "...");
                float wFactor = (float)scaleValue / img.Height;
                img.Mutate(x => x.Resize((img.Width * wFactor).RoundToInt(), scaleValue.RoundToInt(), resampler));
                img.Save(path, enc);
                return;
            }

            // Scale WIDTH in the following cases:
            bool useWidth = scaleMode == SM.Width;
            bool useLongerOnW = (scaleMode == SM.LongerSide && widthLonger);
            bool useShorterOnH = (scaleMode == SM.ShorterSide && heightLonger);
            if (useWidth || useLongerOnW || useShorterOnH)
            {
                if (onlyDownscale && (img.Width <= scaleValue))
                    return;     // don't scale
                Program.Print("[ImgSharp] Scaling to " + scaleValue + "px width with filter " + resamplerName + "...");
                float hFactor = (float)scaleValue / img.Width;
                img.Mutate(x => x.Resize(scaleValue.RoundToInt(), (img.Height * hFactor).RoundToInt(), resampler));
                img.Save(path, enc);
                return;
            }
        }

        static IResampler GetFilter(int randFilterMode, string filterName = "")
        {
            if (randFilterMode == 0) return KnownResamplers.MitchellNetravali;
            if (randFilterMode == 1) return KnownResamplers.Lanczos2;
            if (randFilterMode == 2) return KnownResamplers.Bicubic;
            if (randFilterMode == 3) return KnownResamplers.NearestNeighbor;
            if (randFilterMode == 4) return GetRandomFilter(true);
            if (randFilterMode == 5) return GetRandomFilter();
            return KnownResamplers.MitchellNetravali;
        }

        static IResampler GetRandomFilter(bool onlyBasicFilters = false)
        {
            IResampler[] filtersAll = new IResampler[] { KnownResamplers.Lanczos2, KnownResamplers.Bicubic, KnownResamplers.Robidoux, KnownResamplers.Spline, KnownResamplers.Box, KnownResamplers.Hermite, KnownResamplers.MitchellNetravali, KnownResamplers.Triangle };
            IResampler[] filtersBasic = new IResampler[] { KnownResamplers.Bicubic, KnownResamplers.Triangle, KnownResamplers.MitchellNetravali };

            Random rand = new Random();
            if (onlyBasicFilters) return filtersBasic[rand.Next(filtersBasic.Length)];
            else return filtersAll[rand.Next(filtersAll.Length)];
        }
    }
}
