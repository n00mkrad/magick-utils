using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickUtils
{
    class EffectsTabHelper
    {
        public static bool useGaussNoise;
        public static bool useLapNoise;
        public static bool usePoiNoise;
        public static bool useUniNoise;
        public static int attenMin;
        public static int attenMax;
        public static bool monochrome;

        public static int blurRadiusMin;
        public static int blurRadiusMax;

        public static int medianRadiusMin;
        public static int medianRadiusMax;

        public static void NoisePreview ()
        {
            Program.mainForm.SetNoiseVars();
            FileInfo firstImg = IOUtils.GetFiles()[0];
            string tempImgPath = Path.Combine(IOUtils.GetAppDataDir(), "noisepreview" + firstImg.Extension);
            if(File.Exists(tempImgPath)) File.Delete(tempImgPath);
            File.Copy(firstImg.FullName, tempImgPath);
            Random rand = new Random();
            EffectsUtils.AddNoise(tempImgPath, GetNoiseTypeList(), attenMin, attenMax, monochrome);
            Program.mainForm.PreviewImage(tempImgPath);
        }

        public static void NoiseApply ()
        {
            Program.mainForm.SetNoiseVars();
            EffectsUtils.AddNoiseDir(GetNoiseTypeList(), attenMin, attenMax, monochrome);
        }

        static List<NoiseType> GetNoiseTypeList ()
        {
            List<NoiseType> noiseTypes = new List<NoiseType>();
            if(useGaussNoise) noiseTypes.Add(NoiseType.Gaussian);
            if(useLapNoise) noiseTypes.Add(NoiseType.Laplacian);
            if(usePoiNoise) noiseTypes.Add(NoiseType.Poisson);
            if(useUniNoise) noiseTypes.Add(NoiseType.Uniform);
            return noiseTypes;
        }

        public static void BlurPreview ()
        {
            Program.mainForm.SetBlurVars();
            FileInfo firstImg = IOUtils.GetFiles()[0];
            string tempImgPath = Path.Combine(IOUtils.GetAppDataDir(), "blurpreview" + firstImg.Extension);
            if(File.Exists(tempImgPath)) File.Delete(tempImgPath);
            File.Copy(firstImg.FullName, tempImgPath);
            EffectsUtils.Blur(tempImgPath, blurRadiusMin, blurRadiusMax);
            Program.mainForm.PreviewImage(tempImgPath);
        }

        public static void BlurApply ()
        {
            Program.mainForm.SetBlurVars();
            EffectsUtils.BlurDir(blurRadiusMin, blurRadiusMax);
        }

        public static void MedianPreview()
        {
            Program.mainForm.SetMedianVars();
            FileInfo firstImg = IOUtils.GetFiles()[0];
            string tempImgPath = Path.Combine(IOUtils.GetAppDataDir(), "medianpreview" + firstImg.Extension);
            if (File.Exists(tempImgPath)) File.Delete(tempImgPath);
            File.Copy(firstImg.FullName, tempImgPath);
            EffectsUtils.Median(tempImgPath, medianRadiusMin, medianRadiusMax);
            Program.mainForm.PreviewImage(tempImgPath);
        }

        public static void MedianApply()
        {
            Program.mainForm.SetMedianVars();
            EffectsUtils.MedianDir(medianRadiusMin, medianRadiusMax);
        }
    }
}
