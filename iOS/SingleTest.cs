using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;

namespace iOS;

[TestClass]
public class SingleTest
{
    private IOSDriver<IOSElement>? driver;
    private string configFile = "SingleTest.json";
    private string? configName;

    public SingleTest() { }

    public SingleTest(string _configName, string _configFile)
    {
        configName = _configName;
        configFile = _configFile;
    }

    [TestInitialize]
    public void TestSetup()
    {
        Utils utils = new Utils(configName, "Config/" + configFile);
        driver = new IOSDriver<IOSElement>(new Uri(Utils.HUB_URL), utils.capabilities());
    }

    [TestMethod]
    public void SearchTest()
    {
        if (driver == null)
            throw new Exception("Could not run tests. Driver not initialised");

        var _ = driver.PageSource;
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

        var textButtonId = MobileBy.AccessibilityId("Text Button");
        IOSElement textButton = (IOSElement)wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(textButtonId));
        textButton.Click();
        Thread.Sleep(5000);

        _ = driver.PageSource;

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        var textInputId = MobileBy.AccessibilityId("Text Input");
        IOSElement textInput = (IOSElement)wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(textInputId));
        textInput.SendKeys("hello@browserstack.com\n");
        Thread.Sleep(5000);

        var textOutput = driver.FindElementByAccessibilityId("Text Output");
        Assert.IsTrue(textOutput.Text == "hello@browserstack.com");

        driver.ExecuteScript("browserstack_executor: { \"action\": \"setSessionStatus\", \"arguments\": { \"status\": \"passed\", \"reason\": \"Test Passed!\"} }");
    }

    [TestCleanup]
    public void TestTeardown()
    {
        if (driver != null)
            driver.Quit();
    }
}
