using System;
using System.IO;

namespace BackUpFiles
{
	internal class Program
	{
		static DateTime dataNow = DateTime.Now;
		private static string pathToOriginalFolder = "C:/Users/Алексей/source/repos/BackUpFiles/BackUpFiles/original";
		private static string pathToBackUpFolder = "BackUp/" + dataNow.ToShortDateString();
		private static string pathToIogFile = "LogFile.txt";

		static void Main(string[] args)
		{
			string fileName;
			string sourceFile;
			string destFile;

			if (!File.Exists(pathToIogFile))
				File.Create(pathToIogFile);

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
					try 
					{
						File.Copy(file, destFile, true);
						Logbook($"{dataNow} Creating a copy of the file {fileName} successfully.");
					}
					catch (Exception ex)
					{
						Logbook("Exception: " + ex.Message);
					}
				}
			}

			else
			{
				Console.WriteLine("Source path does not exist!");
				Logbook($"{dataNow} Source path does not exist!");
			}

			Logbook("\n");
			Console.ReadKey();
		}

		private static void Logbook(string records)
		{
			using (StreamWriter streamWriter = new StreamWriter(pathToIogFile, true))
			{
				try
				{
					streamWriter.WriteLine(records);
				}
				catch (Exception ex)
				{
					streamWriter.WriteLine("Exception: " + ex.Message);
				}
			}
		}
	}
}


