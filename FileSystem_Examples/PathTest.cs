using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem_Examples {
	[TestClass]
	public class PathTest {
		[TestMethod]
		public void GetParentPath() {
			string path = System.IO.Directory.GetCurrentDirectory();
			string parentPath = System.IO.Directory.GetParent(path).ToString();
			Assert.AreNotEqual(path, parentPath);
		}

		// this test shows that a \\ slash as trailing characters will affect the result of the parent folder.  make sure you dont have them.
		[TestMethod]
		public void GetParentPathRemoveTrailingBackSlashes() {
			// prepare data
			string path = System.IO.Directory.GetCurrentDirectory();
			var lastFolderNameList = path.Split('\\');
			var lastFolderName = lastFolderNameList[lastFolderNameList.Length - 1];
			path = path + "\\";
			// test
			string parentPath = System.IO.Directory.GetParent(path).ToString();
			// check result
			Assert.IsTrue(parentPath.EndsWith(lastFolderName));

			// remove the back slashes
			path = path.Trim(new char[] { '\\' });
			parentPath = System.IO.Directory.GetParent(path).ToString();
			Assert.IsFalse(parentPath.EndsWith(lastFolderName));
			
		}
	}
}
