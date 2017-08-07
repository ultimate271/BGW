using System.Linq;
namespace BGW.BGShared {
	public class BGLog {
		private System.Collections.Generic.List<LogInfo> LogInfo { get; set; }
		private string LogURI { get; set; }

		public BGLog(BGConfiguration config) {
			this.LogInfo = new System.Collections.Generic.List<BGShared.LogInfo>();
			this.LogURI = config.LogURI;
		}

		public void Add(LogInfo nextEntry) {
			this.LogInfo.Add(nextEntry);
		}

		public void WriteLog() {
			string fileContents = (
				from log in this.LogInfo
				select log.CommentLine
			).Implode('\n');
			System.IO.File.WriteAllText(this.LogURI, fileContents);
		}	
	}
}
