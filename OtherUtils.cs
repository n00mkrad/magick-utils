using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace MagickUtils
{
    class OtherUtils
    {
        public static void DeleteSmallImages (string path, int minPixels)
        {
            MagickImage img = IOUtils.ReadImage(path);
            if(img.Width < minPixels || img.Height < minPixels)
            {
                string fname = Path.ChangeExtension(path, null);
                Program.Print("-> " + fname + " has a resolution of " + img.Width + "x" + img.Height + ", deleting...");
                File.Delete(img.FileName);
            }
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
            img.Depth = bits;
            img.Quality = Program.GetDefaultQuality(img);
            string fname = Path.ChangeExtension(path, null);
            Program.Print("-> " + fname);
            img.Write(path);
        }

        public static void GroupNormalsWithTex (string ext, string[] normalSuffixList, string[] diffuseSuffixList, string setPrefix, bool renLower)
        {
            string copyDiffuseDir = Path.Combine(Program.currentDir, "..", setPrefix + "GroupedDiffuse");
            string copyNormalDir = Path.Combine(Program.currentDir, "..", setPrefix + "GroupedNormal");
            Directory.CreateDirectory(copyDiffuseDir);
            Directory.CreateDirectory(copyNormalDir);

            DirectoryInfo d = new DirectoryInfo(Program.currentDir);
            FileInfo[] Files = d.GetFiles("*." + ext, SearchOption.AllDirectories);
            Program.Print("Renaming all files to lowercase...");
            if(renLower)
            {
                foreach(FileInfo file in Files)
                {
                    if(file.Name.Any(char.IsUpper))
                        File.Move(file.FullName, file.FullName.Replace(file.Name, file.Name.ToLower()));
                }
            }
            Files = d.GetFiles("*." + ext, SearchOption.AllDirectories);
            int i = 1;
            foreach(FileInfo file in Files)
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
                                continue;
                            }
                        }
                    }
                }
            }
        }

        public static void RemoveMissingFiles (string ext, string checkDir)
        {
            DirectoryInfo d = new DirectoryInfo(Program.currentDir);
            FileInfo[] Files = d.GetFiles("*." + ext);
            int i = 1;
            foreach(FileInfo file in Files)
            {
                Program.Print("Checking " + Path.Combine(checkDir, file.Name));
                if(!File.Exists(Path.Combine(checkDir, file.Name)))
                {
                    Program.Print(" -> " + file.Name + " doesn't exist in second dir, deleting...");
                    File.Delete(file.FullName);
                }
            }
        }

        static bool DimensionsMatch (string imgPath1, string imgPath2)
        {
            MagickImage img1 = new MagickImage(imgPath1);
            MagickImage img2 = new MagickImage(imgPath2);
            if(img1.Width == img2.Width && img1.Height == img2.Height)
                return true;
            return false;
        }

        static bool ContainsInsensitive (string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
