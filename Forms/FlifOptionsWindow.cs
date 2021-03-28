using System;
using System.Windows.Forms;
using MagickUtils.Utils;

namespace MagickUtils
{
    public partial class FlifOptionsWindow : Form
    {
        public FlifOptionsWindow ()
        {
            InitializeComponent();
        }

        private void doneBtn_Click (object sender, EventArgs e)
        {
            Close();
        }

        private void effortCombox_SelectedIndexChanged (object sender, EventArgs e)
        {
            FlifInterface.effort = flifEffort.GetInt();
        }

        private void FlifOptionsWindow_Load (object sender, EventArgs e)
        {
            ConfigParser.LoadComboxIndex(flifEnc);
            ConfigParser.LoadGuiElement(flifEffort);
        }

        private void flifEnc_SelectedIndexChanged(object sender, EventArgs e)
        {
            flifWrapperPanel.Visible = flifEnc.SelectedIndex == 1;
        }

        private void FlifOptionsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigParser.SaveComboxIndex(flifEnc);
            ConfigParser.SaveGuiElement(flifEffort);
        }
    }
}
