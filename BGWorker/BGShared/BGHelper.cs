﻿using System;
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
			var query =
				from element in settingsDoc.Root.Elements("setting")
				where element.Attribute("Key") != null
				&& element.Attribute("Value") != null
				&& element.Attribute("Key").Value.Equals(Key)
				select element.Attribute("Value").Value;
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
		public static string Implode(this System.Collections.Generic.IEnumerable<string> stringList, char delimiter) {
			string retVal = "";
			foreach (string s in stringList) {
				retVal += s + delimiter;
			}
			return retVal;
		}
	}
}
