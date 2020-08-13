using System.IO;

namespace MagickUtils
{
    class ScaleUtilsUI
    {
        public static async void ResampleDirRand (int sMin, int sMax, int randFilterMode)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Resampling Image ", counter, files.Length);
                counter++;
                ScaleUtils.RandomResample(file.FullName, sMin, sMax, randFilterMode);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
        }

        public static async void ScaleDir (int sMin, int sMax, int filterMode)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Scaling Image ", counter, files.Length);
                counter++;
                ScaleUtils.Scale(file.FullName, sMin, sMax, filterMode);
                /* if(counter % 3 == 0) */ await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }
    }
}
