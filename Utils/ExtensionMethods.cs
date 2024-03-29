﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagickUtils.Utils;

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

        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static string TrimNumbers(this string s, bool allowDotComma = false)
        {
            if (!allowDotComma)
                s = Regex.Replace(s, "[^0-9]", "");
            else
                s = Regex.Replace(s, "[^.,0-9]", "");
            return s.Trim();
        }

        public static bool IsIntegerString (this string str)
        {
            string newStr  = Regex.Replace(str, "[^0-9]", "");
            return (str.Length == newStr.Length);
        }

        public static int GetInt(this TextBox textbox)
        {
            return GetInt(textbox.Text);
        }

        public static int GetInt(this ComboBox combobox)
        {
            return GetInt(combobox.Text);
        }

        public static int GetInt(this string str)
        {
            if (str.Length < 1 || str == null)
                return 0;
            try { return int.Parse(str.TrimNumbers()); }
            catch (Exception e)
            {
                Logger.Log("Failed to parse \"" + str + "\" to int: " + e);
                return 0;
            }
        }

        public static T[] Slice<T>(this T[] source, int index, int length)
        {
            T[] slice = new T[length];
            Array.Copy(source, index, slice, 0, length);
            return slice;
        }

        public static float GetFloat(this TextBox textbox)
        {
            return GetFloat(textbox.Text);
        }

        public static float GetFloat(this ComboBox combobox)
        {
            return GetFloat(combobox.Text);
        }

        public static float GetFloat(this string str)
        {
            if (str.Length < 1 || str == null)
                return 0f;
            string num = str.TrimNumbers(true).Replace(",", ".");
            float value;
            float.TryParse(num, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
            return value;
        }

        public static string Truncate(this string str, int maxChars)
        {
            return str.Length <= maxChars ? str : str.Substring(0, maxChars) + "...";
        }

        public static int RoundToInt(this float f)
        {
            return (int)Math.Round(f);
        }

        public static string RemoveFirstLines(this string str, int linesCount)
        {
            var lines = Regex.Split(str, "\r\n|\r|\n").Skip(linesCount);
            return string.Join(Environment.NewLine, lines.ToArray());
        }

        public static int CountLines (this string str)
        {
            return Regex.Split(str, "\r\n|\r|\n").Length;
        }

        public static string ToStringDot(this float f, string format = "")
        {
            if (string.IsNullOrWhiteSpace(format))
                return f.ToString().Replace(",", ".");
            else
                return f.ToString(format).Replace(",", ".");
        }

        public static string[] Split(this string str, string trimStr)
        {
            return str.Split(new string[] { trimStr }, StringSplitOptions.None);
        }

        public static string TrimWhitespaces(this string str)
        {
            if (str == null) return str;
            var newString = new StringBuilder();
            bool previousIsWhitespace = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsWhiteSpace(str[i]))
                {
                    if (previousIsWhitespace)
                        continue;
                    previousIsWhitespace = true;
                }
                else
                {
                    previousIsWhitespace = false;
                }
                newString.Append(str[i]);
            }
            return newString.ToString();
        }

        public static int Clamp(this int i, int min, int max)
        {
            if (i < min)
                i = min;
            if (i > max)
                i = max;
            return i;
        }

        public static string[] SplitIntoLines(this string str)
        {
            return Regex.Split(str, "\r\n|\r|\n");
        }

        public static string Trunc(this string inStr, int maxChars, bool addEllipsis = true)
        {
            string str = inStr.Length <= maxChars ? inStr : inStr.Substring(0, maxChars);
            if (addEllipsis && inStr.Length > maxChars)
                str += "…";
            return str;
        }

        public static string Wrap(this string path, bool addSpaceFront = false, bool addSpaceEnd = false)
        {
            string s = "\"" + path + "\"";
            if (addSpaceFront)
                s = " " + s;
            if (addSpaceEnd)
                s = s + " ";
            return s;
        }

        public static string GetParentDir(this string path)
        {
            return Directory.GetParent(path).FullName;
        }
    }
}
