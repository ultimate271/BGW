namespace BGW.BGShared {
	public class BGController {
		private Heart Heart { get; set; }
		private BGLog Log { get; set; }
		private BGConfiguration Configuration { get; set; }

		public System.IO.FileSystemWatcher Watcher { get => Configuration.FileWatcher; }

		public BGController(System.Collections.Specialized.NameValueCollection appSettings) {
			this.Log = new BGLog();

			this.Configuration = new BGConfiguration();
			this.Configuration.SettingInitialized += ((object sender, ILogInfo info) => {
				this.Log.Add(info);
			});
			this.Configuration.Init(appSettings);

			this.Heart = new Heart(this.Configuration);
			this.Heart.FileEdited += ((object sender, ILogInfo info) => {
				this.Log.Add(info);
			});
		}

		public void ProcessFile(ProcessFileInput file) {
			this.Heart.ProcessFile(file.FileInfo, file.PreviousURI);
		}
		public void WriteLog() {
			System.IO.File.WriteAllText(this.Configuration.LogURI, this.Log.WriteLog());
		}
	}
}
