using Ionic.Zip;
using MagickUtils.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickUtils.Interfaces
{
    class HeifInterface
    {
        static string heifExePath;
        static string heifResPath;

        public static void Extract(bool overwrite = false)
        {
            GetPaths();

            if (File.Exists(heifExePath) && !overwrite)
                return;

            File.WriteAllBytes(heifResPath, Resources.heif);
            ZipFile zip = ZipFile.Read(heifResPath);
            foreach (ZipEntry e in zip)
                e.Extract(IOUtils.GetAppDataDir(), ExtractExistingFileAction.OverwriteSilently);

            Program.Print("[HeifInterface] Extracted HEIF resources to " + heifExePath);
        }

        static void GetPaths()
        {
            heifResPath = Path.Combine(IOUtils.GetAppDataDir(), "heif.zip");
            heifExePath = Path.Combine(IOUtils.GetAppDataDir(), "heif", "heif-enc.exe");
        }

        public static string EncodeImage (string path, int q, bool deleteSrc)
        {
            Extract();
            string outPath = Path.ChangeExtension(path, null) + ".heic";
            ProcessStartInfo psi;
            string qualityStr = " -q " + q;
            if (q >= 100) qualityStr = " -L ";
            string args = qualityStr + " -o " + outPath.WrapPath(true, true) + path.WrapPath(true, true);
            psi = new ProcessStartInfo { FileName = heifExePath, Arguments = args };
            psi.WorkingDirectory = Path.GetDirectoryName(heifExePath);
            Program.Print("HEIF args:" + args);
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process heifProcess = new Process { StartInfo = psi };
            heifProcess.Start();
            heifProcess.WaitForExit();
            Program.Print("Done converting " + path);
            if (deleteSrc)
                DelSource(path, outPath);
            return outPath;
        }

        static void DelSource(string sourcePath, string newPath)
        {
            if (Path.GetExtension(sourcePath).ToLower() == Path.GetExtension(newPath).ToLower())
            {
                Program.Print("-> Not deleting " + Path.GetFileName(sourcePath) + " as it was overwritten");
                return;
            }
            Program.Print("-> Deleting source file: " + Path.GetFileName(sourcePath) + "...");
            File.Delete(sourcePath);
        }
    }
}
