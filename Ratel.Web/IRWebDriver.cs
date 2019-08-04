using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Ratel.Web
{
    public interface IRWebDriver : IWebDriver, IWrapsDriver, IJavaScriptExecutor, ITakesScreenshot
    {
        void GetScreenshot(string filePath);
        Navigation Navigate();

        RWebElement FindElement(By by, string name);
        RWebElementCollection FindElements(By by, string name);
    }
}
