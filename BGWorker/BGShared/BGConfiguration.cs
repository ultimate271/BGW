using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGW.BGShared {
	/// <summary>
	/// Class for handling the configuration files that get passed in to the heart.
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
		private string LogURI { get; set; }
		private string FiltersURI { get; set; }
		private string SettingsURI { get; set; }
		
		public List<LogInfo> LogInfo { get; }
	}
}
