using System.Diagnostics;
using JetBrains.Annotations;

namespace JFomit.Functional.Monads;

/// <summary>
/// Marks all types, that present one from a set of supported variants.
/// </summary>
[PublicAPI]
public interface IOneOf;
/// <summary>
/// Marks a variant, that stores an item of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type.</typeparam>
[PublicAPI]
public interface IGenericVariant<out T>
{
    /// <summary>
    /// Gets stored value.
    /// </summary>
    /// <returns>A instance of <typeparamref name="T"/>.</returns>
    T GetValue();
}

/// <summary>
/// A `variant' of any <see cref="IOneOf"/>.
/// </summary>
/// <param name="Value">The stored value.</param>
/// <typeparam name="T">The type.</typeparam>
[PublicAPI]
public readonly record struct Variant<T>(T Value)
{
    /// <inheritdoc />
    public readonly override string ToString() => $"Variant({Value})";
}

/// <summary>
/// A discriminated union of 2 types.
/// </summary>
/// <typeparam name="T1">The first type.</typeparam>
/// <typeparam name="T2">The second type.</typeparam>
[PublicAPI]
public abstract partial record OneOf<T1, T2> : IOneOf
{
    private OneOf() { }

    /// <summary>
    /// A variant for <see cref="OneOf{T1,T2}"/>.
    /// </summary>
    /// <param name="Value">The stored value.</param>
    /// <typeparam name="T">The type.</typeparam>
    public sealed record GenericVariant<T>(T Value) : OneOf<T1, T2>, IGenericVariant<T>
    {
        /// <inheritdoc/>
        public T GetValue() => Value;
        /// <inheritdoc />
        public override string ToString() => $"Variant({GetValue()})";

        /// <summary>
        /// Constructs a <see cref="OneOf{T1,T2}.GenericVariant{T}"/> from a <see cref="Variant{T}"/>.
        /// </summary>
        /// <param name="variant">The <see cref="Variant{T}"/> to convert.</param>
        /// <returns>A <see cref="OneOf{T1,T2}.GenericVariant{T}"/>.</returns>
        public static implicit operator GenericVariant<T>(Variant<T> variant) => new(variant.Value);
    }

    /// <summary>
    /// Constructs a <see cref="OneOf{T1,T2}"/> from a <see cref="Variant{T}"/>.
    /// </summary>
    /// <param name="t1">A <see cref="Variant{T}"/>.</param>
    /// <returns>A <see cref="OneOf{T1,T2}"/>.</returns>
    public static implicit operator OneOf<T1, T2>(Variant<T1> t1) => (GenericVariant<T1>)t1;
    /// <summary>
    /// Constructs a <see cref="OneOf{T1,T2}"/> from a <see cref="Variant{T}"/>.
    /// </summary>
    /// <param name="t2">A <see cref="Variant{T}"/>.</param>
    /// <returns>A <see cref="OneOf{T1,T2}"/>.</returns>
    public static implicit operator OneOf<T1, T2>(Variant<T2> t2) => (GenericVariant<T2>)t2;

    /// <inheritdoc />
    public override string ToString() => this switch
    {
        GenericVariant<T1> v1 => v1.ToString(),
        GenericVariant<T2> v2 => v2.ToString(),

        _ => throw new UnreachableException()
    };

