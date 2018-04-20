using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem_Examples {
	[TestClass]
	public class FileSystemSearchTest {
		[TestMethod]
		public void SearchAFileRecursivelyAndStopOnFirstFound() {
			List<string> path = DirSearch("C:\\temp", "CA_spAccountSelectAll.proc.sql", true);
			Assert.IsTrue(path.Count == 1);
		}

		[TestMethod]
		public void SearchALLFileRecursively() {
			List<string> path = DirSearch("C:\\temp", "CA_spAccountSelectAll.proc.sql", false);
			Assert.IsTrue(path.Count > 1);
		}

		[TestMethod]
		public void GettingSpecialFolders() {
			string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
			string programFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
			string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
			Assert.IsTrue(programFilesPath.ToLower().StartsWith("program"));
			Assert.IsTrue(systemPath.ToLower().StartsWith("program"));
			Assert.IsTrue(programFilesX86.ToLower().StartsWith("program"));
		}

		public void GettingFileAttributes() {
			string filePath = @"c:\test.txt";
			// get file attributes
			FileAttributes fileAttributes = File.GetAttributes(filePath);

		}


		public static List<string> DirSearch(string sDir, string value, bool returnAfterFirst) {
			List<string> fileList = new List<string>();
			try {
				{
					foreach (string d in Directory.GetDirectories(sDir)) {
						foreach (string f in Directory.GetFiles(d, value)) {
							System.Diagnostics.Debug.WriteLine(f);
							string extension = Path.GetExtension(f);
							if (Path.GetFileName(f).ToLower() == Path.GetFileName(value).ToLower()) {
								fileList.Add(f);
								if (returnAfterFirst) {
									return fileList;
								}
							}
						}
						fileList.AddRange(DirSearch(d, value, returnAfterFirst));
						if (returnAfterFirst && fileList.Count > 0) {
							return fileList;
						}
					}
				}
			}
			catch (System.Exception excpt) {
				Console.WriteLine(excpt.Message);
			}
			return fileList;
		}
	}
}
