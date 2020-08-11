using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickUtils
{
    class CropUtils
    {
        public static async void CropDivisibleDir (int divisibleBy)
        {
            int counter = 1;
            FileInfo[] Files = IOUtils.GetFiles();
            Program.Print("Cropping " + Files.Length + " images...");
            Program.PreProcessing();
            foreach (FileInfo file in Files)
            {
                Program.ShowProgress("Cropping Image ", counter, Files.Length);
                counter++;
                CropDivisible(file.FullName, divisibleBy);
                if (counter % 3 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static void CropDivisible (string path, int divisibleBy)
        {
            MagickImage img = IOUtils.ReadImage(path);

            int divisbleWidth = img.Width;
            while (divisbleWidth % divisibleBy != 0)
                divisbleWidth--;

            int divisibleHeight = img.Height;
            while (divisibleHeight % divisibleBy != 0)
                divisibleHeight--;

            if(divisbleWidth == img.Width && divisibleHeight == img.Height)
            {
                Program.Print("-> Skipping " + Path.GetFileName(path) + " as its resolution is already divisible by " + divisibleBy);
            }
            else
            {
                img.Crop(divisbleWidth, divisibleHeight, Gravity.Center);
                img.RePage();
                img.Write(path);
            }
        }
    }
}
