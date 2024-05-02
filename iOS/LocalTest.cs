using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace iOS;

[TestClass]
public class LocalTest : BrowserstackMSTest
{
    [TestMethod]
    public void LocalNetworkTest()
    {
        if (driver == null)
            throw new Exception("Could not run tests. Driver not initialised");

        var _ = driver.PageSource;
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

        var testButtonId = By.XPath("/XCUIElementTypeApplication/XCUIElementTypeWindow/XCUIElementTypeOther/XCUIElementTypeOther/XCUIElementTypeOther/XCUIElementTypeButton/XCUIElementTypeStaticText");
        IOSElement testButton = (IOSElement)wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(testButtonId));
        testButton.Click();
        Thread.Sleep(5000);

        _ = driver.PageSource;

        IOSElement textElement = driver.FindElementByXPath("/XCUIElementTypeApplication/XCUIElementTypeWindow[1]/XCUIElementTypeOther/XCUIElementTypeOther/XCUIElementTypeOther/XCUIElementTypeTextField");
        string text = textElement.Text;

        Assert.IsTrue(text.Contains("Up and running"));

        driver.ExecuteScript("browserstack_executor: { \"action\": \"setSessionStatus\", \"arguments\": { \"status\": \"passed\", \"reason\": \"Test Passed!\"} }");
    }
}

