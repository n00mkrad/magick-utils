using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagickUtils.Utils;

namespace MagickUtils
{
	internal class Config
	{
        private static string configPath;
        private static string[] cachedLines;
        public static bool locked;

        public static void Init()
        {
            configPath = Path.Combine(Paths.GetDataPath(), "config.ini");
            IOUtils.CreateFileIfNotExists(configPath);
            Reload();
        }

        public static async Task Set(string key, string value)
        {
            Logger.Log($"Setting config key '{key}' to '{value}'...", true);

            while (locked)
                await Task.Delay(1);

            Logger.Log($"...unlocked", true);
            locked = true;

            string[] lines = new string[1];

            try
            {
                lines = File.ReadAllLines(configPath);
            }
            catch
            {
                MessageBox.Show("Failed to read config file!\nMagickUtils will try to re-create the file if it does not exist.", "Error");

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
                    locked = false;
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

            locked = false;
        }

        public static async Task<string> Get(string key, string defaultVal)
        {
            await WriteIfDoesntExist(key, defaultVal);
            return await Get(key);
        }

        public static async Task<string> Get(string key, Type type = Type.String)
        {
            try
            {
                for (int i = 0; i < cachedLines.Length; i++)
                {
                    string[] keyValuePair = cachedLines[i].Split('|');

                    if (keyValuePair[0] == key && !string.IsNullOrWhiteSpace(keyValuePair[1]))
                        return keyValuePair[1];
                }

                return await WriteDefaultValIfExists(key, type);
            }
            catch (Exception e)
            {
                Logger.Log($"Failed to get {key.Wrap()} from config! {e.Message}");
            }

            return null;
        }

        public static async Task<bool> GetBool(string key)
        {
            return bool.Parse(await Get(key, Type.Bool));
        }

        public static async Task<bool> GetBool(string key, bool defaultVal)
        {
            await WriteIfDoesntExist(key, (defaultVal ? "True" : "False"));
            return bool.Parse(await Get(key, Type.Bool));
        }

        public static async Task<int> GetInt(string key)
        {
            return (await Get(key, Type.Int)).GetInt();
        }

        public static async Task<int> GetInt(string key, int defaultVal)
        {
            await WriteIfDoesntExist(key, defaultVal.ToString());
            return await GetInt(key);
        }

        public static async Task<float> GetFloat(string key)
        {
            return float.Parse(await Get(key, Type.Float), CultureInfo.InvariantCulture);
        }

        public static async Task<float> GetFloat(string key, float defaultVal)
        {
            await WriteIfDoesntExist(key, defaultVal.ToStringDot());
            return float.Parse(await Get(key, Type.Float), CultureInfo.InvariantCulture);
        }

        public static async Task<string> GetFloatString(string key)
        {
            return (await Get(key, Type.Float)).Replace(",", ".");
        }

        static async Task WriteIfDoesntExist(string key, string val)
        {
            foreach (string line in cachedLines)
                if (line.Contains(key + "|"))
                    return;

            await Set(key, val);
        }

        public enum Type { String, Int, Float, Bool }
        private static async Task<string> WriteDefaultValIfExists(string key, Type type)
        {
            // General
            if (key == "fileOperationsNoFilter")    return await WriteDefault(key, "True");
            if (key == "filenameReplaceIncludeExt") return await WriteDefault(key, "True");
            if (key == "backgroundColor")           return await WriteDefault(key, "000000FF");
            if (key == "pngColorDepth")             return await WriteDefault(key, "0");
            if (key == "procThreads")               return await WriteDefault(key, Environment.ProcessorCount.ToString());
            // JPEG
            if (key == "jpegEnc")               return await WriteDefault(key, "0");
            if (key == "jpegChromaSubsampling") return await WriteDefault(key, "0");
            // DDS
            if (key == "ddsEnc")                return await WriteDefault(key, "0");
            if (key == "ddsCompressionType")    return await WriteDefault(key, "BC1 (DXT1)");
            if (key == "ddsEnableMips")         return await WriteDefault(key, "False");
            if (key == "crunchDxtSpeed")        return await WriteDefault(key, "2");
            if (key == "mipCount")              return await WriteDefault(key, "10");
            // FLIF
            if (key == "flifEnc")       return await WriteDefault(key, "0");
            if (key == "flifEffort")    return await WriteDefault(key, "50");

            if (type == Type.Int || type == Type.Float) return await WriteDefault(key, "0");     // Write default int/float (0)
            if (type == Type.Bool) return await WriteDefault(key, "False");     // Write default bool (False)
            return await WriteDefault(key, "0");
        }

        private static async Task<string> WriteDefault(string key, string def)
        {
            await Set(key, def);
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
