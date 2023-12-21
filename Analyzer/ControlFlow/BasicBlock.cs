namespace Analyzer.ControlFlow;

enum BasicBlockKind
{
    Entry,
    Exit,
    Block
}

class BasicBlock : IEquatable<BasicBlock>
{
    private readonly Operation[] _operations;
    
    private readonly HashSet<BasicBlock> _predecessors;
    private readonly HashSet<BasicBlock> _successors;
    
    public int Ordinal { get; }
    public BasicBlockKind Kind { get; }
    
    public IReadOnlyCollection<Operation> Operations => _operations;
    
    public IReadOnlySet<BasicBlock> Predecessors => _predecessors;
    public IReadOnlySet<BasicBlock> Successors => _successors;

    public BasicBlock(int ordinal, IEnumerable<Operation> operations)
        : this(ordinal, BasicBlockKind.Block, operations)
    {}
    
    public BasicBlock(int ordinal, BasicBlockKind kind) 
        : this(ordinal, kind, Enumerable.Empty<Operation>())
    {}
    
    private BasicBlock(int ordinal, BasicBlockKind kind, IEnumerable<Operation> operations)
    {
        Ordinal = ordinal;
        Kind = kind;
        
        // Да, доп. буферизация, но защищаемся от потенциального изменения кол-ва операций блока снаружи
        _operations = operations.ToArray();
        
        _predecessors = new HashSet<BasicBlock>();
        _successors = new HashSet<BasicBlock>();
    }
    
    /// <summary>
    /// Добавляет родительский блок (<see cref="predecessor"/>) к текущему
    /// Также выставляет для <see cref="predecessor"/> текущий блок как дочерний
    /// </summary>
    public void AddPredecessor(BasicBlock predecessor)
    {
        AddPredecessor(predecessor, internalCall: false);
    }
    
    /// <summary>
    /// Добавляет дочерний блок (<see cref="successor"/>) к текущему
    /// Также выставляет для <see cref="successor"/> текущий блок как родительский
    /// </summary>
    public void AddSuccessor(BasicBlock successor)
    {
        AddSuccessor(successor, internalCall: false);
    }

    private void AddPredecessor(BasicBlock predecessor, bool internalCall)
    {
        _predecessors.Add(predecessor);
        
        // Защита от косвенной рекурсии
        if (!internalCall)
            predecessor.AddSuccessor(this, true);
    }
    
    private void AddSuccessor(BasicBlock successor, bool internalCall)
    {
        _successors.Add(successor);
        
        // Защита от косвенной рекурсии
        if (!internalCall)
            successor.AddPredecessor(this, true);
    }

    public override string ToString()
    {
        return $"BasicBlock [Ordinal: {Ordinal} Kind: {Kind}]";
    }

    public override int GetHashCode()
    {
        return Ordinal;
    }

    public bool Equals(BasicBlock other)
    {
        return this.Ordinal == other?.Ordinal;
    }
}