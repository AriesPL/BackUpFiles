using System;
using System.IO;

namespace BackUpFiles
{
	internal class Program
	{
		static DateTime dataNow = DateTime.Now;
		private static string pathToOriginalFolder = "C:/Users/Алексей/source/repos/BackUpFiles/BackUpFiles/original";
		private static string pathToBackUpFolder = "BackUp/" + dataNow.ToShortDateString();

		static void Main(string[] args)
		{
			string fileName;
			string sourceFile;
			string destFile;

			if (!Directory.Exists(pathToBackUpFolder))
				Directory.CreateDirectory(pathToBackUpFolder);

			if (Directory.Exists(pathToOriginalFolder))
			{
				string[] files = Directory.GetFiles(pathToOriginalFolder);

				foreach (string file in files)
				{
					fileName = Path.GetFileName(file);
					sourceFile = Path.Combine(pathToOriginalFolder, fileName);
					destFile = Path.Combine(pathToBackUpFolder, fileName);
					File.Copy(file, destFile, true);
				}
			}
			else
			{
				Console.WriteLine("Source path does not exist!");
			}

			Console.ReadKey();
		}
	}
}


