using NoStringEvaluating.Factories;
using NoStringEvaluating.Models.Values;

namespace NoStringEvaluating.Services;

internal static class OperationProcessor
{
    #region Math

    public static InternalEvaluatorValue Multiply(in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return a.Number * b.Number;
        }

        return default;
    }

    public static InternalEvaluatorValue Divide(in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return a.Number / b.Number;
        }

        return default;
    }

    public static InternalEvaluatorValue Plus(ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return a.Number + b.Number;
        }

        if (a.IsWord || b.IsWord)
        {
            return factory.Word.Concat(a, b);
        }

        if (a.IsNumber && b.IsDateTime)
        {
            return factory.DateTime.Create(b.DateTime.AddDays(a.Number));
        }

        if (a.IsDateTime && b.IsNumber)
        {
            return factory.DateTime.Create(a.DateTime.AddDays(b.Number));
        }

        return default;
    }

    public static InternalEvaluatorValue Minus(ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return a.Number - b.Number;
        }

        if (a.IsDateTime && b.IsNumber)
        {
            return factory.DateTime.Create(a.DateTime.AddDays(-b.Number));
        }

        if (a.IsDateTime && b.IsDateTime)
        {
            return a.DateTime.Subtract(b.DateTime).TotalDays;
        }

        return default;
    }

    public static InternalEvaluatorValue Power(ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Create(Math.Pow(a.Number, b.Number));
        }

        return default;
    }

    #endregion

    #region Logic

    public static InternalEvaluatorValue Less(ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(a.Number < b.Number);
        }

        if (a.IsDateTime && b.IsDateTime)
        {
            return factory.Boolean.Create(a.DateTime < b.DateTime);
        }

        return default;
    }

    public static InternalEvaluatorValue LessEqual(ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(a.Number <= b.Number);
        }

        if (a.IsDateTime && b.IsDateTime)
        {
            return factory.Boolean.Create(a.DateTime <= b.DateTime);
        }

        return default;
    }

    public static InternalEvaluatorValue More(ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(a.Number > b.Number);
        }

        if (a.IsDateTime && b.IsDateTime)
        {
            return factory.Boolean.Create(a.DateTime > b.DateTime);
        }

        return default;
    }

    public static InternalEvaluatorValue MoreEqual(ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(a.Number >= b.Number);
        }

        if (a.IsDateTime && b.IsDateTime)
        {
            return factory.Boolean.Create(a.DateTime >= b.DateTime);
        }

        return default;
    }

    public static InternalEvaluatorValue Equal(ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(Math.Abs(a.Number - b.Number) < GlobalOptions.FloatingTolerance);
        }

        return factory.Boolean.Create(a.Equals(b));
    }

    public static InternalEvaluatorValue NotEqual(ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(Math.Abs(a.Number - b.Number) > GlobalOptions.FloatingTolerance);
        }

        return factory.Boolean.Create(!a.Equals(b));
    }

    #endregion

    #region Additional logic

    public static InternalEvaluatorValue And(ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(Math.Abs(a.Number) > GlobalOptions.FloatingTolerance && Math.Abs(b.Number) > GlobalOptions.FloatingTolerance);
        }

        return factory.Boolean.Create(a.Boolean && b.Boolean);
    }

    public static InternalEvaluatorValue Or(ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(Math.Abs(a.Number) > GlobalOptions.FloatingTolerance || Math.Abs(b.Number) > GlobalOptions.FloatingTolerance);
        }

        return factory.Boolean.Create(a.Boolean || b.Boolean);
    }

    #endregion
}
