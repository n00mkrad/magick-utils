﻿using MagickUtils.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MagickUtils
{
    class CrunchInterface
    {
        static string crunchExePath;
        public enum DXTQuality { superfast, fast, normal, better, uber };
        public static DXTQuality currentQual;
        public static bool currentMipMode;

        public static void ExtractExe ()
        {
            GetExePath();
            if(File.Exists(crunchExePath))
                return;
            File.WriteAllBytes(crunchExePath, Resources.crunch);
            Program.Print("[CrunchInterface] Extracted crunch.exe to " + crunchExePath);
        }

        public static void DeleteExe ()
        {
            GetExePath();
            File.Delete(crunchExePath);
        }

        static void GetExePath ()
        {
            crunchExePath = Path.Combine(IOUtils.GetAppDataDir(), "crunch.exe");
        }

        public static void CrunchImage (string path, int qMin, int qMax, bool deleteSrc)
        {
            ExtractExe();
            ProcessStartInfo psi;
            string mipMode = "None";
            if(currentMipMode) mipMode = "UseSourceOrGenerate";
            string args1 = "-file \"" + path + "\" -outsamedir";
            string args2 = " -quality " + GetRandomQuality(qMin, qMax)
                + " -fileformat dds"
                + " -mipMode " + mipMode
                + " -dxtQuality " + currentQual.ToString();
            psi = new ProcessStartInfo { FileName = crunchExePath, Arguments = args1 + args2 };
            Program.Print("Crunch args:" + args2);
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process crunchProcess = new Process { StartInfo = psi };
            crunchProcess.Start();
            crunchProcess.WaitForExit();
            Program.Print("Done crunching " + path);
            if(deleteSrc)
                File.Delete(path);
        }

        static int GetRandomQuality (int qMin, int qMax)
        {
            Random rand = new Random();
            return rand.Next(qMin, qMax + 1);
        }
    }
}
