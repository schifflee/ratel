using System;
using System.IO;

namespace Ratel.Test.Helpers
{
    public class ResourcesPath
    {
        public static readonly string ResourcesDirectory = Path.Combine(Environment.CurrentDirectory, "Resources");
        public static readonly string TestAppPath = Path.Combine(Environment.CurrentDirectory, "Resources", "TestApp");
        public static readonly string TestAppZipPath = Path.Combine(Environment.CurrentDirectory, "Resources", "TestApp.zip");
    }
}
