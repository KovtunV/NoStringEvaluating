using System;
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
            return a * b;
        }

        return default;
    }

    public static InternalEvaluatorValue Divide(in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return a / b;
        }

        return default;
    }

    public static InternalEvaluatorValue Plus(in ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return a + b;
        }

        if (a.IsWord || b.IsWord)
        {
            return factory.Word.Concat(a, b);
        }

        if (a.IsNumber && b.IsDateTime)
        {
            return factory.DateTime.Create(b.GetDateTime().AddDays(a.Number));
        }

        if (a.IsDateTime && b.IsNumber)
        {
            return factory.DateTime.Create(a.GetDateTime().AddDays(b.Number));
        }

        return default;
    }

    public static InternalEvaluatorValue Minus(in ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return a - b;
        }

        if (a.IsDateTime && b.IsNumber)
        {
            return factory.DateTime.Create(a.GetDateTime().AddDays(-b.Number));
        }

        if (a.IsDateTime && b.IsDateTime)
        {
            return a.GetDateTime().Subtract(b.GetDateTime()).TotalDays;
        }

        return default;
    }

    public static InternalEvaluatorValue Power(in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return Math.Pow(a, b);
        }

        return default;
    }

    #endregion

    #region Logic

    public static InternalEvaluatorValue Less(in ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(a < b);
        }

        if (a.IsDateTime && b.IsDateTime)
        {
            return factory.Boolean.Create(a.GetDateTime() < b.GetDateTime());
        }

        return default;
    }

    public static InternalEvaluatorValue LessEqual(in ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(a <= b);
        }

        if (a.IsDateTime && b.IsDateTime)
        {
            return factory.Boolean.Create(a.GetDateTime() <= b.GetDateTime());
        }

        return default;
    }

    public static InternalEvaluatorValue More(in ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(a > b);
        }

        if (a.IsDateTime && b.IsDateTime)
        {
            return factory.Boolean.Create(a.GetDateTime() > b.GetDateTime());
        }

        return default;
    }

    public static InternalEvaluatorValue MoreEqual(in ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(a >= b);
        }

        if (a.IsDateTime && b.IsDateTime)
        {
            return factory.Boolean.Create(a.GetDateTime() >= b.GetDateTime());
        }

        return default;
    }

    public static InternalEvaluatorValue Equal(in ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(Math.Abs(a - b) < NoStringEvaluatorConstants.FloatingTolerance);
        }

        return factory.Boolean.Create(a.Equals(b));
    }

    public static InternalEvaluatorValue NotEqual(in ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(Math.Abs(a - b) > NoStringEvaluatorConstants.FloatingTolerance);
        }

        return factory.Boolean.Create(!a.Equals(b));
    }

    #endregion

    #region Additional logic

    public static InternalEvaluatorValue And(in ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(Math.Abs(a) > NoStringEvaluatorConstants.FloatingTolerance && Math.Abs(b) > NoStringEvaluatorConstants.FloatingTolerance);
        }

        return factory.Boolean.Create(a && b);
    }

    public static InternalEvaluatorValue Or(in ValueFactory factory, in InternalEvaluatorValue a, in InternalEvaluatorValue b)
    {
        if (a.IsNumber && b.IsNumber)
        {
            return factory.Boolean.Create(Math.Abs(a) > NoStringEvaluatorConstants.FloatingTolerance || Math.Abs(b) > NoStringEvaluatorConstants.FloatingTolerance);
        }

        return factory.Boolean.Create(a || b);
    }

    #endregion
}
