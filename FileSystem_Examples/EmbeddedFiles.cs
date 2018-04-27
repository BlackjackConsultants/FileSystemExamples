using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace FileSystem_Examples {
    [TestClass]
    public class EmbeddedFiles {
        [TestMethod]
        public void LoadAnEmbeddedFile() {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "FileSystem_Examples.EmbeddedFile.sheet1.xml";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream)) {
                    string result = reader.ReadToEnd();
                    Assert.IsNotNull(result);
                }
        }

        [TestMethod]
        public void SaveFileFromDllStreamFile() {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "FileSystem_Examples.EmbeddedFile.sheet1.xml";
            var tempFile = Path.GetTempPath() + "temp.xml";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
                using (var file = new FileStream(tempFile, FileMode.Create, FileAccess.Write)) {
                    stream.CopyTo(file);
                    System.Diagnostics.Debug.WriteLine("Writing file to: " + tempFile);
                }
            }
        }
    }
}
