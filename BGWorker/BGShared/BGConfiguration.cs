using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGW.BGShared {
	/// <summary>
	/// Class for handling the configuration files.
	/// </summary>
	/// <remarks>
	/// There are two main configuration files
	///	- Log
	///		The log will simply keep track of what has and hasn't been changed.
	///		The log file should change each session, but it will be up to the service to supply a name for the log file, not this class
	///	- Filters
	///		The filters will simply be a list of all of the file filters as a list of strings, each one representing a union on a regex
	/// - Settings
	///		The settings file is an xml file which will keep track of things like the directories that should be changed, and other settings I might think of later
	///		 It will be xml, so it'll be pretty extensible without having to add too much code.
	/// </remarks>
	public class BGConfiguration {
		private string LogDir { get; set; }
		private string LogName { get; set; }
		private string FiltersURI { get; set; }
		private string SettingsURI { get; set; }
		private string WatchDir { get; set; }

		private const string DEFAULT_SETTINGS_URI = @"%USERPROFILE%\_BGWSettings.xml";
		private const string DEFAULT_FILTERS_URI = @"%USERPROFILE%\_BGWFilters";
		private const string DEFAULT_LOGDIR = @"%APPDATA%\BGWorkerLogs";
		private const string DEFAULT_WATCHDIR = @"%USERPROFILE%";
		
		public string LogURI { get => this.LogDir + @"\" + this.LogName; }
		private System.Text.RegularExpressions.Regex _FilterRegex = null;
		public System.Text.RegularExpressions.Regex FilterRegex {
			get => _FilterRegex ?? (_FilterRegex = BGW.BGShared.BGHelper.GetRegex(this.FiltersURI).regex);
		}

		private System.IO.FileSystemWatcher _FileWatcher = null;
		public System.IO.FileSystemWatcher FileWatcher {
			get => _FileWatcher ?? (_FileWatcher = new System.IO.FileSystemWatcher() {
				Path = this.WatchDir,
				IncludeSubdirectories = true,
				Filter = "*",
				NotifyFilter = System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.DirectoryName
			});
		}

		public BGConfiguration(System.Collections.Specialized.NameValueCollection appSettings) {
			//Set the SettingsFileURI.
			this.SettingsURI = System.Environment.ExpandEnvironmentVariables(
				appSettings["SettingsFile"] ?? DEFAULT_SETTINGS_URI
			);
			//Open the SettingsFile.
			var xDoc = System.Xml.Linq.XDocument.Load(this.SettingsURI);

			//Set the filterURI
			this.FiltersURI = xDoc.GetSetting("Filter", DEFAULT_FILTERS_URI);

			//Set the LogDir
			this.LogDir = xDoc.GetSetting("LogDir", DEFAULT_LOGDIR);

			//If the LogDir doesn't exist, create it.
			if (!System.IO.Directory.Exists(this.LogDir)) {
				System.IO.Directory.CreateDirectory(this.LogDir);
			}

			//Set the LogDir
			this.WatchDir = xDoc.GetSetting("WatchDir", DEFAULT_WATCHDIR);

			//Set the LogName
			this.LogName = String.Format("{0:yyyyMMddHHmmss}.log", System.DateTime.Now);

		}
		
	}
}
