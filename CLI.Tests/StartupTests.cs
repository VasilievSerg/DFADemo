using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CLI.Tests;

[TestClass]
public class StartupTests
{
    private const String TaskAttachTests1FilePath = @"TestFiles/TaskAttachTests1.java";
    private const String TaskAttachTests2FilePath = @"TestFiles/TaskAttachTests2.java";
    
    [TestMethod]
    public void TaskAttachTests1()
    {
        var exitCode = (ExitCodes)CLIStartup.Main(new[] { TaskAttachTests1FilePath });
        Assert.AreEqual(ExitCodes.Success, exitCode);
    }
    
    [TestMethod]
    public void TaskAttachTests2()
    {
        var exitCode = (ExitCodes)CLIStartup.Main(new[] { TaskAttachTests2FilePath });
        Assert.AreEqual(ExitCodes.Success, exitCode);
    }
    
    [TestMethod]
    public void EmptyArgs()
    {
        var exitCode = (ExitCodes)CLIStartup.Main(Array.Empty<String>());
        Assert.AreEqual(ExitCodes.StartupFailure, exitCode);
    }
    
    [TestMethod]
    public void TooMuchArgs()
    {
        var exitCode = (ExitCodes)CLIStartup.Main(new[] { TaskAttachTests1FilePath, "doesn't matter" });
        Assert.AreEqual(ExitCodes.StartupFailure, exitCode);
    }
    
    [TestMethod]
    public void MissedSourceFile()
    {
        var exitCode = (ExitCodes)CLIStartup.Main(new[] { "IncorrectPath.java" });
        Assert.AreEqual(ExitCodes.StartupFailure, exitCode);
    }
    
    [TestMethod]
    public void IncorrectExtension()
    {
        var exitCode = (ExitCodes)CLIStartup.Main(new[] { $"{TaskAttachTests1FilePath}.incorrect" });
        Assert.AreEqual(ExitCodes.StartupFailure, exitCode);
    }
}