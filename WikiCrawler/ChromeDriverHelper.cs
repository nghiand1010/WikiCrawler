using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiCrawler
{
    public class ChromeDriverHelper
    {
        public RemoteWebDriver ChromeDriver { get; set; }
        public DriverOptions ChromeOptions { get; set; }
        public WebDriverWait Wait { get; set; }

        public ChromeDriverHelper(RemoteWebDriver driver)
        {
            ChromeDriver = driver;
        }
        public IWebElement FindElementByClassName(string className, int second = 3)
        {
            try
            {
                var wait = new WebDriverWait(ChromeDriver, TimeSpan.FromSeconds(second));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName(className)));
                var result = ChromeDriver.FindElementByClassName(className);
                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception)
            {

            }
            return null;
        }

        public ReadOnlyCollection<IWebElement> FindElementsByClassName(string className, int second = 3)
        {
            try
            {
                var wait = new WebDriverWait(ChromeDriver, TimeSpan.FromSeconds(second));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName(className)));
                return ChromeDriver.FindElementsByClassName(className);
            }
            catch (Exception)
            {

            }
            return null;
        }


        public ReadOnlyCollection<IWebElement> FindElementsByCssSelector(string cssSelector, int second = 3)
        {
            try
            {
                var wait = new WebDriverWait(ChromeDriver, TimeSpan.FromSeconds(second));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelector)));
                var result = ChromeDriver.FindElementsByCssSelector(cssSelector);

                if (result != null)
                {
                    return result;
                }

            }
            catch (Exception ex)
            {
                try
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)ChromeDriver;
                    var jsresult =
                        js.ExecuteScript(
                        $"return document.querySelectorAll('{cssSelector}')");
                    return jsresult as ReadOnlyCollection<IWebElement>;
                }
                catch (Exception)
                {
                }

            }
            return null;
        }

        public IWebElement FindElementByCssSelector(string cssSelector, int second = 3)
        {
            try
            {
                var wait = new WebDriverWait(ChromeDriver, TimeSpan.FromSeconds(second));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelector)));
                return ChromeDriver.FindElementByCssSelector(cssSelector);
            }
            catch (Exception)
            {

            }
            return null;
        }

        public ReadOnlyCollection<IWebElement> FindElementsById(string id, int second = 10)
        {
            try
            {
                var wait = new WebDriverWait(ChromeDriver, TimeSpan.FromSeconds(second));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(id)));
                return ChromeDriver.FindElementsById(id);
            }
            catch (Exception)
            {

            }
            return null;
        }

        public IWebElement FindElementById(string id, int second = 10)
        {
            try
            {
                var wait = new WebDriverWait(ChromeDriver, TimeSpan.FromSeconds(second));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(id)));
                return ChromeDriver.FindElementById(id);
            }
            catch (Exception)
            {

            }
            return null;
        }

        public IWebElement FindElementByXPath(string xpath, int second = 10)
        {
            try
            {
                var wait = new WebDriverWait(ChromeDriver, TimeSpan.FromSeconds(second));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                return ChromeDriver.FindElementByXPath(xpath);
            }
            catch (Exception e)
            {

            }
            return null;
        }


        public ReadOnlyCollection<IWebElement> FindElementsByXPath(string xpath, int second = 3)
        {
            try
            {
                var wait = new WebDriverWait(ChromeDriver, TimeSpan.FromSeconds(second));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                return ChromeDriver.FindElementsByXPath(xpath);
            }
            catch (Exception)
            {

            }
            return null;
        }

        public void Click(IWebElement element)
        {
            ChromeDriver.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            ChromeDriver.ExecuteScript("arguments[0].click();", element);
        }

    }
}
