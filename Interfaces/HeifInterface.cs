using Ionic.Zip;
using MagickUtils.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickUtils.Utils;

namespace MagickUtils.Interfaces
{
    class HeifInterface
    {

        static string GetExePath ()
        {
            return Path.Combine(Paths.GetDataPath(), "runtimes", "heif", "heif-enc.exe");
        }

        public static async Task<string> EncodeImage (string path, int q, bool deleteSrc)
        {
            string outPath = Path.ChangeExtension(path, null) + ".heic";
            ProcessStartInfo psi;
            string qualityStr = " -q " + q;
            if (q >= 100) qualityStr = " -L ";
            string args = qualityStr + " -o " + outPath.WrapPath(true, true) + path.WrapPath(true, true);
            psi = new ProcessStartInfo { FileName = GetExePath(), Arguments = args };
            psi.WorkingDirectory = Path.GetDirectoryName(GetExePath());
            Logger.Log("HEIF args:" + args, true);
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process heifProcess = new Process { StartInfo = psi };
            heifProcess.Start();
            heifProcess.WaitForExit();

            if (deleteSrc)
                DelSource(path, outPath);

            return outPath;
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
