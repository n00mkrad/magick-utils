using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using System.IO;
using System.Diagnostics;
using ImageMagick.Formats.Dds;
using System.Reflection;

namespace MagickUtils
{
    class ConvertUtils
    {
        public static async void ConvertDirToJpeg (int qMin, int qMax, bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                ConvertToJpegRandomQuality(file.FullName, qMin, qMax, delSrc);
                counter++;
                if(counter % 8 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public async static void ConvertDirToPng (int q, bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                ConvertToPng(file.FullName, q, delSrc);
                counter++;
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(true);
        }

        public static async void ConvertDirToDds (bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                counter++;
                ConvertToDds(file.FullName, delSrc);
                if (counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static async void ConvertDirToDdsCrunch (int qMin, int qMax, bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Crunching Image ", counter, files.Length);
                counter++;
                CrunchInterface.CrunchImage(file.FullName, qMin, qMax, delSrc);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static async void ConvertDirToTga (bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                counter++;
                ConvertToTga(file.FullName, delSrc);
                if (counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static async void ConvertDirToWebp (int q, bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                counter++;
                ConvertToWebp(file.FullName, q, delSrc);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static async void ConvertDirToJpeg2000 (int q, bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                counter++;
                ConvertToJpeg2000(file.FullName, q, delSrc);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static async void ConvertDirToFlif (int q, bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                counter++;
                FlifInterface.EncodeImage(file.FullName, q, delSrc);
                await Program.PutTaskDelay();
            }
            Program.PostProcessing();
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
            Program.PostProcessing();
        }

        public static void ConvertToBmp(string path, bool delSource = false)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Bmp;
            string outPath = Path.ChangeExtension(path, null) + ".bmp";
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static async void ConvertDirToAvif(int q, bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach (FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                counter++;
                ConvertToAvif(file.FullName, q, delSrc);
                await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static void ConvertToAvif(string path, int q, bool delSource = false)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Avif;
            img.Quality = q;
            string outPath = Path.ChangeExtension(path, null) + ".avif";
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        static long bytesPre;

        public static void ConvertToJpeg (string path, int q = 95, bool delSource = false)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if(img == null) return;
            img.Format = MagickFormat.Jpeg;
            img.Quality = q;
            string outPath = Path.ChangeExtension(path, null) + ".jpg";
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToJpegRandomQuality (string path, int qMin, int qMax, bool delSource = false)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if(img == null) return;
            img.Format = MagickFormat.Jpeg;
            Random rand = new Random();
            img.Quality = rand.Next(qMin, qMax + 1);
            string outPath = Path.ChangeExtension(path, null) + ".jpg";
            PreProcessing(path, " [JPEG Quality: " + img.Quality + "]");
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToPng(string path, int q = 50, bool delSource = false)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Png;
            img.Quality = q;
            string outPath = Path.ChangeExtension(path, null) + ".png";
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToPngIM (string path, int q = 50, bool delSource = false)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if(img == null) return;
            img.Format = MagickFormat.Png;
            img.Quality = q;
            string outPath = Path.ChangeExtension(path, null) + ".png";
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static async Task ConvertToPngMT(string path, int pngCompressLvl = 0, bool delSource = false)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.Format = MagickFormat.Png;
            img.Quality = pngCompressLvl;
            string outPath = Path.ChangeExtension(path, null) + ".png";
            PreProcessing(path);
            await Task.Delay(1);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToDds (string path, bool delSource = false)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if(img == null) return;
            img.Format = MagickFormat.Dds;
            var defines = new DdsWriteDefines { Compression = DdsCompression.Dxt1 };
            img.Settings.SetDefines(defines);
            string outPath = Path.ChangeExtension(path, null) + ".dds";
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToTga (string path, bool delSource = false)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if(img == null) return;
            img.Format = MagickFormat.Tga;
            string outPath = Path.ChangeExtension(path, null) + ".tga";
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToWebp (string path, int q, bool delSource = false)
        {
            MagickImage img = IOUtils.ReadImage(path);if(img == null) return;
            img.Format = MagickFormat.WebP;
            string outPath = Path.ChangeExtension(path, null) + ".webp";
            img.Quality = q;
            PreProcessing(path);
            img.Write(outPath);
            PostProcessing(img, path, outPath, delSource);
        }

        public static void ConvertToJpeg2000 (string path, int q, bool delSource = false)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if(img == null) return;
            img.Format = MagickFormat.Jp2;
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
            if(img != null)
                img.Dispose();
            long bytesPost = new FileInfo(outPath).Length;
            Program.Print("-> Done. Size pre: " + Format.Filesize(bytesPre) + " - Size post: " + Format.Filesize(bytesPost) + " - Ratio: " + Format.Ratio(bytesPre, bytesPost));
            if(delSource)
                DelSource(sourcePath, outPath);
        }

        static void DelSource (string sourcePath, string newPath)
        {
            if(Path.GetExtension(sourcePath) == Path.GetExtension(newPath))
            {
                Program.Print("-> Not deleting " + Path.GetFileName(sourcePath) + " as it was overwritten");
                return;
            }
            Program.Print("-> Deleting source file: " + Path.GetFileName(sourcePath) + "...");
            File.Delete(sourcePath);
        }
    }
}
