using MagickUtils.Interfaces;
using MagickUtils.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickUtils.MagickUtils
{
    using IF = Program.ImageFormat;

    class ConvertThreaded
    {
        public static async Task EncodeImages(IF format, int qMin, int qMax, bool deleteSource)
        {
            await EncodeImages(IOUtils.GetFiles(), format, qMin, qMax, deleteSource);
        }

        public static async Task EncodeImages(string[] files, IF format, int qMin, int qMax, bool deleteSource)
        {
            FileInfo[] fileInfos = files.Select(x => new FileInfo(x)).ToArray();
            await EncodeImages(fileInfos, format, qMin, qMax, deleteSource);
        }

        public static async Task EncodeImages (FileInfo[] files, IF format, int qMin, int qMax, bool deleteSource)
        {
            Program.PreProcessing();
            int maxThreads = await Config.GetInt("procThreads");
            List<Task> runningTasks = new List<Task>();
            int done = 0;

            foreach (FileInfo file in files)
            {
                while (runningTasks.Count >= maxThreads)
                {
                    await Task.Delay(100);
                    runningTasks = MtUtils.RemoveCompletedTasks(runningTasks, ref done);
                }

                Task newTask = Task.Run(() => EncodeImage(file, format, qMin, qMax, deleteSource));
                runningTasks.Add(newTask);

                Program.ShowProgress("", done, files.Length);
            }

            while (runningTasks.Count > 0)
            {
                Program.ShowProgress("", done, files.Length);
                runningTasks = MtUtils.RemoveCompletedTasks(runningTasks, ref done);
                await Task.Delay(500);
            }

            Program.PostProcessing(files.Length, true);
        }

        static async Task EncodeImage (FileInfo file, IF format, int qMin, int qMax, bool deleteSource)
        {
            if (format == IF.JPG) await ConvertUtils.ConvertToJpeg(file.FullName, qMin, qMax, deleteSource);
            if (format == IF.PNG) await ConvertUtils.ConvertToPng(file.FullName, qMin, deleteSource);
            if (format == IF.WEBP) await ConvertUtils.ConvertToWebp(file.FullName, qMin, qMax, deleteSource);
            if (format == IF.BMP) await ConvertUtils.ConvertToBmp(file.FullName, deleteSource);
            if (format == IF.DDS) await ConvertUtils.ConvertToDds(file.FullName, qMin, qMax, deleteSource);
            if (format == IF.TGA) await ConvertUtils.ConvertToTga(file.FullName, deleteSource);
            if (format == IF.J2K) await ConvertUtils.ConvertToJpeg2000(file.FullName, qMin, deleteSource);
            if (format == IF.AVIF) await ConvertUtils.ConvertToAvif(file.FullName, qMin, deleteSource);
            if (format == IF.FLIF) await ConvertUtils.ConvertToFlif(file.FullName, qMin, deleteSource);
            if (format == IF.HEIF) await ConvertUtils.ConvertToHeif(file.FullName, qMin, deleteSource);
            if (format == IF.JXL) await ConvertUtils.ConvertToJxl(file.FullName, qMin, qMax, deleteSource);
        }
    }
}
