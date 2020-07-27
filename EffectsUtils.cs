using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;

namespace MagickUtils
{
    class EffectsUtils
    {
        public async static void AddNoiseDir (List<NoiseType> noiseTypes, double attenuate)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Adding Noise to Image ", counter, files.Length);
                AddNoise(file.FullName, noiseTypes, attenuate);
                counter++;
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(true);
        }

        public static void AddNoise (string path, List<NoiseType> noiseTypes, double attenuate)
        {
            MagickImage img = IOUtils.ReadImage(path);
            NoiseType chosenNoiseType = GetRandomNoiseType(noiseTypes);
            img.AddNoise(chosenNoiseType, attenuate);
            PreProcessing(path, "- Noise Type: " + chosenNoiseType.ToString());
            img.Write(path);
            PostProcessing(img, path, path);
        }

        public static void AddNoiseMonochrome (string path, List<NoiseType> noiseTypes, double attenuate)
        {
            MagickImage img = IOUtils.ReadImage(path);
            NoiseType chosenNoiseType = GetRandomNoiseType(noiseTypes);
            img.AddNoise(chosenNoiseType, attenuate);
            PreProcessing(path, "- Noise Type: " + chosenNoiseType.ToString());
            img.Write(path);
            PostProcessing(img, path, path);
        }

        static NoiseType GetRandomNoiseType (List<NoiseType> typesList)
        {
            var random = new Random();
            int index = random.Next(typesList.Count);
            return typesList[index];
        }

        static void PreProcessing (string path, string infoSuffix = null)
        {
            Program.Print("-> Processing " + Path.GetFileName(path) + " " + infoSuffix);
            Program.sw.Start();
        }

        static void PostProcessing (MagickImage img, string sourcePath, string outPath, bool delSource = false)
        {
            Program.sw.Stop();
            img.Dispose();
            long bytesPost = new FileInfo(outPath).Length;
            //Program.Print("  -> Done. Size pre: " + Format.Filesize(bytesPre) + " - Size post: " + Format.Filesize(bytesPost) + " - Ratio: " + Format.Ratio(bytesPre, bytesPost));
            Program.Print("  -> Done.");
            if(delSource)
                DelSource(sourcePath);
        }

        static void DelSource (string path)
        {
            Program.Print("  -> Deleting source file: " + Path.GetFileName(path) + "...\n");
            File.Delete(path);
        }
    }
}
