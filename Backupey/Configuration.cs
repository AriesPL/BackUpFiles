using System;
using System.Collections.Generic;
using System.Text;

namespace Backupey
{
	static class Configuration
	{
		public static string GetTimestamp()
		{
			return DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss");
		}
	}
}
