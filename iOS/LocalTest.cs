﻿using BrowserStack;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace iOS;

[TestClass]
public class LocalTest
{
    private IOSDriver<IOSElement>? driver;
    private string configFile = "LocalTest.json";
    private Local? browserStackLocal;

    [TestInitialize]
    public void TestSetup()
    {
        Utils utils = new Utils(null, "Config/" + configFile);
        AppiumOptions capabilities = utils.capabilities();

        // Setup Browserstack Local
        browserStackLocal = new Local();
        List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("key", utils.accessKey()) };
        browserStackLocal.start(bsLocalArgs);

        driver = new IOSDriver<IOSElement>(new Uri(Utils.HUB_URL), capabilities);
    }

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

    [TestCleanup]
    public void TestTeardown()
    {
        if(driver != null)
            driver.Quit();

        if(browserStackLocal != null)
            browserStackLocal.stop();
    }
}

