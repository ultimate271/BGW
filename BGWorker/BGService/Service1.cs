using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BGW.BGService {
	public partial class Service1 : ServiceBase {
		private BGW.BGShared.BGController Controller;
		private BGW.BGShared.BGConfiguration Config;

		public Service1() {
			InitializeComponent();
			//Get config and log file information from app.config
			this.Config = new BGShared.BGConfiguration(System.Configuration.ConfigurationManager.AppSettings);
			this.Controller = new BGShared.BGController(this.Config);

			this.FileWatcher.Path = this.Config.WatchDir;
			this.FileWatcher.Created += ((object sender, System.IO.FileSystemEventArgs e) => {
				this.Controller.ProcessFile(new BGShared.ProcessFileInput() {
					FileInfo = BGShared.BGHelper.GetInfo(e.FullPath)
				});
			});
			this.FileWatcher.Renamed += ((object sender, System.IO.RenamedEventArgs e) => {
				this.Controller.ProcessFile(new BGShared.ProcessFileInput() {
					FileInfo = BGShared.BGHelper.GetInfo(e.FullPath),
					PreviousURI = e.OldFullPath
				});
			});
			this.FileWatcher.EnableRaisingEvents = true;
		}


		protected override void OnStart(string[] args) {
			string contents = "";
			contents += $"LOG URI : {this.Config.LogURI}\n";
			System.IO.File.WriteAllText("C:\\Users\\Brett\\ITFUCKINGWORKSOKAY.txt", contents);

		}

		protected override void OnStop() {
			this.Controller.WriteLog();
		}

	}
}
