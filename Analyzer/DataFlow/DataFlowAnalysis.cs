using Analyzer.ControlFlow;
using Analyzer.Syntax;

namespace Analyzer.DataFlow;

public static class DataFlowAnalysis
{
    public static DataFlowAnalysisResults EvaluateVariables(ControlFlowGraph cfg)
    {
        var entryBlock = cfg.Entry;
        if (entryBlock.Kind != BasicBlockKind.Entry)
            throw new ArgumentException($"EntryBlock was expected. Received kind: {entryBlock.Kind}. Block ordinal: {entryBlock.Ordinal}.");

        // Словарь соответствий базовых блоков к контейнерам переменных, которые они несут на выходе после обработки
        var blockToOutputContainerMap = new Dictionary<BasicBlock, VariablesContainer>();
        
        // Очередь для обхода графа базовых блоков (CFG)
        var traverseQueue = new Queue<BasicBlock>();
        traverseQueue.Enqueue(entryBlock);

        BasicBlock exitBlock = null;
        
        while (traverseQueue.Count != 0)
        {
            var currentBlock = traverseQueue.Dequeue();
            
            // Блок уже был обработан — не процессим его заново
            if (blockToOutputContainerMap.ContainsKey(currentBlock))
                continue;
            
            if (currentBlock.Kind == BasicBlockKind.Exit)
                exitBlock = currentBlock;
            
            VariablesContainer inputContainer = VariablesContainer.Empty;

            bool allPredecessorsReady = true;
            foreach (var predecessor in currentBlock.Predecessors)
            {
                // Если хотя бы один из выходных контейнеров родителей текущего блока не готов,
                // не пытаемся обработать текущий
                if (!blockToOutputContainerMap.TryGetValue(predecessor, out var container))
                {
                    allPredecessorsReady = false;
                    break;
                }
                
                inputContainer.Merge(container);
            }

            if (!allPredecessorsReady)
            {
                traverseQueue.Enqueue(currentBlock);
                continue;
            }

            // Входной контейнер сформирован из зависимостей — обрабатываем текущий блок
            var outputContainer = ProcessBasicBlock(inputContainer, currentBlock);
            blockToOutputContainerMap.Add(currentBlock, outputContainer);
            
            // Ставим дочерние блоки в очередь обработки
            foreach (var successor in currentBlock.Successors)
            {
                traverseQueue.Enqueue(successor);
            }
        }

        if (exitBlock is null)
            throw new InvalidOperationException("Exit block hasn't been processed.");

        return new DataFlowAnalysisResults(blockToOutputContainerMap[exitBlock]);
    }
    
    private static VariablesContainer ProcessBasicBlock(VariablesContainer inputContainer, BasicBlock block)
    {
        var outputContainer = inputContainer;
        foreach (var operation in block.Operations)
        {
            // Применяем функции создания / уничтожения / передачи данных на переменные контейнера
            outputContainer = Transfer(outputContainer, operation);
        }
        
        return outputContainer;
    }

    private static VariablesContainer Transfer(VariablesContainer container, AssignmentOperation assignment)
    {
        var outputContainer = container;
        
        if (   assignment.Kind == AssignmentKind.SimpleAssignment   
            && assignment.Left is VariableReferenceOperation lhs)
        {
            DataFlowVariable dataFlowVariable = default;
            if (assignment.Right is LiteralOperation rhs)
            {
                // Записываем значение целочисленного литерала
                if (int.TryParse(rhs.ValueText, out var intValue))
                {
                    var dataFlowValue = new IntegerDataFlowValue<int>(intValue);
                    dataFlowVariable = new DataFlowVariable(lhs.Symbol, dataFlowValue);
                }
            }
            else if (assignment.Right is VariableReferenceOperation rhsVar)
            {
                // Записываем значение другой переменной
                var value = container.GetValue(rhsVar.Symbol);
                if (value != null)
                {
                    dataFlowVariable = new DataFlowVariable(lhs.Symbol, value);
                }
            }
            
            if (dataFlowVariable != null)
                container.Add(dataFlowVariable);
        }

        return outputContainer;
    }
    
    private static VariablesContainer Transfer(VariablesContainer container, Operation operation)
    {
        var resultContainer = container;
        if (operation is ExpressionStatementOperation { Expression: AssignmentOperation assignOp })
        {
            resultContainer = Transfer(resultContainer, assignOp);
        }

        return resultContainer;
    }
}

public class DataFlowAnalysisResults
{
    private readonly VariablesContainer _container;

    public IEnumerable<String> Variables => _container.VariableNames;
    
    internal DataFlowAnalysisResults(VariablesContainer container)
    {
        _container = container;
    }

    public DataFlowValue GetVariableValue(String varName)
    {
        return _container.GetValue(Symbol.Create(varName));
    }
}
