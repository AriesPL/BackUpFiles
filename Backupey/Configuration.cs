﻿using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace Backupey
{
	static class Configuration
	{
		public static Settings GetSettings()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false);

			IConfiguration configuration = builder.Build();

			var options = configuration.GetSection("Settings").Get<Settings>();
			return options;
		}

		public static string GetTimestamp()
		{
			return DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss");
		}

		public static void CreateLogger(Settings settings, string timestamp)
		{
			var loggerConfiguration = new LoggerConfiguration()
				.MinimumLevel.Is(settings.LogEventLevel)
				.WriteTo.File($"{timestamp}.txt");

			Log.Logger = loggerConfiguration.CreateLogger();
		}
	}
}