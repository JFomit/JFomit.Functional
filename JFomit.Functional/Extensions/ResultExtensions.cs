using JetBrains.Annotations;
using JFomit.Functional.Monads;
using static JFomit.Functional.Prelude;

namespace JFomit.Functional.Extensions;

/// <summary>
/// Contains common operations on <see cref="Result{TSuccess,TError}"/>.
/// </summary>
[PublicAPI]
public static class ResultExtensions
{
    // ReSharper disable InconsistentNaming
    /// <summary>
    /// Flattens a given <see cref="Result{TSuccess,TError}"/>.
    /// </summary>
    /// <param name="result">The object to flatten.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <returns>A <see cref="Result{TSuccess,TError}"/> with one less level of nesting.</returns>
    [Pure]
    public static Result<T, E> Flatten<T, E>(this Result<Result<T, E>, E> result) =>
        result.IsSuccess
            ? result.Success
            : Error(result.Error);
    /// <summary>
    /// Flattens a given <see cref="Result{TSuccess,TError}"/> coercing error types in the process.
    /// </summary>
    /// <param name="result">The object to flatten.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E1">The lower <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <typeparam name="E2">The upper <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <returns>A <see cref="Result{TSuccess,TError}"/> with one less level of nesting.</returns>
    [Pure]
    public static Result<T, E2> Flatten<T, E1, E2>(this Result<Result<T, E1>, E2> result)
        where E1 : E2 => result.IsError ? Error(result.Error) : result.Success.Cast<T, E1, E2>();

    /// <summary>
    /// Upcasts error types. <typeparamref name="E1"/> must coerce to <typeparamref name="E2"/>.
    /// </summary>
    /// <param name="result">The object to cast.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E1">The lower <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <typeparam name="E2">The upper <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <returns>A <see cref="Result{TSuccess,TError}"/> with its error type more general, than <paramref name="result"/>.</returns>
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/"/>
    [Pure]
    public static Result<T, E2> Cast<T, E1, E2>(this Result<T, E1> result)
        where E1 : E2 => result.IsSuccess ? Ok(result.Success) : Result<T, E2>.Fail(result.Error);
    /// <summary>
    /// Upcasts success types. <typeparamref name="T1"/> must coerce to <typeparamref name="T2"/>.
    /// </summary>
    /// <param name="result">The object to cast.</param>
    /// <typeparam name="T1">The lower <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="T2">The upper <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <returns>A <see cref="Result{TSuccess,TError}"/> with its success type more general, than <paramref name="result"/>.</returns>
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/"/>
    [Pure]
    public static Result<T2, E> Cast<T1, T2, E>(this Result<T1, E> result)
        where T1 : T2 => result.IsSuccess ? Result<T2, E>.Ok(result.Success) : Error(result.Error);

    /// <summary>
    /// Projects a wrapped <see cref="Prelude.Ok{T}"/> value to a new form and wraps the output into another <see cref="Result{TSuccess,TError}"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/>.</param>
    /// <param name="func">The selector function.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="TResult">The resulting <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <returns>A <see cref="Result{TSuccess,TError}"/> containing projected value.
    /// If <paramref name="result"/> is <see cref="Prelude.Error{E}"/>, that error is returned instead.</returns>
    public static Result<TResult, E> Select<T, TResult, E>(this Result<T, E> result, [InstantHandle] Func<T, TResult> func) => result.IsError ? Error(result.Error) : Ok(func(result.Success));
    /// <summary>
    /// Projects a wrapped <see cref="Prelude.Ok{T}"/> value together with some arbitrary context
    /// to a new form and wraps the output into another <see cref="Result{TSuccess,TError}"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/>.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="func">The selector function.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="TResult">The resulting <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    /// <returns>A <see cref="Result{TSuccess,TError}"/> containing projected value.
    /// If <paramref name="result"/> is <see cref="Prelude.Error{E}"/>, that error is returned instead.</returns>
    public static Result<TResult, E> Select<T, TResult, E, TContext>(this Result<T, E> result, TContext context, [InstantHandle] Func<T, TContext, TResult> func)
#if NET9_0_OR_GREATER
        where TContext : allows ref struct
#endif
        => result.IsError ? Error(result.Error) : Ok(func(result.Success, context));

    /// <summary>
    /// Projects the value inside a <see cref="Result{TSuccess,TError}"/> to another <see cref="Result{TSuccess,TError}"/>
    /// and flattens the output.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/>.</param>
    /// <param name="func">The selector function.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="TResult">The output type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <returns>A <see cref="Result{TSuccess,TError}"/> containing projected value.
    /// If <paramref name="result"/> is <see cref="Prelude.Error{E}"/>, that error is returned instead.</returns>
    public static Result<TResult, E> SelectMany<T, TResult, E>(this Result<T, E> result, [InstantHandle] Func<T, Result<TResult, E>> func) => result.IsError ? Error(result.Error) : func(result.Success);
    /// <summary>
    /// Projects the value inside a <see cref="Result{TSuccess,TError}"/> to another <see cref="Result{TSuccess,TError}"/>
    /// with arbitrary context and flattens the output.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/>.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="func">The selector function.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="TResult">The output type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    /// <returns>A <see cref="Result{TSuccess,TError}"/> containing projected value.
    /// If <paramref name="result"/> is <see cref="Prelude.Error{E}"/>, that error is returned instead.</returns>
    public static Result<TResult, E> SelectMany<T, TResult, E, TContext>(this Result<T, E> result, TContext context, [InstantHandle] Func<T, TContext, Result<TResult, E>> func)
#if NET9_0_OR_GREATER
        where TContext : allows ref struct
#endif
        => result.IsError ? Error(result.Error) : func(result.Success, context);

