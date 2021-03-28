using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using ImageMagick;
using MagickUtils.Utils;

namespace MagickUtils
{
    class OtherUtils
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

            Logger.Log("Will delete all but " + whitelist.Length + " files out of " + allFiles.Length);
            foreach(FileInfo file in allFiles)
            {
                if(!whitelistedPaths.Contains(file.FullName))
                {
                    Logger.Log("Deleting " + file.FullName + "...");
                    file.Delete();
                }
            }
        }

        public static async void AddSuffixPrefixDir (string text, bool suffix)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles(Config.GetBool("fileOperationsNoFilter"));
            Program.PreProcessing(true, false);
            foreach(FileInfo file in files)
            {
                AddSuffixPrefix(file.FullName, text, suffix);
                Program.ShowProgress("", counter, files.Length);
                counter++;
                if (counter % 100 == 0)
                {
                    Logger.Log("Renamed " + counter + " files...");
                    await Program.PutTaskDelay();
                }
            }
            Program.PostProcessing(files.Length, true, false);
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

        public static async void ReplaceInFilenamesDir (string textToFind, string textToReplace)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles(Config.GetBool("fileOperationsNoFilter"));
            Program.PreProcessing(true, false);
            foreach(FileInfo file in files)
            {
                ReplaceInFilename(file.FullName, textToFind, textToReplace);
                Program.ShowProgress("", counter, files.Length);
                counter++;
                if (counter % 100 == 0)
                {
                    Logger.Log("Processed " + counter + " files...");
                    await Program.PutTaskDelay();
                }
            }
            Logger.Log("Done - Processed " + counter + " files.");
            Program.PostProcessing(files.Length, true, false);
        }

        public static void ReplaceInFilename (string path, string textToFind, string textToReplace)
        {
            string ext = Path.GetExtension(path);
            string newFilename = Path.GetFileNameWithoutExtension(path).Replace(textToFind, textToReplace)+ ext;
            bool includeExtension = Config.GetBool("filenameReplaceIncludeExt");
            if (includeExtension)
                newFilename = Path.GetFileName(path).Replace(textToFind, textToReplace);
            string targetPath = Path.Combine(Path.GetDirectoryName(path), newFilename);
            try
            {
                if(path != targetPath)
                {
                    File.Move(path, targetPath);
                    Logger.Log(Path.GetFileName(path) + " => " + Path.GetFileName(targetPath));
                }
            }
            catch
            {
                Logger.Log("Can't rename " + Path.GetFileName(path) + " to " + Path.GetFileName(targetPath) + "perhaps a file with that name already exists");
            }
        }

        public static async void DelSmallImgsDir (int minSize, ImageSizeFilterUtils.SizeMode scaleMode, ImageSizeFilterUtils.Operator op)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Logger.Log("Checking " + files.Length + " images...");
            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("", counter, files.Length);
                counter++;
                ImageSizeFilterUtils.DeleteSmallImages(file.FullName, scaleMode, op, minSize);
                if (counter % 10 == 0) await Program.PutTaskDelay();
                if(counter % 100 == 0) Logger.Log("Processed " + counter + " files...");
            }
            Program.PostProcessing(files.Length);
        }

        public static void GroupNormalsWithTex (string normalSuffixList, string diffuseSuffixList, bool lowercase)
        {
            string[] nrmSuffixes = normalSuffixList.Split(',');
            string[] albSuffixes = diffuseSuffixList.Split(',');

            string setPrefix = "tex";

            GroupNormalsWithTex(Program.currentExt, nrmSuffixes, albSuffixes, setPrefix, lowercase);
        }

        public static void DelMissing (string checkDir, bool testRun)
        {
            RemoveMissingFiles(checkDir, testRun);
        }

        public static void RemoveMissingFiles (string checkDir, bool testRun)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles(Config.GetBool("fileOperationsNoFilter"));
            FileInfo[] filesCheckDir = IOUtils.GetFiles(Config.GetBool("fileOperationsNoFilter"), checkDir);
            Logger.Log("(1/2) Checking " + files.Length + " files...");
            Logger.Log("Folder B has " + filesCheckDir.Length + " files");
            foreach(FileInfo file in files)
            {
                if(!File.Exists(Path.Combine(checkDir, file.Name)))
                {
                    Program.ShowProgress("", counter, files.Length);
                    counter++;
                    Logger.Log("-> " + file.Name + " doesn't exist in second dir, will delete");
                    if(!testRun) File.Delete(file.FullName);
                    if(counter % 50 == 0) Logger.Log("Processed " + counter + " files...");
                }
            }
            Logger.Log("(2/2) Checking " + filesCheckDir.Length + " files...");
            Logger.Log("Folder A has " + filesCheckDir.Length + " files");
            foreach(FileInfo file in filesCheckDir)
            {
                if(!File.Exists(Path.Combine(Program.currentDir, file.Name)))
                {
                    Program.ShowProgress("", counter, files.Length);
                    counter++;
                    Logger.Log("-> " + file.Name + " doesn't exist in second dir, will delete");
                    if(!testRun) File.Delete(file.FullName);
                    if(counter % 50 == 0) Logger.Log("Processed " + counter + " files...");
                }
            }
            Program.PostProcessing(files.Length, false, false);
        }

        public static async void GroupNormalsWithTex (string ext, string[] normalSuffixList, string[] diffuseSuffixList, string setPrefix, bool renLower)
        {
            string copyDiffuseDir = Path.Combine(Program.currentDir, "..", setPrefix + "GroupedDiffuse");
            string copyNormalDir = Path.Combine(Program.currentDir, "..", setPrefix + "GroupedNormal");
            Directory.CreateDirectory(copyDiffuseDir);
            Directory.CreateDirectory(copyNormalDir);

            DirectoryInfo d = new DirectoryInfo(Program.currentDir);
            FileInfo[] files = d.GetFiles("*." + ext, SearchOption.AllDirectories);
            Logger.Log("Renaming all files to lowercase...");
            if(renLower)
            {
                foreach(FileInfo file in files)
                {
                    if(file.Name.Any(char.IsUpper))
                        File.Move(file.FullName, file.FullName.Replace(file.Name, file.Name.ToLower()));
                }
            }
            files = d.GetFiles("*." + ext, SearchOption.AllDirectories);
            int counter = 1;
            int i = 1;
            foreach(FileInfo file in files)
            {
                string currentFileExt = Path.GetExtension(file.FullName);
                foreach(string suffix in normalSuffixList)
                {
                    if(file.Name.Contains(suffix + currentFileExt))
                    {
                        Logger.Log("\n-> Found Normal Map: " + file.Name);
                        string fnameNoExt = Path.GetFileNameWithoutExtension(file.Name);
                        foreach(string albSuffix in diffuseSuffixList)
                        {
                            string diffuseName = fnameNoExt.Replace(suffix, albSuffix);
                            diffuseName += file.Extension;
                            string diffuseTexPath = file.DirectoryName + "/" + diffuseName;
                            Logger.Log("-> Looking for diffuse texture: " + diffuseTexPath);
                            if(File.Exists(diffuseTexPath) && DimensionsMatch(file.FullName, diffuseTexPath))
                            {
                                Logger.Log("    -> Found diffuse texture: " + Path.GetFileName(diffuseTexPath));
                                File.Copy(diffuseTexPath, copyDiffuseDir + "/" + setPrefix + i + file.Extension, true);
                                Logger.Log("    -> Copied diffuse to " + copyDiffuseDir + "/" + setPrefix + i + file.Extension);
                                File.Copy(file.FullName, copyNormalDir + "/" + setPrefix + i + file.Extension, true);
                                Logger.Log("    -> Copied normal map to " + copyNormalDir + "/" + setPrefix + i + file.Extension);
                                i++;
                                await Program.PutTaskDelay();
                                continue;
                            }
                        }
                    }
                }
                counter++;
                Program.ShowProgress("", counter, files.Length);
            }
            Program.PostProcessing(files.Length, false, false);
        }

        public static void RenameCounterDir(int sortMode, bool zeroPadding, int startAt)
        {
            int counter = startAt;
            FileInfo[] files = IOUtils.GetFiles(Config.GetBool("fileOperationsNoFilter"));
            var filesSorted = files.OrderBy(n => n);
            if(sortMode == 1)
                filesSorted.Reverse();
            Program.PreProcessing(true, false);
            foreach(FileInfo file in files)
            {
                string dir = new DirectoryInfo(file.FullName).Parent.FullName;
                int filesDigits = (int)Math.Floor(Math.Log10((double)files.Length) + 1);
                if(zeroPadding)
                    File.Move(file.FullName, Path.Combine(dir, counter.ToString().PadLeft(filesDigits, '0') + Path.GetExtension(file.FullName)));
                else
                    File.Move(file.FullName, Path.Combine(dir, counter.ToString() + Path.GetExtension(file.FullName)));
                Program.ShowProgress("", counter, files.Length);
                counter++;
                if(counter % 100 == 0) Logger.Log("Renamed " + counter + " files...");
            }
            Program.PostProcessing(files.Length, true, false);
        }

        public static async void AddZeroPaddingDir (int targetLength)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles(Config.GetBool("fileOperationsNoFilter"));
            Program.PreProcessing(true, false);
            foreach(FileInfo file in files)
            {
                string fnameNoExt = Path.GetFileNameWithoutExtension(file.Name);
                string ext = Path.GetExtension(file.Name);
                //string dir = new DirectoryInfo(file.FullName).Parent.FullName;
                //int filesDigits = (int)Math.Floor(Math.Log10((double)files.Length) + 1);
                //File.Move(file.FullName, Path.Combine(fnameNoExt.PadLeft(targetLength, '0') + Path.GetExtension(file.FullName)));
                File.Move(file.FullName, Path.Combine(Path.GetDirectoryName(file.FullName), fnameNoExt.PadLeft(targetLength, '0') + ext));
                Program.ShowProgress("", counter, files.Length);
                counter++;
                if(counter % 100 == 0)
                {
                    Logger.Log("Renamed " + counter + " files...");
                    await Program.PutTaskDelay();
                }
            }
            Program.PostProcessing(files.Length, true, false);
        }

        public static async void PrintImageInfoDir ()
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles(Config.GetBool("fileOperationsNoFilter"));
            Program.PreProcessing(true, false);
            foreach (FileInfo file in files)
            {
                MagickImage img = IOUtils.ReadImage(file.FullName);
                string fnameNoExt = Path.GetFileNameWithoutExtension(file.Name);
                Program.ShowProgress("", counter, files.Length);
                counter++;
                if (counter % 10 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing(files.Length, true, false);
        }

        static bool DimensionsMatch (string imgPath1, string imgPath2)
        {
            MagickImage img1 = new MagickImage(imgPath1);
            MagickImage img2 = new MagickImage(imgPath2);
            if(img1.Width == img2.Width && img1.Height == img2.Height)
                return true;
            return false;
        }

        public static async void RemoveBytesDir (int bytes) 
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles(Config.GetBool("fileOperationsNoFilter"));
            Program.PreProcessing(true, false);
            foreach (FileInfo file in files)
            {
                string fnameNoExt = Path.GetFileNameWithoutExtension(file.Name);
                RemoveBytes(file.FullName, bytes);
                Program.ShowProgress("", counter, files.Length);
                counter++;
                if (counter % 100 == 0)
                {
                    Logger.Log("Processed " + counter + " files...");
                    await Program.PutTaskDelay();
                }
            }
            Program.PostProcessing(files.Length, true, false);
        }

        public static void RemoveBytes (string path, int bytes)
        {
            byte[] x = File.ReadAllBytes(path);
            try
            {
                byte[] temp = new byte[x.Length - bytes];
                ulong tempx = 0;
                for (long i = bytes; i < x.LongLength; i++)
                {
                    temp[tempx] = x[i];
                    tempx++;
                }
                File.WriteAllBytes(path, temp);
            }
            catch
            {
                Logger.Log("Failed to remove bytes on " + path + " (Filesize: " + x.Length + " bytes)");
            }
        }
    }
}
