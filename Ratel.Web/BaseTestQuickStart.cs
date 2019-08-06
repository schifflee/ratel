using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Ratel.Web
{
    [Parallelizable(ParallelScope.Children)]
    public abstract class BaseTestQuickStart
    {
        private static readonly ThreadLocal<TestRunner> RunnerLocal = new ThreadLocal<TestRunner>();
        protected  IRWebDriver Driver => Runner.AutomationManager.Driver;

        protected TestRunner Runner
        {
            get => RunnerLocal.Value;
            set => RunnerLocal.Value = value;
        }

        protected RWebElement El(By by) => Runner.AutomationManager.El(by);
        protected RWebElementCollection Els(By by) => Runner.AutomationManager.Els(by);

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