    /// <summary>
    /// Invokes an action if a <see cref="Result{TSuccess,TError}"/> is <see cref="Prelude.Ok{T}"/>. 
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/>.</param>
    /// <param name="action">The delegate to invoke.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    public static void IFOk<T, E>(this Result<T, E> result, [InstantHandle] Action<T> action)
    {
        if (result.IsSuccess)
        {
            action(result.Success);
        }
    }
    /// <summary>
    /// Invokes an action if a <see cref="Result{TSuccess,TError}"/> is <see cref="Prelude.Ok{T}"/> with
    /// some arbitrary context. 
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/>.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="action">The delegate to invoke.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    public static void IFOk<T, E, TContext>(this Result<T, E> result,
        TContext context,
        [InstantHandle] Action<T, TContext> action)
#if NET9_0_OR_GREATER
        where TContext : allows ref struct
#endif
    {
        if (result.IsSuccess)
        {
            action(result.Success, context);
        }
    }

    /// <summary>
    /// Invokes an action if a <see cref="Result{TSuccess,TError}"/> is <see cref="Prelude.Error{E}"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/>.</param>
    /// <param name="action">The delegate to invoke.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    public static void IfError<T, E>(this Result<T, E> result, [InstantHandle] Action<E> action)
    {
        if (result.IsError)
        {
            action(result.Error);
        }
    }
    /// <summary>
    /// Invokes an action if a <see cref="Result{TSuccess,TError}"/> is <see cref="Prelude.Error{E}"/> with
    /// some arbitrary context.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/>.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="action">The delegate to invoke.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    public static void IfError<T, E, TContext>(this Result<T, E> result,
        TContext context,
        [InstantHandle] Action<E, TContext> action)
#if NET9_0_OR_GREATER
        where TContext : allows ref struct
#endif
    {
        if (result.IsError)
        {
            action(result.Error, context);
        }
    }

    /// <summary>
    /// Matches on a <see cref="Result{TSuccess,TError}"/> and invokes the appropriate delegate.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/> to match.</param>
    /// <param name="ok">The delegate to call if matched <see cref="Prelude.Ok{T}"/> variant.</param>
    /// <param name="fail">The delegate to call if matched <see cref="Prelude.Error{E}"/> variant.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <returns>An instance of <typeparamref name="TResult"/> from invoking either <paramref name="ok"/>
    /// or <paramref name="fail"/>.</returns>
    public static TResult Match<T, E, TResult>(this Result<T, E> result,
        [InstantHandle] Func<T, TResult> ok,
        [InstantHandle] Func<E, TResult> fail) =>
        result.IsError
            ? fail(result.Error)
            : ok(result.Success);
    /// <summary>
    /// Matches on a <see cref="Result{TSuccess,TError}"/> and invokes the appropriate delegate with
    /// some arbitrary context.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/> to match.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="ok">The delegate to call if matched <see cref="Prelude.Ok{T}"/> variant.</param>
    /// <param name="fail">The delegate to call if matched <see cref="Prelude.Error{E}"/> variant.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    /// <returns>An instance of <typeparamref name="TResult"/> from invoking either <paramref name="ok"/>
    /// or <paramref name="fail"/>.</returns>
    public static TResult Match<T, E, TResult, TContext>(this Result<T, E> result,
        TContext context,
        [InstantHandle] Func<T, TContext, TResult> ok,
        [InstantHandle] Func<E, TContext, TResult> fail)
#if NET9_0_OR_GREATER
        where TContext : allows ref struct
#endif
        => result.IsError
            ? fail(result.Error,
                context)
            : ok(result.Success,
                context);

    /// <summary>
    /// Switches on a <see cref="Result{TSuccess,TError}"/> and invokes the appropriate delegate.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/> to switch on.</param>
    /// <param name="ok">The delegate to call if matched <see cref="Prelude.Ok{T}"/> variant.</param>
    /// <param name="fail">The delegate to call if matched <see cref="Prelude.Error{E}"/> variant.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    public static void Switch<T, E>(this Result<T, E> result,
        [InstantHandle] Action<T> ok,
        [InstantHandle] Action<E> fail)
    {
        if (result.IsError)
        {
            fail(result.Error);
        }
        else
        {
            ok(result.Success);
        }
    }
    /// <summary>
    /// Switches on a <see cref="Result{TSuccess,TError}"/> and invokes the appropriate delegate with some
    /// arbitrary context.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/> to switch on.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="ok">The delegate to call if matched <see cref="Prelude.Ok{T}"/> variant.</param>
    /// <param name="fail">The delegate to call if matched <see cref="Prelude.Error{E}"/> variant.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    public static void Switch<T, E, TContext>(this Result<T, E> result,
        TContext context,
        [InstantHandle] Action<T, TContext> ok,
        [InstantHandle] Action<E, TContext> fail)
