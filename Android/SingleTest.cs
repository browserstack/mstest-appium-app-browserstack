using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Android;

[TestClass]
public class SingleTest : BrowserstackMSTest
{
    public SingleTest() { }

    [TestMethod]
    public void SearchTest()
    {
        if (driver == null)
            throw new Exception("Could not run tests. Driver not initialised");

        var _ = driver.PageSource;
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

        var searchElementId = MobileBy.AccessibilityId("Search Wikipedia");
        AndroidElement searchElement = (AndroidElement)wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(searchElementId));
        searchElement.Click();
        Thread.Sleep(2000);

        _ = driver.PageSource;

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        var insertTextElementId = By.Id("org.wikipedia.alpha:id/search_src_text");
        AndroidElement insertTextElement = (AndroidElement)wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(insertTextElementId));
        insertTextElement.SendKeys("BrowserStack");
        Thread.Sleep(5000);

        ReadOnlyCollection<AndroidElement> allProductsName = driver.FindElements(By.ClassName("android.widget.TextView"));
        Assert.IsTrue(allProductsName.Count > 0);

        driver.ExecuteScript("browserstack_executor: { \"action\": \"setSessionStatus\", \"arguments\": { \"status\": \"passed\", \"reason\": \"Test Passed!\"} }");
    }
}
