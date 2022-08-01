using Serilog.Events;

namespace Backupey
{
	public class Settings
	{
		public string[] SourcePaths { get; set; }
		public string TargetPath { get; set; }
		public LogEventLevel LogEventLevel { get; set; }
	}
}
