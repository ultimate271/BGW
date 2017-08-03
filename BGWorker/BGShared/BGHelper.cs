namespace BGW.BGShared {
	public static class BGHelper {
		public static System.IO.FileInfo GetInfo(string FullURI) {
			return new System.IO.FileInfo(FullURI);
		}
	}
}
