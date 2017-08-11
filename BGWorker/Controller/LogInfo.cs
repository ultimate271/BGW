namespace BGW.Controller {
	public interface ILogInfo {
		string CommentLine { get; }
		System.DateTime TimeStamp { get; set; }
	}
}