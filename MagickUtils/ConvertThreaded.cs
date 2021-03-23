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
        public enum Encoder { JpgMagick, JpgMoz, Png, DdsMagick, DdsNv, DdsCrunch, Tga, Webp, Bmp, Avif, J2k, FlifMagick, FlifExe, HeifExe, Jxl }

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

            List<Task> runningTasks = new List<Task>();
            int done = 0;

            foreach (FileInfo file in files)
            {
                while (runningTasks.Count >= Environment.ProcessorCount)
                {
                    await Task.Delay(100);
                    runningTasks = TaskTools.RemoveCompletedTasks(runningTasks, ref done);
                }

                Task newTask = Task.Run(() => EncodeImage(file, format, qMin, qMax, deleteSource));
                runningTasks.Add(newTask);

                Program.ShowProgress("", done, files.Length);
            }

            while (runningTasks.Count > 0)
            {
                Program.ShowProgress("", done, files.Length);
                runningTasks = TaskTools.RemoveCompletedTasks(runningTasks, ref done);
                await Task.Delay(500);
            }

            Program.PostProcessing(true);
        }

        static async Task EncodeImage (FileInfo file, IF format, int qMin, int qMax, bool deleteSource)
        {
            if (format == IF.JPG) ConvertUtils.ConvertToJpeg(file.FullName, qMin, qMax, deleteSource);
            if (format == IF.PNG) ConvertUtils.ConvertToPng(file.FullName, qMin, deleteSource);
            if (format == IF.DDS) ConvertUtils.ConvertToDds(file.FullName, qMin, qMax, deleteSource);
            if (format == IF.TGA) ConvertUtils.ConvertToTga(file.FullName, deleteSource);
        }
    }
}
