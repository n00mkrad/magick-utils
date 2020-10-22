using System;
using System.Windows.Forms;

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
            Config.LoadGuiElement(ddsUseCrunch);
            Config.LoadComboxIndex(dxtSpeed);
            Config.LoadGuiElement(ddsUseMips);
        }

        private void useMipsCbox_CheckedChanged (object sender, EventArgs e)
        {
            CrunchInterface.currentMipMode = ddsUseMips.Checked;
        }

        private void crunchDdsCbox_CheckedChanged (object sender, EventArgs e)
        {
            crunchPanel.Enabled = ddsUseCrunch.Checked;
        }

        private void doneBtn_Click (object sender, EventArgs e)
        {
            Close();
        }

        private void DdsOptionsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.SaveGuiElement(ddsUseCrunch);
            Config.SaveComboxIndex(dxtSpeed);
            Config.SaveGuiElement(ddsUseMips);
        }
    }
}
