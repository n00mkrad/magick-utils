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
    public partial class ImagePreviewPopup : Form
    {
        public ImagePreviewPopup ()
        {
            InitializeComponent();
        }

        private void ImagePreviewPopup_Load (object sender, EventArgs e)
        {
            CenterToScreen();
            WindowState = FormWindowState.Maximized;
            //previewPicbox.ImageLocation = Program.previewImgPath;
            if (File.Exists(Program.previewImgPath))
                imgBox.Image = IOUtils.GetImage(Program.previewImgPath);
        }

        /*
        private void previewPicbox_Click (object sender, EventArgs e)
        {
            if(previewPicbox.SizeMode == PictureBoxSizeMode.CenterImage)
            {
                previewPicbox.SizeMode = PictureBoxSizeMode.Zoom;
                return;
            }

            if(previewPicbox.SizeMode == PictureBoxSizeMode.Zoom)
            {
                previewPicbox.SizeMode = PictureBoxSizeMode.CenterImage;
                return;
            }
        }
        */

        private void ImagePreviewPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            imgBox.Image = null;
        }
    }
}
