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
            Program.logTbox = logTbox;
            Program.progBar = progressBar1;

            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            Program.currentExt = extTbox.Text.Trim();

            InitCombox(formatCombox, 0);
            InitCombox(dxtQualCombox, 1);

            Program.exclIncompatible = ignoreIncompatCbox.Checked;
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
            int qMin = int.Parse(qualityCombox.Text);
            int qMax = qMin;
            if(!string.IsNullOrWhiteSpace(qualityMaxCombox.Text.Trim()))
                qMax = int.Parse(qualityMaxCombox.Text);

            if(selectedFormat == Program.ImageFormat.JPG)
                ConvertUtilsUI.ConvertDirToJpeg(pathTextbox.Text, extTbox.Text, qMin, qMax, recursiveCbox.Checked, delSrcCbox.Checked);

            if(selectedFormat == Program.ImageFormat.PNG)
                ConvertUtilsUI.ConvertDirToPng(pathTextbox.Text, extTbox.Text, qMin, recursiveCbox.Checked, delSrcCbox.Checked);

            if(selectedFormat == Program.ImageFormat.DDS)
            {
                if(crunchDdsCbox.Checked) ConvertUtilsUI.ConvertDirToDdsCrunch(recursiveCbox.Checked, qMin, qMax, delSrcCbox.Checked);
                else ConvertUtilsUI.ConvertDirToDds(pathTextbox.Text, extTbox.Text, recursiveCbox.Checked, delSrcCbox.Checked);
            }
                
            if(selectedFormat == Program.ImageFormat.TGA)
                ConvertUtilsUI.ConvertDirToTga(pathTextbox.Text, extTbox.Text, recursiveCbox.Checked, delSrcCbox.Checked);
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
            ddsOptionsPanel.Visible = false;

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
                qualityCombox.Enabled = crunchDdsCbox.Checked;
                qualityMaxCombox.Enabled = qualityCombox.Enabled;
                ddsOptionsPanel.Visible = true;
                crunchPanel.Enabled = crunchDdsCbox.Checked;
            }
                
            if(formatStrTrim == "TGA")
            {
                selectedFormat = Program.ImageFormat.TGA;
                qualityCombox.Enabled = false;
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
            OtherUtilsUI.RemTransparency(recursiveCbox.Checked, true);
        }

        private void remAlphaBlack_Click (object sender, EventArgs e)
        {
            OtherUtilsUI.RemTransparency(recursiveCbox.Checked, false);
        }

        private void delSmallImagesBtn_Click (object sender, EventArgs e)
        {
            int minSize = int.Parse(delSmallImagesSizeCombox.Text);
            OtherUtilsUI.DelSmallImgsDir(recursiveCbox.Checked, minSize);
        }

        private void delFilesNotMatchingExtBtn_Click (object sender, EventArgs e)
        {
            OtherUtilsUI.DelNotMatchingWildcard(recursiveCbox.Checked);
        }

        private void groupNormalsBtn_Click (object sender, EventArgs e)
        {
            OtherUtilsUI.GroupNormalsWithTex(normalSuffixCombox.Text, diffSuffixCombox.Text, lowercaseCbox.Checked);
        }

        private void crunchDdsCbox_CheckedChanged (object sender, EventArgs e)
        {
            formatCombox_SelectedIndexChanged(null, null);
        }

        private void dxtQualCombox_SelectedIndexChanged (object sender, EventArgs e)
        {
            if(dxtQualCombox.SelectedIndex == 0) CrunchInterface.currentQual = CrunchInterface.DXTQuality.superfast;
            if(dxtQualCombox.SelectedIndex == 1) CrunchInterface.currentQual = CrunchInterface.DXTQuality.fast;
            if(dxtQualCombox.SelectedIndex == 2) CrunchInterface.currentQual = CrunchInterface.DXTQuality.normal;
            if(dxtQualCombox.SelectedIndex == 3) CrunchInterface.currentQual = CrunchInterface.DXTQuality.better;
            if(dxtQualCombox.SelectedIndex == 4) CrunchInterface.currentQual = CrunchInterface.DXTQuality.uber;
        }

        private void useMipsCbox_CheckedChanged (object sender, EventArgs e)
        {
            CrunchInterface.currentMipMode = useMipsCbox.Checked;
        }

        private void ignoreIncompatCbox_CheckedChanged (object sender, EventArgs e)
        {
            Program.exclIncompatible = ignoreIncompatCbox.Checked;
        }
    }
}