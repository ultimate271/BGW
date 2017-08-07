using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGW.BGShared {
	public struct ProcessFileInput {
		public System.IO.FileInfo FileInfo { get; set; }
		public string PreviousURI { get; set; }
	}
}
