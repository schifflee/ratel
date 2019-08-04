using System;
using System.IO;
using System.IO.Compression;

namespace Ratel.Test.Helpers
{
    public class TestApp
    {
        public static void PrepareTestApp()
        {
            var resources = ResourcesPath.ResourcesDirectory;
            Directory.Delete(ResourcesPath.TestAppPath, true);
            ZipFile.ExtractToDirectory(ResourcesPath.TestAppZipPath, resources);
        }
    }
}
