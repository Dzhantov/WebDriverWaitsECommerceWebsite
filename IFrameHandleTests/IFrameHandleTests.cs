using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace IFrameHandleTests
{
    public class IFrameHandleTest
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://codepen.io/pervillalva/full/abPoNLd");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Dispose();
        }

        [Test]
        public void iFrameHandlingTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn"))).Click();

            var dropdownLinks = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Assert.IsTrue(link.Displayed, "Link inside dropdown is not displayed");
            }


            driver.SwitchTo().DefaultContent();
        }
        [Test]
        public void iFrameHandlingByIdTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("result"));

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn"))).Click();

            var dropdownLinks = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Assert.IsTrue(link.Displayed, "Link inside dropdown is not displayed");
            }


            driver.SwitchTo().DefaultContent();
        }

        [Test]
        public void iFrameHandlingByWebElementTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result")));

            driver.SwitchTo().Frame(frameElement);

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn"))).Click();

            var dropdownLinks = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Assert.IsTrue(link.Displayed, "Link inside dropdown is not displayed");
            }


            driver.SwitchTo().DefaultContent();
        }
    }
}