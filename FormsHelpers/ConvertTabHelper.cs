using MagickUtils.Interfaces;
using MagickUtils.MagickUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagickUtils.Utils;

namespace MagickUtils
{
    using IF = Program.ImageFormat;

    class ConvertTabHelper
    {
        public static async Task ConvertFileList (string[] files, ComboBox qualityCombox, ComboBox qualityMaxCombox, IF selectedFormat, CheckBox delSrcCbox)
        {
            int qMin = qualityCombox.GetInt();
            int qMax = qMin;
            if(!string.IsNullOrWhiteSpace(qualityMaxCombox.Text.Trim()))
                qMax = qualityMaxCombox.GetInt();

            foreach(string file in files)
            {
                Logger.Log("\nDropped File: " + Path.GetFileName(file).Truncate(90));
                if(!IOUtils.IsPathDirectory(file))
                {
                    if(selectedFormat == IF.JPG)
                        await ConvertUtils.ConvertToJpeg(file, qMin, qMax, delSrcCbox.Checked);

                    if(selectedFormat == IF.PNG)
                        await ConvertUtils.ConvertToPng(file, qMin, delSrcCbox.Checked);

                    if(selectedFormat == IF.DDS)
                        await ConvertUtils.ConvertToDds(file, qMin, qMax, delSrcCbox.Checked);

                    if(selectedFormat == IF.TGA)
                        await ConvertUtils.ConvertToTga(file, delSrcCbox.Checked);

                    if(selectedFormat == IF.WEBP)
                        await ConvertUtils.ConvertToWebp(file, qMin, qMax, delSrcCbox.Checked);

                    if(selectedFormat == IF.J2K)
                        await ConvertUtils.ConvertToJpeg2000(file, qMin, delSrcCbox.Checked);

                    if(selectedFormat == IF.FLIF)
                    {
                        if(await Config.GetInt("flifEnc") == 1) await FlifInterface.EncodeImage(file, qMin, delSrcCbox.Checked);
                        else await ConvertUtils.ConvertToFlif(file, qMin, delSrcCbox.Checked);
                    }
                    
                    if (selectedFormat == IF.BMP)
                        await ConvertUtils.ConvertToBmp(file, delSrcCbox.Checked);

                    if (selectedFormat == IF.AVIF)
                        await ConvertUtils.ConvertToAvif(file, qMin, delSrcCbox.Checked);

                    if (selectedFormat == IF.HEIF)
                        await HeifInterface.EncodeImage(file, qMin, delSrcCbox.Checked);

                    if (selectedFormat == IF.JXL)
                        await ConvertUtils.ConvertToJxl(file, qMin, qMax, delSrcCbox.Checked);
                }
            }
        }

        public static async Task ConvertUsingPath (ComboBox qualityCombox, ComboBox qualityMaxCombox, IF selectedFormat, CheckBox delSrcCbox)
        {
            int qMin = qualityCombox.GetInt();
            int qMax = qMin;

            if(!string.IsNullOrWhiteSpace(qualityMaxCombox.Text.Trim()))
                qMax = qualityMaxCombox.GetInt();

            await ConvertThreaded.EncodeImages(selectedFormat, qMin, qMin, delSrcCbox.Checked);
        }
    }
}