    /// <summary>
    /// Extends this <see cref="OneOf{T1,T2}"/> with another type.
    /// </summary>
    /// <typeparam name="T3">Another type.</typeparam>
    /// <returns>A <see cref="OneOf{T1,T2,T3}"/>, storing original value.</returns>
    public OneOf<T1, T2, T3> ExtendWith<T3>() => this switch
    {
        GenericVariant<T1> variant => Prelude.Variant(variant.GetValue()),
        GenericVariant<T2> variant => Prelude.Variant(variant.GetValue()),

        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Extends this <see cref="OneOf{T1,T2}"/> with 2 another types.
    /// </summary>
    /// <typeparam name="T3">First additional type.</typeparam>
    /// <typeparam name="T4">Second additional type.</typeparam>
    /// <returns>A <see cref="OneOf{T1,T2,T3,T4}"/>, storing original value.</returns>
    public OneOf<T1, T2, T3, T4> ExtendWith<T3, T4>() => this switch
    {
        GenericVariant<T1> variant => Prelude.Variant(variant.GetValue()),
        GenericVariant<T2> variant => Prelude.Variant(variant.GetValue()),

        _ => throw new UnreachableException()
    };
}

/// <summary>
/// A discriminated union of 3 types.
/// </summary>
/// <typeparam name="T1">The first type.</typeparam>
/// <typeparam name="T2">The second type.</typeparam>
/// <typeparam name="T3">The third type.</typeparam>
[PublicAPI]
public abstract partial record OneOf<T1, T2, T3> : IOneOf
{
    private OneOf() { }

    /// <summary>
    /// A variant for <see cref="OneOf{T1,T2,T3}"/>.
    /// </summary>
    /// <param name="Value">The stored value.</param>
    /// <typeparam name="T">The type.</typeparam>
    public sealed record GenericVariant<T>(T Value) : OneOf<T1, T2, T3>, IGenericVariant<T>
    {
        /// <inheritdoc/>
        public T GetValue() => Value;
        /// <inheritdoc />
        public override string ToString() => $"Variant({GetValue()})";

        /// <summary>
        /// Constructs a <see cref="OneOf{T1,T2,T3}.GenericVariant{T}"/> from a <see cref="Variant{T}"/>.
        /// </summary>
        /// <param name="variant">The <see cref="Variant{T}"/> to convert.</param>
        /// <returns>A <see cref="OneOf{T1,T2,T3}.GenericVariant{T}"/>.</returns>
        public static implicit operator GenericVariant<T>(Variant<T> variant) => new(variant.Value);
    }

    /// <summary>
    /// Constructs a <see cref="OneOf{T1,T2,T3}"/> from a <see cref="Variant{T}"/>.
    /// </summary>
    /// <param name="t1">A <see cref="Variant{T}"/>.</param>
    /// <returns>A <see cref="OneOf{T1,T2,T3}"/>.</returns>
    public static implicit operator OneOf<T1, T2, T3>(Variant<T1> t1) => (GenericVariant<T1>)t1;
    /// <summary>
    /// Constructs a <see cref="OneOf{T1,T2,T3}"/> from a <see cref="Variant{T}"/>.
    /// </summary>
    /// <param name="t2">A <see cref="Variant{T}"/>.</param>
    /// <returns>A <see cref="OneOf{T1,T2,T3}"/>.</returns>
    public static implicit operator OneOf<T1, T2, T3>(Variant<T2> t2) => (GenericVariant<T2>)t2;
    /// <summary>
    /// Constructs a <see cref="OneOf{T1,T2,T3}"/> from a <see cref="Variant{T}"/>.
    /// </summary>
    /// <param name="t3">A <see cref="Variant{T}"/>.</param>
    /// <returns>A <see cref="OneOf{T1,T2,T3}"/>.</returns>
    public static implicit operator OneOf<T1, T2, T3>(Variant<T3> t3) => (GenericVariant<T3>)t3;

    /// <inheritdoc />
    public override string ToString() => this switch
    {
        GenericVariant<T1> v1 => v1.ToString(),
        GenericVariant<T2> v2 => v2.ToString(),
        GenericVariant<T3> v3 => v3.ToString(),

        _ => throw new UnreachableException()
    };

