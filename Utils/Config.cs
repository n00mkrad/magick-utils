using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MagickUtils.Utils;

namespace MagickUtils
{
	internal class Config
	{
        private static string configPath;

        private static string[] cachedLines;

        public static void Init()
        {
            configPath = Path.Combine(Paths.GetDataPath(), "config.ini");
            IOUtils.CreateFileIfNotExists(configPath);
            Reload();
        }

        public static void Set(string key, string value)
        {
            string[] lines = new string[1];

            try
            {
                lines = File.ReadAllLines(configPath);
            }
            catch
            {
                MessageBox.Show("Failed to read config file!\nFlowframes will try to re-create the file if it does not exist.", "Error");

                if (!File.Exists(configPath))
                    Init();
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Split('|')[0] == key)
                {
                    lines[i] = key + "|" + value;
                    File.WriteAllLines(configPath, lines);
                    cachedLines = lines;
                    return;
                }
            }

            List<string> list = lines.ToList();
            list.Add(key + "|" + value);
            list = list.OrderBy(p => p).ToList();

            string newFileContent = "";
            foreach (string line in list)
                newFileContent += line + "\n";

            File.WriteAllText(configPath, newFileContent.Trim());

            cachedLines = list.ToArray();
        }

        public static string Get(string key, string defaultVal)
        {
            WriteIfDoesntExist(key, defaultVal);
            return Get(key);
        }

        public static string Get(string key, Type type = Type.String)
        {
            try
            {
                for (int i = 0; i < cachedLines.Length; i++)
                {
                    string[] keyValuePair = cachedLines[i].Split('|');
                    if (keyValuePair[0] == key && !string.IsNullOrWhiteSpace(keyValuePair[1]))
                        return keyValuePair[1];
                }
                return WriteDefaultValIfExists(key, type);
            }
            catch (Exception e)
            {
                Logger.Log($"Failed to get {key.Wrap()} from config! {e.Message}");
            }
            return null;
        }

        public static bool GetBool(string key)
        {
            return bool.Parse(Get(key, Type.Bool));
        }

        public static bool GetBool(string key, bool defaultVal)
        {
            WriteIfDoesntExist(key, (defaultVal ? "True" : "False"));
            return bool.Parse(Get(key, Type.Bool));
        }

        public static int GetInt(string key)
        {
            return Get(key, Type.Int).GetInt();
        }

        public static int GetInt(string key, int defaultVal)
        {
            WriteIfDoesntExist(key, defaultVal.ToString());
            return GetInt(key);
        }

        public static float GetFloat(string key)
        {
            return float.Parse(Get(key, Type.Float), CultureInfo.InvariantCulture);
        }

        public static float GetFloat(string key, float defaultVal)
        {
            WriteIfDoesntExist(key, defaultVal.ToStringDot());
            return float.Parse(Get(key, Type.Float), CultureInfo.InvariantCulture);
        }

        public static string GetFloatString(string key)
        {
            return Get(key, Type.Float).Replace(",", ".");
        }

        static void WriteIfDoesntExist(string key, string val)
        {
            foreach (string line in cachedLines)
                if (line.Contains(key + "|"))
                    return;
            Set(key, val);
        }

        public enum Type { String, Int, Float, Bool }
        private static string WriteDefaultValIfExists(string key, Type type)
        {
            // General
            if (key == "fileOperationsNoFilter")    return WriteDefault(key, "True");
            if (key == "filenameReplaceIncludeExt") return WriteDefault(key, "True");
            if (key == "backgroundColor")           return WriteDefault(key, "000000FF");
            if (key == "pngColorDepth")             return WriteDefault(key, "0");
            if (key == "procThreads")               return WriteDefault(key, Environment.ProcessorCount.ToString());
            // JPEG
            if (key == "jpegEnc")               return WriteDefault(key, "0");
            if (key == "jpegChromaSubsampling") return WriteDefault(key, "0");
            // DDS
            if (key == "ddsEnc")                return WriteDefault(key, "0");
            if (key == "ddsCompressionType")    return WriteDefault(key, "BC1 (DXT1)");
            if (key == "ddsEnableMips")         return WriteDefault(key, "False");
            if (key == "crunchDxtSpeed")        return WriteDefault(key, "2");
            if (key == "mipCount")              return WriteDefault(key, "10");
            // FLIF
            if (key == "flifEnc")       return WriteDefault(key, "0");
            if (key == "flifEffort")    return WriteDefault(key, "50");

            if (type == Type.Int || type == Type.Float) return WriteDefault(key, "0");     // Write default int/float (0)
            if (type == Type.Bool) return WriteDefault(key, "False");     // Write default bool (False)
            return WriteDefault(key, "0");
        }

        private static string WriteDefault(string key, string def)
        {
            Set(key, def);
            return def;
        }

        private static void Reload()
        {
            List<string> validLines = new List<string>();
            string[] lines = File.ReadAllLines(configPath);
            foreach (string line in lines)
            {
                if (line != null && !string.IsNullOrWhiteSpace(line) && line.Length > 3)
                    validLines.Add(line);
            }
            cachedLines = validLines.ToArray();
        }
    }
}
