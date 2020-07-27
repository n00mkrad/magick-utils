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
    public partial class ImagePreviewPopup : Form
    {
        public ImagePreviewPopup ()
        {
            InitializeComponent();
        }

        private void ImagePreviewPopup_Load (object sender, EventArgs e)
        {
            CenterToScreen();
            previewPicbox.ImageLocation = Program.previewImgPath;
        }
    }
}
