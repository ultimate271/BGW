using System;
using System.Runtime.CompilerServices;
using System.Linq;
namespace BGW.BGShared {
	public static class BGHelper {
		public static System.IO.FileInfo GetInfo(string FullURI) {
			return new System.IO.FileInfo(FullURI);
		}
		public static (System.Text.RegularExpressions.Regex regex, System.Exception e) GetRegex(string FiltersFile) {
			string regexString = "";
			try {
				string FileContents = System.IO.File.ReadAllText(FiltersFile);
				//regexString += System.Text.RegularExpressions.Regex.Replace(FileContents, @"//[^\n]\n", "");
				//This replace sequence will remove all white space, and in addition remove an sections that begin with // to the end of the line
				regexString += System.Text.RegularExpressions.Regex.Replace(FileContents, @"\s|//[^\n]*\n", "");
				return (new System.Text.RegularExpressions.Regex(regexString), null);
			}
			catch(System.Exception e) {
				return (null, e);
			}
		}
		public static string GetSetting(this System.Xml.Linq.XDocument settingsDoc, string key) {
			return GetSetting(settingsDoc, key, "");
		}
		public static string GetSetting(this System.Xml.Linq.XDocument settingsDoc, string Key, string Default) {
			string retVal = (
				from element in settingsDoc.Root.Elements("setting")
				where element.Attribute("Key") != null
				&& element.Attribute("Value") != null
				&& element.Attribute("Key").Value.Equals(Key)
				select element.Attribute("Value").Value
			).DefaultIfEmpty(Default).First();
			retVal = System.Environment.ExpandEnvironmentVariables(retVal);
			return retVal;
		}
		public static System.Collections.Generic.Dictionary<string, string> GetMacroDict(
			this System.Xml.Linq.XDocument settingsDoc
		) {
			var retVal = new System.Collections.Generic.Dictionary<string, string>();
			var query =
				from element in settingsDoc.Root.Elements("macro")
				where element.Attribute("Key") != null
				&& element.Attribute("Value") != null
				group element
				by element.Attribute("Key").Value
				into g
				select new {
					Key = g.Key,
					Value = g.First().Attribute("Value").Value
				}
			;
			foreach (var kvp in query) {
				retVal.Add(kvp.Key, kvp.Value);
			}
			
			return retVal;
		}	
		public static string Implode(this System.Collections.Generic.IEnumerable<string> stringList, char delimiter) {
			string retVal = "";
			foreach (string s in stringList) {
				retVal += s + delimiter;
			}
			return retVal;
		}
	}
}
