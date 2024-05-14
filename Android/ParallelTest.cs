[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace Android;

[TestClass]
public class ParallelTest
{
    [TestMethod]
    [DataRow("samsungs10", "ParallelTest.json")]
    [DataRow("pixel3", "ParallelTest.json")]
    public void TestMethod1(string configName, string configFile)
    {
        var singleTest = new SingleTest(configName, configFile);
        singleTest.TestSetup();
        singleTest.SearchTest();
        singleTest.TestTeardown();
    }
}

