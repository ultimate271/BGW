using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGW.BGTester {
	public class Program {
		static void Main(string[] args) {
			var controller = new BGW.Controller.Controller(System.Configuration.ConfigurationManager.AppSettings);
			var watcher = controller.Watcher;
			watcher.Created += ((object sender, System.IO.FileSystemEventArgs e) => {
				controller.ProcessFile(new BGW.Controller.ProcessFileInput() {
					FileInfo = Shared.Helper.GetInfo(e.FullPath)
				});
			});
			watcher.Renamed += ((object sender, System.IO.RenamedEventArgs e) => {
				controller.ProcessFile(new BGW.Controller.ProcessFileInput() {
					FileInfo = Shared.Helper.GetInfo(e.FullPath),
					PreviousURI = e.OldFullPath
				});
			});
			watcher.EnableRaisingEvents = true;

			while (System.Console.ReadLine() != "exit") { }

			controller.WriteLog();
			/*
			string filtersURI = System.Configuration.ConfigurationManager.AppSettings["FiltersFile"] ?? DEFAULT_FILTERS_FILE;
			var appsettings = System.Configuration.ConfigurationManager.AppSettings;
			string something = appsettings["FiltersFile"];
			var retVal = BGW.BGShared.BGHelper.GetRegex(filtersURI);
			var someregex = retVal.regex;
			var myConfig = new BGW.BGShared.BGConfiguration(System.Configuration.ConfigurationManager.AppSettings);
			var myHeart = new BGW.BGShared.Heart(myConfig);
			myHeart.FileEdited += MyHeart_FileEdited;

			var MyFileWatcher = new System.IO.FileSystemWatcher();

			MyFileWatcher.Path = "C:\\Temp";
			MyFileWatcher.IncludeSubdirectories = true;
			MyFileWatcher.Filter = "*.*";
			MyFileWatcher.NotifyFilter = System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.DirectoryName;

			MyFileWatcher.Created += ((object sender, System.IO.FileSystemEventArgs e) => {
				var file = BGW.BGShared.BGHelper.GetInfo(e.FullPath);
				myHeart.ProcessFile(file);
			});
			MyFileWatcher.Renamed += ((object sender, System.IO.RenamedEventArgs e) => {
				return; //TODO do stuff
			});
			MyFileWatcher.EnableRaisingEvents = true;

			while (true) { }

		}

		private static void MyHeart_FileEdited(object sender, BGShared.LogInfo loginfo) {
			System.Console.WriteLine("MyHeart edited file {0} at {1}", loginfo.FullFileName, loginfo.TimeStamp);
		}
		*/
		}
	}
}
