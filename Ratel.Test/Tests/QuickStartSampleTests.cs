using System;
using System.Collections.Generic;
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
            Driver.Navigate().GoToUrl("https://angular.io/");
            El(By.XPath("//*[@title='Docs']")).Click();
            El(By.Id("introduction-to-the-angular-docs")).Assert.Text.AreEqual("Introduction to the Angular Docs");

        }

        [Test]
        public void AngularResourcesTest()
        {
            Driver.Navigate().GoToUrl("https://angular.io/");
            El(By.XPath("//*[@title='Resources']")).Click();
            El(By.XPath("//*[@id='development']/..")).Assert.Text.AreEqual("Development");
        }
    }
}
