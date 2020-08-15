using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagickUtils.MagickUtils
{
    class TilingUtils
    {
        public static async void TileDir (int tilesize)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();

            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Resampling Image ", counter, files.Length);
                counter++;
                Tile(file.FullName, tilesize);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
        }

        public static void Tile (string path, int tilesize)
        {
            MagickImage img = new MagickImage(path);
            int currOffsetX = 0;
            int currOffsetY = 0;
            while(true)
            {
                //MagickImage tile = new MagickImage(tilesize, tilesize);
            }
        }
    }
}
