using BenchmarkDotNet.Attributes;
using NeinLinq;
using System.Linq.Expressions;

namespace BenchmarkExample;

[MemoryDiagnoser]
public class BenchmarkExample
{
    public TestContext DbContext { get; init; }
    public DateTime From { get; init; }
    public DateTime To { get; init; }

    public BenchmarkExample()
    {
        DbContext = new TestContext();
        From = DateTime.MinValue;
        To = DateTime.MaxValue;
    }

    [Benchmark(Description = "direct expression", Baseline = true)]
    public int QueryDirectExpresion()
    {
        return DbContext.Items
            .Where(i => From <= i.DateTime && i.DateTime <= To)
            .Count();
    }

    [Benchmark(Description = "NeinLinq")]
    public int QueryWithNeinLinq()
    {
        return DbContext.Items.ToEntityInjectable()
            .Where(i => Between(i.DateTime, From, To))
            .Count();
    }

    [InjectLambda]
    private static bool Between(DateTime dt, DateTime from, DateTime to) => Between().Compile()(dt, from, to);

    private static Expression<Func<DateTime, DateTime, DateTime, bool>> Between()
    {
        return (dt, from, to) => from <= dt && dt <= to;
    }
}
