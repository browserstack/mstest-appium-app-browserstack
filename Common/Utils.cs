using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Appium;

namespace Common;

public class Utils
{
    public const string HUB_URL = "http://hub-cloud.browserstack.com/wd/hub";
    public JObject testConfig;

    public Utils(string configName, string configPath)
    {
        string? currentDirectory = GoUpLevels(Directory.GetCurrentDirectory(), 3);
        testConfig = JObject.Parse(File.ReadAllText(Path.Combine(currentDirectory, configPath)));

        // ConfigName is present for parallel test configs.
        if (configName is not null)
        {
            JObject targetConfig = (JObject)testConfig.GetValue(configName);
            if (targetConfig is null)
                throw new Exception("Config Name used for invoking parallel test is not valid");

            testConfig = (JObject)testConfig.GetValue("commonCaps");
            testConfig.Merge(targetConfig);
        }

        if (testConfig is null)
            throw new Exception("Could not parse config from the config json file");
    }

    public AppiumOptions capabilities()
    {
        AppiumOptions capabilities = new AppiumOptions();
        Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();

        // Parse all keys at root level and build AppiumOptions out of it.
        foreach (var property in testConfig.Properties())
        {
            if (property.Value.Type != JTokenType.Object)
                capabilities.AddAdditionalCapability(property.Name, (JValue)property.Value);
        }

        // Update bstack:options with user creds. value in config file takes precedence over env variable.
        if (!testConfig.ContainsKey("bstack:options"))
            testConfig.Add("bstack:options", new JObject());

        if (!testConfig.GetValue("bstack:options").HasValues)
            testConfig["bstack:options"] = new JObject();

        // Prepare bstackOptions to neccessarily contain username and accesskeys.
        testConfig["bstack:options"]["userName"] = username();
        testConfig["bstack:options"]["accessKey"] = accessKey();

        // Parse nested bstack:options and push into AppiumOptions
        JObject bStackOptions = (JObject)testConfig["bstack:options"];
        foreach (var property in bStackOptions.Properties())
        {
            if (property.Value.Type != JTokenType.Object)
                browserstackOptions.Add(property.Name, (JValue)property.Value);
        }

        capabilities.AddAdditionalCapability("bstack:options", browserstackOptions);

        return capabilities;
    }

    private string? username()
    {
        JObject bStackOptions = (JObject)testConfig["bstack:options"];
        string? username = (string?)bStackOptions.GetValue("userName");
        if (username is null)
            username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");

        return username;
    }

    public string? accessKey()
    {
        JObject bStackOptions = (JObject)testConfig["bstack:options"];
        string? accessKey = (string?)bStackOptions.GetValue("accessKey");
        if (accessKey is null)
            accessKey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");

        return accessKey;
    }

    private static string GoUpLevels(string path, int levels)
    {
        // Combine with ".." for each level to go up
        string newPath = path;
        for (int i = 0; i < levels; i++)
        {
            newPath = Path.Combine(newPath, "..");
        }

        // Get the full path after going up the specified levels
        return Path.GetFullPath(newPath);
    }
}

