using JFomit.Functional.Extensions;
using JFomit.Functional.Monads;
using static JFomit.Functional.Prelude;

namespace Test;

public class LinqTests
{
    [Fact]
    public void Validation()
    {
        var obj = new Dto().WithValue(42).WithVariant(new Variant.FloatVariant(500)).Build();
        Assert.Equal(None, obj);
    }

    class Data
    {
        public Option<string> Description;
        public int Value;

        public required Variant Variant;
    }

    class Dto
    {
        Option<string> _description;
        Option<int> _value;
        Option<Variant> _variant;

        public Dto WithDescription(string description)
        {
            _description = Some(description);
            return this;
        }
        public Dto WithValue(int value)
        {
            _value = Some(value);
            return this;
        }
        public Dto WithVariant(Variant variant)
        {
            _variant = Some(variant);
            return this;
        }

        public Option<Data> Build()
            => from value in _value
               where value is > 0 and < 100

               from variant in _variant
               where variant is Variant.IntVariant(> 45)
                             or Variant.StringVariant("The only string")
                             or Variant.FloatVariant(< 0)
               select new Data()
               {
                   Description = _description,
                   Value = value,
                   Variant = variant,
               };
    }

    abstract record Variant
    {
        private Variant() { }

        public sealed record IntVariant(int Value) : Variant;
        public sealed record StringVariant(string Value) : Variant;
        public sealed record FloatVariant(float Value) : Variant;
    }
}
