using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using MagickUtils.Interfaces;
using ImageMagick.Formats;
using MagickUtils.Utils;
using MagickUtils.MagickUtils;

namespace MagickUtils
{
    class ConvertUtils
    {

        public static void ConvertToFlif (string path, int q, bool delSrc)
        {
            long bytesSrc = new FileInfo(path).Length;
            string outPath = Path.ChangeExtension(path, null) + ".flif";

            if (Config.GetInt("flifEnc") == 1)
                FlifInterface.EncodeImage(path, q, delSrc);
            else
                ConvertToFlifMagick(path, q, delSrc);

            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        public static void ConvertToFlifMagick(string path, int q, bool delSrc)
        {

            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Flif;
            img.Quality = q;
            img.Settings.SetDefine(MagickFormat.Flif, "lossless", true);
            string outPath = Path.ChangeExtension(path, null) + ".flif";
            IOUtils.SaveImage(img, outPath);
        }

        public static async void ConvertDirToBmp(bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach (FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                counter++;
                ConvertToBmp(file.FullName, delSrc);
                if (counter % 8 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(files.Length);
        }

        public static void ConvertToBmp(string path, bool delSrc)
        {
            long bytesSrc = new FileInfo(path).Length;
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Bmp;
            string outPath = Path.ChangeExtension(path, null) + ".bmp";
            IOUtils.SaveImage(img, outPath);
            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        public static void ConvertToAvif(string path, int q, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Avif;
            img.Quality = q;
            string outPath = Path.ChangeExtension(path, null) + ".avif";
            IOUtils.SaveImage(img, outPath);
            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        public static void ConvertToJpeg (string path, int qMin, int qMax, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            Random rand = new Random();
            int q = rand.Next(qMin, qMax + 1);
            string outPath = Path.ChangeExtension(path, null) + ".jpg";

            if (Config.GetInt("jpegEnc") == 0)
            {
                MagickImage img = IOUtils.ReadImage(path);
                if (img == null) return;
                img.Format = MagickFormat.Jpeg;
                img.Quality = q;
                img = SetJpegChromaSubsampling(img);
                IOUtils.SaveImage(img, outPath);
            }
            else
            {
                switch (Config.GetInt("jpegChromaSubsampling"))
                {
                    case 0: MozJpeg.Encode(path, outPath, q, MozJpeg.Subsampling.Chroma420); break;
                    case 1: MozJpeg.Encode(path, outPath, q, MozJpeg.Subsampling.Chroma422); break;
                    case 2: MozJpeg.Encode(path, outPath, q, MozJpeg.Subsampling.Chroma444); break;
                }
            }

            PostProcessing(path, outPath, bytesSrc, delSrc, $"JPEG Quality: {q}");
        }

        public static void ConvertToPng(string path, int q = 50, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Png;
            if (Config.GetInt("pngColorDepth") == 1)
                img.Format = MagickFormat.Png24;
            if (Config.GetInt("pngColorDepth") == 2)
                img.Format = MagickFormat.Png32;
            img.Quality = q;
            string outPath = Path.ChangeExtension(path, null) + ".png";

            if (path == outPath)
                File.Delete(path);

            IOUtils.SaveImage(img, outPath);
            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        public static void ConvertToDds (string path, int qMin, int qMax, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            string outPath = Path.ChangeExtension(path, null) + ".dds";

            switch (Config.GetInt("ddsEnc"))
            {
                case 0: ConvertToDdsNative(path); break;
                case 1: DdsInterface.NvCompress(path, outPath); break;
                case 2: DdsInterface.Crunch(path, qMin, qMax); break;
            }

            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        static void ConvertToDdsNative (string path)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Dds;
            DdsCompression compression = DdsCompression.None;

            if (Config.Get("ddsCompressionType").Contains("BC1"))
                compression = DdsCompression.Dxt1;

            int mips = 0;
            if (Config.GetBool("ddsEnableMips")) mips = Config.GetInt("mipCount");
            var defines = new DdsWriteDefines { Compression = compression, Mipmaps = mips, FastMipmaps = true };
            img.Settings.SetDefines(defines);
            string outPath = Path.ChangeExtension(path, null) + ".dds";
            IOUtils.SaveImage(img, outPath);
        }

        public static void ConvertToTga (string path, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            MagickImage img = IOUtils.ReadImage(path);
            if(img == null) return;
            img.Format = MagickFormat.Tga;
            string outPath = Path.ChangeExtension(path, null) + ".tga";
            IOUtils.SaveImage(img, outPath);
            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        public static void ConvertToWebp (string path, int qMin, int qMax, bool delSrc = false)
        {
            //Logger.Log("ConvertToWebp");
            long bytesSrc = new FileInfo(path).Length;
            MagickImage img = IOUtils.ReadImage(path);
            if(img == null) return;
            img.Format = MagickFormat.WebP;
            string outPath = Path.ChangeExtension(path, null) + ".webp";
            Random rand = new Random();
            img.Quality = rand.Next(qMin, qMax + 1);
            if (img.Quality >= 100)
                img.Settings.SetDefine(MagickFormat.WebP, "lossless", true);
            Logger.Log("saving");
            IOUtils.SaveImage(img, outPath);
            PostProcessing(path, outPath, bytesSrc, delSrc, $"WEBP Quality: {img.Quality.ToString().Replace("100", "Lossless")}");
        }

        public static void ConvertToJpeg2000 (string path, int q, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            MagickImage img = IOUtils.ReadImage(path);
            if(img == null) return;
            img.Format = MagickFormat.Jp2;
            string outPath = Path.ChangeExtension(path, null) + ".jp2";
            img.Quality = q;
            IOUtils.SaveImage(img, outPath);
            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        public static void ConvertToJxl(string path, int qMin, int qMax, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            Random rand = new Random();
            int q = rand.Next(qMin, qMax + 1);
            string outPath = Path.ChangeExtension(path, null) + ".jxl";
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Jxl;
            img.Quality = q;
            IOUtils.SaveImage(img, outPath);
            PostProcessing(path, outPath, bytesSrc, delSrc, $"JPEG XL Quality: {q}");
        }

        static void PostProcessing (string sourcePath, string outPath, long bytesSrc, bool delSrc, string note = "")
        {
            long bytesPost = new FileInfo(outPath).Length;
            string noteStr = string.IsNullOrWhiteSpace(note) ? "" : $" ({note})";
            Logger.Log($"-> Saved {Path.GetFileName(outPath)}{noteStr}. Size Before: {FormatUtils.Bytes(bytesSrc)}" +
                $" - Size After: {FormatUtils.Bytes(bytesPost)} - Ratio: {FormatUtils.Ratio(bytesSrc, bytesPost)}");

            if (!string.IsNullOrWhiteSpace(sourcePath) && delSrc)
                DelSource(sourcePath, outPath);
        }

        static void DelSource (string sourcePath, string newPath)
        {
            if(Path.GetExtension(sourcePath).ToLower() == Path.GetExtension(newPath).ToLower())
            {
                Logger.Log("-> Didn't delete " + Path.GetFileName(sourcePath) + " as it was overwritten.");
                return;
            }

            try
            {
                File.Delete(sourcePath);
                Logger.Log("-> Deleted source file: " + Path.GetFileName(sourcePath) + ".");
            }
            catch (Exception e)
            {
                Logger.Log($"-> Failed to selete source file: {Path.GetFileName(sourcePath)}: {e.Message}");
            }
        }

        static MagickImage SetJpegChromaSubsampling (MagickImage img)
        {
            JpegWriteDefines jpegDefines = new JpegWriteDefines();
            int configVal = Config.GetInt("jpegChromaSubsampling");

            if (configVal == 0)
                jpegDefines.SamplingFactor = JpegSamplingFactor.Ratio420;
            if (configVal == 1)
                jpegDefines.SamplingFactor = JpegSamplingFactor.Ratio422;
            if (configVal == 2)
                jpegDefines.SamplingFactor = JpegSamplingFactor.Ratio444;

            img.Settings.SetDefines(jpegDefines);

            return img;
        }
    }
}
