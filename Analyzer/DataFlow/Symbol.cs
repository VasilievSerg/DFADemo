namespace Analyzer.DataFlow;

public class Symbol : IEquatable<Symbol>
{
    private readonly String _name;

    public String Name => _name;
    
    private Symbol(String name)
    {
        _name = name;
    }

    public static Symbol Create(String identifierName)
    {
        return new Symbol(identifierName);
    }

    public bool Equals(Symbol other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return _name.Equals(other._name);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Symbol);
    }

    public override int GetHashCode()
    {
        return _name.GetHashCode();
    }
}