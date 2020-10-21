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
            if (!InputIsValid(minScaleCombox, maxScaleCombox))
                return;

            float sMin = minScaleCombox.GetFloat();
            float sMax = sMin;
            if(!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = maxScaleCombox.GetFloat();
            int filterMode = filterModeCombox.SelectedIndex;
            ScaleUtils.ScaleDir(sMin, sMax, filterMode, filterModeCombox.Text.Trim());
        }

        public static void ScaleFileList (string[] files, ComboBox minScaleCombox, ComboBox maxScaleCombox, ComboBox filterModeCombox)
        {
            if (!InputIsValid(minScaleCombox, maxScaleCombox))
                return;

            float sMin = minScaleCombox.GetFloat();
            float sMax = sMin;
            if(!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = maxScaleCombox.GetFloat();
            int filterMode = filterModeCombox.SelectedIndex;

            foreach(string file in files)
            {
                ScaleUtils.Scale(file, sMin, sMax, filterMode, filterModeCombox.Text.Trim());
            }  
        }

        public static void ResampleUsingPath (ComboBox minScaleCombox, ComboBox maxScaleCombox, ComboBox downFilterCombox, ComboBox upFilterCombox)
        {
            if (!InputIsValid(minScaleCombox, maxScaleCombox))
                return;

            float sMin = minScaleCombox.GetFloat();
            float sMax = sMin;
            if(!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = maxScaleCombox.GetFloat();
            int filterMode = downFilterCombox.SelectedIndex;
            int reupFilterMode = upFilterCombox.SelectedIndex;
            ScaleUtils.ResampleDirRand(sMin, sMax, filterMode, downFilterCombox.Text.Trim(), reupFilterMode, upFilterCombox.Text.Trim());
        }

        public static void ResampleFileList (string[] files, ComboBox minScaleCombox, ComboBox maxScaleCombox, ComboBox downFilterCombox, ComboBox upFilterCombox)
        {
            if (!InputIsValid(minScaleCombox, maxScaleCombox))
                return;

            float sMin = minScaleCombox.GetFloat();
            float sMax = sMin;
            if (!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = maxScaleCombox.GetFloat();
            int filterMode = downFilterCombox.SelectedIndex;
            int reupFilterMode = upFilterCombox.SelectedIndex;

            foreach (string file in files)
            {
                ScaleUtils.RandomResample(file, sMin, sMax, filterMode, downFilterCombox.Text.Trim(), reupFilterMode, upFilterCombox.Text.Trim());
            }
        }

        static bool InputIsValid (ComboBox minScaleBox, ComboBox maxScaleBox)
        {
            if(ScaleUtils.currMode == ScaleUtils.ScaleMode.Percentage)
            {
                return true;
            }
            else if (minScaleBox.Text.IsIntegerString() == false || maxScaleBox.Text.IsIntegerString() == false)
            {
                MessageBox.Show("Invalid input - Floating point numbers can only be entered for percentage scaling.", "Error");
                return false;
            }
            return true;
        }
    }
}
