namespace BGW.Model {
	public delegate void FileEdittedEventHandler(object sender, FileEdittedEventArgs e);
	public struct FileEdittedEventArgs {
		public System.DateTime TimeStamp { get; set; }
		public string FullFileName { get; set; }
		public string PreviousFileName { get; set; }
	}
}
