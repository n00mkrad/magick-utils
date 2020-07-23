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
            CenterToScreen();
            UpdateUI();
        }

        void UpdateUI ()
        {
            crunchDdsCbox.Checked = FormatOptions.ddsUseCrunch;
            crunchPanel.Enabled = FormatOptions.ddsUseCrunch;
        }

        private void dxtQualCombox_SelectedIndexChanged (object sender, EventArgs e)
        {
            if(dxtQualCombox.SelectedIndex == 0) CrunchInterface.currentQual = CrunchInterface.DXTQuality.superfast;
            if(dxtQualCombox.SelectedIndex == 1) CrunchInterface.currentQual = CrunchInterface.DXTQuality.fast;
            if(dxtQualCombox.SelectedIndex == 2) CrunchInterface.currentQual = CrunchInterface.DXTQuality.normal;
            if(dxtQualCombox.SelectedIndex == 3) CrunchInterface.currentQual = CrunchInterface.DXTQuality.better;
            if(dxtQualCombox.SelectedIndex == 4) CrunchInterface.currentQual = CrunchInterface.DXTQuality.uber;
        }

        private void useMipsCbox_CheckedChanged (object sender, EventArgs e)
        {
            CrunchInterface.currentMipMode = useMipsCbox.Checked;
        }

        private void crunchDdsCbox_CheckedChanged (object sender, EventArgs e)
        {
            FormatOptions.ddsUseCrunch = crunchDdsCbox.Checked;
            UpdateUI();
        }

        private void doneBtn_Click (object sender, EventArgs e)
        {
            Close();
        }
    }
}
