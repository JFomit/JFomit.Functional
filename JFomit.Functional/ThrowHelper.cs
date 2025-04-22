using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JFomit.Functional.Monads;

namespace JFomit.Functional;

internal static class ThrowHelper
{
    [DoesNotReturn]
    [DebuggerHidden]
    internal static T ThrowWrongUnwrapException<T>(string message)
        => throw new WrongUnwrapException(message);
}