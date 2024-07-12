using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriverAlertsHandle
{
    public class WebDriverAlertsHandle
    {
        WebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Dispose();
        }

        [Test]
        public void ClickOnJSAlertTest()
        {
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Alert')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "Alert did not open");

            alert.Accept();

            string result = driver.FindElement(By.Id("result")).Text;

            Assert.That(result, Is.EqualTo("You successfully clicked an alert"));
        }

        [Test]
        public void HandlingJSConfirmAlertsTest()
        {
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Confirm')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected");

            alert.Accept();

            string result = driver.FindElement(By.Id("result")).Text;

            Assert.That(result, Is.EqualTo("You clicked: Ok"), "Result message is not as expected aftger accepting the alert.");

            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Confirm')]")).Click();

            alert = driver.SwitchTo().Alert();

            alert.Dismiss();

            result = driver.FindElement(By.Id("result")).Text;

            Assert.That(result, Is.EqualTo("You clicked: Cancel"));
        }

        [Test]
        public void HandlingJSPromptAlertsTest()
        {
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Prompt')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "prompt text is not as expected");

            string inputText = "Hello";
            alert.SendKeys(inputText);

            alert.Accept();

            string result = driver.FindElement(By.Id("result")).Text;
            Assert.That(result, Is.EqualTo("You entered: " + inputText), "Result message is not as expected aftger accepting the alert.");
        }
    }
}