using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Ratel.Web.RWebElementsCollections;

namespace Ratel.Web
{
    [Parallelizable(ParallelScope.Children)]
    public abstract class BaseTestQuickStart
    {
        private static readonly ThreadLocal<TestRunner> RunnerLocal = new ThreadLocal<TestRunner>();
        protected  IRWebDriver Browser => Runner.AutomationManager.Driver;

        protected TestRunner Runner
        {
            get => RunnerLocal.Value;
            set => RunnerLocal.Value = value;
        }

        protected RWebElement El(By by, [CallerMemberName]string elementName = "") => Runner.AutomationManager.El(by, elementName);
        protected RWebElementCollection Els(By by, [CallerMemberName]string elementName = "") => Runner.AutomationManager.Els(by, elementName);

        [SetUp]
        public void Setup()
        {
            Runner = new TestRunner()
                .Run();
        }

        [TearDown]
        public void TearDown()
        {
            Runner.Stop();
        }
    }
}
