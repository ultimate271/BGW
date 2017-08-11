namespace BGW.Controller {
	public struct ProcessFileInput {
		public System.IO.FileInfo FileInfo { get; set; }
		public string PreviousURI { get; set; }
	}
	public partial class Controller {
		public void ProcessFile(ProcessFileInput file) {
			this.Heart.ProcessFile(file.FileInfo, file.PreviousURI);
		}
	}
}
