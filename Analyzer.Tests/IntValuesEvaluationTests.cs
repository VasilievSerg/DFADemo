using Analyzer.ControlFlow;
using Analyzer.DataFlow;
using Analyzer.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DFATests;

[TestClass]
public class IntValuesEvaluationTests
{
    private const String TestCasesFilePath = @"TestFiles/JavaTests.java";

    private IReadOnlyDictionary<String, MethodDeclarationSyntax> NameToSyntaxMap { get; set; }
    
    [TestInitialize]
    public void Init()
    {
        var sources = File.ReadAllText(TestCasesFilePath);
        
        var antlrSyntaxFactory = AntlrSyntaxFactory.Create(TargetLanguage.Java);
            
        var antlrTreeWrapper = antlrSyntaxFactory.CreateTreeWrapper(sources);
        var translator = antlrSyntaxFactory.CreateTranslator();
        
        var methods = new List<MethodDeclarationSyntax>();
        translator.OnMethodDeclarationTranslated += (sender, args) => methods.Add(args.Node);

        translator.Translate(antlrTreeWrapper);
        
        NameToSyntaxMap = methods.ToDictionary(p => p.Name, p => p);
    }
    
    [TestMethod]
    public void TaskAttachTest1()
    {
        var methodName = nameof(TaskAttachTest1);
        var varsToCheck = new[]
        {
            // [1, 4, 5, 6-42]
            ("x", new[] { 1, 4, 5 }.Concat(Enumerable.Range(6, 43 - 6)).ToArray())
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TaskAttachTest2()
    {
        var methodName = nameof(TaskAttachTest2);
        var varsToCheck = new[]
        {
            ("x", new[] { 1, 4, 5, 6 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod0()
    {
        var methodName = nameof(TestMethod0);
        var varsToCheck = new[]
        {
            ("x", new[] { 1, 4, 5, 6 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod1()
    {
        var methodName = nameof(TestMethod1);
        var varsToCheck = new[]
        {
            ("x", new[] { 1 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod2()
    {
        var methodName = nameof(TestMethod2);
        var varsToCheck = new[]
        {
            ("x", new[] { 1, 2 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod3()
    {
        var methodName = nameof(TestMethod3);
        var varsToCheck = new[]
        {
            ("x", new[] { 3 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod4()
    {
        var methodName = nameof(TestMethod4);
        var varsToCheck = new[]
        {
            ("x", new[] { 3, 62 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod5()
    {
        var methodName = nameof(TestMethod5);
        var varsToCheck = new[]
        {
            ("x", new[] { 5 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod6()
    {
        var methodName = nameof(TestMethod6);
        var varsToCheck = new[]
        {
            ("x", new[] { 1, 62 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod7()
    {
        var methodName = nameof(TestMethod7);
        var varsToCheck = new[]
        {
            ("x", new[] { 1, 4, 5, 6 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod8()
    {
        var methodName = nameof(TestMethod8);
        var varsToCheck = new[]
        {
            ("x", new[] { 1, 4, 5 }.Concat(Enumerable.Range(6, 43 - 6)).ToArray())
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod9()
    {
        var methodName = nameof(TestMethod9);
        var varsToCheck = new[]
        {
            ("x", new[] { 1, 62 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod10()
    {
        var methodName = nameof(TestMethod10);
        var varsToCheck = new[]
        {
            ("x", new[] { 1, 62 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod11()
    {
        var methodName = nameof(TestMethod11);
        var varsToCheck = new[]
        {
            ("x", new[] { 1, 62 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod12()
    {
        var methodName = nameof(TestMethod12);
        var varsToCheck = new[]
        {
            ("x", new[] { 1, 9 }),
            ("y", new[] { 62, 8 }),
            ("z", new[] { 73, 7 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod13()
    {
        var methodName = nameof(TestMethod13);
        var varsToCheck = new[]
        {
            ("x", new[] { 1, 9 }),
            ("y", new[] { 1, 9 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod14()
    {
        var methodName = nameof(TestMethod14);
        var varsToCheck = new[]
        {
            ("x", new[] { 10 }),
            ("y", new[] { 1, 9 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod15()
    {
        var methodName = nameof(TestMethod15);
        var varsToCheck = new[]
        {
            ("x", new[] { 1, 9, 10 }),
            ("y", new[] { 1, 9 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod16()
    {
        var methodName = nameof(TestMethod16);
        var varsToCheck = new[]
        {
            ("x", new[] { 10 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod17()
    {
        var methodName = nameof(TestMethod17);
        var varsToCheck = new (String, int[])[0];

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod18()
    {
        var methodName = nameof(TestMethod18);
        var varsToCheck = new[]
        {
            ("x", new[] { 10 }),
            ("y", new[] { 10 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod19()
    {
        var methodName = nameof(TestMethod19);
        var varsToCheck = new[]
        {
            ("x", new[] { 62 }),
            ("y", new[] { 10 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod20()
    {
        var methodName = nameof(TestMethod20);
        var varsToCheck = new[]
        {
            ("x", new[] { 10 }),
            ("y", new[] { 62 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod21()
    {
        var methodName = nameof(TestMethod21);
        var varsToCheck = Array.Empty<(String, int[])>();

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod22()
    {
        var methodName = nameof(TestMethod22);
        var varsToCheck = Array.Empty<(String, int[])>();

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    [TestMethod]
    public void TestMethod23()
    {
        var methodName = nameof(TestMethod23);
        var varsToCheck = new[]
        {
            ("x", new[] { 0, 3 })
        };

        CheckValues(NameToSyntaxMap[methodName], varsToCheck);
    }
    
    static void CheckValues(MethodDeclarationSyntax targetMethodSyntax,
                            IReadOnlyCollection<(String VarName, int[] Values)> valuesToCheck)
    {
        var cfg = ControlFlowGraph.Create(targetMethodSyntax);
        var dfaResults = DataFlowAnalysis.EvaluateVariables(cfg);

        foreach (var varToCheck in valuesToCheck)
        {
            var dataFlowValue = (IntegerDataFlowValue<int>)dfaResults.GetVariableValue(varToCheck.VarName);
            
            var obtainedValues = dataFlowValue.Values;
            var expectedValues = varToCheck.Values;

            bool valuesAreSame = expectedValues.SequenceEqual(obtainedValues); 
            
            Assert.IsTrue(valuesAreSame, 
                $"{Environment.NewLine}Values are not the same." +
                $"{Environment.NewLine}Expected: {varToCheck.VarName} -> [ {String.Join(", ", expectedValues)} ]" +
                $"{Environment.NewLine}Obtained: {varToCheck.VarName} -> [ {String.Join(", ", obtainedValues)} ]");
        }
    }
    

}