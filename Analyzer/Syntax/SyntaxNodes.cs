namespace Analyzer.Syntax;

public abstract class SyntaxNode
{ }

public class UnknownStatementSyntax : StatementSyntax
{ }

public class StatementSyntax : SyntaxNode
{ }

public class ExpressionStatementSyntax : StatementSyntax
{
    public ExpressionSyntax Expression { get; }

    public ExpressionStatementSyntax(ExpressionSyntax expression)
    {
        ArgumentNullException.ThrowIfNull(expression);
        
        Expression = expression;
    }
}

public class IfStatementSyntax : StatementSyntax
{
    public ExpressionSyntax Condition { get; }
    public StatementSyntax[] ThenStatements { get; }
    
    public IfStatementSyntax(ExpressionSyntax condition, StatementSyntax[] bodyStatements)
    {
        ArgumentNullException.ThrowIfNull(condition);
        
        Condition = condition;
        ThenStatements = bodyStatements;
    }
}

public class MethodDeclarationSyntax : SyntaxNode
{
    public String Name { get; }
    public StatementSyntax[] Statements { get; }

    public MethodDeclarationSyntax(String name, StatementSyntax[] statements)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        
        Name = name;
        Statements = statements;
    }
}

public class LiteralExpressionSyntax : ExpressionSyntax
{
    public String Text { get; }
    
    public LiteralExpressionSyntax(String text)
    {
        ArgumentException.ThrowIfNullOrEmpty(text);
        
        Text = text;
    }
}

public class IdentifierNameSyntax : ExpressionSyntax
{
    public String Name { get; }
    
    public IdentifierNameSyntax(String name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        
        Name = name;
    }
}

public class UnknownExpressionSyntax : ExpressionSyntax
{ }

public class ExpressionSyntax : SyntaxNode
{ }

public enum AssignmentKind
{
    SimpleAssignment
}

public class AssignmentExpressionSyntax : ExpressionSyntax
{
    private static readonly
        IReadOnlyDictionary<String, AssignmentKind> _strToKindMap = new Dictionary<string, AssignmentKind>()
        {
            ["="] = AssignmentKind.SimpleAssignment
        };

    public ExpressionSyntax Left { get; }
    public ExpressionSyntax Right { get; }
    
    public AssignmentKind Kind { get; }

    public AssignmentExpressionSyntax(ExpressionSyntax left, ExpressionSyntax right, String kindStr)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        
        Left = left;
        Right = right;
        Kind = _strToKindMap[kindStr];
    }
}