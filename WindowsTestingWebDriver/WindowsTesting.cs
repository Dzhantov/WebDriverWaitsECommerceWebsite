using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace WindowsTestingWebDriver
{
    public class Tests
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");
        }

        [TearDown]
        public void Teardown()
        {
            driver.Close();
            driver.Dispose();
        }

        [Test]
        public void HandleMultipleWindows()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> handle = driver.WindowHandles;

            Assert.That(handle.Count, Is.EqualTo(2), "The number of open tabs should be 2");

            driver.SwitchTo().Window(handle[1]);

            string newWindowContent = driver.FindElement(By.TagName("h3")).Text;
            Assert.That(newWindowContent, Is.EqualTo("New Window"), "Did not find new window");

            string path = Path.Combine(Directory.GetCurrentDirectory(), "content.txt");

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                File.AppendAllText(path, "current handle: " + driver.CurrentWindowHandle);
                File.AppendAllText(path, "page content: " + driver.PageSource);
                driver.Close();
            }
            driver.SwitchTo().Window(handle[0]);
            string originalWindowContent = driver.FindElement(By.TagName("h3")).Text;
            Assert.That(originalWindowContent, Is.EqualTo("Opening a new window"), "Did not find new window");

            File.AppendAllText(path, "current handle: " + driver.CurrentWindowHandle);
            File.AppendAllText(path, "page content: " + driver.PageSource);
        }


        [Test]
        public void HandleNoSuchWindow()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> handle = driver.WindowHandles;

            Assert.That(handle.Count, Is.EqualTo(2), "The number of open tabs should be 2");

            driver.SwitchTo().Window(handle[1]);

            string newWindowContent = driver.FindElement(By.TagName("h3")).Text;
            Assert.That(newWindowContent, Is.EqualTo("New Window"), "Did not find new window");

            driver.Close();

            try
            {
                driver.SwitchTo().Window(handle[1]);
            }
            catch (NoSuchWindowException ex)
            {

                Assert.Pass("No such window was correctly handled");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected error " + ex.Message);
            }
        }
    }
}