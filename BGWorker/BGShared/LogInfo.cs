namespace BGW.BGShared {
	public struct LogInfo {
		public System.DateTime TimeStamp { get; set; }
		public string FullFileName { get; set; }

		public string CommentLine {
			get {
				return string.Format("{0:yyyyMMddHHmmss} : {1} attribute set to hidden", this.TimeStamp, this.FullFileName);
			}
		}


	}
}
