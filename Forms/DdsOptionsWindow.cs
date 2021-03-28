using System;
using System.Windows.Forms;
using MagickUtils.Utils;

namespace MagickUtils
{
    public partial class DdsOptionsWindow : Form
    {
        public DdsOptionsWindow ()
        {
            InitializeComponent();
        }

        private void DdsOptionsWindow_Load (object sender, EventArgs e)
        {
            ConfigParser.LoadComboxIndex(ddsEnc);
            ConfigParser.LoadGuiElement(ddsCompressionType);
            ConfigParser.LoadGuiElement(ddsEnableMips);
        }

        private void doneBtn_Click (object sender, EventArgs e)
        {
            Close();
        }

        private void DdsOptionsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigParser.SaveComboxIndex(ddsEnc);
            ConfigParser.SaveGuiElement(ddsCompressionType);
            ConfigParser.SaveGuiElement(ddsEnableMips);
        }

        private void ddsCompressionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddsCompressionType.SelectedIndex > 1 && ddsEnc.SelectedIndex == 0)
            {
                MessageBox.Show("ImageMagick only supports ARGB and DXT1 formats!\nPlease use NvCompress or Crunch for other formats.", "Error");
                ddsCompressionType.SelectedIndex = 1;
            }
        }
    }
}
