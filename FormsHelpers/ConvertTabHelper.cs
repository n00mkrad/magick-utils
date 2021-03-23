using MagickUtils.Interfaces;
using MagickUtils.MagickUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagickUtils
{
    using IF = Program.ImageFormat;

    class ConvertTabHelper
    {
        public static void ConvertFileList (string[] files, ComboBox qualityCombox, ComboBox qualityMaxCombox, IF selectedFormat, CheckBox delSrcCbox)
        {
            int qMin = qualityCombox.GetInt();
            int qMax = qMin;
            if(!string.IsNullOrWhiteSpace(qualityMaxCombox.Text.Trim()))
                qMax = qualityMaxCombox.GetInt();

            foreach(string file in files)
            {
                Program.Print("\nDropped File: " + Path.GetFileName(file).Truncate(90));
                if(!IOUtils.IsPathDirectory(file))
                {
                    if(selectedFormat == IF.JPG)
                        ConvertUtils.ConvertToJpeg(file, qMin, qMax, delSrcCbox.Checked);

                    if(selectedFormat == IF.PNG)
                        ConvertUtils.ConvertToPng(file, qMin, delSrcCbox.Checked);

                    if(selectedFormat == IF.DDS)
                        ConvertUtils.ConvertToDds(file, qMin, qMax, delSrcCbox.Checked);

                    if(selectedFormat == IF.TGA)
                        ConvertUtils.ConvertToTga(file, delSrcCbox.Checked);

                    if(selectedFormat == IF.WEBP)
                        ConvertUtils.ConvertToWebp(file, qMin, qMax, delSrcCbox.Checked);

                    if(selectedFormat == IF.J2K)
                        ConvertUtils.ConvertToJpeg2000(file, qMin, delSrcCbox.Checked);

                    if(selectedFormat == IF.FLIF)
                    {
                        if(Config.GetInt("flifEnc") == 1) FlifInterface.EncodeImage(file, qMin, delSrcCbox.Checked);
                        else ConvertUtils.ConvertToFlif(file, qMin, delSrcCbox.Checked);
                    }
                    
                    if (selectedFormat == IF.BMP)
                        ConvertUtils.ConvertToBmp(file, delSrcCbox.Checked);

                    if (selectedFormat == IF.AVIF)
                        ConvertUtils.ConvertToAvif(file, qMin, delSrcCbox.Checked);

                    if (selectedFormat == IF.HEIF)
                        HeifInterface.EncodeImage(file, qMin, delSrcCbox.Checked);

                    if (selectedFormat == IF.JXL)
                        ConvertUtils.ConvertToJxl(file, qMin, qMax, delSrcCbox.Checked);
                }
            }
        }

        public static async Task ConvertUsingPath (ComboBox qualityCombox, ComboBox qualityMaxCombox, IF selectedFormat, CheckBox delSrcCbox)
        {
            int qMin = int.Parse(qualityCombox.Text.Trim());
            int qMax = qMin;
            if(!string.IsNullOrWhiteSpace(qualityMaxCombox.Text.Trim()))
                qMax = int.Parse(qualityMaxCombox.Text.Trim());

            if(selectedFormat == IF.JPG)
                await ConvertThreaded.EncodeImages(IF.JPG, qMin, qMin, delSrcCbox.Checked);

            if (selectedFormat == IF.PNG)
                await ConvertThreaded.EncodeImages(IF.PNG, qMin, qMin, delSrcCbox.Checked);

            if (selectedFormat == IF.DDS)
                await ConvertThreaded.EncodeImages(IF.DDS, qMin, qMin, delSrcCbox.Checked);

            if (selectedFormat == IF.TGA)
                await ConvertThreaded.EncodeImages(IF.TGA, qMin, qMin, delSrcCbox.Checked);

            if (selectedFormat == IF.WEBP)
                ConvertUtils.ConvertDirToWebp(qMin, qMax, delSrcCbox.Checked);

            if(selectedFormat == IF.J2K)
                ConvertUtils.ConvertDirToJpeg2000(qMin, delSrcCbox.Checked);

            if(selectedFormat == IF.FLIF)
                ConvertUtils.ConvertDirToFlif(Config.GetInt("flifEnc") == 1, qMin, delSrcCbox.Checked);

            if (selectedFormat == IF.BMP)
                ConvertUtils.ConvertDirToBmp(delSrcCbox.Checked);

            if (selectedFormat == IF.AVIF)
                ConvertUtils.ConvertDirToAvif(qMin, delSrcCbox.Checked);

            if (selectedFormat == IF.HEIF)
                ConvertUtils.ConvertDirToHeif(qMin, delSrcCbox.Checked);

            if (selectedFormat == IF.JXL)
                ConvertUtils.ConvertDirToJxl(qMin, qMax, delSrcCbox.Checked);
        }
    }
}
