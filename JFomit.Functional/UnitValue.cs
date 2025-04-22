using System.Collections;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace JFomit.Functional;

/// <summary>
/// Represents a type populated with only one value, called <see cref="Value"/>.
/// </summary>
/// <seealso href="https://en.wikipedia.org/wiki/Unit_type"/>
[PublicAPI]
public readonly record struct Unit() : IEnumerable
{
    /// <summary>
    /// Provides a one and only instance of <see cref="Functional.Unit"/>.
    /// </summary>
    public static Unit Value => default;

    /// <inheritdoc cref="object.Equals(object)"/>
    public bool Equals(Unit _) => true;
    /// <inheritdoc cref="object.GetHashCode"/>
    public override int GetHashCode() => 1916020601;
    /// <inheritdoc cref="object.ToString"/>
    public override string ToString() => nameof(Unit);

    /// <summary>
    /// Internal method used to support collection-initializier syntax. Does nothing.
    /// </summary>
    public void Add(Absurd _) { }
    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => new UnitEnumerator();
}

/// <summary>
/// Internal struct used for collection-initializer syntax.
/// </summary>
internal class UnitEnumerator : IEnumerator
{
    public Absurd Current => null!;
    object IEnumerator.Current => Current;
    public bool MoveNext() { return false; }
    public void Reset() { }
}