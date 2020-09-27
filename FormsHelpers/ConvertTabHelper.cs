using System;
using System.Collections.Generic;
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
            int qMin = int.Parse(qualityCombox.Text.Trim());
            int qMax = qMin;
            if(!string.IsNullOrWhiteSpace(qualityMaxCombox.Text.Trim()))
                qMax = int.Parse(qualityMaxCombox.Text.Trim());

            foreach(string file in files)
            {
                Program.Print("Convert Dragndrop: " + file);
                if(!IOUtils.IsPathDirectory(file))
                {
                    if(selectedFormat == IF.JPG)
                        ConvertUtils.ConvertToJpegRandomQuality(file, qMin, qMax, delSrcCbox.Checked);

                    if(selectedFormat == IF.PNG)
                        ConvertUtils.ConvertToPngIM(file, qMin, delSrcCbox.Checked);

                    if(selectedFormat == IF.DDS)
                    {
                        if(FormatOptions.ddsUseCrunch) ConvertUtils.ConvertDirToDdsCrunch(qMin, qMax, delSrcCbox.Checked);
                        else ConvertUtils.ConvertToDds(file, delSrcCbox.Checked);
                    }

                    if(selectedFormat == IF.TGA)
                        ConvertUtils.ConvertToTga(file, delSrcCbox.Checked);

                    if(selectedFormat == IF.WEBP)
                        ConvertUtils.ConvertToWebp(file, qMin, delSrcCbox.Checked);

                    if(selectedFormat == IF.J2K)
                        ConvertUtils.ConvertToJpeg2000(file, qMin, delSrcCbox.Checked);

                    if(selectedFormat == IF.FLIF)
                        FlifInterface.EncodeImage(file, qMin, delSrcCbox.Checked);

                    if (selectedFormat == IF.BMP)
                        ConvertUtils.ConvertToBmp(file, delSrcCbox.Checked);

                    if (selectedFormat == IF.AVIF)
                        ConvertUtils.ConvertToAvif(file, qMin, delSrcCbox.Checked);
                }
            }
        }

        public static void ConvertUsingPath (ComboBox qualityCombox, ComboBox qualityMaxCombox, IF selectedFormat, CheckBox delSrcCbox)
        {
            int qMin = int.Parse(qualityCombox.Text.Trim());
            int qMax = qMin;
            if(!string.IsNullOrWhiteSpace(qualityMaxCombox.Text.Trim()))
                qMax = int.Parse(qualityMaxCombox.Text.Trim());

            if(selectedFormat == IF.JPG)
                ConvertUtils.ConvertDirToJpeg(qMin, qMax, delSrcCbox.Checked);

            if(selectedFormat == IF.PNG)
                ConvertUtils.ConvertDirToPng(qMin, delSrcCbox.Checked);

            if(selectedFormat == IF.DDS)
            {
                if(FormatOptions.ddsUseCrunch) ConvertUtils.ConvertDirToDdsCrunch(qMin, qMax, delSrcCbox.Checked);
                else ConvertUtils.ConvertDirToDds(delSrcCbox.Checked);
            }

            if(selectedFormat == IF.TGA)
                ConvertUtils.ConvertDirToTga(delSrcCbox.Checked);

            if(selectedFormat == IF.WEBP)
                ConvertUtils.ConvertDirToWebp(qMin, delSrcCbox.Checked);

            if(selectedFormat == IF.J2K)
                ConvertUtils.ConvertDirToJpeg2000(qMin, delSrcCbox.Checked);

            if(selectedFormat == IF.FLIF)
                ConvertUtils.ConvertDirToFlif(qMin, delSrcCbox.Checked);

            if (selectedFormat == IF.BMP)
                ConvertUtils.ConvertDirToBmp(delSrcCbox.Checked);

            if (selectedFormat == IF.AVIF)
                ConvertUtils.ConvertDirToAvif(qMin, delSrcCbox.Checked);
        }
    }
}
