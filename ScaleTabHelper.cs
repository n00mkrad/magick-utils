using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagickUtils
{
    class ScaleTabHelper
    {
        public static void ScaleUsingPath (ComboBox minScaleCombox, ComboBox maxScaleCombox, ComboBox filterModeCombox)
        {
            int sMin = int.Parse(minScaleCombox.Text);
            int sMax = sMin;
            if(!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = int.Parse(maxScaleCombox.Text);
            int filterMode = filterModeCombox.SelectedIndex;
            ScaleUtils.ScaleDir(sMin, sMax, filterMode);
        }

        public static void ScaleFileList (string[] files, ComboBox minScaleCombox, ComboBox maxScaleCombox, ComboBox filterModeCombox)
        {
            int sMin = int.Parse(minScaleCombox.Text);
            int sMax = sMin;
            if(!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = int.Parse(maxScaleCombox.Text);
            int filterMode = filterModeCombox.SelectedIndex;

            foreach(string file in files)
            {
                ScaleUtils.Scale(file, sMin, sMax, filterMode);
            }  
        }

        public static void ResampleUsingPath (ComboBox minScaleCombox, ComboBox maxScaleCombox, ComboBox filterModeCombox)
        {
            int sMin = int.Parse(minScaleCombox.Text);
            int sMax = sMin;
            if(!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = int.Parse(maxScaleCombox.Text);
            int filterMode = filterModeCombox.SelectedIndex;
            ScaleUtils.ResampleDirRand(sMin, sMax, filterMode);
        }

        public static void ResampleFileList (string[] files, ComboBox minScaleCombox, ComboBox maxScaleCombox, ComboBox filterModeCombox)
        {
            int sMin = int.Parse(minScaleCombox.Text);
            int sMax = sMin;
            if(!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = int.Parse(maxScaleCombox.Text);
            int filterMode = filterModeCombox.SelectedIndex;

            foreach(string file in files)
            {
                ScaleUtils.RandomResample(file, sMin, sMax, filterMode);
            }
        }
    }
}
