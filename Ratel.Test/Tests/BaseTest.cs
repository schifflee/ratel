using NUnit.Framework;
using Ratel.Test.Helpers;
using Ratel.Web;

namespace Ratel.Test.Tests
{
    public class BaseTest
    {
        protected TestRunner Runner;

        [SetUp]
        public void Setup()
        {
            Runner = new TestRunner
            {
                Config = {BaseUrl = ResourcesPath.ResourcesDirectory }
            };
            Runner.Run();
        }

        [TearDown]
        public void TearDown()
        {
            Runner.Stop();
        }
    }
}
