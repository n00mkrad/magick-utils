using ImageMagick;
using Ionic.Zip;
using MagickUtils.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using MagickUtils.Utils;
using Paths = MagickUtils.Utils.Paths;
using System.Linq;

namespace MagickUtils
{
    class DdsInterface
    {
        static string nvCompExePath;
        static string crunchExePath;
        static string texconvExePath;

        public enum CrunchPreset { superfast, fast, normal, better, uber };
        public static CrunchPreset crunchPreset;
        //public static bool currentMipMode;

        static void GetPaths()
        {
            nvCompExePath = Path.Combine(Paths.GetDataPath(), "runtimes", "dds", "nvcompress.exe");
            crunchExePath = Path.Combine(Paths.GetDataPath(), "runtimes", "dds", "crunch.exe");
            texconvExePath = Path.Combine(Paths.GetDataPath(), "runtimes", "dds", "texconv.exe");
        }

        public static async Task Crunch(string inpath, int qMin, int qMax)
        {
            GetPaths();
            SetEncSpeed();

            bool convertFirst = Path.GetExtension(inpath).ToLower() != ".png";

            if (convertFirst)
                inpath = ConvertToPng(inpath);

            string dxtString = (await Config.Get("ddsCompressionType")).Split(' ')[0].ToUpper().Replace("ARGB", "A8R8G8B8");;
            string mipMode = (await Config.GetBool("ddsEnableMips")) ? "UseSourceOrGenerate"  : "None";
            string args = $" -file {inpath.Wrap()} -outsamedir";
            string args2 = $" -quality {GetRandomQuality(qMin, qMax)} -fileformat dds -mipMode {mipMode} -dxtQuality {crunchPreset} {dxtString}";
            ProcessStartInfo psi = new ProcessStartInfo { FileName = crunchExePath, Arguments = args + args2, WindowStyle = ProcessWindowStyle.Hidden };
            Logger.Log("Running Crunch:" + args2, true);
            Process crunchProcess = new Process { StartInfo = psi };
            crunchProcess.Start();
            crunchProcess.WaitForExit();

            if (convertFirst)
                File.Delete(inpath);
        }

        public static async Task NvCompress(string inpath, string outpath)
        {
            GetPaths();
            string sourcePath = inpath;
            bool convert = Path.GetExtension(inpath).ToLower() != ".png";
            if (convert)
                inpath = ConvertToPng(inpath);

            string dxtString = (await Config.Get("ddsCompressionType")).Split(' ')[0].ToLower().Replace("argb", "rgb");

            string mipStr = "-nomips";
            if (await Config.GetBool("ddsEnableMips")) mipStr = "";

            string args = $" -{dxtString} -alpha {mipStr} {inpath.Wrap()} {outpath.Wrap()}";
            ProcessStartInfo psi = new ProcessStartInfo { FileName = nvCompExePath, Arguments = args, WindowStyle = ProcessWindowStyle.Hidden };
            Process nvCompress = new Process { StartInfo = psi };
            //Logger.Log("-> Running NvCompress:" + args.Split('"')[0]);
            nvCompress.Start();
            nvCompress.WaitForExit();

            if (convert)
                File.Delete(inpath);
        }

        public static async Task Texconv (string inpath)
        {
            GetPaths();

            string[] supported = new string[] { "png", "jpg", "jpeg", "webp", "dds", "bmp" };
            bool convertFirst = !supported.Contains(Path.GetExtension(inpath).ToLower().Replace(".", ""));

            if (convertFirst)
                inpath = ConvertToPng(inpath);

            string format = (await Config.Get("ddsCompressionType")).Split(' ')[0].ToLower();
            format = format.Replace("argb", "R8G8B8A8_UNORM").Replace("bc1", "BC1_UNORM").Replace("bc2", "BC2_UNORM").Replace("bc3", "BC3_UNORM")
                .Replace("bc4", "BC4_UNORM").Replace("bc5", "BC5_UNORM").Replace("bc6", "BC6H_UF16").Replace("bc7", "BC7_UNORM");
            string mips = (await Config.GetBool("ddsEnableMips")) ? "-m 4" : "-m 0";
            string args = $" -y -nologo {mips} -f {format} -o {inpath.GetParentDir().Wrap()} {inpath.Wrap()}";
            ProcessStartInfo psi = new ProcessStartInfo { FileName = texconvExePath, Arguments = args, WindowStyle = ProcessWindowStyle.Hidden };
            Process crunchProcess = new Process { StartInfo = psi };
            crunchProcess.Start();
            crunchProcess.WaitForExit();

            if (convertFirst)
                File.Delete(inpath);
        }

        static string ConvertToPng (string inpath)
        {
            string newPath = Path.ChangeExtension(inpath, "png");
            MagickImage img = IOUtils.ReadImage(inpath);
            img.Format = MagickFormat.Png00;
            img.Quality = 0;    // Disable PNG compression for speed
            img.Write(newPath);
            Logger.Log("Input is not a PNG - Converted temporarily to PNG for compatibility", true);
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

        static async Task SetEncSpeed()
        {
            switch (await Config.GetInt("crunchDxtSpeed"))
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
