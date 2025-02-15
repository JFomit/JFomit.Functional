#if NETSTANDARD2_0
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics;

/// <summary>
/// Exception thrown when the program executes an instruction that was thought to be unreachable.
/// </summary>
public sealed class UnreachableException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnreachableException"/> class with the default error message.
    /// </summary>
    public UnreachableException()
        : base("Executed code that was thought to be unreachable.")
    { }
}
#endif