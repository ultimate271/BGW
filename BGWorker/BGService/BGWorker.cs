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
	public partial class BGWorker : ServiceBase {
		private BGW.BGShared.BGController Controller { get; set; }
		private System.IO.FileSystemWatcher Watcher { get; set; }

		public BGWorker() {
			InitializeComponent();
		}


		protected override void OnStart(string[] args) {
			this.Controller = new BGShared.BGController(System.Configuration.ConfigurationManager.AppSettings);
			this.Watcher = this.Controller.Watcher;

			this.Watcher.Created += ((object sender, System.IO.FileSystemEventArgs e) => {
				this.Controller.ProcessFile(new BGShared.ProcessFileInput() {
					FileInfo = BGShared.BGHelper.GetInfo(e.FullPath)
				});
			});
			this.Watcher.Renamed += ((object sender, System.IO.RenamedEventArgs e) => {
				this.Controller.ProcessFile(new BGShared.ProcessFileInput() {
					FileInfo = BGShared.BGHelper.GetInfo(e.FullPath),
					PreviousURI = e.OldFullPath
				});
			});
			this.Watcher.EnableRaisingEvents = true;
		}

		protected override void OnStop() {
			this.Controller.WriteLog();
		}

	}
}
