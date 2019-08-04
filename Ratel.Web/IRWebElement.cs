using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Ratel.Web.Asserts;

namespace Ratel.Web
{
    public interface IRWebElement : IWebElement
    {
        string Name { get; set; }

        string Style { get;}

        string Value { get;}

        bool Exist { get; }

        AssertRWebElement Assert { get; }
        RWebElement FindElement(By by, string name);
        RWebElementCollection FindElements(By by, string name);
        IWebElement Find();
        IWebElement GetElementClickable();
        IWebElement GetElementVisible();
        IWebElement GetElementExist();

    }
}
