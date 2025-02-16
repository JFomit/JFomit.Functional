using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace JFomit.Functional.Monads;

/// <summary>
/// Wraps a value that might not exist.<br/>
/// <see cref="Option{T}"/> can be thought of as a discriminated union of two states:
/// <list type="bullet">
/// <item><see cref="Prelude.Some{T}"/>, containing a value, or</item>
/// <item><see cref="Prelude.None"/>, which is empty.</item>
/// </list>
///
/// </summary>
/// <seealso href="https://en.wikipedia.org/wiki/Option_type"/>
/// <typeparam name="T">The inner type.</typeparam>
[PublicAPI]
public readonly struct Option<T> : IEnumerable<T>, IComparable<Option<T>>, IEquatable<Option<T>>
{
    /// <summary>
    /// Weather this instance contains a valid value of type <typeparamref name="T"/>.
    /// </summary>
    /// <returns><see langword="true"/>, if <see cref="Option{T}"/> contains a valid value;
    /// <see langword="false"/>, otherwise.</returns>
    public bool IsSome { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }
    /// <summary>
    /// Weather this instance is empty (i.e. <see cref="Prelude.None"/>).
    /// </summary>
    /// <returns><see langword="true"/>, if <see cref="Option{T}"/> contains <see cref="Prelude.None"/>;
    /// <see langword="false"/>, otherwise.</returns>
    public bool IsNone => !IsSome;

    /// <summary>
    /// Extracts wrapped value.
    /// </summary>
    /// <exception cref="WrongUnwrapException">if called on <see cref="Prelude.None"/> variant.</exception>
    public T Value
    {
        // Pulling throw out increases chances of Option<T>.Value to be inlined
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => IsSome ? _value : ThrowHelper.ThrowWrongUnwrapException<T>("Tried to get the value from a 'None' variant of Option<T>.");
    }

    private readonly T _value;

    /// <exclude/>
    [Obsolete("Options can only be constructed with methods, such as Prelude.Some(T).", true)]
    public Option()
    {
        IsSome = false;
        _value = default!;
    }
    // For creating 'None' variant
    private Option(bool isSome)
    {
        Debug.Assert(!isSome, "Tried to create an Option<T> of some value with 'none' constructor.");

        IsSome = false;
        _value = default!;
    }
    // For creating 'Some' variant
    private Option(T value)
    {
        IsSome = true;
        _value = value;
    }
    /// <summary>
    /// Converts a given <see cref="SomeVariant{T}"/> to <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="some">The instance to convert.</param>
    /// <returns>An <see cref="Option{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Option<T>(SomeVariant<T> some) => new(some.Value);
    /// <summary>
    /// Converts a given <see cref="NoneVariant"/> to <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="_">The instance to convert.</param>
    /// <returns>An <see cref="Option{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Option<T>(NoneVariant _) => new(false);
    /// <summary>
    /// Converts a given <see cref="NoneVariant{T}"/> to <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="_">The instance to convert.</param>
    /// <returns>An <see cref="Option{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Option<T>(NoneVariant<T> _) => new(false);

    /// <summary>
    /// Returns a <see cref="string"/> representing current <see cref="Option{T}"/> instance.
    /// </summary>
    public override string ToString() => IsNone ? "None" : $"Some({Value})";

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
    public Enumerator GetEnumerator() => new(this);

    /// <inheritdoc cref="IComparable{T}.CompareTo"/>
    public int CompareTo(Option<T> other)
    {
        if (IsSome)
        {
            return other.TryUnwrap(out var value) ? Comparer<T>.Default.Compare(_value, value) : 1;
        }

        return other.IsSome ? -1 : 0;
    }

    /// <summary>
    /// Extracts value inside this <see cref="Option{T}"/> instance. If the <see cref="Option{T}"/>
    /// is <see cref="Prelude.None"/>, a <see cref="WrongUnwrapException"/> is thrown.
    /// </summary>
    /// <exception cref="WrongUnwrapException">if tried to unwrap <see cref="Prelude.None"/> variant.</exception>
    /// <returns>The inner value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Unwrap() => Value;
    /// <summary>
    /// Extracts value inside this <see cref="Option{T}"/> instance. If the <see cref="Option{T}"/>
    /// is <see cref="Prelude.None"/>, a <paramref name="other"/> is returned instead.
    /// </summary>
    /// <param name="other">The alternative value.</param>
    /// <returns>The inner value or <paramref name="other"/>.</returns>
    [Pure]
    public T UnwrapOr(T other) => IsSome ? _value : other;
    /// <summary>
    /// Extracts value inside this <see cref="Option{T}"/> instance. If the <see cref="Option{T}"/>
    /// is <see cref="Prelude.None"/>, result of calling <paramref name="other"/> is returned instead.
    /// The delegate is not invoked otherwise.
    /// </summary>
    /// <param name="other">The provider for the alternative value.</param>
    /// <returns>The inner value or result of the invocation of <paramref name="other"/>.</returns>
    public T UnwrapOrElse([InstantHandle] Func<T> other) =>
        TryUnwrap(out var value) ? value : other();
    /// <summary>
    /// Extracts value inside this <see cref="Option{T}"/> instance. If the <see cref="Option{T}"/>
    /// is <see cref="Prelude.None"/>, returns <see langword="default"/> for type <typeparamref name="T"/>.
    /// </summary>
    /// <returns>The inner value or <see langword="default"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T UnwrapOrDefault() => IsSome ? _value : default!;
    /// <summary>
    /// Tries to extract the value inside this <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="value">When this method returns, contains the inner value, if this <see cref="Option{T}"/>
    /// is <see cref="Prelude.Some{T}"/>;
    /// otherwise, the default value for the type of the <paramref name="value"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <returns><see langword="true"/>, if extracted the value from <see cref="Option{T}"/>; false, otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryUnwrap([NotNullWhen(true)] out T? value)
    {
        if (IsSome)
        {
            value = _value!;
            return true;
        }

        value = default;
        return false;
    }

    /// <summary>
    /// Extracts the inner value and, if failed, throws a <see cref="WrongUnwrapException"/> with user-defined message.
    /// </summary>
    /// <param name="message">The message of thrown exception.</param>
    /// <returns>The inner value.</returns>
    [Pure]
    public T Expect(string message)
        => IsSome ? _value : ThrowHelper.ThrowWrongUnwrapException<T>(message);

    /// <inheritdoc/>
    public bool Equals(Option<T> other)
        => IsSome
            ? other.TryUnwrap(out var value) &&
              EqualityComparer<T>.Default.Equals(_value, value)
            : other.IsNone;

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Option<T> other)
        {
            return Equals(other);
        }

        return false;
    }

    /// <inheritdoc cref="IEnumerator{T}"/>
    public struct Enumerator(Option<T> option) : IEnumerator<T>
    {
        /// <inheritdoc cref="IEnumerator{T}.Current"/>
        public readonly T Current => option.UnwrapOrDefault();

        readonly object IEnumerator.Current => Current!;

        private int _pos = option.IsSome ? -1 : 0;

        /// <inheritdoc cref="IEnumerator.MoveNext"/>
        public bool MoveNext() => ++_pos <= 0;
        /// <inheritdoc cref="IEnumerator.Reset"/>
        public void Reset() => _pos = option.IsSome ? -1 : 0;
        /// <inheritdoc />
        public readonly void Dispose() { }
    }
    /// <inheritdoc cref="object.GetHashCode()"/>
    public override int GetHashCode()
    {
#if NETSTANDARD2_0
        if (IsSome)
        {
            return 0;
        }
        unchecked // Overflow is fine, just wrap
        {
            const int hash = 17;
            var vHash = _value?.GetHashCode() ?? 0;
            return hash * 23 + vHash;
        }
#else
        return IsSome ? HashCode.Combine(_value) : HashCode.Combine(default(T));
#endif
    }

    /// <inheritdoc/>
    public static bool operator ==(Option<T> left, Option<T> right) => left.Equals(right);
    /// <inheritdoc/>
    public static bool operator !=(Option<T> left, Option<T> right) => !(left == right);

    /// <inheritdoc/>
    /// <seealso cref="CompareTo(Option{T})"/>
    public static bool operator <(Option<T> left, Option<T> right) => left.CompareTo(right) < 0;
    /// <inheritdoc/>
    /// <seealso cref="CompareTo(Option{T})"/>
    public static bool operator <=(Option<T> left, Option<T> right) => left.CompareTo(right) <= 0;
    /// <inheritdoc/>
    /// <seealso cref="CompareTo(Option{T})"/>
    public static bool operator >(Option<T> left, Option<T> right) => left.CompareTo(right) > 0;
    /// <inheritdoc/>
    /// <seealso cref="CompareTo(Option{T})"/>
    public static bool operator >=(Option<T> left, Option<T> right) => left.CompareTo(right) >= 0;
}

