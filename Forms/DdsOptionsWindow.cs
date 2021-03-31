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

        private void CheckCompat(object sender, EventArgs e)
        {
            if(ddsCompressionType.SelectedIndex > 1 && ddsEnc.SelectedIndex == 0)
            {
                MessageBox.Show("ImageMagick only supports ARGB and DXT1 formats!\nPlease use NvCompress or Crunch for other formats.", "Error");
                ddsCompressionType.SelectedIndex = 1;
            }

            if (ddsEnc.Text.ToLower().Contains("crunch") && ddsCompressionType.Text.ToLower().Contains("bc7"))
            {
                MessageBox.Show("Crunch doesn't support BC7!\nPlease use Texconv (fast) or NvCompress (slow) for BC7.", "Error");
                ddsCompressionType.SelectedIndex = 1;
            }
        }
    }
}
