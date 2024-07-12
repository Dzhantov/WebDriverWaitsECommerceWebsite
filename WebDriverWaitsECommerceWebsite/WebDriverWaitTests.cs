using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriverWaitsECommerceWebsite
{
    public class WebDriverWaitTests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://practice.bpbonline.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Dispose();
        }

        [Test]
        public void SearchForProductInTheWebsite()
        {
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("keyboard");
            driver.FindElement(By.XPath("//input[@type='image'][@title=' Quick Find ']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();
                Assert.IsTrue(driver.PageSource.Contains("keyboard"), "The product 'keyboard' was not found in the shopping cart");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }
        }

        [Test]
        public void SearchForJunkTest()
        {
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("junk");
            driver.FindElement(By.XPath("//input[@type='image'][@title=' Quick Find ']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();
            }
            catch (NoSuchElementException ex)
            {
                Assert.Pass("NoSuchElementException was thrown");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }
        }
    }
}