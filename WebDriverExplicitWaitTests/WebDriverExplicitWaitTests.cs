using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebDriverExplicitWaitTests
{
    public class WebDriverExplicitWaitTests
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
        public void SearchForKeayboardExplicitWaitTest()
        {
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("keyboard");
            driver.FindElement(By.XPath("//input[@type='image'][@title=' Quick Find ']")).Click();

            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(e => e.FindElement(By.LinkText("Buy Now"))).Click();

                string productName = driver.FindElement(By.XPath("//tbody//tr//a//strong")).Text;

                Assert.IsTrue(productName == "Microsoft Internet Keyboard PS/2");
                Assert.IsTrue(driver.PageSource.Contains("keyboard"), "The product 'keyboard' was not found in the shopping cart");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }
        }

        [Test]
        public void SearchForJunkExplicitWaitTest()
        {
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("junk");
            driver.FindElement(By.XPath("//input[@type='image'][@title=' Quick Find ']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                //wait.Until(e => e.FindElement(By.LinkText("Buy Now"))).Click();
                var noProductMessage = wait.Until(e => e.FindElement(By.XPath("//div[@class='contentText']//p")));

                string productName = noProductMessage.Text;

                Assert.IsTrue(productName == "There is no product that matches the search criteria.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }
        }
    }
}