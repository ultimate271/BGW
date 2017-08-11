using System.Linq;
using BGW.Shared;
namespace BGW.Controller {
	public delegate void LogEventHandler (object sender, LogInfoSettingInit info);
	public class Log {
		private System.Collections.Generic.List<ILogInfo> LogInfo { get; set; }

		public Log() {
			this.LogInfo = new System.Collections.Generic.List<ILogInfo>();
		}

		public void Add(ILogInfo nextEntry) {
			this.LogInfo.Add(nextEntry);
		}

		public string WriteLog() {
			return (
				from log in this.LogInfo
				orderby log.TimeStamp
				select log.CommentLine
			).Implode('\n');
		}	
	}
}
