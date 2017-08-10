namespace BGW.BGShared {
	public struct LogInfoSettingInit : ILogInfo {
		public System.DateTime TimeStamp { get; set; }
		public string SettingName { get; set; }
		public string NewValue { get; set; }

		public string CommentLine {
			get => string.Format(
				"{0:yyyyMMddHHmmss} : Setting {1} set to {2}",
				this.TimeStamp,
				this.SettingName,
				this.NewValue
			);
		}	
	}
}
