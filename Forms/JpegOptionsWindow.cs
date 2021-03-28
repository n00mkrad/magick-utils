using System;
using System.Windows.Forms;
using MagickUtils.Utils;

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
            ConfigParser.LoadComboxIndex(jpegEnc);
            ConfigParser.LoadComboxIndex(jpegChromaSubsampling);
        }

        private void JpegOptionsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigParser.SaveComboxIndex(jpegEnc);
            ConfigParser.SaveComboxIndex(jpegChromaSubsampling);
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
