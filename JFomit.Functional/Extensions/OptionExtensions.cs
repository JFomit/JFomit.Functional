using JetBrains.Annotations;
using JFomit.Functional.Monads;

namespace JFomit.Functional.Extensions;

/// <summary>
/// Contains common operations on the <see cref="Option{T}"/>.
/// </summary>
[PublicAPI]
public static class OptionExtensions
{
    /// <summary>
    /// Flattens a nested <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="option">The nested <see cref="Option{T}"/>.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>An <see cref="Option{T}"/> with one less level of nesting.</returns>
    [Pure]
    public static Option<T> Flatten<T>(this Option<Option<T>> option)
        => option.IsNone ? Prelude.None : option.Value;
    /// <summary>
    /// Flattens an <see cref="Option{T}"/> of nullable struct.
    /// <see langword="null"/> becomes <see cref="Prelude.None"/>, <typeparamref name="T"/> becomes <see cref="Prelude.Some{T}"/>.
    /// </summary>
    /// <param name="option">The nested <see cref="Option{T}"/>.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>An <see cref="Option{T}"/> of <typeparamref name="T"/>.</returns>
    [Pure]
    public static Option<T> Flatten<T>(this Option<T?> option)
        where T : struct => option.IsNone ? Prelude.None : option.Value.ToOption();
    /// <summary>
    /// Flattens a nullable <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="option">The <see cref="Nullable{T}"/> of option.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>An <see cref="Option{T}"/> of <typeparamref name="T"/>.</returns>
    [Pure]
    public static Option<T> Flatten<T>(this Option<T>? option)
        where T : struct => option ?? Prelude.None;

    /// <summary>
    /// Projects an element wrapped in <see cref="Option{T}"/> to another <see cref="Option{T}"/>
    /// and flattens the result. If <paramref name="option"/> is <see cref="Prelude.None"/>, a <see cref="Prelude.None"/> is returned instead.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/> to bind.</param>
    /// <param name="func">The projecting function.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <returns>An <see cref="Option{T}"/> - result of invoking <paramref name="func"/> on the
    /// value inside <paramref name="option"/> or <see cref="Prelude.None"/>, if the value was <see cref="Prelude.None"/>.</returns>
    public static Option<TResult> SelectMany<T, TResult>(
        this Option<T> option,
        [InstantHandle] Func<T, Option<TResult>> func)
        => option.IsNone ? Prelude.None : func(option.Value);
    /// <summary>
    /// Projects an element wrapped in <see cref="Option{T}"/> together with arbitrary context to another <see cref="Option{T}"/>
    /// and flattens the result. If <paramref name="option"/> is <see cref="Prelude.None"/>, a <see cref="Prelude.None"/> is returned instead.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/> to bind.</param>
    /// <param name="context">The additional context passed.</param>
    /// <param name="func">The projecting function.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    /// <returns>An <see cref="Option{T}"/> - result of invoking <paramref name="func"/> on the
    /// value inside <paramref name="option"/> or <see cref="Prelude.None"/>, if the value was <see cref="Prelude.None"/>.</returns>
    public static Option<TResult> SelectMany<T, TResult, TContext>(
        this Option<T> option,
        TContext context,
        [InstantHandle] Func<T, TContext, Option<TResult>> func)
        => option.IsNone ? Prelude.None : func(option.Value, context);

