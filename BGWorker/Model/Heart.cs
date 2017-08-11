using System.Text.RegularExpressions;
using System.IO;
using System.Xml.Linq;

namespace BGW.Model {
	/// <summary>
	/// Class which will implement the logic of what goes on.
	/// </summary>
	public class Heart {
		private Regex HideFilter { get; set; }

		public event FileEdittedEventHandler FileEdited = ((object sender, FileEdittedEventArgs e) => { });

		/// <summary>
		/// Constructor which takes in the configuration xml and creates a heart from it.
		/// </summary>
		/// <param name="xDoc"></param>
		public Heart (Regex HideFilter) {
			this.HideFilter = HideFilter ?? new Regex("");
		}

		public void ProcessFile(FileInfo procFile) {
			this.ProcessFile(procFile, "");
		}
		/// <summary>
		/// Takes in a file and processes it based on the information about it.
		/// </summary>
		/// <param name="procFile">The file to be processed</param>
		/// <param name="PreviousURI">The previous URI, passed in for logging sake</param>
		public void ProcessFile(FileInfo procFile, string PreviousURI) {
			//If the file is already hidden, do nothing
			if ((procFile.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) {
				return;
			}
			//If the filename matches a file that should be hidden
			if (this.HideFilter.IsMatch(procFile.Name)) {
				//Set the file attributes to hidden
				procFile.Attributes = procFile.Attributes | FileAttributes.Hidden;
				//Add the change to the log.
				FileEdited.Invoke(this, new FileEdittedEventArgs() {
					TimeStamp = System.DateTime.Now,
					FullFileName = procFile.FullName,
					PreviousFileName = PreviousURI
				});
			}
		}
	}
}
