using System.Linq;
namespace BGW.BGShared {
	public delegate void LogEventHandler (object sender, ILogInfo info);
	public class BGLog {
		private System.Collections.Generic.List<ILogInfo> LogInfo { get; set; }

		public BGLog() {
			this.LogInfo = new System.Collections.Generic.List<BGShared.ILogInfo>();
		}

		public void Add(ILogInfo nextEntry) {
			this.LogInfo.Add(nextEntry);
		}

		public string WriteLog() {
			return (
				from log in this.LogInfo
				select log.CommentLine
			).Implode('\n');
		}	
	}
}
