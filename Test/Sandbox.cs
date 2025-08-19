using BenchmarkDotNet.Attributes;
using JFomit.Functional.Extensions;
using JFomit.Functional.Monads;
using static JFomit.Functional.Prelude;

var summary = BenchmarkDotNet.Running.BenchmarkRunner.Run<Bench>();
return;

var num = Some(32);
var name = Some("hello");

var r = from n in num
        from a in name
        select a + n;

[MemoryDiagnoser]
public class Bench
{
    [Benchmark]
    public Option<Data> TestAllocations()
    {
        var b = new Builder();
        return b.WithX(42).WithY("ss").WithZ(3.14f).Build();
    }
}

public record Data(int X, string Y, float Z);
public class Builder
{
    Option<int> X = None;
    Option<string> Y = None;
    Option<float> Z = None;

    public Builder WithX(int x)
    {
        X = Some(x);
        return this;
    }
    public Builder WithY(string y)
    {
        Y = Some(y);
        return this;
    }
    public Builder WithZ(float z)
    {
        Z = Some(z);
        return this;
    }

    public Option<Data> Build()
        => from x in X
           from y in Y
           from z in Z
           select new Data(x, y, z);
}
