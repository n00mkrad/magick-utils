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
    public partial class PngOptionsWindow : Form
    {
        public PngOptionsWindow()
        {
            InitializeComponent();
        }

        private void PngOptionsWindow_Load(object sender, EventArgs e)
        {
            Config.LoadComboxIndex(pngColorDepth);
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PngOptionsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.SaveComboxIndex(pngColorDepth);
        }
    }
}