#if NET9_0_OR_GREATER
        where TContext : allows ref struct
#endif
    {
        if (result.IsError)
        {
            fail(result.Error, context);
        }
        else
        {
            ok(result.Success, context);
        }
    }

    // ReSharper disable once InvalidXmlDocComment
    /// <summary>
    /// Extracts the inner <see cref="Prelude.Ok{T}"/> value. If instead <paramref name="result"/> is
    /// <see cref="Prelude.Error{E}"/> variant, throws wrapped <see cref="Exception"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/>.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="TException">The <see cref="Exception"/> error type.</typeparam>
    /// <returns>A wrapped value.</returns>
    /// <exception cref="TException">If the <see cref="Result{TSuccess,TError}"/> was <see cref="Prelude.Error{E}"/>.</exception>
    [Pure]
    public static T UnwrapOrThrow<T, TException>(this Result<T, TException> result)
        where TException : Exception
    {
        return result.IsSuccess ? result.Success : throw result.Error;
    }
    /// <summary>
    /// Extracts the inner <see cref="Prelude.Ok{T}"/> value. If <see cref="Result{TSuccess,TError}"/> is
    /// <see cref="Prelude.Error{E}"/>, throws <see cref="WrongUnwrapException"/> with encoded message.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/>.</param>
    /// <typeparam name="T">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <returns>A wrapped value.</returns>
    /// <exception cref="WrongUnwrapException">If <paramref name="result"/> is <see cref="Prelude.Error{E}"/>.</exception>
    [Pure]
    public static T Unwrap<T>(this Result<T, string> result) =>
        result.IsSuccess
            ? result.Success
            : ThrowHelper.ThrowWrongUnwrapException<T>(result.Error);

    /// <summary>
    /// Converts the given <see cref="Result{TSuccess,TError}"/> to an <see cref="Option{T}"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{TSuccess,TError}"/>.</param>
    /// <returns><see cref="Some{T}(T)"/> if <paramref name="result"/> is <see cref="Ok{T}(T)"/>;
    /// <see cref="None"/> otherwise.
    /// </returns>
    [Pure]
    public static Option<T> ToOption<T, E>(this Result<T, E> result) => result.IsSuccess ? Some(result.Success!) : None;

    /// <summary>
    /// Combines to <see cref="Result{TSuccess,TError}"/>s into one according to a selector function.
    /// </summary>
    /// <param name="result">The first <see cref="Result{TSuccess,TError}"/>.</param>
    /// <param name="other">The second <see cref="Result{TSuccess,TError}"/>.</param>
    /// <param name="selector">The selector function.</param>
    /// <typeparam name="T1">The first <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="T2">The second <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <returns>A <see cref="Result{TSuccess,TError}"/>, containing the output of <paramref name="selector"/>.
    /// If the either <paramref name="result"/> or <paramref name="other"/> is <see cref="Prelude.Error{E}"/>,
    /// that error is returned instead.
    /// </returns>
    public static Result<TResult, E> Zip<T1, T2, TResult, E>(this Result<T1, E> result,
        Result<T2, E> other,
        [InstantHandle] Func<T1, T2, TResult> selector)
        =>
            result.TryUnwrapSuccess(out var v1) && other.TryUnwrapSuccess(out var v2)
                ? Ok(selector(v1, v2))
                : Error(result.Error);
    /// <summary>
    /// Combines to <see cref="Result{TSuccess,TError}"/>s into one according to a selector function with some
    /// arbitrary context.
    /// </summary>
    /// <param name="result">The first <see cref="Result{TSuccess,TError}"/>.</param>
    /// <param name="other">The second <see cref="Result{TSuccess,TError}"/>.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="selector">The selector function.</param>
    /// <typeparam name="T1">The first <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="T2">The second <see cref="Prelude.Ok{T}"/> type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <typeparam name="E">The <see cref="Prelude.Error{E}"/> type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    /// <returns>A <see cref="Result{TSuccess,TError}"/>, containing the output of <paramref name="selector"/>.
    /// If the either <paramref name="result"/> or <paramref name="other"/> is <see cref="Prelude.Error{E}"/>,
    /// that error is returned instead.
    /// </returns>
    public static Result<TResult, E> Zip<T1, T2, TResult, E, TContext>(this Result<T1, E> result,
        Result<T2, E> other,
        TContext context,
        [InstantHandle] Func<T1, T2, TContext, TResult> selector)
        =>
            result.TryUnwrapSuccess(out var v1) && other.TryUnwrapSuccess(out var v2)
                ? Ok(selector(v1, v2, context))
                : Error(result.Error);
    // ReSharper restore InconsistentNaming
}