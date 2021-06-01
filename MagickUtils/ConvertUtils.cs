using System;
using System.Threading.Tasks;
using ImageMagick;
using System.IO;
using MagickUtils.Interfaces;
using ImageMagick.Formats;
using MagickUtils.Utils;

namespace MagickUtils
{
    class ConvertUtils
    {

        public static async Task ConvertToFlif(string path, int q, bool delSrc)
        {
            long bytesSrc = new FileInfo(path).Length;
            string outPath = Path.ChangeExtension(path, null) + ".flif";

            if (await Config.GetInt("flifEnc") == 1)
                await FlifInterface.EncodeImage(path, q, delSrc);
            else
                await ConvertToFlifMagick(path, q, delSrc);

            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        public static async Task ConvertToFlifMagick(string path, int q, bool delSrc)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Flif;
            img.Quality = q;
            img.Settings.SetDefine(MagickFormat.Flif, "lossless", true);
            string outPath = Path.ChangeExtension(path, null) + ".flif";
            IOUtils.SaveImage(img, outPath);
        }

        public static async Task ConvertToHeif(string path, int q, bool delSrc)
        {
            long bytesSrc = new FileInfo(path).Length;
            string outPath = Path.ChangeExtension(path, null) + ".heic";
            await HeifInterface.EncodeImage(path, q, delSrc);
            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        public static async Task ConvertToBmp(string path, bool delSrc)
        {
            long bytesSrc = new FileInfo(path).Length;
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Bmp;
            string outPath = Path.ChangeExtension(path, null) + ".bmp";
            IOUtils.SaveImage(img, outPath);
            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        public static async Task ConvertToAvif(string path, int q, bool delSrc = false)
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

        public static async Task ConvertToJpeg(string path, int qMin, int qMax, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            Random rand = new Random();
            int q = rand.Next(qMin, qMax + 1);
            string outPath = Path.ChangeExtension(path, null) + ".jpg";

            if (await Config.GetInt("jpegEnc") == 0) // Native Magick JPEG Encoder
            {
                MagickImage img = IOUtils.ReadImage(path);
                if (img == null) return;
                img.Format = MagickFormat.Jpeg;
                img.Quality = q;
                img = await SetJpegChromaSubsampling(img);
                IOUtils.SaveImage(img, outPath);
            }
            else // MozJPEG Encoder
            {
                bool convert = !IsPng(path);
                if (convert) path = await ConvertToTempPng(path);

                switch (await Config.GetInt("jpegChromaSubsampling"))
                {
                    case 0: MozJpeg.Encode(path, outPath, q, MozJpeg.Subsampling.Chroma420); break;
                    case 1: MozJpeg.Encode(path, outPath, q, MozJpeg.Subsampling.Chroma422); break;
                    case 2: MozJpeg.Encode(path, outPath, q, MozJpeg.Subsampling.Chroma444); break;
                }

                if (convert) IOUtils.TryDeleteIfExists(path);
            }

            PostProcessing(path, outPath, bytesSrc, delSrc, $"JPEG Quality: {q}");
        }

        public static async Task ConvertToPng(string path, int q = 50, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Png;
            if (await Config.GetInt("pngColorDepth") == 1)
                img.Format = MagickFormat.Png24;
            if (await Config.GetInt("pngColorDepth") == 2)
                img.Format = MagickFormat.Png32;
            img.Quality = q;
            string outPath = Path.ChangeExtension(path, null) + ".png";

            if (path == outPath)
                File.Delete(path);

            IOUtils.SaveImage(img, outPath);
            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        public static async Task ConvertToDds(string path, int qMin, int qMax, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            string outPath = Path.ChangeExtension(path, null) + ".dds";

            switch (await Config.GetInt("ddsEnc"))
            {
                case 0: await ConvertToDdsNative(path); break;
                case 1: await DdsInterface.NvCompress(path, outPath); break;
                case 2: await DdsInterface.Texconv(path); break;
                case 3: await DdsInterface.Crunch(path, qMin, qMax); break;
            }

            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        static async Task ConvertToDdsNative(string path)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Dds;
            DdsCompression compression = DdsCompression.None;

            if ((await Config.Get("ddsCompressionType")).Contains("BC1"))
                compression = DdsCompression.Dxt1;

            int mips = 0;
            if (await Config.GetBool("ddsEnableMips")) mips = await Config.GetInt("mipCount");
            var defines = new DdsWriteDefines { Compression = compression, Mipmaps = mips, FastMipmaps = true };
            img.Settings.SetDefines(defines);
            string outPath = Path.ChangeExtension(path, null) + ".dds";
            IOUtils.SaveImage(img, outPath);
        }

        public static async Task ConvertToTga(string path, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Tga;
            string outPath = Path.ChangeExtension(path, null) + ".tga";
            IOUtils.SaveImage(img, outPath);
            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        public static async Task ConvertToWebp(string path, int qMin, int qMax, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.WebP;
            string outPath = Path.ChangeExtension(path, null) + ".webp";
            Random rand = new Random();
            img.Quality = rand.Next(qMin, qMax + 1);

            if (img.Quality >= 100)
                img.Settings.SetDefine(MagickFormat.WebP, "lossless", true);

            string saveNote = $"WEBP Quality: {img.Quality.ToString().Replace("100", "Lossless")}";
            IOUtils.SaveImage(img, outPath);
            PostProcessing(path, outPath, bytesSrc, delSrc, saveNote);
        }

        public static async Task ConvertToJpeg2000(string path, int q, bool delSrc = false)
        {
            long bytesSrc = new FileInfo(path).Length;
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Jp2;
            string outPath = Path.ChangeExtension(path, null) + ".jp2";
            img.Quality = q;
            IOUtils.SaveImage(img, outPath);
            PostProcessing(path, outPath, bytesSrc, delSrc);
        }

        public static async Task ConvertToJxl(string path, int qMin, int qMax, bool delSrc = false)
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

        public static bool IsPng (string inPath)
        {
            return Path.GetExtension(inPath).ToLower() == ".png";
        }

        public static async Task<string> ConvertToTempPng(string inPath)
        {
            string outPath = Path.ChangeExtension(inPath, null) + ".temp.png";
            MagickImage img = IOUtils.ReadImage(inPath);
            img.Format = MagickFormat.Png00;
            img.Quality = 0;    // Disable PNG compression for speed
            img.Write(outPath);
            //Logger.Log("Input is not a PNG - Converted temporarily to PNG for compatibility", true);
            return outPath;
        }

        public static void PostProcessing(string sourcePath, string outPath, long bytesSrc, bool delSrc, string note = "")
        {
            long bytesPost = new FileInfo(outPath).Length;
            string noteStr = string.IsNullOrWhiteSpace(note) ? "" : $" ({note})";
            string filename = Path.GetFileNameWithoutExtension(outPath).Trunc(35) + Path.GetExtension(outPath);
            Logger.Log(
                $"Saved {filename}{noteStr}. Size Before: {FormatUtils.Bytes(bytesSrc)}" +
                $" - Size After: {FormatUtils.Bytes(bytesPost)} ({FormatUtils.Ratio(bytesSrc, bytesPost)})");

            if (!string.IsNullOrWhiteSpace(sourcePath) && delSrc)
                DelSource(sourcePath, outPath);
        }

        static void DelSource(string sourcePath, string newPath)
        {
            if (Path.GetExtension(sourcePath).ToLower() == Path.GetExtension(newPath).ToLower())
            {
                Logger.Log("Didn't delete " + Path.GetFileName(sourcePath) + " as it was overwritten.");
                return;
            }

            try
            {
                File.Delete(sourcePath);
                Logger.Log("Deleted source file: " + Path.GetFileName(sourcePath) + ".");
            }
            catch (Exception e)
            {
                Logger.Log($"Failed to selete source file: {Path.GetFileName(sourcePath)}: {e.Message}");
            }
        }

        static async Task<MagickImage> SetJpegChromaSubsampling(MagickImage img)
        {
            JpegWriteDefines jpegDefines = new JpegWriteDefines();
            int configVal = await Config.GetInt("jpegChromaSubsampling");

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
