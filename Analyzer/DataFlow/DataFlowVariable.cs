namespace Analyzer.DataFlow;

class DataFlowVariable
{
    private readonly DataFlowValue _value;
    public DataFlowValue Value => _value;
    
    public Symbol Symbol { get; }

    public DataFlowVariable(Symbol symbol, DataFlowValue value)
    {
        Symbol = symbol;
        
        // Клонируем контейнер значения, чтобы избежать потенциальных протечек значений
        // из-за ссылки двух переменных на один контейнер
        _value = value.Clone();
    }

    public void MergeValue(DataFlowValue dataFlowValue)
    {
        _value.Merge(dataFlowValue);
    }
}