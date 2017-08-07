namespace BGW.BGShared {
	public class BGController {
		private Heart Heart { get; set; }
		private BGLog Log { get; set; }

		public BGController(BGConfiguration config) {
			this.Heart = new Heart(config);
			this.Heart.FileEdited += ((object sender, LogInfo info) => {
				this.Log.Add(info);
			});
			this.Log = new BGLog(config);
		}

		public void ProcessFile(ProcessFileInput file) {
			this.Heart.ProcessFile(file.FileInfo, file.PreviousURI);
		}
		public void WriteLog() {
			this.Log.WriteLog();
		}
	}
}
