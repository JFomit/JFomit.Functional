using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JFomit.Functional.Monads;

namespace JFomit.Functional;

[DebuggerStepThrough]
internal static class ThrowHelper
{
    [DoesNotReturn]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static T ThrowWrongUnwrapException<T>(string message)
        => throw new WrongUnwrapException(message);

    [DoesNotReturn]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void ThrowWrongUnwrapException(string message)
        => throw new WrongUnwrapException(message);
}