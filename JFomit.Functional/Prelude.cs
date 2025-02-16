using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using JFomit.Functional.Monads;

namespace JFomit.Functional;

/// <summary>
/// A class with common static methods exposed in a convenient manner.
/// <example>
/// <code>
/// using static Prelude;
/// Option&lt;int&gt; intOption = Some(42);
/// </code>
/// </example>
/// </summary>
[PublicAPI]
public static class Prelude
{
    /// <inheritdoc cref="UnitValue.Unit"/>
    public static UnitValue Unit => default;

    /// <summary>
    /// Wraps a value into an <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>An <see cref="Option{T}"/>.</returns>
    [Pure]
    public static Option<T> Some<T>([DisallowNull] T value)
    {
        Debug.Assert(value is not null);
        return new SomeVariant<T>(value);
    }

    /// <summary>
    /// Wraps a <see cref="Unit"/> into an <see cref="Option{T}"/>.
    /// </summary>
    /// <returns>An <see cref="Option{T}"/>.</returns>
    [Pure]
    public static Option<UnitValue> Some() => new SomeVariant<UnitValue>(Unit);

    /// <summary>
    /// Returns a None variant of <see cref="Option{T}"/>.
    /// </summary>
    public static NoneVariant None => new();

#if NETSTANDARD2_0
#else
    /// <summary>
    /// Tries to parse an instance of <typeparamref name="T"/> from <paramref name="s"/> according to
    /// <paramref name="provider"/>.
    /// </summary>
    /// <param name="s">The <see cref="String"/> to parse.</param>
    /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns><see cref="Some{T}"/> if successful; <see cref="None"/>, otherwise.</returns>
    [Pure]
    public static Option<T> Parse<T>(string? s, IFormatProvider? provider = null)
        where T : IParsable<T> => T.TryParse(s, provider, out var value) ? Some(value) : None;
#endif

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="dict">The dictionary.</param>
    /// <param name="key">The key. Must be not null.</param>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <returns><see cref="Some{T}"/> if value was successfully found; otherwise, <see cref="None"/>.</returns>
    [Pure]
    public static Option<TValue?> GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict,
        TKey key) =>
        (dict.TryGetValue(key, out var value)
            ? Some(value!)
            : None)!;

    /// <summary>
    /// Wraps a given <paramref name="value"/> into an <see cref="OkVariant{TSuccess}"/> of
    /// <see cref="Result{TSuccess,TError}"/>.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>An instance of <see cref="OkVariant{TSuccess}"/>, with unspecified error type.</returns>
    [Pure]
    public static OkVariant<T> Ok<T>(T value) => new(value);
    /// <summary>
    /// Wraps a <see cref="Unit"/> into an <see cref="OkVariant{TSuccess}"/> of
    /// <see cref="Result{TSuccess,TError}"/>.
    /// </summary>
    /// <returns>An instance of <see cref="OkVariant{TSuccess}"/>, with unspecified error type.</returns>
    [Pure]
    public static OkVariant<UnitValue> Ok() => new(Unit);

    // ReSharper disable InconsistentNaming
    /// <summary>
    /// Wraps a given error object into a <see cref="FailVariant{TError}"/> of <see cref="Result{TSuccess,TError}"/>.
    /// </summary>
    /// <param name="error">The error value.</param>
    /// <typeparam name="E">The type.</typeparam>
    /// <returns>An instance of <see cref="FailVariant{TError}"/>, with unspecified success type.</returns>
    [Pure]
    public static FailVariant<E> Error<E>(E error) => new(error);

    /// <summary>
    /// Wraps a <see cref="Unit"/> into a <see cref="FailVariant{TError}"/> of <see cref="Result{TSuccess,TError}"/>.
    /// </summary>
    /// <returns>An instance of <see cref="FailVariant{TError}"/> with unspecified success type.</returns>
    [Pure]
    public static FailVariant<UnitValue> Error() => new(Unit);
    // ReSharper restore InconsistentNaming

    /// <summary>
    /// Invokes a passed delegate. If in the process of invocation an exception is thrown
    /// it is caught and returned in <see cref="Error{E}"/> variant.
    /// </summary>
    /// <param name="func">The delegate to invoke.</param>
    /// <typeparam name="T">The <see cref="Ok{T}"/> type.</typeparam>
    /// <returns>A <see cref="Result{TSuccess,TError}"/>, which contains a value returned by <paramref name="func"/>
    /// or thrown <see cref="Exception"/>.
    /// </returns>
    public static Result<T, Exception> Catch<T>([InstantHandle] Func<T> func)
    {
        try
        {
            return Ok(func());
        }
        catch (Exception e)
        {
            return Error(e);
        }
    }
    /// <summary>
    /// Invokes a passed delegate. If in the process of invocation an exception is thrown
    /// it is caught and returned in <see cref="Error{E}"/> variant.
    /// </summary>
    /// <param name="func">The delegate to invoke.</param>
    /// <returns>A <see cref="Result{TSuccess,TError}"/>, which contains a <see cref="Ok"/>
    /// or thrown <see cref="Exception"/>.
    /// </returns>
    public static Result<UnitValue, Exception> Catch([InstantHandle] Action func)
    {
        try
        {
            func();
            return Ok();
        }
        catch (Exception e)
        {
            return Error(e);
        }
    }

    /// <summary>
    /// Wraps a value into a <see cref="Monads.Variant{T}"/>.
    /// </summary>
    /// <returns>A <see cref="Monads.Variant{T}"/>. </returns>
    public static Variant<T> Variant<T>(T value) => new(value);

    /// <summary>
    /// Flattens a passed tuple.
    /// </summary>
    /// <param name="tuple">The tuple to flatten.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <returns>A flattened <see cref="ValueTuple{T1,T2,T3}"/>.</returns>
    [Pure]
    public static (T1, T2, T3) Flatten<T1, T2, T3>(this ((T1, T2), T3) tuple) =>
        (tuple.Item1.Item1, tuple.Item1.Item2, tuple.Item2);
    /// <summary>
    /// Flattens a passed tuple.
    /// </summary>
    /// <param name="tuple">The tuple to flatten.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <returns>A flattened <see cref="ValueTuple{T1,T2,T3}"/>.</returns>
    [Pure]
    public static (T1, T2, T3) Flatten<T1, T2, T3>(this (T1, (T2, T3)) tuple) =>
        (tuple.Item1, tuple.Item2.Item1, tuple.Item2.Item2);

    /// <summary>
    /// Flattens a passed tuple.
    /// </summary>
    /// <param name="tuple">The tuple to flatten.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <typeparam name="T4">The fourth type.</typeparam>
    /// <returns>A flattened <see cref="ValueTuple{T1,T2,T3,T4}"/>.</returns>
    [Pure]
    public static (T1, T2, T3, T4) Flatten<T1, T2, T3, T4>(this ((T1, T2), T3, T4) tuple) =>
        (tuple.Item1.Item1, tuple.Item1.Item2, tuple.Item2, tuple.Item3);
    /// <summary>
    /// Flattens a passed tuple.
    /// </summary>
    /// <param name="tuple">The tuple to flatten.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <typeparam name="T4">The fourth type.</typeparam>
    /// <returns>A flattened <see cref="ValueTuple{T1,T2,T3,T4}"/>.</returns>
    [Pure]
    public static (T1, T2, T3, T4) Flatten<T1, T2, T3, T4>(this (T1, (T2, T3), T4) tuple) =>
        (tuple.Item1, tuple.Item2.Item1, tuple.Item2.Item2, tuple.Item3);
    /// <summary>
    /// Flattens a passed tuple.
    /// </summary>
    /// <param name="tuple">The tuple to flatten.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <typeparam name="T4">The fourth type.</typeparam>
    /// <returns>A flattened <see cref="ValueTuple{T1,T2,T3,T4}"/>.</returns>
    [Pure]
    public static (T1, T2, T3, T4) Flatten<T1, T2, T3, T4>(this (T1, T2, (T3, T4)) tuple) =>
        (tuple.Item1, tuple.Item2, tuple.Item3.Item1, tuple.Item3.Item2);
    /// <summary>
    /// Flattens a passed tuple.
    /// </summary>
    /// <param name="tuple">The tuple to flatten.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <typeparam name="T4">The fourth type.</typeparam>
    /// <returns>A flattened <see cref="ValueTuple{T1,T2,T3,T4}"/>.</returns>
    [Pure]
    public static (T1, T2, T3, T4) Flatten<T1, T2, T3, T4>(this ((T1, T2, T3), T4) tuple) =>
        (tuple.Item1.Item1, tuple.Item1.Item2, tuple.Item1.Item3, tuple.Item2);
    /// <summary>
    /// Flattens a passed tuple.
    /// </summary>
    /// <param name="tuple">The tuple to flatten.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <typeparam name="T4">The fourth type.</typeparam>
    /// <returns>A flattened <see cref="ValueTuple{T1,T2,T3,T4}"/>.</returns>
    [Pure]
    public static (T1, T2, T3, T4) Flatten<T1, T2, T3, T4>(this (T1, (T2, T3, T4)) tuple) =>
        (tuple.Item1, tuple.Item2.Item1, tuple.Item2.Item2, tuple.Item2.Item3);
}
