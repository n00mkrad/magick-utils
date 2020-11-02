using ImageMagick;
using MagickUtils.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickUtils
{
    class InpaintUtils
    {
        public enum PatternType { ThinLines, ThickLines, Rings, Bubbles, Grid }

        public async static void EraseDir(List<PatternType> patterns, int scale, MagickColor color)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            Program.PreProcessing();
            foreach (FileInfo file in files)
            {
                Program.ShowProgress("Adding Erase Markers to Image ", counter, files.Length);
                Erase(file.FullName, patterns, scale, color);
                counter++;
                if (counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(true);
        }

        public static void Erase(string path, List<PatternType> patterns, int scale, MagickColor color)
        {
            if (patterns.Count < 1) return;
            MagickImage img = IOUtils.ReadImage(path);
            if (img == null) return;
            PatternType chosenPattern = GetRandomPatternType(patterns);
            PreProcessing(path, "- Pattern: " + chosenPattern.ToString());
            Random rand = new Random();
            MagickImage patternImg = new MagickImage(GetInpaintImage(chosenPattern));
            patternImg.FilterType = FilterType.Point;
            patternImg.Colorize(color, new Percentage(100));
            patternImg.BackgroundColor = MagickColors.Transparent;
            patternImg.Rotate(RandRange(0, 360));
            double geomW = img.Width * RandRange(1.0f, 2.0f) * GetScaleMultiplier(scale);
            double geomH = img.Height * RandRange(1.0f, 2.0f) * GetScaleMultiplier(scale);
            //Program.Print("geomW: " + geomW + " - geomH: " + geomW);
            MagickGeometry upscaleGeom = new MagickGeometry(Math.Round(geomW) + "x" + Math.Round(geomH) + "!");
            patternImg.Resize(upscaleGeom);
            patternImg.BitDepth(Channels.Alpha, 1);
            img.Composite(patternImg, Gravity.Center, CompositeOperator.Over);
            img.Write(path);
            PostProcessing(img, path, path);
        }

        static double RandRange(double min, double max)
        {
            double output = new Random().NextDouble() * (max - min) + min;
            return output;
        }

        static PatternType GetRandomPatternType(List<PatternType> typesList)
        {
            var random = new Random();
            int index = random.Next(typesList.Count);
            return typesList[index];
        }

        static float GetScaleMultiplier (int scale)
        {
            if(scale == 0) return 0.5f;
            if(scale == 1) return 1.0f;
            if(scale == 2) return 1.5f;
            if(scale == 3) return 2.0f;
            return 1.0f;
        }

        static byte[] GetInpaintImage (PatternType pattern, int res = 1)
        {
            if(pattern == PatternType.ThinLines)
            {
                if (res == 0) return Resources.inpaint_thinlines1_512px;
                if (res == 1) return Resources.inpaint_thinlines1_1024px;
                if (res == 2) return Resources.inpaint_thinlines1_2048px;
            }
            if (pattern == PatternType.ThickLines)
            {
                if (res == 0) return Resources.inpaint_thicklines1_512px;
                if (res == 1) return Resources.inpaint_thicklines1_1024px;
                if (res == 2) return Resources.inpaint_thicklines1_2048px;
            }
            if(pattern == PatternType.Rings)
            {
                if(res == 0) return Resources.inpaint_circles_512px;
                if(res == 1) return Resources.inpaint_circles_1024px;
                if(res == 2) return Resources.inpaint_circles_2048px;
            }
            if (pattern == PatternType.Bubbles)
            {
                if (res == 0) return Resources.inpaint_filledcircles_512px;
                if (res == 1) return Resources.inpaint_filledcircles_1024px;
                if (res == 2) return Resources.inpaint_filledcircles_2048px;
            }
            if (pattern == PatternType.Grid)
            {
                if (res == 0) return Resources.inpaint_grid_512px;
                if (res == 1) return Resources.inpaint_grid_1024px;
                if (res == 2) return Resources.inpaint_grid_2048px;
            }
            return null;
        }

        static void PreProcessing(string path, string infoSuffix = null)
        {
            Program.Print("-> Processing " + Path.GetFileName(path) + " " + infoSuffix);
            Program.sw.Start();
        }

        static void PostProcessing(MagickImage img, string sourcePath, string outPath, bool delSource = false)
        {
            Program.sw.Stop();
            if (img != null)
                img.Dispose();
            long bytesPost = new FileInfo(outPath).Length;
            Program.Print("-> Done.");
            if (delSource)
                DelSource(sourcePath);
        }

        static void DelSource(string path)
        {
            Program.Print("-> Deleting source file: " + Path.GetFileName(path) + "...\n");
            File.Delete(path);
        }
    }
}
