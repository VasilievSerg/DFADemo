using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Analyzer.Parsers;

public interface IAntlrTreeWrapper
{
    IParseTree GetTreeRoot();
    void Traverse(AbstractParseTreeVisitor<Object> visitor);
}

abstract class 
AntlrTreeWrapper<TParser, TLexer> : IAntlrTreeWrapper 
    where TParser : Parser
    where TLexer : Lexer
{
    protected TParser Parser { get; }
    protected TLexer Lexer { get; }

    protected AntlrTreeWrapper(String sources)
    {
        var antlrStream = new AntlrInputStream(sources);
        Lexer = CreateLexer(antlrStream);
        
        var commonTokenStream = new CommonTokenStream(Lexer);

        Parser = CreateParser(commonTokenStream);
    }

    protected abstract TLexer CreateLexer(AntlrInputStream antlrInputStream);
    protected abstract TParser CreateParser(CommonTokenStream commonTokenStream);

    public void Traverse(AbstractParseTreeVisitor<Object> visitor)
    {
        var treeRoot = GetTreeRoot();
        visitor.Visit(treeRoot);
    }

    public abstract IParseTree GetTreeRoot();
}