using ImageMagick;
using MagickUtils.MagickUtils;
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
            if(OSUtils.IsUserAdministrator())
                MessageBox.Show("MagickUtils is running as administrator. This will break Drag-n-Drop functionality.", "Warning");

            CenterToScreen();
            Config.fileOperationsNoExtFilter = noExtFilterForFileOpsCbox.Checked;
            Config.backgroundColor = confBgColor.Text.Trim().Replace("#", "");
            Config.ReadConfig();
            Config.WriteConfig(false);   // Write, in case we have new values that are not in the config yet

            Program.logTbox = logTbox;
            Program.progBar = progressBar1;

            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            Program.currentExt = extTbox.Text.Trim();

            InitCombox(formatCombox, 0);
            InitCombox(colorDepthCombox, 3);
            InitCombox(suffixPrefixCombox, 0);
            InitCombox(inpaintColorCombox, 0);
            InitCombox(inpaintScaleCombox, 1);
            InitCombox(delImgsMode, 0);
            InitCombox(delImgOperator, 0);
            InitCombox(cropDivision, 1);
            InitCombox(cropRelGrav, 1);
            InitCombox(cropRelSizeMode, 0);
            InitCombox(geomRotationCombox, 0);
            InitCombox(geomFlipAxis, 0);
            InitCombox(cropDivisibleGrav, 1);
            InitCombox(cropAbsGrav, 1);
            InitCombox(padMode, 0);
            InitCombox(renameCounterMode, 0);
            InitCombox(tileMode, 0);
            InitCombox(zeroPaddingCombox, 0);

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
            if(!Program.IsPathValid(Program.currentDir)) return;
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
            if(!Program.IsPathValid(Program.currentDir)) return;
            if(scaleResampleCombox.SelectedIndex == 0)
                ScaleTabHelper.ScaleUsingPath(minScaleCombox, maxScaleCombox, filterModeCombox);
            if(scaleResampleCombox.SelectedIndex == 1)
                ScaleTabHelper.ResampleUsingPath(minScaleCombox, maxScaleCombox, filterModeCombox);
        }

        private void autoLevelBtn_Click (object sender, EventArgs e)
        {
            if(!Program.IsPathValid(Program.currentDir)) return;
            AdjustUtils.AutoLevel(Program.currentDir, Program.currentExt, recursiveCbox.Checked);
        }

        private void remAlphaWhite_Click (object sender, EventArgs e)
        {
            if(!Program.IsPathValid(Program.currentDir)) return;
            ColorUtils.RemTransparencyDir(1);
        }

        private void remAlphaBlack_Click (object sender, EventArgs e)
        {
            if(!Program.IsPathValid(Program.currentDir)) return;
            ColorUtils.RemTransparencyDir(0);
        }

        private void delSmallImagesBtn_Click (object sender, EventArgs e)
        {
            if(!Program.IsPathValid(Program.currentDir)) return;
            int minSize = int.Parse(delSmallImagesSizeCombox.Text);
            ImageSizeFilterUtils.SizeMode sizeMode = ImageSizeFilterUtils.SizeMode.Height;
            if (delImgsMode.SelectedIndex == 1) sizeMode = ImageSizeFilterUtils.SizeMode.Width;
            if (delImgsMode.SelectedIndex == 2) sizeMode = ImageSizeFilterUtils.SizeMode.LongerSide;
            if (delImgsMode.SelectedIndex == 3) sizeMode = ImageSizeFilterUtils.SizeMode.ShorterSide;
            if (delImgsMode.SelectedIndex == 4) sizeMode = ImageSizeFilterUtils.SizeMode.EitherSide;
            if (delImgsMode.SelectedIndex == 5) sizeMode = ImageSizeFilterUtils.SizeMode.BothSides;
            ImageSizeFilterUtils.Operator op = ImageSizeFilterUtils.Operator.IsNot;
            if (delImgOperator.SelectedIndex == 1) op = ImageSizeFilterUtils.Operator.Is;
            if (delImgOperator.SelectedIndex == 2) op = ImageSizeFilterUtils.Operator.IsSmaller;
            if (delImgOperator.SelectedIndex == 3) op = ImageSizeFilterUtils.Operator.IsBigger;
            if (delImgOperator.SelectedIndex == 4) op = ImageSizeFilterUtils.Operator.Divisible;
            if (delImgOperator.SelectedIndex == 5) op = ImageSizeFilterUtils.Operator.NotDivisible;
            OtherUtils.DelSmallImgsDir(minSize, sizeMode, op);
        }

        private void delFilesNotMatchingExtBtn_Click (object sender, EventArgs e)
        {
            if(!Program.IsPathValid(Program.currentDir)) return;
            OtherUtils.DelNotMatchingWildcard(recursiveCbox.Checked);
        }

        private void groupNormalsBtn_Click (object sender, EventArgs e)
        {
            if(!Program.IsPathValid(Program.currentDir)) return;
            OtherUtils.GroupNormalsWithTex(normalSuffixCombox.Text, diffSuffixCombox.Text, lowercaseCbox.Checked);
        }

        private void applyColorDepthBtn_Click (object sender, EventArgs e)
        {
            if(!Program.IsPathValid(Program.currentDir)) return;
            if(colorDepthCombox.SelectedIndex == 0) ColorUtils.SetColorDepthDir(24);
            if(colorDepthCombox.SelectedIndex == 1) ColorUtils.SetColorDepthDir(16);
            if(colorDepthCombox.SelectedIndex == 2) ColorUtils.SetColorDepthDir(12);
            if(colorDepthCombox.SelectedIndex == 3) ColorUtils.SetColorDepthDir(8);
            if(colorDepthCombox.SelectedIndex == 4) ColorUtils.SetColorDepthDir(7);
            if(colorDepthCombox.SelectedIndex == 5) ColorUtils.SetColorDepthDir(6);
            if(colorDepthCombox.SelectedIndex == 6) ColorUtils.SetColorDepthDir(5);
            if(colorDepthCombox.SelectedIndex == 7) ColorUtils.SetColorDepthDir(4);
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

        public void PreviewImage (string imgPath)
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
            if(Program.IsPathValid(Program.currentDir))
                EffectsTabHelper.NoiseApply();
        }

        private void button2_Click (object sender, EventArgs e)
        {
            bool suffix = suffixPrefixCombox.SelectedIndex != 0;
            if(String.IsNullOrWhiteSpace(suffixPrefixTbox.Text))
                Program.Print("Can't add empty text to filenames!");
            else
                OtherUtils.AddSuffixPrefixDir(suffixPrefixTbox.Text, suffix);
        }

        private void replaceBtn_Click (object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(replaceInputTbox.Text))
                Program.Print("Can't replace an empty string!");
            else
                OtherUtils.ReplaceInFilenamesDir(replaceInputTbox.Text, replaceOutputTbox.Text);
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
            ColorUtils.RemTransparencyDir(2);
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
            if(scaleResampleCombox.SelectedIndex == 0)
                ScaleTabHelper.ScaleFileList(files, minScaleCombox, maxScaleCombox, filterModeCombox);
            if(scaleResampleCombox.SelectedIndex == 1)
                ScaleTabHelper.ResampleFileList(files, minScaleCombox, maxScaleCombox, filterModeCombox);

        }

        private void saveCfgBtn_Click (object sender, EventArgs e)
        {
            Config.WriteConfig();
        }

        private void noExtFilterForFileOpsCbox_CheckedChanged (object sender, EventArgs e)
        {
            Config.fileOperationsNoExtFilter = noExtFilterForFileOpsCbox.Checked;
        }

        private void tabPage6_Enter (object sender, EventArgs e)
        {
            noExtFilterForFileOpsCbox.Checked = Config.fileOperationsNoExtFilter;
        }

        private void blurPrevBtn_Click (object sender, EventArgs e)
        {
            if(Program.IsPathValid(Program.currentDir))
                EffectsTabHelper.BlurPreview();
        }

        public void SetNoiseVars ()
        {
            EffectsTabHelper.useGaussNoise = gaussNoiseCbox.Checked;
            EffectsTabHelper.useLapNoise = lapNoiseCbox.Checked;
            EffectsTabHelper.usePoiNoise = poiNoiseCbox.Checked;
            EffectsTabHelper.useUniNoise = uniNoiseCbox.Checked;
            EffectsTabHelper.attenMin = int.Parse(attenuateCombox.Text.Trim());
            if(String.IsNullOrWhiteSpace(attenuateMaxCombox.Text.Trim()))
                EffectsTabHelper.attenMax = EffectsTabHelper.attenMin;
            else
                EffectsTabHelper.attenMax = int.Parse(attenuateMaxCombox.Text.Trim());
            EffectsTabHelper.monochrome = monoChrCbox.Checked;
        }

        public void SetBlurVars ()
        {
            EffectsTabHelper.blurRadiusMin = int.Parse(blurRadiusCombox.Text.Trim());
            if(String.IsNullOrWhiteSpace(blurRadiusMaxCombox.Text.Trim()))
                EffectsTabHelper.blurRadiusMax = EffectsTabHelper.blurRadiusMin;
            else
                EffectsTabHelper.blurRadiusMax = int.Parse(blurRadiusMaxCombox.Text.Trim());
        }

        private void blurBtn_Click (object sender, EventArgs e)
        {
            if(Program.IsPathValid(Program.currentDir))
                EffectsTabHelper.BlurApply();
        }

        private void noisePreviewBtn_Click (object sender, EventArgs e)
        {
            if(Program.IsPathValid(Program.currentDir))
                EffectsTabHelper.NoisePreview();
        }

        private void delMissingBtn_Click (object sender, EventArgs e)
        {
            if(Program.IsPathValid(Program.currentDir))
                OtherUtils.DelMissing(checkDirTbox.Text.Trim(), false);
        }

        private void delMissingTestBtn_Click (object sender, EventArgs e)
        {
            if(Program.IsPathValid(Program.currentDir))
                OtherUtils.DelMissing(checkDirTbox.Text.Trim(), true);
        }

        private void inpaintEraseBtn_Click(object sender, EventArgs e)
        {
            if(Program.IsPathValid(Program.currentDir)) if(Program.IsPathValid(Program.currentDir))
                    InpaintTabHelper.EraseDir(inpaintThinLines, inpaintThickLines, inpaintRings, inpaintBubbles, inpaintGrid, inpaintColorCombox, inpaintScaleCombox);
        }

        private void cropBtn_Click(object sender, EventArgs e)
        {
            if(cropTabControl.SelectedIndex == 0)
                CropTabHelper.CropRelative(cropRelSizeMin, cropRelSizeMax, cropRelSizeMode, cropRelGrav);
            if(cropTabControl.SelectedIndex == 1)
                CropTabHelper.CropAbsolute(cropAbsW, cropAbsH, cropAbsGrav);
            if(cropTabControl.SelectedIndex == 2)
                CropTabHelper.CropDivisible(cropDivision, cropDivisibleGrav, cropDivisibleExpand);
            if(cropTabControl.SelectedIndex == 3)
                CropTabHelper.CropPadding(padPixMin, padPixMax, padMode);
            if(cropTabControl.SelectedIndex == 4)
                CropTabHelper.CropTiles(tilingW, tilingH, tileMode);
        }

        private void tabPage7_DragEnter (object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tabPage7_DragDrop (object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            InpaintTabHelper.EraseFileList(files, inpaintThinLines, inpaintThickLines, inpaintRings, inpaintBubbles, inpaintGrid, inpaintColorCombox, inpaintScaleCombox);
        }

        private void colorLayerBtn_Click (object sender, EventArgs e)
        {
            if(Program.IsPathValid(Program.currentDir))
                ColorUtils.LayerColorDir(colorLayerTbox.Text.Trim().Replace("#", ""));
        }

        private void tileBtn_Click (object sender, EventArgs e)
        {

        }

        private void rotateBtn_Click (object sender, EventArgs e)
        {
            GeometryUtils.RotateMode rotateMode = GeometryUtils.RotateMode.Rot90;
            if(geomRotationCombox.SelectedIndex == 1) rotateMode = GeometryUtils.RotateMode.Rot180;
            if(geomRotationCombox.SelectedIndex == 2) rotateMode = GeometryUtils.RotateMode.Rot270;
            if(geomRotationCombox.SelectedIndex == 3) rotateMode = GeometryUtils.RotateMode.Random;
            if(geomRotationCombox.SelectedIndex == 4) rotateMode = GeometryUtils.RotateMode.RandomAll;
            GeometryUtils.RotateDir(rotateMode);
        }

        private void flipBtn_Click (object sender, EventArgs e)
        {
            GeometryUtils.FlipMode flipMode = GeometryUtils.FlipMode.Hor;
            if(geomFlipAxis.SelectedIndex == 1) flipMode = GeometryUtils.FlipMode.Vert;
            if(geomFlipAxis.SelectedIndex == 2) flipMode = GeometryUtils.FlipMode.Random;
            GeometryUtils.FlipDir(flipMode);
        }

        private void confBgColor_TextChanged (object sender, EventArgs e)
        {
            Config.backgroundColor = confBgColor.Text.Trim().Replace("#", "");
        }

        private void renameCounterBtn_Click (object sender, EventArgs e)
        {
            OtherUtils.RenameCounterDir(renameCounterMode.SelectedIndex);
        }

        private void mergeAllBtn_Click (object sender, EventArgs e)
        {
            CropUtils.MergeAllDir();
        }

        private void edgeDetectBtn_Click (object sender, EventArgs e)
        {
            EffectsUtils.EdgeDetectDir();
        }

        private void ditherBtn_Click (object sender, EventArgs e)
        {
            int colorsMin = ditherColorsMin.GetInt();
            int colorsMax = colorsMin;
            if(!string.IsNullOrWhiteSpace(ditherColorsMax.Text))
                colorsMax = ditherColorsMax.GetInt();
            ColorUtils.DitherDir(colorsMin, colorsMax);
        }

        private void bgColorSelectBtn_Click (object sender, EventArgs e)
        {
            bgColorDialog.ShowDialog();
            string colorStr = ColorTranslator.ToHtml(Color.FromArgb(bgColorDialog.Color.ToArgb())).Replace("#", "") + "FF";
            Config.backgroundColor = colorStr;
            confBgColor.Text = colorStr;
        }

        private void zeropadBtn_Click (object sender, EventArgs e)
        {
            OtherUtils.AddZeroPaddingDir(zeroPaddingCombox.GetInt());
        }
    }
}