using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using System.IO;
using ImageMagick.Formats.Dds;

namespace MagickUtils
{
    class ConvertUtils
    {
        static long bytesPre;

        public static void ConvertToJpeg (string path, int q = 95, bool delSource = false)
        {
            MagickImage img = new MagickImage(path) { Format = MagickFormat.Jpeg };
            img.Quality = q;
            string outPath = Path.ChangeExtension(path, null) + ".jpg";
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToJpegRandomQuality (string path, int qMin, int qMax, bool delSource = false)
        {
            MagickImage img = new MagickImage(path) { Format = MagickFormat.Jpeg };
            Random rand = new Random();
            img.Quality = rand.Next(qMin, qMax + 1);
            string outPath = Path.ChangeExtension(path, null) + ".jpg";
            PreProcessing(path, " [JPEG Quality: " + img.Quality + "]");
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToPng (string path, int pngCompressLvl = 0, bool delSource = false)
        {
            MagickImage img = new MagickImage(path) { Format = MagickFormat.Png };
            img.Quality = pngCompressLvl;
            string outPath = Path.ChangeExtension(path, null) + ".png";
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToDds (string path, bool delSource = false)
        {
            MagickImage img = new MagickImage(path) { Format = MagickFormat.Dds };
            var defines = new DdsWriteDefines { Compression = DdsCompression.Dxt1 };
            img.Settings.SetDefines(defines);
            string outPath = Path.ChangeExtension(path, null) + ".dds";
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToTga (string path, bool delSource = false)
        {
            MagickImage img = new MagickImage(path) { Format = MagickFormat.Tga };
            string outPath = Path.ChangeExtension(path, null) + ".tga";
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToWebp (string path, int q, bool delSource = false)
        {
            MagickImage img = new MagickImage(path) { Format = MagickFormat.WebP };
            string outPath = Path.ChangeExtension(path, null) + ".webp";
            img.Quality = q;
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToJpeg2000 (string path, int q, bool delSource = false)
        {
            MagickImage img = new MagickImage(path) { Format = MagickFormat.Jp2 };
            string outPath = Path.ChangeExtension(path, null) + ".jp2";
            img.Quality = q;
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        static void PreProcessing (string path, string infoSuffix = null)
        {
            bytesPre = 0;
            bytesPre = new FileInfo(path).Length;
            Program.Print("-> Processing " + Path.GetFileName(path) + " " + infoSuffix);
            Program.sw.Start();
        }

        static void PostProcessing (MagickImage img, string sourcePath, string outPath, bool delSource)
        {
            Program.sw.Stop();
            img.Dispose();
            long bytesPost = new FileInfo(outPath).Length;
            Program.Print("  -> Done. Size pre: " + Format.Filesize(bytesPre) + " - Size post: " + Format.Filesize(bytesPost) + " - Ratio: " + Format.Ratio(bytesPre, bytesPost));
            if(delSource)
                DelSource(sourcePath);
        }

        static void DelSource (string path)
        {
            Program.Print("  -> Deleting source file: " + Path.GetFileName(path) + "...\n");
            File.Delete(path);
        }
    }
}
