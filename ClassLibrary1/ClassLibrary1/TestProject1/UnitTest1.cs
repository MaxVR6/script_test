using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.IO;
using System.Diagnostics;
using AutoItX3Lib;

namespace Test_visual_loadButton
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //RemoteWebDriver driver = new FirefoxDriver();
            RemoteWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(@"https://www.4invest.net/ru/login.html");
            driver.FindElementByName("login").SendKeys("xxxxxxx");
            driver.FindElementByName("password").SendKeys("xxxxxxxxx");
            driver.FindElementById("button-1787-btnWrap").Click();
            driver.FindElementById("button-1013-btnWrap").Click();
            driver.FindElementById("menuitem-1015-textEl").Click();
            var AutoItPath2ScriptFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Script.exe";
            Process.Start(AutoItPath2ScriptFile);
            driver.FindElementById("form-2281-outerCt").Click();    //Select photo link
            driver.FindElementById("button-2285").Click();          //Upload button
            driver.Quit();
        }
    }
}