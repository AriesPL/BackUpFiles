﻿using System;
using System.IO;

namespace BackUpFiles
{
	internal class Program
	{
		static DateTime dataNow = DateTime.Now;
		private static string pathToOriginalFolder = "C:/Users/Алексей/source/repos/BackUpFiles/BackUpFiles/original";
		private static string pathToBackUpFolder = "BackUp/" + dataNow.ToShortDateString();
		private static string pathToLogFile = "LogFile.txt";

		static void Main(string[] args)
		{
			if (!File.Exists(pathToLogFile))
				File.Create(pathToLogFile);

			if (!Directory.Exists(pathToBackUpFolder))
				Directory.CreateDirectory(pathToBackUpFolder);

			if (Directory.Exists(pathToOriginalFolder))
			{
				string[] files = Directory.GetFiles(pathToOriginalFolder);

				foreach (string file in files)
				{
					string fileName = Path.GetFileName(file);
					//string sourceFile = Path.Combine(pathToOriginalFolder, fileName);
					string destFile = Path.Combine(pathToBackUpFolder, fileName);
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


