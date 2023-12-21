using System.Numerics;

namespace Analyzer.DataFlow;

public class IntegerDataFlowValue<T> : DataFlowValue where T: IBinaryInteger<T>
{
    private readonly HashSet<T> _values;
    public IReadOnlySet<T> Values => _values;

    private IntegerDataFlowValue(IntegerDataFlowValue<T> value) 
        : this()
    {
        AppendValues(value.Values);
    }
    
    public IntegerDataFlowValue()
    {
        _values = new HashSet<T>();
    }

    public IntegerDataFlowValue(T value)
        : this()
    {
        AppendValue(value);
    }

    public void AppendValue(T value)
    {
        _values.Add(value);
    }
    
    public void AppendValues(IEnumerable<T> values)
    {
        _values.UnionWith(values);
    }

    internal override void Merge(DataFlowValue value)
    {
        if (value is not IntegerDataFlowValue<T> intValue)
            throw new InvalidOperationException($"Unable to merge values: argument is not of the {nameof(IntegerDataFlowValue<T>)} type.");
        
        _values.UnionWith(intValue.Values);
    }

    internal override DataFlowValue Clone()
    {
        return new IntegerDataFlowValue<T>(this);
    }
}

public abstract class DataFlowValue
{
    internal abstract void Merge(DataFlowValue value);
    internal abstract DataFlowValue Clone();
}