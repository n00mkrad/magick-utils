﻿using ImageMagick;
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
            Program.PostProcessing();
        }

        public static void LayerColor (string path, string color)
        {
            MagickImage img = new MagickImage(path);
            MagickColor imgColor = new MagickColor("#" + color);
            MagickImage overlay = new MagickImage(imgColor, img.Width, img.Height);
            img.Composite(overlay, Gravity.Center, CompositeOperator.Over);
            img.Write(path);
        }

        public static async void RemTransparencyDir (byte mode)
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
            Program.PostProcessing();
        }

        public static void RemoveTransparency (string path, byte mode)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if(mode == 0) img.ColorAlpha(MagickColors.Black);
            if(mode == 1) img.ColorAlpha(MagickColors.White);
            if(mode == 2) img.Alpha(AlphaOption.Off);
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
            Program.PostProcessing();
        }

        public static void SetColorDepth (string path, int bits)
        {
            MagickImage img = IOUtils.ReadImage(path);
            img.BitDepth(bits);
            img.Quality = Program.GetDefaultQuality(img);
            img.Write(path);
        }

        public static async void DitherDir (int colorsMin, int colorsMax)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Dithering Image ", counter, files.Length);
                counter++;
                Dither(file.FullName, colorsMin, colorsMax);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static void Dither (string path, int colorsMin, int colorsMax)
        {
            MagickImage img = IOUtils.ReadImage(path);
            QuantizeSettings quantSettings = new QuantizeSettings();
            int colors = new Random().Next(colorsMin, colorsMax);
            Program.Print("-> Colors: " + colors);
            quantSettings.Colors = colors;
            quantSettings.DitherMethod = DitherMethod.FloydSteinberg;
            img.Quantize(quantSettings);
            img.Quality = Program.GetDefaultQuality(img);
            img.Write(path);
        }
    }
}