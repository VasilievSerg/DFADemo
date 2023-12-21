using Analyzer.Syntax;

namespace Analyzer.Parsers.Java;

class AntlrJavaSyntaxFactory : AntlrSyntaxFactory
{
    public override IAntlrTreeWrapper CreateTreeWrapper(String sources)
    {
        return new AntlrJavaTreeWrapper(sources);
    }

    public override IAntlrTreeTranslator CreateTranslator()
    {
        return new AntlrJavaTranslator();
    }
}