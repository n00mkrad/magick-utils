using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using ImageMagick;

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
                RemoveTransparency(file.FullName, mode);
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
                SetColorDepth(file.FullName, bits);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static async void DelSmallImgsDir (int minSize, ImageSizeFilterUtils.SizeMode scaleMode, ImageSizeFilterUtils.Operator op)
        {
            int counter = 1;
            FileInfo[] Files = IOUtils.GetFiles();
            Program.Print("Checking " + Files.Length + " images...");
            Program.PreProcessing();
            foreach(FileInfo file in Files)
            {
                Program.ShowProgress("", counter, Files.Length);
                counter++;
                ImageSizeFilterUtils.DeleteSmallImages(file.FullName, scaleMode, op, minSize);
                if (counter % 10 == 0) await Program.PutTaskDelay();
                if(counter % 100 == 0) Program.Print("Processed " + counter + " files...");
            }
            Program.PostProcessing();
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
            FileInfo[] files = IOUtils.GetFiles(Config.fileOperationsNoExtFilter);
            FileInfo[] filesCheckDir = IOUtils.GetFiles(Config.fileOperationsNoExtFilter, checkDir);
            Program.Print("(1/2) Checking " + files.Length + " files...");
            Program.Print("Folder B has " + filesCheckDir.Length + " files");
            foreach(FileInfo file in files)
            {
                if(!File.Exists(Path.Combine(checkDir, file.Name)))
                {
                    Program.ShowProgress("", counter, files.Length);
                    counter++;
                    Program.Print(" -> " + file.Name + " doesn't exist in second dir, will delete");
                    if(!testRun) File.Delete(file.FullName);
                    if(counter % 50 == 0) Program.Print("Processed " + counter + " files...");
                }
            }
            Program.Print("(2/2) Checking " + filesCheckDir.Length + " files...");
            Program.Print("Folder A has " + filesCheckDir.Length + " files");
            foreach(FileInfo file in filesCheckDir)
            {
                if(!File.Exists(Path.Combine(Program.currentDir, file.Name)))
                {
                    Program.ShowProgress("", counter, files.Length);
                    counter++;
                    Program.Print(" -> " + file.Name + " doesn't exist in second dir, will delete");
                    if(!testRun) File.Delete(file.FullName);
                    if(counter % 50 == 0) Program.Print("Processed " + counter + " files...");
                }
            }
            Program.PostProcessing(false, false);
        }

        public static async void LayerColorDir (string color)
        {
            int counter = 1;
            FileInfo[] files = IOUtils.GetFiles();
            Program.PreProcessing();
            foreach(FileInfo file in files)
            {
                Program.ShowProgress("Overlaying color on image ", counter, files.Length);
                counter++;
                LayerColor(file.FullName, color);
                if(counter % 2 == 0) await Program.PutTaskDelay();
            }
            Program.PostProcessing();
        }

        public static void LayerColor (string path, string color)
        {
            MagickImage img = new MagickImage(path);
            MagickColor imgColor = new MagickColor("#" + color);
            MagickImage overlay = new MagickImage(imgColor, img.Width, img.Height);
            img.Composite(overlay, Gravity.Center, CompositeOperator.Over);
            img.Write(path);
        }

        public static void RemoveTransparency (string path, byte mode)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if(mode == 0) img.ColorAlpha(MagickColors.Black);
            if(mode == 1) img.ColorAlpha(MagickColors.White);
            if(mode == 2) img.Alpha(AlphaOption.Off);
            Program.Print("-> " + Path.GetFileNameWithoutExtension(path));
            img.Write(path);
        }

        public static void SetColorDepth (string path, int bits)
        {
            MagickImage img = IOUtils.ReadImage(path);
            img.BitDepth(bits);
            //img.Depth = bits;
            img.Quality = Program.GetDefaultQuality(img);
            string fname = Path.GetFileName(path);
            Program.Print("-> " + fname);
            img.Write(path);
        }

        public static async void GroupNormalsWithTex (string ext, string[] normalSuffixList, string[] diffuseSuffixList, string setPrefix, bool renLower)
        {
            string copyDiffuseDir = Path.Combine(Program.currentDir, "..", setPrefix + "GroupedDiffuse");
            string copyNormalDir = Path.Combine(Program.currentDir, "..", setPrefix + "GroupedNormal");
            Directory.CreateDirectory(copyDiffuseDir);
            Directory.CreateDirectory(copyNormalDir);

            DirectoryInfo d = new DirectoryInfo(Program.currentDir);
            FileInfo[] files = d.GetFiles("*." + ext, SearchOption.AllDirectories);
            Program.Print("Renaming all files to lowercase...");
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
                        Program.Print("\n-> Found Normal Map: " + file.Name);
                        string fnameNoExt = Path.GetFileNameWithoutExtension(file.Name);
                        foreach(string albSuffix in diffuseSuffixList)
                        {
                            string diffuseName = fnameNoExt.Replace(suffix, albSuffix);
                            diffuseName += file.Extension;
                            string diffuseTexPath = file.DirectoryName + "/" + diffuseName;
                            Program.Print("  -> Looking for diffuse texture: " + diffuseTexPath);
                            if(File.Exists(diffuseTexPath) && DimensionsMatch(file.FullName, diffuseTexPath))
                            {
                                Program.Print("    -> Found diffuse texture: " + Path.GetFileName(diffuseTexPath));
                                File.Copy(diffuseTexPath, copyDiffuseDir + "/" + setPrefix + i + file.Extension, true);
                                Program.Print("    -> Copied diffuse to " + copyDiffuseDir + "/" + setPrefix + i + file.Extension);
                                File.Copy(file.FullName, copyNormalDir + "/" + setPrefix + i + file.Extension, true);
                                Program.Print("    -> Copied normal map to " + copyNormalDir + "/" + setPrefix + i + file.Extension);
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
            Program.PostProcessing(false, false);
        }

        static bool DimensionsMatch (string imgPath1, string imgPath2)
        {
            MagickImage img1 = new MagickImage(imgPath1);
            MagickImage img2 = new MagickImage(imgPath2);
            if(img1.Width == img2.Width && img1.Height == img2.Height)
                return true;
            return false;
        }
    }
}
