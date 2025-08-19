using JFomit.Functional.Extensions;
using JFomit.Functional.Monads;
using static JFomit.Functional.Prelude;

namespace Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var num = Some(32);
        var name = Some("hello");

        var r = from n in num
                where n > 42
                from a in name
                select a + n;

        Assert.Equal(None, r);
    }
}
