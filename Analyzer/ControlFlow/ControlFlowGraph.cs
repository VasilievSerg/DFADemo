using Analyzer.Syntax;

namespace Analyzer.ControlFlow;

public class ControlFlowGraph
{
    internal BasicBlock Entry { get; }

    private ControlFlowGraph(BasicBlock entryBlock)
    {
        Entry = entryBlock;
    }
    
    public static ControlFlowGraph Create(MethodDeclarationSyntax methodDeclaration)
    {
        var ordinal = 0;
        var entryBlock = new BasicBlock(ordinal++, BasicBlockKind.Entry);

        var statements = methodDeclaration.Statements;
        var operations = statements.Select(Operation.Create);
        var statementOperations = operations.OfType<StatementOperation>();

        var currentLevelBlocks = new Stack<BasicBlock>();
        currentLevelBlocks.Push(entryBlock);
        CreateBlock(ref ordinal, statementOperations, currentLevelBlocks);

        var exitBlock = new BasicBlock(ordinal, BasicBlockKind.Exit);
        while (currentLevelBlocks.TryPop(out var predecessor))
        {
            exitBlock.AddPredecessor(predecessor);
        }

        var cfg = new ControlFlowGraph(entryBlock);
        return cfg;
    }

    /// <summary>
    /// Метод создания базового блока.
    /// Если встречает операцию перехода, создаёт связанную между собой цепочку блоков.
    /// </summary>
    /// <param name="ordinal">Порядковый номер следующего блока</param>
    /// <param name="operations">Список операций базового блока. Может быть разбит по нескольким блокам</param>
    /// <param name="currentBlockPredecessors">Родители текущего (создаваемого) блока</param>
    static void CreateBlock(ref int ordinal, 
                            IEnumerable<StatementOperation> operations,
                            Stack<BasicBlock> currentBlockPredecessors)
    {
        BasicBlock currentBlock = null;
        var currentBlockOperations = new List<Operation>();
        foreach (var operation in operations)
        {
            currentBlockOperations.Add(operation);
            if (operation is IfOperation ifOperation)
            {
                // Если встретили операцию перехода, фиксируем текущий блок и уходим в рекурсивное создание ответвлений
                currentBlock = new BasicBlock(ordinal++, currentBlockOperations);
                while (currentBlockPredecessors.TryPop(out var predecessor))
                {
                    currentBlock.AddPredecessor(predecessor);
                }
                
                currentBlockOperations.Clear();
                currentBlockPredecessors.Push(currentBlock);
                
                // Создаём блок / цепочка условных блоков для jump-ответвления
                CreateBlock(ref ordinal, ifOperation.ThenOperations, currentBlockPredecessors);
                currentBlockPredecessors.Push(currentBlock);
            }
        }

        // Создаём базовый блок из накопленных ранее операций, связывая его с родителями
        currentBlock = new BasicBlock(ordinal++, currentBlockOperations);
        while (currentBlockPredecessors.TryPop(out var predecessor))
        {
            currentBlock.AddPredecessor(predecessor);
        }
        
        currentBlockPredecessors.Push(currentBlock);
    }
}