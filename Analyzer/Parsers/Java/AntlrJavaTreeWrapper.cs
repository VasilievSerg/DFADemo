using Analyzer.Parsers.Java.Grammars;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Analyzer.Parsers.Java;

class AntlrJavaTreeWrapper : AntlrTreeWrapper<JavaParser, JavaLexer>
{
    public AntlrJavaTreeWrapper(String sources) 
        : base(sources)
    { }
    
    protected override JavaLexer CreateLexer(AntlrInputStream antlrInputStream)
    {
        return new JavaLexer(antlrInputStream);
    }

    protected override JavaParser CreateParser(CommonTokenStream commonTokenStream)
    {
        return new JavaParser(commonTokenStream);
    }

    public override IParseTree GetTreeRoot()
    {
        return Parser.compilationUnit();
    }
}