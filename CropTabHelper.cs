using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;

namespace MagickUtils
{
    class CropTabHelper
    {
        public static void CropRelative (TextBox minSizeBox, TextBox maxSizeBox, ComboBox sizeModeBox, ComboBox gravBox)
        {
            int minSize = minSizeBox.GetInt();
            int maxSize = minSize;
            if(!string.IsNullOrWhiteSpace(maxSizeBox.Text))
                maxSize = maxSizeBox.GetInt();
            Gravity grav = GetGravity(gravBox.SelectedIndex);
            CropUtils.SizeMode sizeMode = CropUtils.SizeMode.Percentage;
            if(sizeModeBox.SelectedIndex == 1) sizeMode = CropUtils.SizeMode.Height;
            if(sizeModeBox.SelectedIndex == 2) sizeMode = CropUtils.SizeMode.Width;
            if(sizeModeBox.SelectedIndex == 3) sizeMode = CropUtils.SizeMode.Longer;
            if(sizeModeBox.SelectedIndex == 4) sizeMode = CropUtils.SizeMode.Shorter;
            CropUtils.CropRelativeDir(minSize, maxSize, sizeMode, grav);
        }

        public static void CropAbsolute (TextBox wBox, TextBox hBox, ComboBox gravBox)
        {
            CropUtils.CropAbsoluteDir(wBox.GetInt(), hBox.GetInt(), GetGravity(gravBox.SelectedIndex));
        }

        public static Gravity GetGravity (int selectedIndex)
        {
            switch(selectedIndex)
            {
                case 0: return Gravity.West;
                case 1: return Gravity.Center;
                case 2: return Gravity.East;
                case 3: return Gravity.Northwest;
                case 4: return Gravity.North;
                case 5: return Gravity.Northeast;
                case 6: return Gravity.Southwest;
                case 7: return Gravity.South;
                case 8: return Gravity.Southeast;
            }
            return Gravity.Center;
        }
    }
}
