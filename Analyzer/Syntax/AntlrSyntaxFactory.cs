using Analyzer.Parsers;
using Analyzer.Parsers.Java;

namespace Analyzer.Syntax;

public enum TargetLanguage
{
    Java
}

public abstract class AntlrSyntaxFactory
{
    private static readonly
        IReadOnlyDictionary<TargetLanguage, Func<AntlrSyntaxFactory>>
        _languageToSyntaxFactoryCreatorMap = new Dictionary<TargetLanguage, Func<AntlrSyntaxFactory>>()
        {
            { TargetLanguage.Java, () => new AntlrJavaSyntaxFactory() }
        };
    
    public static AntlrSyntaxFactory Create(TargetLanguage language)
    {
        if (!_languageToSyntaxFactoryCreatorMap.TryGetValue(language, out var factoryCreator))
        {
            var supportedLanguages = String.Join(", ", _languageToSyntaxFactoryCreatorMap.Keys);
            throw new ArgumentException($"Unsupported language: {language}. Supported languages: {supportedLanguages}.");
        }

        return factoryCreator();
    }

    public abstract IAntlrTreeWrapper CreateTreeWrapper(String sources);

    public abstract IAntlrTreeTranslator CreateTranslator();
}