using JetBrains.Annotations;

namespace JFomit.Functional;

/// <summary>
/// Represents a type populated with only one value, called <see cref="Unit"/>.
/// </summary>
/// <seealso href="https://en.wikipedia.org/wiki/Unit_type"/>
[PublicAPI]
public readonly record struct UnitValue
{
    /// <summary>
    /// Provides a one and only instance of <see cref="UnitValue"/>.
    /// </summary>
    public static UnitValue Unit => default;

    /// <inheritdoc cref="object.Equals(object)"/>
    public bool Equals(UnitValue _) => true;
    /// <inheritdoc cref="object.GetHashCode"/>
    public override int GetHashCode() => 1916020601;
    /// <inheritdoc cref="object.ToString"/>
    public override string ToString() => nameof(Unit);
}