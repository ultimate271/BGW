namespace BGW.Controller {
	public partial class Controller {
		private BGW.Model.Heart Heart { get; set; }
		private Log Log { get; set; }
		private Configuration Configuration { get; set; }

		public System.IO.FileSystemWatcher Watcher { get { return this.Configuration.FileWatcher; } }

		public Controller(System.Collections.Specialized.NameValueCollection appSettings) {
			this.Log = new Log();

			this.Configuration = new Configuration();
			this.Configuration.SettingInitialized += ((object sender, LogInfoSettingInit info) => {
				this.Log.Add(info);
			});
			this.Configuration.Init(appSettings);

			this.Heart = new Model.Heart(this.Configuration.FilterRegex);
			this.Heart.FileEdited += ((object sender, Model.FileEdittedEventArgs e) => {
				this.Log.Add(new LogInfoFileEdit {
					FullFileName = e.FullFileName,
					PreviousFileName = e.PreviousFileName,
					TimeStamp = e.TimeStamp
				});
			});
		}

		public void WriteLog() {
			System.IO.File.WriteAllText(this.Configuration.LogURI, this.Log.WriteLog());
		}
	}
}
