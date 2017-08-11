namespace BGW.Controller {
	public struct ProcessDirectoryInput {
		public System.IO.DirectoryInfo DirectoryInfo;
	}
	public partial class Controller {
		/// <summary>
		/// Process all the files in a directory recursively
		/// </summary>
		/// <remarks>Invokes this.ProcessFile</remarks>
		/// <param name="input"></param>
		public void ProcessDirectory (ProcessDirectoryInput input) {
			//Process all the files in the directory itself
			foreach (var file in input.DirectoryInfo.GetFiles()) {
				this.ProcessFile(new ProcessFileInput() {
					FileInfo = file
				});
			}
			//Process the subdirectories recursively
			foreach (var subdir in input.DirectoryInfo.GetDirectories()) {
				this.ProcessDirectory(new ProcessDirectoryInput() {
					DirectoryInfo = subdir
				});
			}
		}
	}
}
