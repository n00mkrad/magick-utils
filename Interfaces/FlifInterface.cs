using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ImageMagick;
using MagickUtils.Properties;
using MagickUtils.Utils;
using Paths = MagickUtils.Utils.Paths;
using System.Threading.Tasks;

namespace MagickUtils
{
    class FlifInterface
    {
        public static int effort = 60;


        static string GetExePath ()
        {
            return Path.Combine(Paths.GetDataPath(), "runtimes", "flif", "flif.exe");
        }

        public static async Task<string> EncodeImage (string path, int q, bool deleteSrc)
        {
            string outPath = Path.ChangeExtension(path, null) + ".flif";
            ProcessStartInfo psi;
            string args1 = " -e -Q" + q + " -E" + effort + " --overwrite";
            string args2 = " \"" + path + "\" \"" + outPath + "\"";
            psi = new ProcessStartInfo { FileName = GetExePath(), Arguments = args1 + args2 };
            psi.WorkingDirectory = Path.GetDirectoryName(GetExePath());
            Logger.Log("FLIF args:" + args1, true);
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process flifProcess = new Process { StartInfo = psi };
            flifProcess.Start();
            flifProcess.WaitForExit();

            if(deleteSrc)
                DelSource(path, outPath);

            return outPath;
        }

        public static string DecodeImage (string path, bool deleteSrc, bool tempExtension = false)
        {
            string outPath = Path.ChangeExtension(path, null) + ".png";
            if(tempExtension) outPath = Path.ChangeExtension(path, null) + ".pngtmp";
            ProcessStartInfo psi;
            string args = " -d \"" + path + "\" \"" + outPath + "\"";
            psi = new ProcessStartInfo { FileName = GetExePath(), Arguments = args };
            psi.WorkingDirectory = Path.GetDirectoryName(GetExePath());
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process flifProcess = new Process { StartInfo = psi };
            flifProcess.Start();
            flifProcess.WaitForExit();
            if (deleteSrc)
                DelSource(path, outPath);

            return outPath;
        }

        public static MagickImage DecodeToMagickImage (string path, bool deleteSrc)
        {
            string outPath = DecodeImage(path, deleteSrc, true);
            MagickImage image = new MagickImage(File.ReadAllBytes(outPath));  // Store images as bytes so we can delete the actual file
            File.Delete(outPath);
            return image;
        }

        static void DelSource(string sourcePath, string newPath)
        {
            if (Path.GetExtension(sourcePath).ToLower() == Path.GetExtension(newPath).ToLower())
            {
                Logger.Log("-> Not deleting " + Path.GetFileName(sourcePath) + " as it was overwritten");
                return;
            }
            Logger.Log("-> Deleting source file: " + Path.GetFileName(sourcePath) + "...");
            File.Delete(sourcePath);
        }
    }
}
