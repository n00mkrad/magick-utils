using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagickUtils.Forms
{
    public partial class JpegOptionsWindow : Form
    {
        public JpegOptionsWindow()
        {
            InitializeComponent();
        }

        private void jpegEnc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //mozJpegOptionsPanel.Visible = jpegEnc.SelectedIndex == 1;
        }

        private void JpegOptionsWindow_Load(object sender, EventArgs e)
        {
            Config.LoadComboxIndex(jpegEnc);
            Config.LoadComboxIndex(jpegChromaSubsampling);
        }

        private void JpegOptionsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.SaveComboxIndex(jpegEnc);
            Config.SaveComboxIndex(jpegChromaSubsampling);
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
