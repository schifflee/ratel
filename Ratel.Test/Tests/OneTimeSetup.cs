using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Ratel.Test.Helpers;

namespace Ratel.Test.Tests
{
    [SetUpFixture]
    public class OneTimeSetup
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestApp.PrepareTestApp();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }
    }
}
