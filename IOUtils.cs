using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ImageMagick;

namespace MagickUtils
{
    class IOUtils
    {
        // Returns a MagickImage but converts the file first if it's not compatible with IM.
        public static MagickImage ReadImage (string path)
        {
            if(Path.GetExtension(path) == "flif")   // IM does not support FLIF, so we convert it first
            {
                return FlifInterface.DecodeToMagickImage(path, false);
            }
            else
            {
                return new MagickImage(path);
            }
        }

        public static long GetDirSize (DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach(FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach(DirectoryInfo di in dis)
            {
                size += GetDirSize(di);
            }
            return size;
        }

        public static FileInfo[] GetFiles ()
        {
            string path = Program.currentDir;
            string ext = Program.currentExt;
            bool recursive = Program.recursive;
            Stopwatch getFilesSw = new Stopwatch(); getFilesSw.Start();
            Program.Print("Getting file list...");
            var exts = new[] { ".png", ".jpg", ".jpeg", ".dds", ".bmp", ".tga", ".webp", ".heic", ".jp2", ".flif" };
            if(!Program.IsPathValid(path))
            {
                MessageBox.Show("Invalid path!", "Error");
                return new FileInfo[0];
            }
            IEnumerable<string> filePaths;
            SearchOption rec = SearchOption.AllDirectories;
            SearchOption top = SearchOption.TopDirectoryOnly;
            StringComparison ignCase = StringComparison.OrdinalIgnoreCase;
            if(recursive)
            {
                if(Program.exclIncompatible)
                    filePaths = Directory.GetFiles(path, "*." + ext, rec).Where(file => exts.Any(x => file.EndsWith(x, ignCase)));
                else
                    filePaths = Directory.GetFiles(path, "*." + ext, rec);
            }
            else
            {
                if(Program.exclIncompatible)
                    filePaths = Directory.GetFiles(path, "*." + ext, top).Where(file => exts.Any(x => file.EndsWith(x, ignCase)));
                else
                    filePaths = Directory.GetFiles(path, "*." + ext, top);
            }
            List<FileInfo> fileInfos = new List<FileInfo>();
            foreach(string s in filePaths)
                fileInfos.Add(new FileInfo(s));
            FileInfo[] fileInfoArray = fileInfos.ToArray();
            Program.Print("Got file list in " + Format.TimeSw(getFilesSw));
            return fileInfoArray;
        }

        public static string GetAppDataDir ()
        {
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dir = Path.Combine(appDataDir, "MagickUtils");
            Directory.CreateDirectory(dir);
            return dir;
        }
    }
}
