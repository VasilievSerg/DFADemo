using System.Collections.Immutable;

namespace Analyzer.DataFlow;

class VariablesContainer
{
    private readonly Dictionary<Symbol, DataFlowVariable> _containerMap;

    private IEnumerable<DataFlowVariable> Variables => _containerMap.Select(kvp => kvp.Value);

    internal IEnumerable<String> VariableNames => Variables.Select(p => p.Symbol.Name);
    
    public static VariablesContainer Empty => new VariablesContainer();
    
    private VariablesContainer() 
        : this(ImmutableDictionary<Symbol, DataFlowVariable>.Empty)
    {
        _containerMap = new Dictionary<Symbol, DataFlowVariable>();
    }

    private VariablesContainer(IDictionary<Symbol, DataFlowVariable> containerMap)
    {
        _containerMap = new Dictionary<Symbol, DataFlowVariable>(containerMap);
    }
    
    public void Add(DataFlowVariable variable)
    {
        _containerMap[variable.Symbol] = variable;
    }

    public void Merge(VariablesContainer container)
    {
        foreach (var otherVariable in container.Variables)
        {
            if (_containerMap.TryGetValue(otherVariable.Symbol, out var currentVariable))
            {
                currentVariable.MergeValue(otherVariable.Value);
            }
            else
            {
                _containerMap.Add(otherVariable.Symbol, otherVariable);
            }
        }
    }

    public DataFlowValue GetValue(Symbol variableSymbol)
    {
        _containerMap.TryGetValue(variableSymbol, out var dataFlowVariable);
        return dataFlowVariable?.Value;
    }
}