    /// <summary>
    /// Projects an element wrapped in <see cref="Option{T}"/> to a new form and
    /// wraps it into another <see cref="Option{T}"/>.
    /// If <paramref name="option"/> is <see cref="Prelude.None"/>, a <see cref="Prelude.None"/> is returned instead.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/> to map.</param>
    /// <param name="selector">The projecting function.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <returns>An <see cref="Option{T}"/> - result of invoking <paramref name="selector"/> on the
    /// value inside <paramref name="option"/> wrapped afterward or <see cref="Prelude.None"/>, if the value was <see cref="Prelude.None"/>.</returns>
    public static Option<TResult> Select<T, TResult>(
        this Option<T> option,
        [InstantHandle] Func<T, TResult> selector)
        => option.TryUnwrap(out var value) ? Prelude.Some(selector(value)!) : Prelude.None;
    /// <summary>
    /// Projects an element wrapped in <see cref="Option{T}"/> together with arbitrary context
    /// to a new form and wraps it into another <see cref="Option{T}"/>.
    /// If <paramref name="option"/> is <see cref="Prelude.None"/>, a <see cref="Prelude.None"/> is returned instead.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/> to map.</param>
    /// <param name="context">The additional context passed.</param>
    /// <param name="selector">The projecting function.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <returns>An <see cref="Option{T}"/> - result of invoking <paramref name="selector"/> on the
    /// value inside <paramref name="option"/> wrapped afterward or <see cref="Prelude.None"/>, if the value was <see cref="Prelude.None"/>.</returns>
    public static Option<TResult> Select<T, TContext, TResult>(
        this Option<T> option,
        TContext context,
        [InstantHandle] Func<T, TContext, TResult> selector)
        => option.TryUnwrap(out var value) ? Prelude.Some(selector(value, context)!) : Prelude.None;

    /// <summary>
    /// Filters the value wrapped in <see cref="Option{T}"/> according to the <paramref name="predicate"/>.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/> to filter.</param>
    /// <param name="predicate">The predicate function.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns><see cref="Prelude.Some{T}"/>, if <see cref="Option{T}"/> contains value matched
    /// by <paramref name="predicate"/>; <see cref="Prelude.None"/> otherwise.</returns>
    public static Option<T> Where<T>(
        this Option<T> option,
        [InstantHandle] Func<T, bool> predicate)
        => option.TryUnwrap(out var value) && predicate(value) ? Prelude.Some(value) : Prelude.None;
    /// <summary>
    /// Filters the value wrapped in <see cref="Option{T}"/> according to the <paramref name="predicate"/>
    /// and some arbitrary context.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/> to filter.</param>
    /// <param name="context">The context to pass to the <paramref name="predicate"/>.</param>
    /// <param name="predicate">The predicate function.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    /// <returns><see cref="Prelude.Some{T}"/>, if <see cref="Option{T}"/> contains value matched
    /// by <paramref name="predicate"/>; <see cref="Prelude.None"/> otherwise.</returns>
    public static Option<T> Where<T, TContext>(
        this Option<T> option,
        TContext context,
        [InstantHandle] Func<T, TContext, bool> predicate)
        => option.TryUnwrap(out var value) && predicate(value, context) ? Prelude.Some(value) : Prelude.None;

    /// <summary>
    /// Checks weather <see cref="Option{T}"/> contains a valid value.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/>.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns><see langword="true"/> if <paramref name="option"/> contains a value; otherwise,
    /// <see langword="false"/>.</returns>
    [Pure]
    public static bool Any<T>(this Option<T> option) => option.IsSome;
    /// <summary>
    /// Checks weather <see cref="Option{T}"/> contains a value matched by <paramref name="predicate"/>.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/>.</param>
    /// <param name="predicate">The predicate.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns><see langword="true"/> if <paramref name="option"/> contains a value, matched by
    /// <paramref name="predicate"/>; otherwise, <see langword="false"/>.</returns>
    public static bool Any<T>(
        this Option<T> option,
        [InstantHandle] Func<T, bool> predicate)
        => option.TryUnwrap(out var value) && predicate(value);
    /// <summary>
    /// Checks weather <see cref="Option{T}"/> contains a value matched by <paramref name="predicate"/>
    /// with some arbitrary context.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/>.</param>
    /// <param name="context">The context.</param>
    /// <param name="predicate">The predicate.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    /// <returns><see langword="true"/> if <paramref name="option"/> contains a value, matched by
    /// <paramref name="predicate"/>; otherwise, <see langword="false"/>.</returns>
    public static bool Any<T, TContext>(
        this Option<T> option,
        TContext context,
        [InstantHandle] Func<T, TContext, bool> predicate)
        => option.TryUnwrap(out var value) && predicate(value, context);

