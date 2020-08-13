using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagickUtils
{
    class InpaintTabHelper
    {
        public static void EraseDir (CheckBox thinLines, CheckBox thickLines, CheckBox rings, CheckBox circles, CheckBox grid, ComboBox colorSelect, ComboBox scale)
        {
            List<InpaintUtils.PatternType> patternsToUse = new List<InpaintUtils.PatternType>();
            if(thinLines.Checked) patternsToUse.Add(InpaintUtils.PatternType.ThinLines);
            if(thickLines.Checked) patternsToUse.Add(InpaintUtils.PatternType.ThickLines);
            if(rings.Checked) patternsToUse.Add(InpaintUtils.PatternType.Rings);
            if(circles.Checked) patternsToUse.Add(InpaintUtils.PatternType.Bubbles);
            if(grid.Checked) patternsToUse.Add(InpaintUtils.PatternType.Grid);
            MagickColor color = new MagickColor("#00FF00");
            if(colorSelect.SelectedIndex == 1) /* Pink */ color = new MagickColor("#FF00AA");
            if(colorSelect.SelectedIndex == 2) /* Black */ color = MagickColors.Black;
            if(colorSelect.SelectedIndex == 3) /* White */ color = MagickColors.White;
            InpaintUtils.EraseDir(patternsToUse, scale.SelectedIndex, color);
        }

        public static void EraseFileList (string[] files, CheckBox thinLines, CheckBox thickLines, CheckBox circles, CheckBox grid, ComboBox colorSelect, ComboBox scale)
        {
            List<InpaintUtils.PatternType> patternsToUse = new List<InpaintUtils.PatternType>();
            if(thinLines.Checked) patternsToUse.Add(InpaintUtils.PatternType.ThinLines);
            if(thickLines.Checked) patternsToUse.Add(InpaintUtils.PatternType.ThickLines);
            if(circles.Checked) patternsToUse.Add(InpaintUtils.PatternType.Bubbles);
            if(grid.Checked) patternsToUse.Add(InpaintUtils.PatternType.Grid);
            MagickColor color = new MagickColor("#00FF00");
            if(colorSelect.SelectedIndex == 1) /* Pink */ color = new MagickColor("#FF00AA");
            if(colorSelect.SelectedIndex == 2) /* Black */ color = MagickColors.Black;
            if(colorSelect.SelectedIndex == 3) /* White */ color = MagickColors.White;

            foreach(string file in files)
            {
                InpaintUtils.Erase(file, patternsToUse, scale.SelectedIndex, color);
            }
        }
    }
}
