using System;
using System.IO;

namespace MagickUtils
{
    class IOUtils
    {
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
    }
}
