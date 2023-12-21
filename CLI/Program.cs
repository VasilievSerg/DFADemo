using Analyzer.ControlFlow;
using Analyzer.DataFlow;
using Analyzer.Syntax;

namespace CLI;

public class CLIStartup
{
    private static readonly IReadOnlyDictionary<String, TargetLanguage> _extensionsToLanguageMap =
        new Dictionary<String, TargetLanguage>(StringComparer.OrdinalIgnoreCase)
        {
            { ".java", TargetLanguage.Java }
        };
    
    public static int Main(String[] args)
    {
        var exitCode = ExitCodes.Success;
        
        try
        {
            CheckArguments(args, out var targetFile, out var targetLanguage);

            var sources = File.ReadAllText(targetFile);

            var antlrSyntaxFactory = AntlrSyntaxFactory.Create(targetLanguage);
            
            var antlrTreeWrapper = antlrSyntaxFactory.CreateTreeWrapper(sources);
            var translator = antlrSyntaxFactory.CreateTranslator();
            
            var allMethods = new List<MethodDeclarationSyntax>();
            translator.OnMethodDeclarationTranslated 
                += (sender, eventArgs) => allMethods.Add(eventArgs.Node);

            translator.Translate(antlrTreeWrapper);
            
            foreach (var method in allMethods)
            {
                var cfg = ControlFlowGraph.Create(method);
                var dfaResults = DataFlowAnalysis.EvaluateVariables(cfg);
                
                Console.Out.WriteLine($"Method: {method.Name}");
                foreach (var variable in dfaResults.Variables)
                {
                    var integerDataFlowValue = dfaResults.GetVariableValue(variable) as IntegerDataFlowValue<int>;
                    if (integerDataFlowValue != null)
                    {
                        Console.Out.WriteLine($"{variable}: [{String.Join(", ", integerDataFlowValue.Values)}]");
                    }
                }
                
                Console.Out.WriteLine();
            }
        }
        catch (InvalidStartupException ex)
        {
            Console.Error.WriteLine($"Unable to launch the analyzer. {ex.Message}");
            exitCode = ExitCodes.StartupFailure;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
            exitCode = ExitCodes.CommonException;
        }
        
        return (int)exitCode;
    }
    
    static void CheckArguments(String[] args, 
                               out String targetFilePath, 
                               out TargetLanguage sourcesLanguage)
    {
        if (args.Length != 1)
        {
            throw new InvalidStartupException(
                "Invalid arguments count. Usage example: DataFlowAnalyzer.CLI.dll pathToSources.java");
        }

        targetFilePath = args[0];
        if (!File.Exists(targetFilePath))
        {
            throw new InvalidStartupException($"Target file is not found: {targetFilePath}");
        }

        var ext = Path.GetExtension(targetFilePath);
        if (!_extensionsToLanguageMap.TryGetValue(ext, out sourcesLanguage))
        {
            throw new InvalidStartupException($"Unsupported target file extension: {ext}");
        }
    }
}

public enum ExitCodes
{
    Success = 0,
    StartupFailure,
    CommonException
}

public class InvalidStartupException : Exception
{
    internal InvalidStartupException(String message)
        : base(message)
    {}
}