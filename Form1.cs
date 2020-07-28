using ImageMagick;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            //TextBox tbx = this.Controls.Find("nameMustNotContainTbox", true).FirstOrDefault() as TextBox;
            //tbx.Visible = false;

            Program.logTbox = logTbox;
            Program.progBar = progressBar1;

            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            Program.currentExt = extTbox.Text.Trim();

            InitCombox(formatCombox, 0);
            InitCombox(colorDepthCombox, 3);
            InitCombox(suffixPrefixCombox, 0);

            IOUtils.recursive = recursiveCbox.Checked;

            ScaleUtils.onlyDownscale = onlyDownscaleCbox.Checked;
        }

        void InitCombox (ComboBox cbox, int index)
        {
            cbox.SelectedIndex = index;
            cbox.Text = cbox.Items[index].ToString();
        }

        private void convertStartBtn_Click (object sender, EventArgs e)
        {
            ConvertTabHelper.ConvertUsingPath(qualityCombox, qualityMaxCombox, selectedFormat, delSrcCbox);
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
            ScaleTabHelper.ScaleUsingPath(minScaleCombox, maxScaleCombox, filterModeCombox);
        }

        private void autoLevelBtn_Click (object sender, EventArgs e)
        {
            AdjustUtilsUI.AutoLevel(Program.currentDir, Program.currentExt, recursiveCbox.Checked);
        }

        private void remAlphaWhite_Click (object sender, EventArgs e)
        {
            OtherUtilsUI.RemTransparencyDir(1);
        }

        private void remAlphaBlack_Click (object sender, EventArgs e)
        {
            OtherUtilsUI.RemTransparencyDir(0);
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
            IOUtils.recursive = recursiveCbox.Checked;
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
            scaleModeCombox_SelectedIndexChanged(null, null);
            appendFilterCbox_CheckedChanged(null, null);
            noOverwriteCbox_CheckedChanged(null, null);
        }

        private void scaleModeCombox_SelectedIndexChanged (object sender, EventArgs e)
        {
            onlyDownscaleCbox.Enabled = true;
            if(scaleModeCombox.SelectedIndex == 0)
            {
                ScaleUtils.currMode = ScaleUtils.ScaleMode.Percentage;
                onlyDownscaleCbox.Enabled = false;
            }   
            if(scaleModeCombox.SelectedIndex == 1)
                ScaleUtils.currMode = ScaleUtils.ScaleMode.Height;
            if(scaleModeCombox.SelectedIndex == 2)
                ScaleUtils.currMode = ScaleUtils.ScaleMode.Width;
            if(scaleModeCombox.SelectedIndex == 3)
                ScaleUtils.currMode = ScaleUtils.ScaleMode.LongerSide;
            if(scaleModeCombox.SelectedIndex == 4)
                ScaleUtils.currMode = ScaleUtils.ScaleMode.ShorterSide;
        }

        private void filterModeCombox_SelectedIndexChanged (object sender, EventArgs e)
        {
            appendFilterCbox.Visible = false;
            if(filterModeCombox.Text.ToLower().Contains("random"))
                appendFilterCbox.Visible = true;
        }

        private void appendFilterCbox_CheckedChanged (object sender, EventArgs e)
        {
            ScaleUtils.appendFiltername = appendFilterCbox.Checked;
        }

        private void noOverwriteCbox_CheckedChanged (object sender, EventArgs e)
        {
            ScaleUtils.dontOverwrite = noOverwriteCbox.Checked;
        }

        private void MainForm_DragEnter (object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void MainForm_DragDrop (object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach(string file in files)
            {
                Program.Print("Dragndrop: " + file);
                if(IOUtils.IsPathDirectory(file))
                {
                    Program.Print("Setting directory to " + file);
                    Program.currentDir = file;
                    pathTextbox.Text = file;
                }
                else
                {
                    Program.Print("Previewing " + file + "...");
                    PreviewImage(file);
                }
            }
        }

        void PreviewImage (string imgPath)
        {
            MagickImage tempImg = IOUtils.ReadImage(imgPath);
            tempImg.Format = MagickFormat.Png;
            string tempImgPath = Path.Combine(IOUtils.GetAppDataDir(), "previewImg.png");
            tempImg.Write(tempImgPath);
            Program.previewImgPath = tempImgPath;
            var imgForm = new ImagePreviewPopup();
            imgForm.Show();
        }

        private void addNoiseBtn_Click (object sender, EventArgs e)
        {
            EffectsUtils.AddNoiseDir(GetNoiseTypeList(), int.Parse(attenuateCombox.Text.Trim()), monoChrCbox.Checked);
        }

        List<NoiseType> GetNoiseTypeList ()
        {
            List<NoiseType> noiseTypes = new List<NoiseType>();
            if(gaussNoiseCbox.Checked) noiseTypes.Add(NoiseType.Gaussian);
            if(lapNoiseCbox.Checked) noiseTypes.Add(NoiseType.Laplacian);
            if(multGaussNoiseCbox.Checked) noiseTypes.Add(NoiseType.MultiplicativeGaussian);
            if(poiNoiseCbox.Checked) noiseTypes.Add(NoiseType.Poisson);
            if(uniNoiseCbox.Checked) noiseTypes.Add(NoiseType.Uniform);
            return noiseTypes;
        }

        private void button2_Click (object sender, EventArgs e)
        {
            bool suffix = suffixPrefixCombox.SelectedIndex != 0;
            if(String.IsNullOrWhiteSpace(suffixPrefixTbox.Text))
                Program.Print("Can't add empty text to filenames!");
            else
                OtherUtilsUI.AddSuffixPrefixDir(suffixPrefixTbox.Text, suffix);
        }

        private void replaceBtn_Click (object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(replaceInputTbox.Text))
                Program.Print("Can't replace an empty string!");
            else
                OtherUtilsUI.ReplaceInFilenamesDir(replaceInputTbox.Text, replaceOutputTbox.Text);
        }

        private void nameMustContainTbox_TextChanged (object sender, EventArgs e)
        {
            IOUtils.nameMustContain = nameMustContainTbox.Text;
        }

        private void nameMustNotContainTbox_TextChanged (object sender, EventArgs e)
        {
            IOUtils.nameMustNotContain = nameMustNotContainTbox.Text;
        }

        private void alphaOffBtn_Click (object sender, EventArgs e)
        {
            OtherUtilsUI.RemTransparencyDir(2);
        }

        private void noisePreviewBtn_Click (object sender, EventArgs e)
        {
            FileInfo firstImg = IOUtils.GetFiles()[0];
            string tempImgPath = Path.Combine(IOUtils.GetAppDataDir(), "noisepreview" + firstImg.Extension);
            if(File.Exists(tempImgPath)) File.Delete(tempImgPath);
            File.Copy(firstImg.FullName, tempImgPath);
            EffectsUtils.AddNoise(tempImgPath, GetNoiseTypeList(), int.Parse(attenuateCombox.Text.Trim()), monoChrCbox.Checked);
            PreviewImage(tempImgPath);
        }

        private void tabPage1_DragEnter (object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tabPage1_DragDrop (object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            ConvertTabHelper.ConvertFileList(files, qualityCombox, qualityMaxCombox, selectedFormat, delSrcCbox);
        }

        private void tabPage2_DragEnter (object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tabPage2_DragDrop (object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            ScaleTabHelper.ScaleFileList(files, minScaleCombox, maxScaleCombox, filterModeCombox);
        }
    }
}