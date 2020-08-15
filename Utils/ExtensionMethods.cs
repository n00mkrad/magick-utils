using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagickUtils
{
    public static class ExtensionMethods
    {
        public static double NextDoubleLog(this Random random, double minValue, double maxValue)
        {
            // TODO: some validation here...
            bool posAndNeg = minValue < 0d && maxValue > 0d;
            double minAbs = Math.Min(Math.Abs(minValue), Math.Abs(maxValue));
            double maxAbs = Math.Max(Math.Abs(minValue), Math.Abs(maxValue));

            int sign;
            if (!posAndNeg)
                sign = minValue < 0d ? -1 : 1;
            else
            {
                // if both negative and positive results are expected we select the sign based on the size of the ranges
                double sample = random.NextDouble();
                var rate = minAbs / maxAbs;
                var absMinValue = Math.Abs(minValue);
                bool isNeg = absMinValue <= maxValue ? rate / 2d > sample : rate / 2d < sample;
                sign = isNeg ? -1 : 1;

                // now adjusting the limits for 0..[selected range]
                minAbs = 0d;
                maxAbs = isNeg ? absMinValue : Math.Abs(maxValue);
            }

            // Possible double exponents are -1022..1023 but we don't generate too small exponents for big ranges because
            // that would cause too many almost zero results, which are much smaller than the original NextDouble values.
            double minExponent = minAbs == 0d ? -16d : Math.Log(minAbs, 2d);
            double maxExponent = Math.Log(maxAbs, 2d);
            if (minExponent == maxExponent)
                return minValue;

            // We decrease exponents only if the given range is already small. Even lower than -1022 is no problem, the result may be 0
            if (maxExponent < minExponent)
                minExponent = maxExponent - 4;

            double result = sign * Math.Pow(2d, NextDoubleLinear(random, minExponent, maxExponent));

            // protecting ourselves against inaccurate calculations; however, in practice result is always in range.
            return result < minValue ? minValue : (result > maxValue ? maxValue : result);
        }

        static double NextDoubleLinear(this Random random, double minValue, double maxValue)
        {
            // TODO: some validation here...
            double sample = random.NextDouble();
            return (maxValue * sample) + (minValue * (1d - sample));
        }

        public static float NextFloat(this Random random)
        {
            double mantissa = (random.NextDouble() * 2.0) - 1.0;
            // choose -149 instead of -126 to also generate subnormal floats (*)
            double exponent = Math.Pow(2.0, random.Next(-126, 127));
            return (float)(mantissa * exponent);
        }

        public static string TrimNumbers(this string s)
        {
            s = Regex.Replace(s, "[^.0-9]", "");
            return s.Trim();
        }

        public static int GetInt(this TextBox textbox)
        {
            return int.Parse(textbox.Text.TrimNumbers());
        }

        public static int GetInt(this ComboBox combobox)
        {
            return int.Parse(combobox.Text.TrimNumbers());
        }
    }
}
