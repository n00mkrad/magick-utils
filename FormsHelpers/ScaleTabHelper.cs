﻿using System;
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
            float sMin = minScaleCombox.GetFloat();
            float sMax = sMin;
            if(!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = maxScaleCombox.GetFloat();
            int filterMode = filterModeCombox.SelectedIndex;
            ScaleUtils.ScaleDir(sMin, sMax, filterMode);
        }

        public static void ScaleFileList (string[] files, ComboBox minScaleCombox, ComboBox maxScaleCombox, ComboBox filterModeCombox)
        {
            float sMin = minScaleCombox.GetFloat();
            float sMax = sMin;
            if(!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = maxScaleCombox.GetFloat();
            int filterMode = filterModeCombox.SelectedIndex;

            foreach(string file in files)
            {
                ScaleUtils.Scale(file, sMin, sMax, filterMode);
            }  
        }

        public static void ResampleUsingPath (ComboBox minScaleCombox, ComboBox maxScaleCombox, ComboBox downFilterCombox, ComboBox upFilterCombox)
        {
            float sMin = minScaleCombox.GetFloat();
            float sMax = sMin;
            if(!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = maxScaleCombox.GetFloat();
            int filterMode = downFilterCombox.SelectedIndex;
            int reupFilterMode = upFilterCombox.SelectedIndex;
            ScaleUtils.ResampleDirRand(sMin, sMax, filterMode, reupFilterMode);
        }

        public static void ResampleFileList (string[] files, ComboBox minScaleCombox, ComboBox maxScaleCombox, ComboBox downFilterCombox, ComboBox upFilterCombox)
        {
            float sMin = minScaleCombox.GetFloat();
            float sMax = sMin;
            if (!string.IsNullOrWhiteSpace(maxScaleCombox.Text.Trim()))
                sMax = maxScaleCombox.GetFloat();
            int filterMode = downFilterCombox.SelectedIndex;
            int reupFilterMode = upFilterCombox.SelectedIndex;

            foreach (string file in files)
            {
                ScaleUtils.RandomResample(file, sMin, sMax, filterMode, reupFilterMode);
            }
        }
    }
}
