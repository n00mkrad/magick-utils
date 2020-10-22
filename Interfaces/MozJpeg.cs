using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MozJpegSharp;

namespace MagickUtils.Interfaces
{
    class MozJpeg
    {
        public enum Subsampling { Chroma420, Chroma422, Chroma444 }

        public static void Encode(string inPath, string outPath, int q, Subsampling subsampling, bool printSubsampling = true)
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
                    Program.Print("-> Chroma Subsampling: " + subSample.ToString().Replace("Chrominance", ""));

                compressed = compressor.Compress(bmp, subSample, q, TJFlags.None);
                File.WriteAllBytes(outPath, compressed);

                //Program.Print("[MozJpeg] Written image to " + outPath);
            }
            catch (Exception e)
            {
                Program.Print("MozJpeg Error: " + e.Message);
            }
        }
    }
}
