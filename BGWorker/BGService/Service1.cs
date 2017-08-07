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
		private System.IO.FileSystemWatcher FileWatcher;
		private BGW.BGShared.Heart Heart;
		private const string DEFAULT_FILTERS_FILE = "%USERPROFILE%\\.BGWFilters";

		public Service1() {
			InitializeComponent();
		}


		protected override void OnStart(string[] args) {
			//Get config and log file information from app.config
			string settingsURI = System.Configuration.ConfigurationManager.AppSettings["SettingsFile"] ?? DEFAULT_FILTERS_FILE;
			//Instansiate an instance of Heart based on that information.
			//Instansiate an instance of FileSystemWatcher.
			if (this.FileWatcher == null) {
				this.FileWatcher = new System.IO.FileSystemWatcher() {
					IncludeSubdirectories = true,
					EnableRaisingEvents = true,
					Filter = "*.*",
					NotifyFilter = System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.DirectoryName,
					Path = "TODO INSERT PATH HERE"
				};
			}
			this.FileWatcher.Created += ((object sender, System.IO.FileSystemEventArgs e) => {
				var file = BGW.BGShared.BGHelper.GetInfo(e.FullPath);
				this.Heart.ProcessFile(file);
			});
			this.FileWatcher.Renamed += ((object sender, System.IO.RenamedEventArgs e) => {
				return; //TODO do stuff
			});
		}

		protected override void OnStop() {
		}
	}
}
