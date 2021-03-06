﻿namespace BGW.Controller {
	public struct LogInfoFileEdit : ILogInfo {
		public System.DateTime TimeStamp { get; set; }
		public string FullFileName { get; set; }
		public string PreviousFileName { get; set; }

		public string CommentLine {
			get {
				return string.Format(
			   "{0:yyyyMMddHHmmss} : {1} attribute set to hidden{2}",
			   this.TimeStamp,
			   this.FullFileName,
			   !System.String.IsNullOrEmpty(this.PreviousFileName)
				   ? string.Format(" Previous Filename: {0}", this.PreviousFileName)
				   : ""
				);
			}
		}	
	}
}
