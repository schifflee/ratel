using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Ratel.Web.Asserts;
using Ratel.Web.Shoulds;

namespace Ratel.Web
{
    public interface IRWebElement : IWebElement
    {
        string Name { get; set; }

        string Style { get;}

        string Value { get;}

        bool Exist { get; }

        new RWebElement Clear();

        new RWebElement SendKeys(string text);

        new RWebElement Submit();
        RWebElement Click(int count, Func<Be, RWebElement> func);

        RWebElement Click(Func<Be, RWebElement> func);
        RWebElement Click(int count);

        new RWebElement Click();

        AssertRWebElement Assert { get; }
        RWebElement FindElement(By by, string name);
        RWebElementCollection FindElements(By by, string name);
        IWebElement FindWithCache();
        IWebElement Find();
        Should Should { get; }
    }
}
