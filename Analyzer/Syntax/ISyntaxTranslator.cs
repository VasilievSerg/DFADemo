namespace Analyzer.Syntax;

public class NodeTranslatedEventArgs<T> : EventArgs where T : SyntaxNode
{
    public T Node { get; }
    
    public NodeTranslatedEventArgs(T node)
    {
        Node = node;
    }
}

public interface ISyntaxTranslator
{
    public delegate void NodeTranslationEventHandler<T>(Object sender, NodeTranslatedEventArgs<T> e) where T: SyntaxNode;
    
    public event NodeTranslationEventHandler<MethodDeclarationSyntax> OnMethodDeclarationTranslated;
}