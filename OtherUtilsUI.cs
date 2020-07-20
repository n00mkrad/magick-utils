using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MagickUtils
{
    class OtherUtilsUI
    {
        public static void OtherUtilChooser (string ext)
        {
            Console.Write("Available commands:\n");
            Console.Write("1 Delete Small Images\n2 Delete Images Not In Both Folders [!!!]\n3 Delete Files Not Matching Wildcard/Extension");
            Console.Write("\n\n");

            Console.Write("Enter your command number: ");
            string cmd = Console.ReadLine();

            if(int.Parse(cmd) == 1)
                DelSmallImgsDir(ext);
            if(int.Parse(cmd) == 2)
                DelMissing(ext);
            if(int.Parse(cmd) == 3)
                DelNotMatchingWildcard(ext);
        }

        static void DelNotMatchingWildcard (string ext)
        {
            string recursive = "n";
            Console.Write("[Default: n] Recursive (include all subfolders)? (y/n): ");
            string recursiveInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(recursiveInput.Trim())) recursive = recursiveInput;

            DirectoryInfo d = new DirectoryInfo(Program.currentDir);
            FileInfo[] whitelist;
            FileInfo[] allFiles;

            if(Program.IsTrue(recursive))
            {
                allFiles = d.GetFiles("*.*", SearchOption.AllDirectories);
                whitelist = d.GetFiles("*." + ext, SearchOption.AllDirectories);
            }
            else
            {
                allFiles = d.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                whitelist = d.GetFiles("*." + ext, SearchOption.TopDirectoryOnly);
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

        static void DelSmallImgsDir (string ext)
        {
            string minAxisLength = "128";
            Console.Write("[Default: 128] Set minimum length of shorter axis: ");
            string minAxisLengthInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(minAxisLengthInput.Trim())) minAxisLength = minAxisLengthInput;

            int counter = 1;
            DirectoryInfo d = new DirectoryInfo(Program.currentDir);
            FileInfo[] Files = d.GetFiles("*." + ext);
            Program.Print("Checking " + Files.Length + " images...");
            foreach(FileInfo file in Files)
            {
                // Print("Checking Image " + counter + "/" + Files.Length);
                counter++;
                OtherUtils.DeleteSmallImages(file.FullName, int.Parse(minAxisLength));
            }
        }

        public static void GroupNormalsWithTex (string ext)
        {
            string setPrefix = "unnamed";
            Console.Write("[Default: 'unnamed'] Suffix for this texture set (e.g. game name): ");
            string setSuffixInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(setSuffixInput.Trim())) setPrefix = setSuffixInput;

            string lowercase = "y";
            Console.Write("[Default: y] Rename all files to lowercase first? (y/n): ");
            string lowercaseInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(lowercaseInput.Trim())) lowercase = lowercaseInput;

            string normalSuffix = "_n";
            Console.Write("[Default: '_n'] Suffixes of normal map textures, comma-separated: ");
            string normalSuffixInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(normalSuffixInput.Trim())) normalSuffix = normalSuffixInput;

            string[] nrmSuffixes = normalSuffix.Split(',');

            string albedoSuffix = "";
            Console.Write("[Default: ''] Suffixes of albedo/base textures (can be empty for no suffix), comma-separated: ");
            string albedoSuffixInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(albedoSuffixInput.Trim())) albedoSuffix = albedoSuffixInput;

            string[] albSuffixes = albedoSuffix.Split(',');

            OtherUtils.GroupNormalsWithTex(ext, nrmSuffixes, albSuffixes, setPrefix, Program.IsTrue(lowercase));
        }

        public static void DelMissing (string ext)
        {
            string checkDir = "";
            Console.Write("Enter second directory to check for paired files: ");
            string dir2Input = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(dir2Input.Trim())) checkDir = dir2Input;

            /*
            string matchSize = "y";
            Console.Write("[Default: y] Also match image size? (y/n): NOT IMPLEMENTED YET");
            string matchSizeInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(matchSizeInput.Trim())) matchSize = matchSizeInput;
            */

            OtherUtils.RemoveMissingFiles(ext, checkDir);
        }
    }
}
