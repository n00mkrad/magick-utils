using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace MagickUtils
{
    class OtherUtilsUI
    {

        public static void DelNotMatchingWildcard (bool recursive)
        {
            DirectoryInfo d = new DirectoryInfo(Program.currentDir);
            FileInfo[] whitelist;
            FileInfo[] allFiles;

            if(recursive)
            {
                allFiles = d.GetFiles("*.*", SearchOption.AllDirectories);
                whitelist = d.GetFiles("*." + Program.currentExt, SearchOption.AllDirectories);
            }
            else
            {
                allFiles = d.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                whitelist = d.GetFiles("*." + Program.currentExt, SearchOption.TopDirectoryOnly);
            }

            List<string> whitelistedPaths = new List<string>();
            foreach(FileInfo file in whitelist)
                whitelistedPaths.Add(file.FullName);

            Program.Print("Will delete all but " + whitelist.Length + " files out of " + allFiles.Length);
            foreach(FileInfo file in allFiles)
            {
                if(!whitelistedPaths.Contains(file.FullName))
                {
                    Program.Print("Deleting " + file.FullName + "...");
                    file.Delete();
                }
            }
        }

        public static async void RemTransparencyDir (byte mode)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Removing Alpha on Image ", counter, files.Length);
                counter++;
                OtherUtils.RemoveTransparency(file.FullName, mode);
                if(counter % 5 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static void AddSuffixPrefixDir (string text, bool suffix)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles(Config.fileOperationsNoExtFilter);
            Program.PreProcessing(true, false);
            foreach(FileInfo file in files)
            {
                AddSuffixPrefix(file.FullName, text, suffix);
                Program.ShowProgress("", counter, files.Length);
                counter++;
                if(counter % 100 == 0) Program.Print("Renamed " + counter + " files...");
            }
            Program.PostProcessing(true, false);
        }

        public static void AddSuffixPrefix (string path, string text, bool suffix)
        {
            string pathNoExt = Path.ChangeExtension(path, null);
            string ext = Path.GetExtension(path);

            if(suffix)
                File.Move(path, pathNoExt + text + ext);
            else
                File.Move(path, Path.Combine(Path.GetDirectoryName(path), (text + Path.GetFileNameWithoutExtension(path) + ext)));
        }

        public static void ReplaceInFilenamesDir (string textToFind, string textToReplace)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles(Config.fileOperationsNoExtFilter);
            Program.PreProcessing(true, false);
            foreach(FileInfo file in files)
            {
                ReplaceInFilename(file.FullName, textToFind, textToReplace);
                Program.ShowProgress("", counter, files.Length);
                counter++;
                if(counter % 100 == 0) Program.Print("Processed " + counter + " files...");
            }
            Program.Print("Done - Processed " + counter + " files.");
            Program.PostProcessing(true, false);
        }

        public static void ReplaceInFilename (string path, string textToFind, string textToReplace)
        {
            string ext = Path.GetExtension(path);
            string newFilename = Path.GetFileNameWithoutExtension(path).Replace(textToFind, textToReplace);
            string targetPath = Path.Combine(Path.GetDirectoryName(path), newFilename + ext);
            if(File.Exists(targetPath))
            {
                Program.Print("Skipped " + path + " because a file with the target name already exists.");
                return;
            }
            File.Move(path, targetPath);
        }

        public static async void SetColorDepth (int bits)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Setting color depth to " + bits + " on ", counter, files.Length);
                counter++;
                OtherUtils.SetColorDepth(file.FullName, bits);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static void DelSmallImgsDir (int minAxisLength)
        {
            int counter = 1;
            FileInfo[] Files = IOUtils.GetFiles();
            Program.Print("Checking " + Files.Length + " images...");
            Program.PreProcessing();
            foreach(FileInfo file in Files)
            {
                Program.ShowProgress("", counter, Files.Length);
                counter++;
                OtherUtils.DeleteSmallImages(file.FullName, minAxisLength);
            }
            Program.PostProcessing();
        }

        public static void GroupNormalsWithTex (string normalSuffixList, string diffuseSuffixList, bool lowercase)
        {
            string[] nrmSuffixes = normalSuffixList.Split(',');
            string[] albSuffixes = diffuseSuffixList.Split(',');

            string setPrefix = "tex";

            OtherUtils.GroupNormalsWithTex(Program.currentExt, nrmSuffixes, albSuffixes, setPrefix, lowercase);
        }

        public static void DelMissing (string checkDir, bool testRun)
        {
            RemoveMissingFiles(Program.currentDir, checkDir, testRun);
        }

        public static void RemoveMissingFiles (string path, string checkDir, bool testRun)
        {
            FileInfo[] Files = IOUtils.GetFiles(Config.fileOperationsNoExtFilter);
            Program.Print("Checking " + Files.Length + " files...");
            foreach(FileInfo file in Files)
            {
                if(!File.Exists(Path.Combine(checkDir, file.Name)))
                {
                    Program.Print(" -> " + file.Name + " doesn't exist in second dir, will delete");
                    if(!testRun)
                        File.Delete(file.FullName);
                }
            }
        }
    }
}
