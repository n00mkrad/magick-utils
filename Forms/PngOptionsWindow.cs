using System;
using System.Windows.Forms;
using MagickUtils.Utils;

namespace MagickUtils.Forms
{
    public partial class PngOptionsWindow : Form
    {
        public PngOptionsWindow()
        {
            InitializeComponent();
        }

        private void PngOptionsWindow_Load(object sender, EventArgs e)
        {
            ConfigParser.LoadComboxIndex(pngColorDepth);
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PngOptionsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigParser.SaveComboxIndex(pngColorDepth);
        }
    }
}
