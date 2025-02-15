using System.Diagnostics.CodeAnalysis;
using JFomit.Functional.Monads;

namespace JFomit.Functional;

internal static class ThrowHelper
{
    [DoesNotReturn]
    internal static T ThrowWrongUnwrapException<T>(string message)
        => throw new WrongUnwrapException(message);
}