    /// <inheritdoc cref="Any{T}(JFomit.Functional.Monads.Option{T}, Func{T,bool})"/>
    public static bool All<T>(this Option<T> option, [InstantHandle] Func<T, bool> predicate)
        => Any(option, predicate);
    /// <inheritdoc cref="Any{T,TContext}"/>
    public static bool All<T, TContext>(
        this Option<T> option,
        TContext context,
        [InstantHandle] Func<T, TContext, bool> predicate)
        => Any(option, context, predicate);

    /// <inheritdoc cref="Option{T}.Unwrap"/>
    [Pure]
    public static T Single<T>(this Option<T> option) => option.Unwrap();
    /// <inheritdoc cref="Option{T}.UnwrapOrDefault"/>
    public static T SingleOrDefault<T>(this Option<T> option, T other) => option.UnwrapOr(other);
    /// <inheritdoc cref="Option{T}.UnwrapOrElse"/>
    public static T SingleOrDefault<T>(this Option<T> option, [InstantHandle] Func<T> other)
        => option.UnwrapOrElse(other);

    /// <summary>
    /// Executes an <see cref="Action{T}"/> if <see cref="Option{T}"/> contains a value.
    /// </summary>
    /// <param name="option">The option.</param>
    /// <param name="action">The action.</param>
    /// <typeparam name="T">The type.</typeparam>
    public static void IfSome<T>(this Option<T> option, [InstantHandle] Action<T> action)
    {
        if (option.IsSome)
        {
            action(option.Value);
        }
    }

    /// <summary>
    /// Executes an <see cref="Action{T,TContext}"/> if <see cref="Option{T}"/> contains a value,
    /// with some arbitrary context.
    /// </summary>
    /// <param name="option">The option.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="action">The action.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    public static void IfSome<T, TContext>(this Option<T> option, TContext context, [InstantHandle] Action<T, TContext> action)
    {
        if (option.IsSome)
        {
            action(option.Value, context);
        }
    }

    /// <summary>
    /// Executes an <see cref="Action{T}"/> if <see cref="Option{T}"/> is <see cref="Prelude.None"/>.
    /// </summary>
    /// <param name="option">The option.</param>
    /// <param name="action">The action.</param>
    /// <typeparam name="T">The type.</typeparam>
    public static void IfNone<T>(this Option<T> option, [InstantHandle] Action action)
    {
        if (option.IsNone)
        {
            action();
        }
    }

    /// <summary>
    /// Executes an <see cref="Action{T,TContext}"/> if <see cref="Option{T}"/> is <see cref="Prelude.None"/>,
    /// with some arbitrary context.
    /// </summary>
    /// <param name="option">The option.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="action">The action.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    public static void IfNone<T, TContext>(this Option<T> option, TContext context, [InstantHandle] Action<TContext> action)
    {
        if (option.IsNone)
        {
            action(context);
        }
    }

    /// <summary>
    /// Matches the given <see cref="Option{T}"/> and invokes a matching variant delegate.
    /// </summary>
    /// <param name="option">The option to match on.</param>
    /// <param name="ok">The <see cref="Func{T,TResult}"/> that runs when <paramref name="option"/> is <see cref="Prelude.Some{T}"/>.</param>
    /// <param name="err">The <see cref="Func{TResult}"/> that runs when <paramref name="option"/> is <see cref="Prelude.None"/>.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <returns>An object of <typeparamref name="TResult"/> - result of invoking either <paramref name="ok"/>
    /// or <paramref name="err"/>.</returns>
    public static TResult Match<T, TResult>(this Option<T> option,
        [InstantHandle] Func<T, TResult> ok,
        [InstantHandle] Func<TResult> err) =>
        option.IsNone
            ? err()
            : ok(option.Value);

    /// <summary>
    /// Matches the given <see cref="Option{T}"/> and invokes a matching variant delegate,
    /// with some arbitrary context.
    /// </summary>
    /// <param name="option">The option to match on.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="ok">The <see cref="Func{T,TContext,TResult}"/> that runs when <paramref name="option"/> is <see cref="Prelude.Some{T}"/>.</param>
    /// <param name="err">The <see cref="Func{TContext,TResult}"/> that runs when <paramref name="option"/> is <see cref="Prelude.None"/>.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    /// <returns>An object of <typeparamref name="TResult"/> - result of invoking either <paramref name="ok"/>
    /// or <paramref name="err"/>.</returns>
    public static TResult Match<T, TResult, TContext>(this Option<T> option,
        TContext context,
        [InstantHandle] Func<T, TContext, TResult> ok,
        [InstantHandle] Func<TContext, TResult> err) =>
        option.IsNone
            ? err(context)
            : ok(option.Value,
                context);