    /// <summary>
    /// Extends this <see cref="OneOf{T1,T2,T3}"/> with another type.
    /// </summary>
    /// <typeparam name="T4">Another type.</typeparam>
    /// <returns>A <see cref="OneOf{T1,T2,T3,T4}"/>, storing original value.</returns>
    public OneOf<T1, T2, T3, T4> ExtendWith<T4>() => this switch
    {
        GenericVariant<T1> variant => Prelude.Variant(variant.GetValue()),
        GenericVariant<T2> variant => Prelude.Variant(variant.GetValue()),
        GenericVariant<T3> variant => Prelude.Variant(variant.GetValue()),

        _ => throw new UnreachableException()
    };
}
/// <summary>
/// A discriminated union of 4 types.
/// </summary>
/// <typeparam name="T1">The first type.</typeparam>
/// <typeparam name="T2">The second type.</typeparam>
/// <typeparam name="T3">The third type.</typeparam>
/// <typeparam name="T4">The third type.</typeparam>
[PublicAPI]
public abstract partial record OneOf<T1, T2, T3, T4> : IOneOf
{
    private OneOf() { }

    /// <summary>
    /// A variant for <see cref="OneOf{T1,T2,T3,T4}"/>.
    /// </summary>
    /// <param name="Value">The stored value.</param>
    /// <typeparam name="T">The type.</typeparam>
    public sealed record GenericVariant<T>(T Value) : OneOf<T1, T2, T3, T4>, IGenericVariant<T>
    {
        /// <inheritdoc/>
        public T GetValue() => Value;
        /// <inheritdoc />
        public override string ToString() => $"Variant({GetValue()})";

        /// <summary>
        /// Constructs a <see cref="OneOf{T1,T2,T3,T4}.GenericVariant{T}"/> from a <see cref="Variant{T}"/>.
        /// </summary>
        /// <param name="variant">The <see cref="Variant{T}"/> to convert.</param>
        /// <returns>A <see cref="OneOf{T1,T2,T3,T4}.GenericVariant{T}"/>.</returns>
        public static implicit operator GenericVariant<T>(Variant<T> variant) => new(variant.Value);
    }

    /// <summary>
    /// Constructs a <see cref="OneOf{T1,T2,T3,T4}"/> from a <see cref="Variant{T}"/>.
    /// </summary>
    /// <param name="t1">A <see cref="Variant{T}"/>.</param>
    /// <returns>A <see cref="OneOf{T1,T2,T3,T4}"/>.</returns>
    public static implicit operator OneOf<T1, T2, T3, T4>(Variant<T1> t1) => (GenericVariant<T1>)t1;
    /// <summary>
    /// Constructs a <see cref="OneOf{T1,T2,T3,T4}"/> from a <see cref="Variant{T}"/>.
    /// </summary>
    /// <param name="t2">A <see cref="Variant{T}"/>.</param>
    /// <returns>A <see cref="OneOf{T1,T2,T3,T4}"/>.</returns>
    public static implicit operator OneOf<T1, T2, T3, T4>(Variant<T2> t2) => (GenericVariant<T2>)t2;
    /// <summary>
    /// Constructs a <see cref="OneOf{T1,T2,T3,T4}"/> from a <see cref="Variant{T}"/>.
    /// </summary>
    /// <param name="t3">A <see cref="Variant{T}"/>.</param>
    /// <returns>A <see cref="OneOf{T1,T2,T3,T4}"/>.</returns>
    public static implicit operator OneOf<T1, T2, T3, T4>(Variant<T3> t3) => (GenericVariant<T3>)t3;
    /// <summary>
    /// Constructs a <see cref="OneOf{T1,T2,T3,T4}"/> from a <see cref="Variant{T}"/>.
    /// </summary>
    /// <param name="t4">A <see cref="Variant{T}"/>.</param>
    /// <returns>A <see cref="OneOf{T1,T2,T3,T4}"/>.</returns>
    public static implicit operator OneOf<T1, T2, T3, T4>(Variant<T4> t4) => (GenericVariant<T4>)t4;

    /// <inheritdoc />
    public override string ToString() => this switch
    {
        GenericVariant<T1> v1 => v1.ToString(),
        GenericVariant<T2> v2 => v2.ToString(),
        GenericVariant<T3> v3 => v3.ToString(),
        GenericVariant<T4> v4 => v4.ToString(),

        _ => throw new UnreachableException()
    };
}