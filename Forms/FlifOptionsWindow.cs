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
            Config.LoadComboxIndex(flifEnc);
            Config.LoadGuiElement(flifEffort);
        }

        private void flifEnc_SelectedIndexChanged(object sender, EventArgs e)
        {
            flifWrapperPanel.Visible = flifEnc.SelectedIndex == 1;
        }

        private void FlifOptionsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.SaveComboxIndex(flifEnc);
            Config.SaveGuiElement(flifEffort);
        }
    }
}