    /// <summary>
    /// Switches on the given <see cref="Option{T}"/> and invokes a matching variant action.
    /// </summary>
    /// <param name="option">The option to switch on.</param>
    /// <param name="ok">The <see cref="Action{T,TResult}"/> that runs when <paramref name="option"/> is <see cref="Prelude.Some{T}"/>.</param>
    /// <param name="err">The <see cref="Action{TResult}"/> that runs when <paramref name="option"/> is <see cref="Prelude.None"/>.</param>
    /// <typeparam name="T">The type.</typeparam>
    public static void Switch<T>(
        this Option<T> option,
        [InstantHandle] Action<T> ok,
        [InstantHandle] Action err)
    {
        if (option.IsNone)
        {
            err();
        }
        else
        {
            ok(option.Value);
        }
    }
    /// <summary>
    /// Switches on the given <see cref="Option{T}"/> and invokes a matching variant action,
    /// with some arbitrary context.
    /// </summary>
    /// <param name="option">The option to switch on.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="ok">The <see cref="Action{T,TContext,TResult}"/> that runs when <paramref name="option"/> is <see cref="Prelude.Some{T}"/>.</param>
    /// <param name="err">The <see cref="Action{TContext,TResult}"/> that runs when <paramref name="option"/> is <see cref="Prelude.None"/>.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    public static void Switch<T, TContext>(
        this Option<T> option,
        TContext context,
        [InstantHandle] Action<T, TContext> ok,
        [InstantHandle] Action<TContext> err)
    {
        if (option.IsNone)
        {
            err(context);
        }
        else
        {
            ok(option.Value, context);
        }
    }

    /// <summary>
    /// Converts a given <see cref="Option{T}"/> to a <see cref="Result{TSuccess,TError}"/>,
    /// with supplied object of <typeparamref name="E"/>.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
    /// <param name="error">The error object.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <typeparam name="E">The error type.</typeparam>
    /// <returns>Ok(<typeparamref name="T"/>)
    /// if <paramref name="option"/> is <see cref="Prelude.Some{T}"/>; Error(<typeparamref name="E"/>) otherwise.</returns>
    [Pure]
    // ReSharper disable once InconsistentNaming
    public static Result<T, E> ToResult<T, E>(this Option<T> option, E error)
        => option.TryUnwrap(out var value) ? Prelude.Ok(value) : Prelude.Error(error);

    /// <summary>
    /// Joins two <see cref="Option{T}"/> instances into one according to a <paramref name="selector"/> function.
    /// </summary>
    /// <param name="option">The first <see cref="Option{T}"/>.</param>
    /// <param name="other">The second <see cref="Option{T}"/>.</param>
    /// <param name="selector">The joining function.</param>
    /// <typeparam name="T1">The type of <paramref name="option"/>.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="other"/>.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <returns>An <see cref="Option{T}"/> of <typeparamref name="TResult"/> - the result of applying
    /// <paramref name="selector"/> onto values from <paramref name="option"/> and <paramref name="other"/>.
    /// If either of <paramref name="option"/> or <paramref name="other"/> where <see cref="Prelude.None"/>, this method also
    /// returns <see cref="Prelude.None"/>.</returns>
    public static Option<TResult> Zip<T1, T2, TResult>(this Option<T1> option, Option<T2> other,
        [InstantHandle] Func<T1, T2, TResult> selector)
        => option.TryUnwrap(out var v1) && other.TryUnwrap(out var v2) ? Prelude.Some(selector(v1, v2)!) : Prelude.None;
    /// <summary>
    /// Joins two <see cref="Option{T}"/> instances into one, packing values into a <see cref="ValueTuple{T1,T2}"/>.
    /// </summary>
    /// <param name="option">The first <see cref="Option{T}"/>.</param>
    /// <param name="other">The second <see cref="Option{T}"/>.</param>
    /// <typeparam name="T1">The type of <paramref name="option"/>.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="other"/>.</typeparam>
    /// <returns>An <see cref="Option{T}"/> of <see cref="ValueTuple{T1,T2}"/>.
    /// If either of <paramref name="option"/> or <paramref name="other"/> where <see cref="Prelude.None"/>, this method also
    /// returns <see cref="Prelude.None"/>.</returns>
    [Pure]
    public static Option<(T1, T2)> Zip<T1, T2>(this Option<T1> option, Option<T2> other)
        => option.TryUnwrap(out var v1) && other.TryUnwrap(out var v2) ? Prelude.Some((v1, v2)) : Prelude.None;

    /// <summary>
    /// Converts a given <see cref="Option{T}"/> into an instance of <see cref="Nullable{T}"/>.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
    /// <typeparam name="T">The struct type.</typeparam>
    /// <returns>A <see cref="Nullable{T}"/>.</returns>
    [Pure]
    public static T? ToNullable<T>(this Option<T> option)
        where T : struct => option.IsSome ? option.Value : null;
    /// <summary>
    /// Converts a given <see cref="Nullable{T}"/> into an instance of <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="nullable">The <see cref="Nullable{T}"/> to convert.</param>
    /// <typeparam name="T">The struct type.</typeparam>
    /// <returns>An <see cref="Option{T}"/>.</returns>
    [Pure]
    public static Option<T> ToOption<T>(this T? nullable)
        where T : struct => nullable.HasValue ? Prelude.Some(nullable.Value) : Prelude.None;

    /// <summary>
    /// Allows to clean up any unmanaged resources used by the wrapped instance.
    /// If the <paramref name="option"/> is <see cref="Prelude.Some{T}"/>, then
    /// <see cref="IDisposable.Dispose"/> method is called on the wrapped object.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/>.</param>
    /// <typeparam name="T">The type.</typeparam>
    public static void Dispose<T>(this Option<T> option)
        where T : IDisposable
    {
        if (option.TryUnwrap(out var value))
        {
            value.Dispose();
        }
    }
}

