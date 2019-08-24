using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using Ratel.Web;

namespace Ratel.Test.Tests
{
    public class QuickStartSampleTests : BaseTestQuickStart
    {
        [Test]
        public void AngularDocsTest()
        {
            Browser.Navigate().GoToUrl("https://angular.io/");
            El(By.XPath("//*[@title='Docs']")).Click(x => x.Exist());
            El(By.Id("introduction-to-the-angular-docs")).Assert.Text.Is.Equal("Introduction to the Angular Docs");
        }

        [Test]
        public void AngularResourcesTest()
        {
            Browser.Navigate().GoToUrl("https://angular.io/");

            Els(By.XPath("//*[@title='Resources']")).Filter.Attribute("").Contains("dd").First().Click();

        }
    }
}
