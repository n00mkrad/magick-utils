using ImageMagick;
using MagickUtils.Forms;
using MagickUtils.Interfaces;
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

            Config.Init();
            //HeifInterface.Extract(true);

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
            InitCombox(renameCounterMode, 0);
            InitCombox(renameCounterPadding, 1);
            InitCombox(tileMode, 0);
            InitCombox(zeroPaddingCombox, 0);
            InitCombox(ditherTypeCombox, 0);
            InitCombox(scaleResampleCombox, 0);
            InitCombox(scaleModeCombox, 0);
            InitCombox(filterModeCombox, 0);
            // Crop/Expand/Tile
            InitCombox(cropRelSizeMode, 0);
            InitCombox(cropAbsGrav, 1);
            InitCombox(cropDivisibleGrav, 1);
            InitCombox(padMode, 0);

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
            formatQualityLabel.Text = "";

            if (formatStrTrim == "JPEG")
            {
                selectedFormat = Program.ImageFormat.JPG;
                qualityMaxCombox.Enabled = true;
                formatQualityLabel.Text = "JPEG Quality: 0 - 100. Default: 95";
                formatOptionsBtn.Visible = true;
            } 

            if(formatStrTrim == "PNG")
            {
                selectedFormat = Program.ImageFormat.PNG;
                formatQualityLabel.Text = "PNG Compression Strength: 0 (Raw) - 100 (Max). Default: 30";
                formatOptionsBtn.Visible = true;
            }

            if(formatStrTrim == "DDS")
            {
                selectedFormat = Program.ImageFormat.DDS;
                qualityCombox.Enabled = Config.GetInt("ddsEnc") == 2;
                qualityMaxCombox.Enabled = qualityCombox.Enabled;
                formatOptionsBtn.Visible = true;
            }

            if (formatStrTrim == "BMP")
            {
                selectedFormat = Program.ImageFormat.BMP;
                qualityCombox.Enabled = false;
            }

            if (formatStrTrim == "TGA")
            {
                selectedFormat = Program.ImageFormat.TGA;
                qualityCombox.Enabled = false;
            }

            if(formatStrTrim == "WEBP")
            {
                selectedFormat = Program.ImageFormat.WEBP;
                qualityMaxCombox.Enabled = true;
                formatQualityLabel.Text = "WEBP Quality: 0 - 99. 100 for Lossless. Default: 93";
            }

            if(formatStrTrim == "JPEG 2000")
            {
                selectedFormat = Program.ImageFormat.J2K;
                formatQualityLabel.Text = "JPEG 2000 Quality: 0 - 100";
            }

            if(formatStrTrim == "FLIF")
            {
                selectedFormat = Program.ImageFormat.FLIF;
                formatOptionsBtn.Visible = true;
                formatQualityLabel.Text = "FLIF Quality: 0 - 100";
            }

            if (formatStrTrim == "AVIF")
            {
                selectedFormat = Program.ImageFormat.AVIF;
                formatQualityLabel.Text = "AVIF Quality: 0 - 100";
            }

            if (formatStrTrim == "HEIF")
            {
                selectedFormat = Program.ImageFormat.HEIF;
                formatQualityLabel.Text = "HEIF Quality: 0 - 99. Default: 50. Use 100 for lossless mode.";
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
                ScaleTabHelper.ResampleUsingPath(minScaleCombox, maxScaleCombox, filterModeCombox, resampleReupscaleFilterBox);
        }

        private void autoLevelBtn_Click (object sender, EventArgs e)
        {
            if(!Program.IsPathValid(Program.currentDir)) return;
            AdjustUtils.AutoLevel(Program.currentDir, Program.currentExt, recursiveCbox.Checked);
        }

        private void remAlphaBlack_Click (object sender, EventArgs e)
        {
            if(!Program.IsPathValid(Program.currentDir)) return;
            ColorUtils.RemTransparencyDir(ColorUtils.NoAlphaMode.Fill);
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
            if (selectedFormat == Program.ImageFormat.JPG)
                new JpegOptionsWindow().Show();
            if (selectedFormat == Program.ImageFormat.DDS)
                new DdsOptionsWindow().Show();
            if(selectedFormat == Program.ImageFormat.FLIF)
                new FlifOptionsWindow().Show();
            if (selectedFormat == Program.ImageFormat.PNG)
                new PngOptionsWindow().Show();
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
            if (tempImg == null) return;
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
            ColorUtils.RemTransparencyDir(ColorUtils.NoAlphaMode.Off);
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
                ScaleTabHelper.ResampleFileList(files, minScaleCombox, maxScaleCombox, filterModeCombox, resampleReupscaleFilterBox);

        }

        private void saveCfgBtn_Click (object sender, EventArgs e)
        {
            Config.SaveGuiElement(fileOperationsNoFilter);
            Config.SaveGuiElement(filenameReplaceIncludeExt);
            Config.SaveGuiElement(backgroundColor);
            Config.SaveGuiElement(pngQ);
            Program.Print("Saved config file.");
        }

        private void tabPage6_Enter (object sender, EventArgs e)
        {
            Config.LoadGuiElement(fileOperationsNoFilter);
            Config.LoadGuiElement(filenameReplaceIncludeExt);
            Config.LoadGuiElement(backgroundColor);
            Config.LoadGuiElement(pngQ);
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
            EffectsTabHelper.blurRadiusMin = blurRadiusCombox.GetInt();
            if(String.IsNullOrWhiteSpace(blurRadiusMaxCombox.Text.Trim()))
                EffectsTabHelper.blurRadiusMax = EffectsTabHelper.blurRadiusMin;
            else
                EffectsTabHelper.blurRadiusMax = blurRadiusMaxCombox.GetInt();
        }

        public void SetMedianVars()
        {
            EffectsTabHelper.medianRadiusMin = medianRadiusMin.GetInt();
            if (String.IsNullOrWhiteSpace(medianRadiusMax.Text.Trim()))
                EffectsTabHelper.medianRadiusMax = EffectsTabHelper.medianRadiusMin;
            else
                EffectsTabHelper.medianRadiusMax = medianRadiusMax.GetInt();
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
                CropTabHelper.CropTiles(tilingW, tilingH, tileMode, tileDelSrc.Checked);
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

        private void rotateBtn_Click (object sender, EventArgs e)
        {
            if (!Program.IsPathValid(Program.currentDir)) return;
            GeometryUtils.RotateMode rotateMode = GeometryUtils.RotateMode.Rot90;
            if(geomRotationCombox.SelectedIndex == 1) rotateMode = GeometryUtils.RotateMode.Rot180;
            if(geomRotationCombox.SelectedIndex == 2) rotateMode = GeometryUtils.RotateMode.Rot270;
            if(geomRotationCombox.SelectedIndex == 3) rotateMode = GeometryUtils.RotateMode.Random;
            if(geomRotationCombox.SelectedIndex == 4) rotateMode = GeometryUtils.RotateMode.RandomAll;
            GeometryUtils.RotateDir(rotateMode);
        }

        private void flipBtn_Click (object sender, EventArgs e)
        {
            if (!Program.IsPathValid(Program.currentDir)) return;
            GeometryUtils.FlipMode flipMode = GeometryUtils.FlipMode.Hor;
            if(geomFlipAxis.SelectedIndex == 1) flipMode = GeometryUtils.FlipMode.Vert;
            if(geomFlipAxis.SelectedIndex == 2) flipMode = GeometryUtils.FlipMode.Random;
            GeometryUtils.FlipDir(flipMode);
        }

        private void renameCounterBtn_Click (object sender, EventArgs e)
        {
            if (Program.IsPathValid(Program.currentDir))
                OtherUtils.RenameCounterDir(renameCounterMode.SelectedIndex, renameCounterPadding.SelectedIndex == 1, renameCounterStartAt.GetInt());
        }

        private void mergeAllBtn_Click (object sender, EventArgs e)
        {
            if (Program.IsPathValid(Program.currentDir))
                CropUtils.MergeAllDir();
        }

        private void edgeDetectBtn_Click (object sender, EventArgs e)
        {
            if (Program.IsPathValid(Program.currentDir))
                EffectsUtils.EdgeDetectDir();
        }

        private void ditherBtn_Click (object sender, EventArgs e)
        {
            int colorsMin = ditherColorsMin.GetInt();
            int colorsMax = colorsMin;
            if(!string.IsNullOrWhiteSpace(ditherColorsMax.Text))
                colorsMax = ditherColorsMax.GetInt();
            if(ditherTypeCombox.SelectedIndex == 0) ColorUtils.DitherDir(colorsMin, colorsMax, ColorUtils.DitherType.FloydSteinberg);
            if(ditherTypeCombox.SelectedIndex == 1) ColorUtils.DitherDir(colorsMin, colorsMax, ColorUtils.DitherType.Riemersma);
            if(ditherTypeCombox.SelectedIndex == 2) ColorUtils.DitherDir(colorsMin, colorsMax, ColorUtils.DitherType.Ordered4x4);
            if(ditherTypeCombox.SelectedIndex == 3) ColorUtils.DitherDir(colorsMin, colorsMax, ColorUtils.DitherType.Halftone4x4);
            if (ditherTypeCombox.SelectedIndex == 4) ColorUtils.DitherDir(colorsMin, colorsMax, ColorUtils.DitherType.Random);
        }

        private void bgColorSelectBtn_Click (object sender, EventArgs e)
        {
            bgColorDialog.ShowDialog();
            string colorStr = ColorTranslator.ToHtml(Color.FromArgb(bgColorDialog.Color.ToArgb())).Replace("#", "") + "FF";
            backgroundColor.Text = colorStr;
        }

        private void zeropadBtn_Click (object sender, EventArgs e)
        {
            if (Program.IsPathValid(Program.currentDir))
                OtherUtils.AddZeroPaddingDir(zeroPaddingCombox.GetInt());
        }

        private void layerColorPickerBtn_Click (object sender, EventArgs e)
        {
            layerColorDialog.ShowDialog();
            string colorStr = ColorTranslator.ToHtml(Color.FromArgb(layerColorDialog.Color.ToArgb())).Replace("#", "") + "FF";
            colorLayerTbox.Text = colorStr;
        }

        private void scaleResampleCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetResampleOptions(scaleResampleCombox.SelectedIndex == 1);
        }

        void SetResampleOptions (bool state)
        {
            resampleReupscaleFilterLabel.Visible = state;
            resampleReupscaleFilterBox.Visible = state;
        }

        private void printInfoBtn_Click(object sender, EventArgs e)
        {
            if (Program.IsPathValid(Program.currentDir))
                OtherUtils.PrintImageInfoDir();
        }

        private void deleteGrayscaleImgsBtn_Click(object sender, EventArgs e)
        {
            if (Program.IsPathValid(Program.currentDir))
                ColorUtils.DeleteGrayscaleImgDir(grayscaleThreshTbox.GetFloat(), false);
        }

        private void deleteColorImgsBtn_Click(object sender, EventArgs e)
        {
            if (Program.IsPathValid(Program.currentDir))
                ColorUtils.DeleteGrayscaleImgDir(grayscaleThreshTbox.GetFloat(), true);
        }

        private void removeBytesBtn_Click(object sender, EventArgs e)
        {
            if (Program.IsPathValid(Program.currentDir))
                OtherUtils.RemoveBytesDir(removeBytesAmount.GetInt());
        }

        private void ditherTypeCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool usesColorAmount = (ditherTypeCombox.SelectedIndex == 0 || ditherTypeCombox.SelectedIndex == 1);
            ditherColorsMin.Enabled = usesColorAmount;
            ditherColorsMax.Enabled = usesColorAmount;
        }

        private void medianPreviewBtn_Click(object sender, EventArgs e)
        {
            EffectsTabHelper.MedianPreview();
        }

        private void medianFilterBtn_Click(object sender, EventArgs e)
        {
            EffectsTabHelper.MedianApply();
        }

        private void haloBtn_Click(object sender, EventArgs e)
        {
            int radMin = haloRadiusMin.GetInt();
            int radMax = radMin;
            if (!String.IsNullOrWhiteSpace(haloRadiusMax.Text.Trim()))
                radMax = haloRadiusMax.GetInt();
            EffectsUtils.HaloDir(radMin, radMax);
        }
    }
}