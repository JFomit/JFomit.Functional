using JFomit.Functional.Extensions;
using static JFomit.Functional.Prelude;

var num = Some(32);
var name = Some("hello");

var r = from n in num
        from a in name
        select a + n;
