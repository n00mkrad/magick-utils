
using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickUtils.MagickUtils
{
    class GeometryUtils
    {
        public enum RotateMode { Rot90, Rot180, Rot270, Random, RandomAll }
        public static async void RotateDir (RotateMode mode)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Rotating Image ", counter, files.Length);
                counter++;
                Rotate(file.FullName, mode);
                if(counter % 3 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static void Rotate (string path, RotateMode mode)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if(mode == RotateMode.Rot90)
                img.Rotate(90);
            if(mode == RotateMode.Rot180)
                img.Rotate(180);
            if(mode == RotateMode.Rot270)
                img.Rotate(270);
            if(mode == RotateMode.Random)
                img.Rotate(GetRandomRot(false));
            if(mode == RotateMode.RandomAll)
                img.Rotate(GetRandomRot(true));

            img.Write(path);
        }

        static int GetRandomRot (bool includeZero = false)
        {
            int[] values = new int[] { 90, 180, 270 };
            if(includeZero) values = new int[] { 0, 90, 180, 270 };
            Random random = new Random();
            int i = random.Next(0, values.Length);
            return values[i];
        }

        public static async void FlipDir (FlipMode mode)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Flipping Image ", counter, files.Length);
                counter++;
                Flip(file.FullName, mode);
                if(counter % 3 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public enum FlipMode { Hor, Vert, Random }
        public static void Flip (string path, FlipMode mode)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if(mode == FlipMode.Hor)
            {
                img.Flop();
            } 
            if(mode == FlipMode.Vert)
            {
                img.Flip();
            }
            if(mode == FlipMode.Random)
            {
                if(new Random().Next(2) == 0)
                    img.Flip();
                else
                    img.Flop();
            }
                
            string fname = Path.GetFileName(path);
            Program.Print("-> " + fname);
            img.Write(path);
        }
    }
}
