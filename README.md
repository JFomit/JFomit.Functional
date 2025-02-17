# JFomit.Functional

## About

*JFomit.Functional* - is a simple C# library designed to provide common functional types and operations on them.

So far this library has:
1. An `Option<T>` type, which wraps an object that might not exist;
2. A `Result<T, E>` type, which wraps an object that might not exist due to some error;
3. A family of `OneOf` types, which are basically discriminated unions over 2 to 4 types.

The library also provides a set of common operations on these types:

```c#
using JFomit.Functional.Extensions;
using JFomit.Functional.Monads;
using static JFomit.Functional.Prelude;

Console.WriteLine("Enter a number:");

Option<int> parsedValue = Parse<int>(Console.ReadLine());
parsedValue.Select(n => n * n).Switch(
    some: (int value) => Console.WriteLine($"Your number squared is {value}!"),
    none: () => Console.WriteLine("Failed to parse a number :_(")
);

static Option<int> Parse<T>(string? s)
    where T : IParsable<T> => T.TryParse(s, null, out var value) ? Some(value) : None;
```

For more examples as well as complete api reference, check [documentation](https://jfomit.github.io/JFomit.Functional/index.html).

## Quick Start Notes:

Add a `JFomit.Functional` Nuget package to your project:

```shell
dotnet add package JFomit.Functional
```

Enjoy!

## License

This project is licensed under the **MIT License**. See [LICENSE.md](LICENSE.md) for details.
The project also includes third-party code. See [NOTICE.md](NOTICE.md) for details.
