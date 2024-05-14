[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace iOS;

[TestClass]
public class ParallelTest
{
    [TestMethod]
    [DataRow("iPhone14Pro", "ParallelTest.json")]
    [DataRow("iPhone14", "ParallelTest.json")]
    public void TestMethod1(string configName, string configFile)
    {
        var singleTest = new SingleTest(configName, configFile);
        singleTest.TestSetup();
        singleTest.SearchTest();
        singleTest.TestTeardown();
    }
}

