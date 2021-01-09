# NoStringEvaluating
Fast and easy mathematical evaluation without endless string parsing! Parses string formula one time and use its object sequence in each evaluation. Moreover, provides user defined functions and variables.

[![Build Status](https://travis-ci.org/KovtunV/NoStringEvaluating.svg?branch=master)](https://travis-ci.org/KovtunV/NoStringEvaluating)
[![NuGet version (SimpleCAP)](https://img.shields.io/nuget/v/NoStringEvaluating.svg?style=flat-square)](https://www.nuget.org/packages/NoStringEvaluating)
[![NuGet Download](https://img.shields.io/nuget/dt/NoStringEvaluating.svg?style=flat-square)](https://www.nuget.org/packages/NoStringEvaluating)

------------
<!--ts-->

   * [Features](#Features)
   * [Performance](#Performance)
      * [Testing machine](#Testing-machine)
      * [Testing formulas](#Testing-formulas)
      * [100 000 calculations](#100-000-calculations)
      * [10 000 000 calculations](#10-000-000-calculations)
      * [Memory usage](#Memory-usage)
      * [Conclusion](#Conclusion)
   * [Quick start](#Quick-start)
      * [Initializing](#Initializing)
      * [Usage](#Usage)
   * [Variables](#Variables)
   * [Operators](#Operators)
   * [Boolean operators](#Boolean-operators)
   * [Functions](#Functions)
      * [Math](#Math)
      * [Logic](#Logic)
   * [Options](#Options)
   * [Documentation](#Documentation)
      * [IFormulaParser](#IFormulaParser)
      * [IFunctionReader](#IFunctionReader)
      * [IFormulaCache](#IFormulaCache)
      * [IFormulaChecker](#IFormulaChecker)
      * [INoStringEvaluator](#INoStringEvaluator)
   * [TODO](#TODO)

<!--te-->

------------

## Features

- Fast math evaluation;
- Zero-allocation code (object pooling);
- User defined functions;
- User defined variables with any chars.

## Performance
Compared with a good solution [mXparser](https://github.com/mariuszgromada/MathParser.org-mXparser "mXparser")

- In general, **x6** faster!

### Testing machine
- Laptop
- CPU i7-4710HQ without turbo boost, with fixed 2.5 GHz
- RAM DDR3 1600 MHz

### Testing formulas
|  № | Formula |
| ------------ | ------------ |
| Empty  |   |
|  NumberOnly | 3 |
| 1 |  3 \* 9 |
| 2 | 3 \* 9 / 456 \* 32 + 12 / 17 - 3 |
| 3 | 3 \* (9 / 456 \* (32 + 12)) / 17 - 3  |
| 4 | (2 + 6 - (13 \* 24 + 5 / (123 - 364 + 23))) - (2 + 6 - (13 \* 24 + 5 / (123 - 364 + 23))) + (2 + 6 - (13 \* 24 + 5 / (123 - 364 + 23))) \* 345 \* ((897 - 323)/ 23)  |
| 5 | [Arg1] \* [Arg2] + [Arg3] - [Arg4] |
| 6 | [Arg1] \* ([Arg2] + [Arg3]) - [Arg4] / ([Arg5] - [Arg6]) + 45 \* [Arg7] + (([Arg8] \* 56 + (12 + [Arg9]))) - [Arg10] |
| 7 | add(1; 2; 3) |
| 8 | add(add(5; 1) - add(5; 2; 3)) |
| 9 | if([Arg1]; add(56 + 9 / 12 \* 123.596; or(78; 9; 5; 2; 4; 5; 8; 7); 45;5); 9) \*     24 + 52 -33 |
| 10 | kov(1; 2; 3) - kovt(8; 9)  |

It used to write variables with brackets, but now you can write them without brackets. See more in [Variables](#Variables).

### 100 000 calculations

Less is better.

![image graph](Images/graph100k.png)
![image table](Images/table100k.png)


### 10 000 000 calculations

Less is better.

![image graph](Images/graph100m.png)
![image table](Images/table100m.png)

### Memory usage

![image graph](Images/memory.png)

### Conclusion
In formulas with variables I update all variables before each evaluation. As you can see, this solution faster in all cases and it doesn't matter how many calculations you have 100 000 or 10 000 000.

Benchmark code you can find in **ConsoleApp.Benchmark**.

Benchmark excel sheet you can find here - **Benchmark.xlsx**.

## Quick start
### Initializing
Basically, this solution has developed for web api projects, so you should add implementations in Startup.cs:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    // ......
    services.AddNoStringEvaluator();
}
```

### Usage
Add **INoStringEvaluator** to your controller, service, etc..
And just send **string** or **FormulaNodes** to evaluation:
```csharp
public class MyService
{
    private INoStringEvaluator _noStringEvaluator;
    public MyService(INoStringEvaluator noStringEvaluator)
    {
        _noStringEvaluator = noStringEvaluator;
    }

    public double Calc(string formula)
    {
        return _noStringEvaluator.Calc(formula);
    }
}
```
If you have variables, you can send **IDictionary** or your **IVariablesContainer** implementation:
```csharp
public class MyService
{
    private INoStringEvaluator _noStringEvaluator;

    public MyService(INoStringEvaluator noStringEvaluator)
    {
        _noStringEvaluator = noStringEvaluator;
    }

    public double Calc(string formula, IDictionary<string, double> variables)
    {
        return _noStringEvaluator.Calc(formula, variables);
    }
}
```

If you have custom functions, you shoud use **IFunctionReader** in startup:
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // ...
    var functionReader = app.ApplicationServices.GetRequiredService<IFunctionReader>();
    functionReader.AddFunction(new Func_kov());
}
```

## Variables

You can use two types of variables:
- Simple variable
- Bordered variable

Simple variable means that it named without unique symbols and starts with a letter. Only one extra symbol is possible, it's "_"

Some examples:
- "25 + myArgument - 1"
- "25 + myArg1 - 2"
- "arg5684argArg_arg"
- "25 + myArgument_newAge - 3"

Bordered variable means that it has difficult name with any symbols, except for square brackets.

Some examples:
- "25 + [myVariable and some words] - 1"
- "25 + [Provider("my provider").Month(1).Price] - 2"
- "[myVariable ♥]"
- "[simpleVariable]"

Needless to say, you can write simple variable with brackets too.


## Operators

| Key word  |  Description | Example  |
| ------------ | ------------ | ------------ |
| +  | Addition  | a + b  |
|  - |  Subtraction |  a - b |
|  \* | Multiplication  | a \* b  |
|  / | Division  |  a / b |
| ^|  Exponentiation |  a^b |

## Boolean operators

| Key word  |  Description | Example  |
| ------------ | ------------ | ------------ |
| <  | Lower than | a < b  |
| <=  | Lower or equal| a <= b  |
| >  | Greater than | a > b  |
| >=  | Greater or equal | a >= b  |
| ==  | Equality | a == b  |
| !=  | Inequation | a != b  |
| &&  | Logical conjunction (AND)  | a && b  |
| \|\|  | Logical disjunction (OR)  | a \|\| b  |

## Functions

### Math
| Key word  |  Description | Example  |
| ------------ | ------------ | ------------ |
| add  | Summation operator  | add(a1; a2; ...; an)  |
|  mean |  Mean / average value | mean(a1; a2; ...; an) |


### Logic

| Key word  |  Description | Example  |
| ------------ | ------------ | ------------ |
|  if | If function  | if(cond; expr-if-true; expr-if-false) |
|  and | Logical conjunction (AND)  |   and(a1; a2; ...; an) |
| or |  Logical disjunction (OR) |  or(a1; a2; ...; an) |
| not |  Negation function |  not(x) |


## Options
When you use **AddNoStringEvaluator** in **startup.cs** you can configure evaluator.

There are two options:

- FloatingTolerance (default is 0.0001)
- FloatingPointSymbol (default is FloatingPointSymbol.Dot)

To illustrate, I change floating point from default **dot** to **comma**:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // ......
    services.AddNoStringEvaluator(opt => opt.FloatingPointSymbol = FloatingPointSymbol.Comma);
}
```

## Documentation
Solution contains five following services:
- IFormulaParser
- IFunctionReader
- IFormulaCache
- IFormulaChecker
- INoStringEvaluator

One optional interface, you can implement if IDictionary is inconvenient.
- IVariablesContainer

Furthermore, two object pools:
- `ObjectPool.Create<Stack<double>>`
- `ObjectPool.Create<List<double>>`

### IFormulaParser
Performs two functions:
- Parsing from char collection to object sequence
- Reversing sequeance as **Reverse Polish notation**

Contains two methods:
- `FormulaNodes Parse(string formula)`
- `FormulaNodes Parse(ReadOnlySpan<char> formula)`

### IFunctionReader
Performs using user defined functions.

Contains two methods:
- `void AddFunction(IFunction func, bool replace = false)`
- `bool TryProceedFunction(List<IFormulaNode> nodes, ReadOnlySpan<char> formula, ref int index)`

### IFormulaCache
Performs formula caching. It is used by default **INoStringEvaluator** implementation. It uses **IFormulaParser** which parses string formula to object sequence.

Contains one method:
- `FormulaNodes GetFormulaNodes(string formula)`

### IFormulaChecker
Performs syntax checking.

Contains two methods:
- `CheckFormulaResult CheckSyntax(string formula)`
- `CheckFormulaResult CheckSyntax(ReadOnlySpan<char> formula)`

### INoStringEvaluator
Performs evaluating :relaxed:

Contains six methods:
- `double Calc(string formula, IVariablesContainer variables)`
- `double Calc(FormulaNodes formulaNodes, IVariablesContainer variables)`
- `double Calc(string formula, IDictionary<string, double> variables)`
- `double Calc(FormulaNodes formulaNodes, IDictionary<string, double> variables)`
- `double Calc(string formula)`
- `double Calc(FormulaNodes formulaNodes)`

## TODO
I am going to add these features:
- Add more functions
- Add default variables, kinda Pi