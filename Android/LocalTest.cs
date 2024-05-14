using BrowserStack;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace Android;

[TestClass]
public class LocalTest
{
    private AndroidDriver<AndroidElement>? driver;
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

        driver = new AndroidDriver<AndroidElement>(new Uri(Utils.HUB_URL), capabilities);
    }

    [TestMethod]
    public void LocalNetworkTest()
    {
        if (driver == null)
            throw new Exception("Could not run tests. Driver not initialised");

        var _ = driver.PageSource;
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

        var testActionId = By.Id("com.example.android.basicnetworking:id/test_action");
        AndroidElement testActionElement = (AndroidElement)wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(testActionId));
        testActionElement.Click();
        Thread.Sleep(5000);

        _ = driver.PageSource;

        ReadOnlyCollection<AndroidElement> textviewElements = driver.FindElementsByClassName("android.widget.TextView");
        string text = textviewElements.Last().Text;

        Assert.IsTrue(text.Contains("The active connection is wifi"));
        Assert.IsTrue(text.Contains("Up and running"));

        driver.ExecuteScript("browserstack_executor: { \"action\": \"setSessionStatus\", \"arguments\": { \"status\": \"passed\", \"reason\": \"Test Passed!\"} }");
    }

    [TestCleanup]
    public void TestTeardown()
    {
        if (driver != null)
            driver.Quit();

        if (browserStackLocal != null)
            browserStackLocal.stop();
    }
}

