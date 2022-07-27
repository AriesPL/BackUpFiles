using System;
using System.IO;

namespace BackUpFiles
{
	internal class Program
	{
		private static string pathToOriginalFolder = "C:/Users/Алексей/source/repos/BackUpFiles/BackUpFiles/original";
		private static string pathToBackUpFolder = "BackUp";

		static void Main(string[] args)
		{
            string fileName = "1.txt";

            string sourceFile = Path.Combine(pathToOriginalFolder, fileName);
            string destFile = Path.Combine(pathToBackUpFolder, fileName);

            Directory.CreateDirectory(pathToBackUpFolder);

            File.Copy(sourceFile, destFile, true);

            if (Directory.Exists(pathToOriginalFolder))
            {
                string[] files = Directory.GetFiles(pathToOriginalFolder);

                foreach (string file in files)
                {
                    fileName = Path.GetFileName(file);
                    destFile = Path.Combine(pathToBackUpFolder, fileName);
                    File.Copy(file, destFile, true);
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}


