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

        static void GetPaths()
        {
            nvCompExePath = Path.Combine(Paths.GetDataPath(), "runtimes", "dds", "nvcompress.exe");
            crunchExePath = Path.Combine(Paths.GetDataPath(), "runtimes", "dds", "crunch.exe");
            texconvExePath = Path.Combine(Paths.GetDataPath(), "runtimes", "dds", "texconv.exe");
        }

        public static async Task Crunch(string inPath, int qMin, int qMax)
        {
            GetPaths();
            SetEncSpeed();

            bool convert = !ConvertUtils.IsPng(inPath);
            if (convert) inPath = await ConvertUtils.ConvertToTempPng(inPath);

            string dxtString = (await Config.Get("ddsCompressionType")).Split(' ')[0].ToUpper().Replace("ARGB", "A8R8G8B8");;
            string mipMode = (await Config.GetBool("ddsEnableMips")) ? "UseSourceOrGenerate"  : "None";
            string args = $" -file {inPath.Wrap()} -outsamedir";
            string args2 = $" -quality {GetRandomQuality(qMin, qMax)} -fileformat dds -mipMode {mipMode} -dxtQuality {crunchPreset} {dxtString}";
            ProcessStartInfo psi = new ProcessStartInfo { FileName = crunchExePath, Arguments = args + args2, WindowStyle = ProcessWindowStyle.Hidden };
            Logger.Log("Running Crunch:" + args2, true);
            Process crunchProcess = new Process { StartInfo = psi };
            crunchProcess.Start();
            crunchProcess.WaitForExit();

            if (convert) IOUtils.TryDeleteIfExists(inPath);
        }

        public static async Task NvCompress(string inPath, string outpath)
        {
            GetPaths();
            string sourcePath = inPath;

            bool convert = !ConvertUtils.IsPng(inPath);
            if (convert) inPath = await ConvertUtils.ConvertToTempPng(inPath);

            string dxtString = (await Config.Get("ddsCompressionType")).Split(' ')[0].ToLower().Replace("argb", "rgb");

            string mipStr = "-nomips";
            if (await Config.GetBool("ddsEnableMips")) mipStr = "";

            string args = $" -{dxtString} -alpha {mipStr} {inPath.Wrap()} {outpath.Wrap()}";
            ProcessStartInfo psi = new ProcessStartInfo { FileName = nvCompExePath, Arguments = args, WindowStyle = ProcessWindowStyle.Hidden };
            Process nvCompress = new Process { StartInfo = psi };
            Logger.Log("-> Running NvCompress:" + args.Split('"')[0], true);
            nvCompress.Start();
            nvCompress.WaitForExit();

            if (convert) IOUtils.TryDeleteIfExists(inPath);
        }

        public static async Task Texconv (string inPath)
        {
            GetPaths();

            string[] supported = new string[] { "png", "jpg", "jpeg", "webp", "dds", "bmp" };
            bool convert = !supported.Contains(Path.GetExtension(inPath).ToLower().Replace(".", ""));
            if (convert) inPath = await ConvertUtils.ConvertToTempPng(inPath);

            string format = (await Config.Get("ddsCompressionType")).Split(' ')[0].ToLower();
            format = format.Replace("argb", "R8G8B8A8_UNORM").Replace("bc1", "BC1_UNORM").Replace("bc2", "BC2_UNORM").Replace("bc3", "BC3_UNORM")
                .Replace("bc4", "BC4_UNORM").Replace("bc5", "BC5_UNORM").Replace("bc6", "BC6H_UF16").Replace("bc7", "BC7_UNORM");
            string mips = (await Config.GetBool("ddsEnableMips")) ? "-m 4" : "-m 0";
            string args = $" -y -nologo {mips} -f {format} -o {inPath.GetParentDir().Wrap()} {inPath.Wrap()}";
            ProcessStartInfo psi = new ProcessStartInfo { FileName = texconvExePath, Arguments = args, WindowStyle = ProcessWindowStyle.Hidden };
            Process crunchProcess = new Process { StartInfo = psi };
            crunchProcess.Start();
            crunchProcess.WaitForExit();

            if (convert) IOUtils.TryDeleteIfExists(inPath);
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
