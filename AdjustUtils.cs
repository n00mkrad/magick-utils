using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace MagickUtils
{
    class AdjustUtils
    {
        public static void AutoGamma (string path)
        {
            MagickImage img = IOUtils.ReadImage(path);
            string fname = Path.ChangeExtension(path, null);
            Print("-> " + fname + "\n");
            img.AutoGamma();
            img.Write(path);
        }

        public static void AutoLevel (string path)
        {
            MagickImage img = IOUtils.ReadImage(path);
            string fname = Path.ChangeExtension(path, null);
            Print("-> " + fname + "\n");
            img.AutoLevel();
            img.Write(path);
        }

        public static void AutoThreshold (string path)
        {
            MagickImage img = IOUtils.ReadImage(path);
            string fname = Path.ChangeExtension(path, null);
            Print("-> " + fname + "\n");
            img.AutoThreshold(AutoThresholdMethod.Kapur);
            img.Write(path);
        }

        static void Print (string s)
        {
            Console.WriteLine(s);
        }
    }
}
