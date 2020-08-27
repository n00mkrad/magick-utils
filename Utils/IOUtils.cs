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
        public static bool recursive;
        public static bool exclIncompatible = true;
        public static string nameMustContain;
        public static string nameMustNotContain;

        // Returns a MagickImage but converts the file first if it's not compatible with IM.
        public static MagickImage ReadImage (string path, bool showInfo = true)
        {
            try
            {
                if(Path.GetExtension(path).ToLower() == "flif")   // IM does not support FLIF, so we convert it first
                {
                    return FlifInterface.DecodeToMagickImage(path, false);
                }
                else
                {
                    MagickImage img = new MagickImage(path);
                    if(showInfo)
                        Program.Print("-> Loaded image " + Path.GetFileName(path) + " (" + img.ToString() + ")");
                    return img;
                }
            }
            catch
            {
                Program.Print("Error reading " + Path.GetFileName(path) + "!");
            }
            return null;
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

        public static FileInfo[] GetFiles (bool allFiles = false, string customDir = "")
        {
            string path = Program.currentDir;
            if(!string.IsNullOrWhiteSpace(customDir))
                path = customDir;
            string ext = Program.currentExt;
            bool recursive = IOUtils.recursive;
            Program.Print("Getting file list for " + path + "...");
            var exts = new[] { ".png", ".jpg", ".jpeg", ".dds", ".bmp", ".gif", ".tga", ".webp", ".heic", ".jp2", ".flif" };
            if(!Program.IsPathValid(path))
                return new FileInfo[0];
            IEnumerable<string> filePaths;
            SearchOption rec = SearchOption.AllDirectories;
            SearchOption top = SearchOption.TopDirectoryOnly;
            StringComparison ignCase = StringComparison.OrdinalIgnoreCase;
            if(recursive)
            {
                // Get files recursively and check if the extension is compatible
                if(!allFiles)
                    filePaths = Directory.GetFiles(path, "*." + ext, rec).Where(file => exts.Any(x => file.EndsWith(x, ignCase)));
                else
                    filePaths = Directory.GetFiles(path, "*." + ext, rec);
            }
            else
            {
                // Get files (path root only) and check if the extension is compatible
                if(!allFiles)
                    filePaths = Directory.GetFiles(path, "*." + ext, top).Where(file => exts.Any(x => file.EndsWith(x, ignCase)));
                else
                    filePaths = Directory.GetFiles(path, "*." + ext, top);
            }
            List<FileInfo> fileInfos = new List<FileInfo>();
            // Create FileInfo list from paths + filter
            foreach(string s in filePaths)
            {
                bool notWhitelisted = !string.IsNullOrEmpty(nameMustContain) && !s.Contains(nameMustContain);
                bool blacklisted = !string.IsNullOrEmpty(nameMustNotContain) && s.Contains(nameMustNotContain);
                if(!notWhitelisted && !blacklisted)
                    fileInfos.Add(new FileInfo(s));
            }
            FileInfo[] fileInfoArray = fileInfos.ToArray();
            return fileInfoArray;
        }

        public static string GetAppDataDir ()
        {
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dir = Path.Combine(appDataDir, "MagickUtils");
            Directory.CreateDirectory(dir);
            return dir;
        }

        public static bool IsPathDirectory (string path)
        {
            if(path == null) throw new ArgumentNullException("path");
            path = path.Trim();

            if(Directory.Exists(path))
                return true;

            if(File.Exists(path))
                return false;

            // if has trailing slash then it's a directory
            if(new[] { "\\", "/" }.Any(x => path.EndsWith(x)))
                return true; // ends with slash

            // if has extension then its a file; directory otherwise
            return string.IsNullOrWhiteSpace(Path.GetExtension(path));
        }

        public static bool IsFileValid (string path)
        {
            if(path == null)
                return false;
            if(!File.Exists(path))
                return false;
            //if(new FileInfo(path).Length < 128)  <-- too much of an overhead for small files? maybe benchmark?
                //return false;

            return true;
        }
    }
}
