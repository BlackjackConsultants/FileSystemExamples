using System;
using System.IO;
using System.IO.Packaging;
using System.Text;
using FileSystem_Examples.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FileSystem_Examples {
    /// <summary>
    /// The following example shows the basic steps for creating a Package. In this example, a package is created to contain a document together with a graphic image 
    /// that is displayed as part of the document. (This is similar to the case in which an HTML file has an <IMG> tag that references an external image file.) 
    /// Two PackageRelationship elements are also included in the package. The first, a "package-level" relationship, defines the document part as the package's 
    /// root element. A second, "part-level" relationship defines the association between the document part (the "source" of the part-level relationship) and its
    ///  use of the image part (the "target" of the part-level relationship). For the complete sample, see Writing a Package Sample.
    /// </summary>
    [TestClass]
    public class Packaging {
        /// <summary>
        /// creates a zip file from a package (or folder)
        /// </summary>
        [TestMethod]
        public void CreatingPackage(){
            string zipFileName = @"test.zip";

            using (Package package = ZipPackage.Open(zipFileName, FileMode.Create)) {
                string startFolder = @"ExcelFileExtract\Sample";

                foreach (string currentFile in Directory.GetFiles(startFolder, "*.*", SearchOption.AllDirectories)) {
                    System.Diagnostics.Debug.WriteLine("------------------------------------------------------------------------------------------------------------");
                    System.Diagnostics.Debug.WriteLine("Packing " + currentFile);
                    Uri relUri = PackageHelper.GetRelativeUri(currentFile);

                    PackagePart packagePart = package.CreatePart(relUri, System.Net.Mime.MediaTypeNames.Application.Octet, CompressionOption.Maximum);
                    using (FileStream fileStream = new FileStream(currentFile, FileMode.Open, FileAccess.Read)) {
                        if (packagePart != null)
                            PackageHelper.CopyStream(fileStream, packagePart.GetStream());
                    }
                    System.Diagnostics.Debug.WriteLine("PackagePart Uri: " + packagePart.Uri);
                }
            }

            Assert.IsTrue(File.Exists(zipFileName));
        }

    }
}