/// <summary>
/// Contains alternative implementation of some methods from <see cref="OptionExtensions"/>.
/// </summary>
[PublicAPI]
public static class AlternativeOptionExtensions
{
    /// <summary>
    /// Flattens an <see cref="Option{T}"/> of nullable class.
    /// <see langword="null"/> becomes <see cref="Prelude.None"/>, <typeparamref name="T"/> becomes <see cref="Prelude.Some{T}"/>.
    /// </summary>
    /// <param name="option">The nested <see cref="Option{T}"/>.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>An <see cref="Option{T}"/> of <typeparamref name="T"/>.</returns>
    [Pure]
    public static Option<T> Flatten<T>(this Option<T?> option)
        where T : class => option.IsNone ? Prelude.None : option.Value.ToOption();
    /// <summary>
    /// Flattens a nullable <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="option">The <see cref="Nullable{T}"/> of option.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>An <see cref="Option{T}"/> of <typeparamref name="T"/>.</returns>
    [Pure]
    public static Option<T> Flatten<T>(this Option<T>? option)
        where T : class => option ?? Prelude.None;

    /// <summary>
    /// Converts a given <see cref="Option{T}"/> into an instance of <see cref="Nullable{T}"/>.
    /// </summary>
    /// <param name="option">The <see cref="Option{T}"/> to convert.</param>
    /// <typeparam name="T">The class type.</typeparam>
    /// <returns>A <see cref="Nullable{T}"/>.</returns>
    [Pure]
    public static T? ToNullable<T>(this Option<T> option)
        where T : class => option.IsSome ? option.Value : null;
    /// <summary>
    /// Converts a given <see cref="Nullable{T}"/> into an instance of <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="nullable">The <see cref="Nullable{T}"/> to convert.</param>
    /// <typeparam name="T">The class type.</typeparam>
    /// <returns>An <see cref="Option{T}"/>.</returns>
    [Pure]
    public static Option<T> ToOption<T>(this T? nullable)
        where T : class => nullable is not null ? Prelude.Some(nullable) : Prelude.None;
}