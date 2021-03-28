using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MagickUtils.Utils;
using MozJpegSharp;

namespace MagickUtils.Interfaces
{
    class MozJpeg
    {
        public enum Subsampling { Chroma420, Chroma422, Chroma444 }

        public static void Encode(string inPath, string outPath, int q, Subsampling subsampling, bool printSubsampling = false)
        {
            try
            {
                Bitmap bmp = (Bitmap)IOUtils.GetImage(inPath);
                var compressor = new TJCompressor();
                byte[] compressed;

                TJSubsamplingOption subSample = TJSubsamplingOption.Chrominance420;
                if (subsampling == Subsampling.Chroma422) subSample = TJSubsamplingOption.Chrominance422;
                if (subsampling == Subsampling.Chroma444) subSample = TJSubsamplingOption.Chrominance444;

                if (printSubsampling)
                    Logger.Log("-> Chroma Subsampling: " + subSample.ToString().Replace("Chrominance", ""));

                compressed = compressor.Compress(bmp, subSample, q, TJFlags.None);
                File.WriteAllBytes(outPath, compressed);

                //Logger.Log("[MozJpeg] Written image to " + outPath);
            }
            catch (Exception e)
            {
                Logger.Log("MozJpeg Error: " + e.Message);
            }
        }
    }
}
