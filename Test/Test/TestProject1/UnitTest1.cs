using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Interactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        static string url = "https://xxxxxxxxxxxxxxx.xx/xxxxx";

        ChromeDriver driver = new ChromeDriver();
        Boolean imageExists;
        IJavaScriptExecutor js;
        int total, totalNew, start, finish, dist, ap;
        string source = null;
        static string logFilePath = Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath) + "\\Log.txt";
        IWebElement img;
        StreamWriter sw = new StreamWriter(logFilePath);

        public void Scroller()
        {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                Thread.Sleep(5000);
                driver.Keyboard.SendKeys(Keys.ArrowUp);
                driver.Keyboard.SendKeys(Keys.ArrowDown);
                Thread.Sleep(5000);
        }

        [TestMethod]
        public void Test_Main()
        {
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
            driver.Navigate().GoToUrl(url);
            
            do
            {
                total = driver.FindElements(By.XPath("//img[contains(@class, 'oneImg')]")).Count;
                Scroller();
                totalNew = driver.FindElements(By.XPath("//img[contains(@class, 'oneImg')]")).Count;
            }
            while (total < totalNew);

            for (int i = 0; i < totalNew; i++)
            {
                try
                {
                    img = driver.FindElements(By.XPath("//img[contains(@class, 'oneImg')]"))[i];
                    source = img.GetAttribute("src").Replace("%20", " ");
                    start = source.IndexOf("/z_");
                    if (start == -1)
                        start = source.IndexOf("/b_");
                    finish = source.IndexOf(".jpg");
                    dist = finish - start;
                    source = source.Substring(start + 1, dist - 1).Replace("z_%E2%84%96 ", "");
                    do
                    {
                        ap = source.IndexOf("'");
                        if (ap > 0)
                            source = source.Substring(ap + 1, source.Length - ap - 1);
                    } while (ap > 0);
                    sw.WriteLine("Image #" + i.ToString() + ": [" + source + "] is visible");
                    js = driver as IJavaScriptExecutor;
                    imageExists = (Boolean)js.ExecuteScript("return arguments[0].complete && typeof arguments[0].naturalWidth != \"undefined\" && arguments[0].naturalWidth > 0", driver.FindElement(By.XPath("//img[contains(@src, '" + source + "')]")));
                    Assert.IsTrue(imageExists);
                }
                catch(Exception)
                {
                    driver.Navigate().GoToUrl("Failed image #"+ i + ". With name " + source);
                    sw.WriteLine("Failed image #" + i + ". With name [" + source + "]");
                    sw.Close();
                }
            }
            sw.WriteLine("The end");
            driver.Quit();
            sw.Close();
        }
    }
}