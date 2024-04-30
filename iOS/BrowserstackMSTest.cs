namespace iOS;

public class BrowserstackMSTest
{
    protected IOSDriver<IOSElement>? driver;

    public BrowserstackMSTest() { }

    [TestInitialize]
    public void TestSetup()
    {
        AppiumOptions appiumOptions = new AppiumOptions();
        driver = new IOSDriver<IOSElement>(new Uri("http://127.0.0.1:4723/wd/hub"), appiumOptions);
    }

    [TestCleanup]
    public void TestTeardown()
    {
        if (driver != null)
            driver.Quit();
    }
}

