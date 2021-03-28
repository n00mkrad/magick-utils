using ImageMagick;
using Ionic.Zip;
using MagickUtils.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using MagickUtils.Utils;
using Paths = MagickUtils.Utils.Paths;

namespace MagickUtils
{
    class DdsInterface
    {
        static string ddsResPath;
        static string nvCompExePath;
        static string crunchExePath;

        public enum CrunchPreset { superfast, fast, normal, better, uber };
        public static CrunchPreset crunchPreset;
        //public static bool currentMipMode;

        public static void Extract(bool overwrite = false)
        {
            GetPaths();

            if (File.Exists(nvCompExePath) && File.Exists(crunchExePath) && !overwrite)
                return;

            File.WriteAllBytes(ddsResPath, Resources.dds);
            ZipFile zip = ZipFile.Read(ddsResPath);
            foreach (ZipEntry e in zip)
                e.Extract(Paths.GetDataPath(), ExtractExistingFileAction.OverwriteSilently);

            Logger.Log("[DdsInterface] Extracted DDS encoding resources to " + ddsResPath);
        }

        static void GetPaths()
        {
            ddsResPath = Path.Combine(Paths.GetDataPath(), "dds.zip");
            nvCompExePath = Path.Combine(Paths.GetDataPath(), "dds", "nvcompress.exe");
            crunchExePath = Path.Combine(Paths.GetDataPath(), "dds", "crunch.exe");
        }

        public static void Crunch(string inpath, int qMin, int qMax)
        {
            SetEncSpeed();
            Extract();

            string sourcePath = inpath;
            bool convert = Path.GetExtension(inpath).ToLower() != ".png";
            if (convert)
                inpath = ConvertToPng(inpath);

            string dxtString = Config.Get("ddsCompressionType").Split(' ')[0].ToUpper().Replace("ARGB", "A8R8G8B8");

            string mipMode = "None";
            if (Config.GetBool("ddsEnableMips")) mipMode = "UseSourceOrGenerate";

            string args = $" -file {inpath.WrapPath()} -outsamedir";
            string args2 = $" -quality {GetRandomQuality(qMin, qMax)} -fileformat dds -mipMode {mipMode} -dxtQuality {crunchPreset}";
            ProcessStartInfo psi = new ProcessStartInfo { FileName = crunchExePath, Arguments = args + args2, WindowStyle = ProcessWindowStyle.Hidden };
            Logger.Log("-> Running Crunch:" + args2);
            Process crunchProcess = new Process { StartInfo = psi };
            crunchProcess.Start();
            crunchProcess.WaitForExit();

            if (convert)
                File.Delete(inpath);
        }

        public static void NvCompress(string inpath, string outpath)
        {
            Extract();
            string sourcePath = inpath;
            bool convert = Path.GetExtension(inpath).ToLower() != ".png";
            if (convert)
                inpath = ConvertToPng(inpath);

            string dxtString = Config.Get("ddsCompressionType").Split(' ')[0].ToLower().Replace("argb", "rgb");

            string mipStr = "-nomips";
            if (Config.GetBool("ddsEnableMips")) mipStr = "";

            string args = $" -{dxtString} -alpha { mipStr} {inpath.WrapPath()} {outpath.WrapPath()}";
            ProcessStartInfo psi = new ProcessStartInfo { FileName = nvCompExePath, Arguments = args, WindowStyle = ProcessWindowStyle.Hidden };
            Process nvCompress = new Process { StartInfo = psi };
            //Logger.Log("-> Running NvCompress:" + args.Split('"')[0]);
            nvCompress.Start();
            nvCompress.WaitForExit();

            if (convert)
                File.Delete(inpath);
        }

        static string ConvertToPng (string inpath)
        {
            string newPath = Path.ChangeExtension(inpath, "png");
            MagickImage img = IOUtils.ReadImage(inpath);
            img.Format = MagickFormat.Png00;
            img.Quality = 0;    // Disable PNG compression for speed
            img.Write(newPath);
            Logger.Log("-> Input is not a PNG - Converted temporarily to PNG for compatibility");
            return newPath;
        }

        static long bytesPre = 0;
        static void PreProcessing(string path, string infoSuffix = null)
        {
            bytesPre = 0;
            bytesPre = new FileInfo(path).Length;
            //Logger.Log("-> Processing " + Path.GetFileName(path) + " " + infoSuffix);
        }

        static void PostProcessing(MagickImage img, string sourcePath, string outPath, bool delSource)
        {
            if (img != null)
                img.Dispose();
            long bytesPost = new FileInfo(outPath).Length;
            Logger.Log($"-> Done. Size pre: {FormatUtils.Bytes(bytesPre)} - Size post: {FormatUtils.Bytes(bytesPost)} - Ratio: {FormatUtils.Ratio(bytesPre, bytesPost)}");
            if (delSource)
                File.Delete(sourcePath);
        }

        static int GetRandomQuality(int qMin, int qMax)
        {
            Random rand = new Random();
            return rand.Next(qMin, qMax + 1);
        }

        static void SetEncSpeed()
        {
            switch (Config.GetInt("crunchDxtSpeed"))
            {
                case 0: crunchPreset = CrunchPreset.superfast; break;
                case 1: crunchPreset = CrunchPreset.fast; break;
                case 2: crunchPreset = CrunchPreset.normal; break;
                case 3: crunchPreset = CrunchPreset.better; break;
                case 4: crunchPreset = CrunchPreset.uber; break;
            }
        }
    }
}
