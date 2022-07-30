using System;
using System.IO;

namespace Backupey
{
	static class Program
	{
		static void Main(string[] args)
		{
			var timestamp = Configuration.GetTimestamp();
			string pathToOriginalFolder = "C:/Users/Алексей/source/repos/BackUpFiles/BackUpFiles/original";
			string pathToBackUpFolder = "BackUp/"+timestamp;

			if (!Directory.Exists(pathToBackUpFolder))
				Directory.CreateDirectory(pathToBackUpFolder);

			if (Directory.Exists(pathToOriginalFolder))
			{
				string[] files = Directory.GetFiles(pathToOriginalFolder);

				foreach (string file in files)
				{
					string fileName = Path.GetFileName(file);
					string destFile = Path.Combine(pathToBackUpFolder, fileName);
					try
					{
						File.Copy(file, destFile, true);
						Logbook($"{timestamp} Creating a copy of the file {fileName} successfully.");
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
				Logbook($"{timestamp} Source path does not exist!");
			}

			Logbook("\n");
		}

		private static void Logbook(string records)
		{
			string pathToLogFile = "LogFile.txt";

			if (!File.Exists(pathToLogFile))
				File.Create(pathToLogFile);

			using (StreamWriter streamWriter = new StreamWriter(pathToLogFile, true))
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

