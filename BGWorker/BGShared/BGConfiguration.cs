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
		private string _LogDir = DEFAULT_LOGDIR;
		private string _FiltersURI = DEFAULT_FILTERS_URI;
		private string _SettingsURI = DEFAULT_SETTINGS_URI;
		private string _WatchDir = DEFAULT_WATCHDIR;
		private string _LogName;

		private string LogDir {
			get => _LogDir;
			set {
				_LogDir = value;
				SettingInitialized?.Invoke(this, new LogInfoSettingInit() {
					SettingName = "LogDir",
					NewValue = value,
					TimeStamp = System.DateTime.Now
				});
			}
		}
		private string FiltersURI {
			get => _FiltersURI;
			set {
				_FiltersURI = value;
				SettingInitialized?.Invoke(this, new LogInfoSettingInit() {
					SettingName = "FiltersURI",
					NewValue = value,
					TimeStamp = System.DateTime.Now
				});
			}
		}
		private string SettingsURI {
			get => _SettingsURI;
			set {
				_SettingsURI = value;
				SettingInitialized?.Invoke(this, new LogInfoSettingInit() {
					SettingName = "SettingsURI",
					NewValue = value,
					TimeStamp = System.DateTime.Now
				});
			}
		}
		private string WatchDir {
			get => _WatchDir;
			set {
				_WatchDir = value;
				SettingInitialized?.Invoke(this, new LogInfoSettingInit() {
					SettingName = "WatchDir",
					NewValue = value,
					TimeStamp = System.DateTime.Now
				});
			}
		}
		private string LogName {
			get => _LogName;
			set {
				_LogName = value;
				SettingInitialized?.Invoke(this, new LogInfoSettingInit() {
					SettingName = "LogName",
					NewValue = value,
					TimeStamp = System.DateTime.Now
				});
			}
		}

		//TODO change all of this shit to use a dictionary shenanigans
		private const string DEFAULT_SETTINGS_URI = @"C:\Users\Brett\_BGWSettings.xml";
		private const string DEFAULT_FILTERS_URI = @"C:\Users\Brett\_BGWFilters";
		private const string DEFAULT_LOGDIR = @"C:\Users\Brett\AppData\Roaming\BGWorkerLogs";
		private const string DEFAULT_WATCHDIR = @"C:\Users\Brett";

		private System.Collections.Generic.Dictionary<string, string> MacroDict;

		public event LogEventHandler SettingInitialized;

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

		public BGConfiguration() { }
		public void Init(System.Collections.Specialized.NameValueCollection appSettings) {
			//Set the SettingsFileURI.
			this.SettingsURI = appSettings["SettingsFile"] ?? DEFAULT_SETTINGS_URI;
			//Open the SettingsFile.
			var xDoc = System.Xml.Linq.XDocument.Load(this.SettingsURI);

			this.MacroDict = xDoc.GetMacroDict();
			//Set the filterURI
			this.FiltersURI = this.ExpandMacroVariables(
				xDoc.GetSetting("Filter", DEFAULT_FILTERS_URI)
			);

			//Set the LogDir
			this.LogDir = this.ExpandMacroVariables(
				xDoc.GetSetting("LogDir", DEFAULT_LOGDIR)
			);

			//If the LogDir doesn't exist, create it.
			if (!System.IO.Directory.Exists(this.LogDir)) {
				System.IO.Directory.CreateDirectory(this.LogDir);
			}

			//Set the LogDir
			this.WatchDir = this.ExpandMacroVariables(
				xDoc.GetSetting("WatchDir", DEFAULT_WATCHDIR)
			);

			//Set the LogName
			this.LogName = String.Format("{0:yyyyMMddHHmmss}.log", System.DateTime.Now);
		}

		private string ExpandMacroVariables(string input) {
			//Matchs everywhere where it looks like ${keyname} and replaces keyname with the value in the MacroDict
			//If the key isn't found in the dictionary, just keep it as is.
			return System.Text.RegularExpressions.Regex.Replace(
				input,
				@"\${(?<Key>\w*)}",
				((System.Text.RegularExpressions.Match m) => {
					try {
						return this.MacroDict[m.Groups["Key"].Value];
					}
					catch (KeyNotFoundException) {
						return m.Value;
					}
				})
			);
		}
		
	}
}
