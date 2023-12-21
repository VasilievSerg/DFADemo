using Analyzer.DataFlow;
using Analyzer.Syntax;

namespace Analyzer.ControlFlow;

class AssignmentOperation : ExpressionOperation
{
    public Operation Left { get; }
    public Operation Right { get; }
    
    public AssignmentKind Kind { get; }

    public AssignmentOperation(AssignmentExpressionSyntax assignmentExpression) 
        : base(assignmentExpression)
    {
        Left = Create(assignmentExpression.Left);
        Right = Create(assignmentExpression.Right);

        Kind = assignmentExpression.Kind;
    }
}

class VariableReferenceOperation : ExpressionOperation
{
    public Symbol Symbol { get; }

    public VariableReferenceOperation(IdentifierNameSyntax identifierName) 
        : base(identifierName)
    {
        Symbol = Symbol.Create(identifierName.Name);
    }
}

class LiteralOperation : ExpressionOperation
{
    public String ValueText { get; }

    public LiteralOperation(LiteralExpressionSyntax node) 
        : base(node)
    {
        ValueText = node.Text;
    }
}

class ExpressionOperation : Operation
{
    public ExpressionOperation(ExpressionSyntax expression)
        : base(expression)
    {}
}

class ExpressionStatementOperation : StatementOperation
{
    public ExpressionOperation Expression { get; }
    
    public ExpressionStatementOperation(ExpressionStatementSyntax expressionStatementSyntax)
        : base(expressionStatementSyntax)
    {
        Expression = Create(expressionStatementSyntax.Expression) as ExpressionOperation;
    }
}

class StatementOperation : Operation
{
    public StatementOperation(StatementSyntax statement) 
        : base(statement)
    {
    }
}

class IfOperation : StatementOperation
{
    public StatementOperation[] ThenOperations { get; }
    
    public IfOperation(IfStatementSyntax ifStatementSyntax)
        : base(ifStatementSyntax)
    {
        ThenOperations = ifStatementSyntax.ThenStatements
                                          .Select(Operation.Create)
                                          .OfType<StatementOperation>()
                                          .ToArray();
    }
}

class UnknownOperation : Operation
{
    public UnknownOperation(SyntaxNode node)
        : base(node)
    { }
}

abstract class Operation
{
    private readonly SyntaxNode _node;

    protected Operation(SyntaxNode node)
    {
        _node = node;
    }
    
    public static Operation Create(SyntaxNode node)
    {
        return node switch
        {
            AssignmentExpressionSyntax assignment 
                => new AssignmentOperation(assignment),
            
            IfStatementSyntax ifStatement 
                => new IfOperation(ifStatement),
            
            LiteralExpressionSyntax literal 
                => new LiteralOperation(literal),
            
            IdentifierNameSyntax identifier 
                => new VariableReferenceOperation(identifier),
            
            ExpressionStatementSyntax expressionStatement 
                => new ExpressionStatementOperation(expressionStatement),
            
            StatementSyntax statement 
                => new StatementOperation(statement),
            
            _ => new UnknownOperation(node)
        };
    }
    
}
