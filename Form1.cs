using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagickUtils
{
    public partial class MainForm : Form
    {
        static Program.ImageFormat selectedFormat;
        

        public MainForm ()
        {
            InitializeComponent();
        }

        private void MainForm_Load (object sender, EventArgs e)
        {
            CenterToScreen();

            Program.logTbox = logTbox;
            Program.progBar = progressBar1;

            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            Program.currentExt = extTbox.Text.Trim();

            InitCombox(formatCombox, 0);
            InitCombox(colorDepthCombox, 3);

            Program.exclIncompatible = ignoreIncompatCbox.Checked;
            Program.recursive = recursiveCbox.Checked;
        }

        void InitCombox (ComboBox cbox, int index)
        {
            cbox.SelectedIndex = index;
            cbox.Text = cbox.Items[index].ToString();
        }

        private void label2_Click (object sender, EventArgs e)
        {

        }

        private void tabPage2_Click (object sender, EventArgs e)
        {

        }

        private void convertStartBtn_Click (object sender, EventArgs e)
        {
            int qMin = int.Parse(qualityCombox.Text.Trim());
            int qMax = qMin;
            if(!string.IsNullOrWhiteSpace(qualityMaxCombox.Text.Trim()))
                qMax = int.Parse(qualityMaxCombox.Text.Trim());

            if(selectedFormat == Program.ImageFormat.JPG)
                ConvertUtilsUI.ConvertDirToJpeg(qMin, qMax, delSrcCbox.Checked);

            if(selectedFormat == Program.ImageFormat.PNG)
                ConvertUtilsUI.ConvertDirToPng(qMin, delSrcCbox.Checked);

            if(selectedFormat == Program.ImageFormat.DDS)
            {
                if(FormatOptions.ddsUseCrunch) ConvertUtilsUI.ConvertDirToDdsCrunch(qMin, qMax, delSrcCbox.Checked);
                else ConvertUtilsUI.ConvertDirToDds(delSrcCbox.Checked);
            }
                
            if(selectedFormat == Program.ImageFormat.TGA)
                ConvertUtilsUI.ConvertDirToTga(delSrcCbox.Checked);

            if(selectedFormat == Program.ImageFormat.WEBP)
                ConvertUtilsUI.ConvertDirToWebp(qMin, delSrcCbox.Checked);

            if(selectedFormat == Program.ImageFormat.J2K)
                ConvertUtilsUI.ConvertDirToJpeg2000(qMin, delSrcCbox.Checked);

            if(selectedFormat == Program.ImageFormat.FLIF)
                ConvertUtilsUI.ConvertDirToFlif(qMin, delSrcCbox.Checked);
        }

        private void pathTextbox_TextChanged (object sender, EventArgs e)
        {
            Program.currentDir = pathTextbox.Text;
        }

        private void formatCombox_SelectedIndexChanged (object sender, EventArgs e)
        {
            string formatStrTrim = formatCombox.Text.Trim();
            qualityCombox.Enabled = true;
            qualityMaxCombox.Enabled = false;
            formatOptionsBtn.Visible = false;

            if(formatStrTrim == "JPEG")
            {
                selectedFormat = Program.ImageFormat.JPG;
                qualityMaxCombox.Enabled = true;
            } 

            if(formatStrTrim == "PNG")
            {
                selectedFormat = Program.ImageFormat.PNG;
            }

            if(formatStrTrim == "DDS")
            {
                selectedFormat = Program.ImageFormat.DDS;
                qualityCombox.Enabled = FormatOptions.ddsUseCrunch;
                qualityMaxCombox.Enabled = qualityCombox.Enabled;
                formatOptionsBtn.Visible = true;
            }
                
            if(formatStrTrim == "TGA")
            {
                selectedFormat = Program.ImageFormat.TGA;
                qualityCombox.Enabled = false;
            }

            if(formatStrTrim == "WEBP")
            {
                selectedFormat = Program.ImageFormat.WEBP;
            }

            if(formatStrTrim == "JPEG 2000")
            {
                selectedFormat = Program.ImageFormat.J2K;
            }

            if(formatStrTrim == "FLIF")
            {
                selectedFormat = Program.ImageFormat.FLIF;
                formatOptionsBtn.Visible = true;
            }

            CheckDelSourceFormat();
        }

        void CheckDelSourceFormat ()
        {
            delSrcCbox.Enabled = true;
            if(formatCombox.Text.Trim().ToLower() == Program.currentExt.ToLower())
            {
                delSrcCbox.Checked = false;
                delSrcCbox.Enabled = false;
            }
        }

        private void extTbox_TextChanged (object sender, EventArgs e)
        {
            Program.currentExt = extTbox.Text.Trim();
            CheckDelSourceFormat();
        }

        private void DoScaleBtn_Click (object sender, EventArgs e)
        {
            int sMin = int.Parse(minScaleCombox.Text);
            int sMax = sMin;
            if(!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = int.Parse(maxScaleCombox.Text);
            int filterMode = filterModeCombox.SelectedIndex;
            bool useHeight = scaleModeCombox.SelectedIndex == 1;
            ScaleUtilsUI.ScaleDir(Program.currentDir, Program.currentExt, sMin, sMax, filterMode, useHeight, recursiveCbox.Checked);
        }

        private void autoLevelBtn_Click (object sender, EventArgs e)
        {
            AdjustUtilsUI.AutoLevel(Program.currentDir, Program.currentExt, recursiveCbox.Checked);
        }

        private void remAlphaWhite_Click (object sender, EventArgs e)
        {
            OtherUtilsUI.RemTransparency(true);
        }

        private void remAlphaBlack_Click (object sender, EventArgs e)
        {
            OtherUtilsUI.RemTransparency(false);
        }

        private void delSmallImagesBtn_Click (object sender, EventArgs e)
        {
            int minSize = int.Parse(delSmallImagesSizeCombox.Text);
            OtherUtilsUI.DelSmallImgsDir(minSize);
        }

        private void delFilesNotMatchingExtBtn_Click (object sender, EventArgs e)
        {
            OtherUtilsUI.DelNotMatchingWildcard(recursiveCbox.Checked);
        }

        private void groupNormalsBtn_Click (object sender, EventArgs e)
        {
            OtherUtilsUI.GroupNormalsWithTex(normalSuffixCombox.Text, diffSuffixCombox.Text, lowercaseCbox.Checked);
        }

        private void ignoreIncompatCbox_CheckedChanged (object sender, EventArgs e)
        {
            Program.exclIncompatible = ignoreIncompatCbox.Checked;
        }

        private void applyColorDepthBtn_Click (object sender, EventArgs e)
        {
            if(colorDepthCombox.SelectedIndex == 0) OtherUtilsUI.SetColorDepth(24);
            if(colorDepthCombox.SelectedIndex == 1) OtherUtilsUI.SetColorDepth(16);
            if(colorDepthCombox.SelectedIndex == 2) OtherUtilsUI.SetColorDepth(12);
            if(colorDepthCombox.SelectedIndex == 3) OtherUtilsUI.SetColorDepth(8);
            if(colorDepthCombox.SelectedIndex == 4) OtherUtilsUI.SetColorDepth(7);
            if(colorDepthCombox.SelectedIndex == 5) OtherUtilsUI.SetColorDepth(6);
            if(colorDepthCombox.SelectedIndex == 6) OtherUtilsUI.SetColorDepth(5);
            if(colorDepthCombox.SelectedIndex == 7) OtherUtilsUI.SetColorDepth(4);
        }

        private void recursiveCbox_CheckedChanged (object sender, EventArgs e)
        {
            Program.recursive = recursiveCbox.Checked;
        }

        private void formatOptionsBtn_Click (object sender, EventArgs e)
        {
            var ddsForm = new DdsOptionsWindow();
            var flifForm = new FlifOptionsWindow();
            if(selectedFormat == Program.ImageFormat.DDS)
                ddsForm.Show();
            if(selectedFormat == Program.ImageFormat.FLIF)
                flifForm.Show();
        }

        private void MainForm_Activated (object sender, EventArgs e)
        {
            formatCombox_SelectedIndexChanged(null, null);
        }
    }
}