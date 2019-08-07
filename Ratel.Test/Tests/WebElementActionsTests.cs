using NUnit.Framework;
using Ratel.Test.Pages;

namespace Ratel.Test.Tests
{
    public class WebElementActionsTests : BaseTest
    {

        [Test]
        public void SendKeysTest()
        {
            var text = "Test text";

            Runner
                .OpenPage(x => new TestAppPage(x))
                .EnterFullName(text)
                .AssertFullNameValueAreEqual(text)
                .PhycicalProductsSelected();
        }
    }
}