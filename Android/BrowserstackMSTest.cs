namespace Android;

public class BrowserstackMSTest
{
    public AndroidDriver<AndroidElement>? driver;

    public BrowserstackMSTest() { }

    [TestInitialize]
    public void TestSetup()
    {
        AppiumOptions appiumOptions = new AppiumOptions();
        driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), appiumOptions);
    }

    [TestCleanup]
    public void TestTeardown()
    {
        if (driver != null)
            driver.Quit();
    }
}