/// <summary>
/// <see cref="Prelude.Some{T}"/> variant.
/// </summary>
/// <param name="Value">Wrapped value.</param>
/// <typeparam name="T">The type of the wrapped value.</typeparam>
[PublicAPI]
public readonly record struct SomeVariant<T>(T Value)
{
    /// <inheritdoc cref="object.ToString"/>
    public override string ToString() => $"Some({Value})";
}

/// <summary>
/// <see cref="Prelude.None"/> variant.
/// </summary>
[PublicAPI]
public readonly record struct NoneVariant
{
    /// <inheritdoc cref="object.ToString"/>
    public override string ToString() => "None";
}
/// <summary>
/// Typed <see cref="Prelude.None"/> variant.
/// </summary>
/// <typeparam name="T">The `type' of <see cref="Prelude.None"/>.</typeparam>
[PublicAPI]
public readonly record struct NoneVariant<T>
{
    /// <summary>
    /// Converts an untyped <see cref="Prelude.None"/> to typed one.
    /// </summary>
    /// <param name="_">The value to convert.</param>
    /// <returns>A <see cref="NoneVariant{T}"/>.</returns>
    public static implicit operator NoneVariant<T>(NoneVariant _) => new();
    /// <inheritdoc cref="object.ToString"/>
    public override string ToString() => $"None<{typeof(T).Name}>";
}
/// <summary>
/// Double-typed <see cref="Prelude.None"/> variant.
/// </summary>
/// <typeparam name="T1">The first type.</typeparam>
/// <typeparam name="T2">The second type.</typeparam>
[PublicAPI]
public readonly record struct NoneVariant<T1, T2>
{
    /// <summary>
    /// Converts an untyped <see cref="Prelude.None"/> to a double-typed one.
    /// </summary>
    /// <param name="_">The value to convert.</param>
    /// <returns>A <see cref="NoneVariant{T1,T2}"/></returns>
    public static implicit operator NoneVariant<T1, T2>(NoneVariant _) => new();
    /// <inheritdoc cref="object.ToString()"/>
    public override string ToString() => $"None<{typeof(T1).Name}, {typeof(T2).Name}>";
}
