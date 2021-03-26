using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickUtils.MagickUtils
{
    class ColorUtils
    {

        public static async void LayerColorDir (string color)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Overlaying color on image ", counter, files.Length);
                counter++;
                LayerColor(file.FullName, color);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(files.Length);
        }

        public static void LayerColor (string path, string color)
        {
            MagickImage img = new MagickImage(path);
            MagickColor imgColor = new MagickColor("#" + color);
            MagickImage overlay = new MagickImage(imgColor, img.Width, img.Height);
            img.Composite(overlay, Gravity.Center, CompositeOperator.Over);
            img.Write(path);
        }

        public static async void RemTransparencyDir (NoAlphaMode mode)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();

            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Removing Alpha on Image ", counter, files.Length);
                counter++;
                RemoveTransparency(file.FullName, mode);
                if(counter % 5 == 0) await Program.PutTaskDelay();
            }

            Program.PostProcessing(files.Length);
        }

        public enum NoAlphaMode { Off, Fill }
        public static void RemoveTransparency (string path, NoAlphaMode mode)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            if (mode == NoAlphaMode.Fill)
            {
                MagickImage bg = new MagickImage(new MagickColor("#" + Config.Get("backgroundColor")), img.Width, img.Height);
                bg.BackgroundColor = new MagickColor("#" + Config.Get("backgroundColor"));
                bg.Composite(img, CompositeOperator.Over);
                img = bg;
            }
            if (mode == NoAlphaMode.Off)
                img.Alpha(AlphaOption.Off);
            img.Write(path);
        }


        public static async void SetColorDepthDir (int bits)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Setting color depth to " + bits + " on image ", counter, files.Length);
                counter++;
                SetColorDepth(file.FullName, bits);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(files.Length);
        }

        public static void SetColorDepth (string path, int bits)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.BitDepth(bits);
            img.Quality = Program.GetDefaultQuality(img);
            img.Write(path);
        }

        public enum DitherType { FloydSteinberg, Riemersma, Ordered4x4, Halftone4x4, Random }
        public static async void DitherDir (int colorsMin, int colorsMax, DitherType type)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Dithering Image ", counter, files.Length);
                counter++;
                Dither(file.FullName, colorsMin, colorsMax, type);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(files.Length);
        }

        public static void Dither (string path, int colorsMin, int colorsMax, DitherType type)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            int colors = new Random().Next(colorsMin, colorsMax);
            //Program.Print("-> Colors: " + colors + ", Dither Method: " + quantSettings.DitherMethod.ToString());
            DitherWithMethod(img, colors, type);
            img.Quality = Program.GetDefaultQuality(img);
            img.Write(path);
        }

        static void DitherWithMethod (MagickImage img, int colors, DitherType type)
        {
            if(type != DitherType.Random)
                Program.Print("-> Colors: " + colors + ", Dither Method: " + type.ToString());

            if (type == DitherType.FloydSteinberg)
                img.Quantize(new QuantizeSettings { Colors = colors, DitherMethod = DitherMethod.FloydSteinberg });

            if (type == DitherType.Riemersma)
                img.Quantize(new QuantizeSettings { Colors = colors, DitherMethod = DitherMethod.Riemersma });

            if (type == DitherType.Ordered4x4)
                img.OrderedDither("o4x4");

            if (type == DitherType.Halftone4x4)
                img.OrderedDither("h4x4a");

            var random = new Random();
            int i = random.Next(0, 4);

            if (type == DitherType.Random)
            {
                if (i == 0)
                    DitherWithMethod(img, colors, DitherType.FloydSteinberg);
                if (i == 1)
                    DitherWithMethod(img, colors, DitherType.Riemersma);
                if (i == 2)
                    DitherWithMethod(img, colors, DitherType.Ordered4x4);
                if (i == 3)
                    DitherWithMethod(img, colors, DitherType.Halftone4x4);
            }

        }

        public static async void DeleteGrayscaleImgDir (float thresh, bool invert)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();
            foreach (FileInfo file in files)
            {
                Program.ShowProgress("", counter, files.Length);
                counter++;
                DeleteGrayscaleImg(file.FullName, thresh, invert);
                if (counter % 5 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(files.Length);
        }

        public static void DeleteGrayscaleImg (string path, float thresh, bool invert)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            img.ColorSpace = ColorSpace.HSL;
            float saturation = 100f;
            using (IMagickImage channel = img.Separate(Channels.Gray).First())
            {
                saturation = channel.FormatExpression("%[fx:mean]").GetFloat();
                Program.Print("Image Saturation: " + saturation.ToString("0.00"));
            }

            if (!invert && saturation < thresh)
            {
                Program.Print("Deleting " + Path.GetFileName(path));
                File.Delete(path);
            }
                
            if (invert && saturation > thresh)
            {
                Program.Print("Deleting " + Path.GetFileName(path));
                File.Delete(path);
            }

        }
    }
}
