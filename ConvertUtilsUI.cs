using System;
using System.IO;
using System.Diagnostics;

namespace MagickUtils
{
    class ConvertUtilsUI
    {
        public static async void ConvertDirToJpeg (int qMin, int qMax, bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                ConvertUtils.ConvertToJpegRandomQuality(file.FullName, qMin, qMax, delSrc);
                counter++;
                if(counter % 20 == 0) await Program.PutTaskDelay();
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
                ConvertUtils.ConvertToPng(file.FullName, q, delSrc);
                counter++;
                if(counter % 5 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(true);
        }


        public static void ConvertDirToDds (bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                counter++;
                ConvertUtils.ConvertToDds(file.FullName, delSrc);
            }
            Program.PostProcessing();
        }

        public static void ConvertDirToDdsCrunch (int qMin, int qMax, bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Crunching Image ", counter, files.Length);
                counter++;
                CrunchInterface.CrunchImage(file.FullName, qMin, qMax, delSrc);
            }
            Program.PostProcessing();
        }

        public static void ConvertDirToTga (bool delSrc)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Converting Image ", counter, files.Length);
                counter++;
                ConvertUtils.ConvertToTga(file.FullName, delSrc);
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
                ConvertUtils.ConvertToWebp(file.FullName, q, delSrc);
                if(counter % 5 == 0) await Program.PutTaskDelay();
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
                ConvertUtils.ConvertToJpeg2000(file.FullName, q, delSrc);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }
    }
}
