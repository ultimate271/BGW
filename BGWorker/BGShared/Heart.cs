using System.Text.RegularExpressions;
using System.IO;
using System.Xml.Linq;

namespace BGW.BGShared {
	/// <summary>
	/// Class which will implement the logic of what goes on.
	/// </summary>
	public class Heart {
		private Regex HideFilter { get; set; }
		private DirectoryInfo BaseDir { get; set; }
		private BGConfiguration BGConfiguration { get; set; }
		

		/// <summary>
		/// Constructor which takes in the configuration xml and creates a heart from it.
		/// </summary>
		/// <param name="xDoc"></param>
		public Heart (XDocument xDoc) {
			this.HideFilter = new Regex("Pattern");
		}

		/// <summary>
		/// Takes in a file and processes it based on the information about it.
		/// </summary>
		/// <param name="procFile">The file to be processed</param>
		public void ProcessFile(FileInfo procFile) {
			//If the filename matches a file that should be hidden
			if (this.HideFilter.IsMatch(procFile.Name)) {
				//Set the file attributes to hidden
				procFile.Attributes = procFile.Attributes | FileAttributes.Hidden;
				//Add the change to the log.
				BGConfiguration.LogInfo.Add(new LogInfo() {
					TimeStamp = System.DateTime.Now,
					FullFileName = procFile.FullName
				});
			}
		}
	}
}
