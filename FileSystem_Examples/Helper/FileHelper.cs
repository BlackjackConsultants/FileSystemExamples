using System.IO;
using System.Reflection;

namespace FileSystem_Examples.Helper {
    public static class FileHelper {
        public static void SaveEmbeddedFile(string resourceName, string fileName, Assembly assembly) {
            if (assembly == null) {
                assembly = Assembly.GetExecutingAssembly();
            }
            using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write)) {
                    stream.CopyTo(file);
                    System.Diagnostics.Debug.WriteLine("Writing file to: " + fileName);
                }
            }
        }

        public static void SaveEmbeddedFile(string resourceName, string fileName) {
            SaveEmbeddedFile(resourceName, fileName, null);
        }
    }
}
