using Analyzer.Parsers.Java.Grammars;
using Analyzer.Syntax;

namespace Analyzer.Parsers.Java;

public interface IAntlrTreeTranslator : ISyntaxTranslator
{
    void Translate(IAntlrTreeWrapper antlrTreeWrapper);
}

class AntlrJavaTranslator : JavaParserBaseVisitor<Object>, IAntlrTreeTranslator
{
    public void Translate(IAntlrTreeWrapper antlrTreeWrapper)
    {
        antlrTreeWrapper.Traverse(this);
    }
    
    public override Object VisitMethodDeclaration(JavaParser.MethodDeclarationContext context)
    {
        var methodDeclarationSyntax = Translate(context);
        
        OnMethodDeclarationTranslated?.Invoke(this, new NodeTranslatedEventArgs<MethodDeclarationSyntax>(methodDeclarationSyntax));
        
        return base.VisitMethodDeclaration(context);
    }

    private StatementSyntax Translate(JavaParser.StatementContext statement)
    {
        StatementSyntax statementSyntax = null;
        if (statement != null)
        {
            if (statement.IF() != null)
            {
                statementSyntax = TranslateIfStatement(statement);
            }
            else if (statement.statementExpression != null)
            {
                statementSyntax = new ExpressionStatementSyntax(Translate(statement.statementExpression));
            }
        }

        if (statementSyntax == null)
        {
            statementSyntax = new UnknownStatementSyntax();
        }

        return statementSyntax;
    }

    private ExpressionSyntax Translate(JavaParser.ExpressionContext expression)
    {
        ExpressionSyntax expressionSyntax;
        if (IsAssignmentExpr(expression))
        {
            expressionSyntax = TranslateAssignment(expression);
        }
        else if (TeyGetIdentifierContext(expression, out var identifierContext))
        {
            expressionSyntax = Translate(identifierContext);
        }
        else if (TryGetLiteralContext(expression, out var literalContext))
        {
            expressionSyntax = Translate(literalContext);
        }
        else
        {
            expressionSyntax = new UnknownExpressionSyntax();
        }

        return expressionSyntax;
    }

    public MethodDeclarationSyntax Translate(JavaParser.MethodDeclarationContext methodDeclaration)
    {
        var name = methodDeclaration.identifier().IDENTIFIER().Symbol.Text;
        var statements = methodDeclaration.methodBody()
                                         ?.block()
                                         ?.blockStatement()
                                         ?.Select(p => p.statement())
                                          .Select(Translate)
                                          .ToArray();

        var methodDeclarationSyntax = new MethodDeclarationSyntax(name, statements);
        
        return methodDeclarationSyntax;
    }

    private ExpressionSyntax Translate(JavaParser.IdentifierContext identifier)
    {
        var identifierNameSyntax = new IdentifierNameSyntax(identifier.IDENTIFIER().Symbol.Text);
        return identifierNameSyntax;
    }

    private ExpressionSyntax Translate(JavaParser.LiteralContext literal)
    {
        ExpressionSyntax literalSyntax = default;
        
        var intLiteral = literal.integerLiteral()?.DECIMAL_LITERAL()?.Symbol?.Text;

        if (intLiteral != null)
        {
            literalSyntax = new LiteralExpressionSyntax(intLiteral);
        }
        else
        {
            literalSyntax = new UnknownExpressionSyntax();
        }

        return literalSyntax;
    }
    
    private IfStatementSyntax TranslateIfStatement(JavaParser.StatementContext ifStatement)
    {
        ParseIfStatement(ifStatement, out var conditionalExpressionContext, out var thenStatementsContexts);

        var translatedConditionalExpr = Translate(conditionalExpressionContext);
        var translatedThenStatements = thenStatementsContexts.Select(Translate)
                                                             .ToArray();
        
        var ifStatementSyntax = new IfStatementSyntax(translatedConditionalExpr, translatedThenStatements);
        return ifStatementSyntax;
    }

    private ExpressionSyntax TranslateAssignment(JavaParser.ExpressionContext assignment)
    {
        var lhs = assignment.children[0] as JavaParser.ExpressionContext;
        var rhs = assignment.children[2] as JavaParser.ExpressionContext;

        var assignmentSyntax = new AssignmentExpressionSyntax(Translate(lhs),
                                                              Translate(rhs),
                                                              assignment.bop.Text);

        return assignmentSyntax;
    }

    #region Utils

    private static void ParseIfStatement(JavaParser.StatementContext ifStatement,
                                         out JavaParser.ExpressionContext conditionalExpression,
                                         out IEnumerable<JavaParser.StatementContext> thenStatements)
    {
        conditionalExpression = null;
        thenStatements = null;

        conditionalExpression = ifStatement.parExpression()?.expression();

        var thenStatementContext = ifStatement.statement().FirstOrDefault();

        if (thenStatementContext?.block() is JavaParser.BlockContext blockContext)
        {
            thenStatements = blockContext.blockStatement()
                                         .Select(p => p.statement());
        }
        else
        {
            thenStatements = new[] { thenStatementContext };
        }
    }

    // На данный момент транслируем только простые присваивания
    private static bool IsAssignmentExpr(JavaParser.ExpressionContext expression)
    {
        return    expression.ChildCount == 3 
               && expression.bop?.Text switch
        {
            "=" => true,
            _ => false
        };
    }
    
    private static bool TeyGetIdentifierContext(JavaParser.ExpressionContext expression, 
                                                out JavaParser.IdentifierContext identifierContext)
    {
        identifierContext = expression.primary()?.identifier();
        return identifierContext != null;
    }

    private static bool TryGetLiteralContext(JavaParser.ExpressionContext expression, 
                                             out JavaParser.LiteralContext literalContext)
    {
        literalContext = expression.primary()?.literal();
        return literalContext != null;
    }
    
    #endregion Utils

    public event ISyntaxTranslator.NodeTranslationEventHandler<MethodDeclarationSyntax> OnMethodDeclarationTranslated;

}