namespace BGW.BGShared {
	public struct LogInfo {
		public System.DateTime TimeStamp { get; set; }
		public string FullFileName { get; set; }
		public string PreviousFileName { get; set; }

		public string CommentLine {
			get => string.Format(
				"{0:yyyyMMddHHmmss} : {1} attribute set to hidden{2}",
				this.TimeStamp,
				this.FullFileName,
				this.PreviousFileName != ""
					? string.Format(" Previous Filename: {0}", this.PreviousFileName)
					: ""
			);
		}	


	}
}
