using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;

namespace MagickUtils
{
    class EffectsUtils
    {
        public async static void AddNoiseDir (List<NoiseType> noiseTypes, double attenMin, double attenMax, bool monoChrome)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Adding Noise to Image ", counter, files.Length);
                AddNoise(file.FullName, noiseTypes, attenMin, attenMax, monoChrome);
                counter++;
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(true);
        }

        public static void AddNoise (string path, List<NoiseType> noiseTypes, double attenMin, double attenMax, bool monoChrome)
        {
            if(noiseTypes.Count < 1) return;
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            NoiseType chosenNoiseType = GetRandomNoiseType(noiseTypes);
            PreProcessing(path, "- Noise Type: " + chosenNoiseType.ToString());
            Random rand = new Random();
            double att = (double)rand.Next((int)attenMin, (int)attenMax + 1);
            Program.Print("-> Using attenuate factor " + att);
            if(monoChrome)
            {
                MagickImage noiseImg = new MagickImage(MagickColors.White, img.Width, img.Height);
                noiseImg.AddNoise(chosenNoiseType, att);
                noiseImg.ColorSpace = ColorSpace.LinearGray;
                noiseImg.Write(Path.Combine(IOUtils.GetAppDataDir(), "lastnoiseimg.png"));
                img.Composite(noiseImg, CompositeOperator.Multiply);
            }
            else
            {
                img.AddNoise(chosenNoiseType, att);
            }
            img.Write(path);
            PostProcessing(img, path, path);
        }

        static NoiseType GetRandomNoiseType (List<NoiseType> typesList)
        {
            var random = new Random();
            int index = random.Next(typesList.Count);
            return typesList[index];
        }

        public static async void BlurDir (int radiusMin, int radiusMax)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Blurring Image ", counter, files.Length);
                Blur(file.FullName, radiusMin, radiusMax);
                counter++;
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(true);
        }

        public static void Blur (string path, int radiusMin, int radiusMax)
        {
            PreProcessing(path);
            MagickImage sourceImg = new MagickImage(path);
            var image = Image.FromFile(path);
            var blur = new SuperfastBlur.GaussianBlur(image as Bitmap);
            Random rand = new Random();
            int blurRad = rand.Next(radiusMin, radiusMax + 1);
            Program.Print("-> Using blur radius " + blurRad);
            var result = blur.Process(blurRad);
            string tempPath = Path.Combine(IOUtils.GetAppDataDir(), "blur-out.png");
            result.Save(tempPath, ImageFormat.Png);
            result.Dispose();
            image.Dispose();
            MagickImage outImg = new MagickImage(tempPath);
            outImg.Format = sourceImg.Format;
            outImg.Write(path);
            PostProcessing(null, path, path);
        }

        public async static void EdgeDetectDir ()
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Running Edge Detection on Image ", counter, files.Length);
                EdgeDetect(file.FullName);
                counter++;
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(true);
        }

        public static void EdgeDetect (string path)
        {
            PreProcessing(path);
            MagickImage img = new MagickImage(path);
            img.CannyEdge(0, 1, new Percentage(5), new Percentage(20));
            img.Negate();
            img.Write(path);
            PostProcessing(null, path, path);
        }

        public async static void MedianDir(int radiusMin, int radiusMax)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach (FileInfo file in files)
            {
                Program.ShowProgress("Running Median Filter on Image ", counter, files.Length);
                Median(file.FullName, radiusMin, radiusMax);
                counter++;
                if (counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(true);
        }

        public static void Median(string path, int radiusMin, int radiusMax)
        {
            PreProcessing(path);
            MagickImage img = new MagickImage(path);
            Random rand = new Random();
            int radius = rand.Next(radiusMin, radiusMax + 1);
            Program.Print("-> Using median radius " + radius);
            img.MedianFilter(radius);
            img.Write(path);
            PostProcessing(null, path, path);
        }

        public async static void HaloDir(int radiusMin, int radiusMax)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach (FileInfo file in files)
            {
                Program.ShowProgress("Running Halo Effect on Image ", counter, files.Length);
                Halo(file.FullName, radiusMin, radiusMax);
                counter++;
                if (counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(true);
        }

        public static void Halo(string path, int radiusMin, int radiusMax)
        {
            PreProcessing(path);
            MagickImage img = new MagickImage(path);
            Random rand = new Random();
            int radius = rand.Next(radiusMin, radiusMax + 1);
            Program.Print("-> Using halo intensity " + radius);

            MagickImage sharpenImg = new MagickImage(img);
            sharpenImg.Sharpen(0, radius);
            img.Composite(sharpenImg, CompositeOperator.Lighten);

            img.Write(path);
            PostProcessing(null, path, path);
        }

        static void PreProcessing (string path, string infoSuffix = null)
        {
            Program.Print("-> Processing " + Path.GetFileName(path) + " " + infoSuffix);
            Program.sw.Start();
        }

        static void PostProcessing (MagickImage img, string sourcePath, string outPath, bool delSource = false)
        {
            Program.sw.Stop();
            if(img != null)
                img.Dispose();
            long bytesPost = new FileInfo(outPath).Length;
            //Program.Print("-> Done. Size pre: " + Format.Filesize(bytesPre) + " - Size post: " + Format.Filesize(bytesPost) + " - Ratio: " + Format.Ratio(bytesPre, bytesPost));
            Program.Print("Done.");
            if(delSource)
                DelSource(sourcePath);
        }

        static void DelSource (string path)
        {
            Program.Print("-> Deleting source file: " + Path.GetFileName(path) + "...\n");
            File.Delete(path);
        }
    }
}
