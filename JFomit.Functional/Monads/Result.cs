using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace JFomit.Functional.Monads;

/// <summary>
/// Stores either a value of type <typeparamref name="TSuccess"/> or, on failure, an error value
/// of type <typeparamref name="TError"/>.
/// </summary>
/// <typeparam name="TSuccess">The <see cref="Prelude.Ok{T}"/> type.</typeparam>
/// <typeparam name="TError">The <see cref="Prelude.Error{E}"/> type.</typeparam>
/// <seealso href="https://en.wikipedia.org/wiki/Result_type"/>
[PublicAPI]
public readonly struct Result<TSuccess, TError>
{
    /// <summary>
    /// Weather this <see cref="Result{TSuccess,TError}"/> is <see cref="Prelude.Ok{T}"/>.
    /// </summary>
    public bool IsSuccess { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }
    /// <summary>
    /// Weather this <see cref="Result{TSuccess,TError}"/> is <see cref="Prelude.Error{E}"/>.
    /// </summary>
    public bool IsError => !IsSuccess;

    /// <summary>
    /// Extracts the inner <typeparamref name="TSuccess"/> value. Throws on <see cref="Prelude.Error{E}"/>.
    /// </summary>
    /// <exception cref="WrongUnwrapException">if this <see cref="Result{TSuccess,TError}"/> is <see cref="Prelude.Error{E}"/>.</exception>
    public TSuccess Success
    {
        get
        {
            if (IsSuccess)
            {
                return _success;
            }
            else
            {
                ThrowHelper.ThrowWrongUnwrapException("Tried to get the value from a 'Error' variant of Result<TSuccess, TError>.");
                return default!;
            }
        }
    }

    /// <summary>
    /// Extracts the inner <typeparamref name="TError"/> value. Throws on <see cref="Prelude.Ok{T}"/>.
    /// </summary>
    /// <exception cref="WrongUnwrapException">if this <see cref="Result{TSuccess,TError}"/> is <see cref="Prelude.Ok{T}"/>.</exception>
    public TError Error
    {
        get
        {
            if (IsError)
            {
                return _error;
            }
            else
            {
                ThrowHelper.ThrowWrongUnwrapException("Tried to get the error from a 'Success' variant of Result<TSuccess, TError>.");
                return default!;
            }
        }
    }

    private readonly TSuccess _success;
    private readonly TError _error;

    /// <exclude/>
    [Obsolete("Results can only be constructed with methods, such as Prelude.Ok(T) or Prelude.Error(E).", true)]
    public Result()
    {
        IsSuccess = false;

        _success = default!;
        _error = default!;
    }
    // For creating 'Error' variant
    private Result(TError error)
    {
        IsSuccess = false;

        _success = default!;
        _error = error;
    }
    // For creating 'Success' variant
    private Result(TSuccess success)
    {
        IsSuccess = true;

        _success = success;
        _error = default!;
    }

    /// <summary>
    /// Constructs a new <see cref="Result{TSuccess,TError}"/> instance from a
    /// given <typeparamref name="TSuccess"/> value.
    /// </summary>
    /// <param name="success">The success value.</param>
    /// <returns>A <see cref="Prelude.Ok{T}"/> variant of <see cref="Result{TSuccess,TError}"/>.</returns>
    public static Result<TSuccess, TError> Ok(TSuccess success) => new(success);
    /// <summary>
    /// Constructs a new <see cref="Result{TSuccess,TError}"/> instance from a
    /// given <typeparamref name="TError"/> value.
    /// </summary>
    /// <param name="error">The error value.</param>
    /// <returns>A <see cref="Prelude.Error{E}"/> variant of <see cref="Result{TSuccess,TError}"/>.</returns>
    public static Result<TSuccess, TError> Fail(TError error) => new(error);

    /// <summary>
    /// Converts a given <see cref="OkVariant{TSuccess}"/> to a <see cref="Result{TSuccess,TError}"/>.
    /// </summary>
    /// <param name="ok">The value to convert.</param>
    /// <returns>An <see cref="Prelude.Ok{T}"/> variant of <see cref="Result{TSuccess,TError}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Result<TSuccess, TError>(OkVariant<TSuccess> ok) => new(ok.Success);
    /// <summary>
    /// Converts a given <see cref="FailVariant{TError}"/> to a <see cref="Result{TSuccess,TError}"/>.
    /// </summary>
    /// <param name="fail">The value to convert.</param>
    /// <returns>An <see cref="Prelude.Error{E}"/> variant of <see cref="Result{TSuccess,TError}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Result<TSuccess, TError>(FailVariant<TError> fail) => new(fail.Error);

    /// <inheritdoc cref="object.ToString"/>
    public override string ToString()
        => IsSuccess ? $"Ok({Success})" : $"Error({Error})";

    /// <summary>
    /// Extracts the success value and, if failed, throws a <see cref="WrongUnwrapException"/> with user-defined message.
    /// </summary>
    /// <param name="message">The message of thrown exception.</param>
    /// <returns>The success value.</returns>
    [Pure]
    public TSuccess Expect(string message)
        => TryUnwrapSuccess(out var success)
            ? success
            : ThrowHelper.ThrowWrongUnwrapException<TSuccess>(message);
    /// <summary>
    /// Extracts the error value and, if failed, throws a <see cref="WrongUnwrapException"/> with user-defined message.
    /// </summary>
    /// <param name="message">The message of thrown exception.</param>
    /// <returns>The error value.</returns>
    [Pure]
    public TError ExpectError(string message)
        => TryUnwrapError(out var error)
            ? error
            : ThrowHelper.ThrowWrongUnwrapException<TError>(message);

    /// <summary>
    /// Extracts the inner <see cref="Prelude.Ok{T}"/> value. Throws if <see cref="Result{TSuccess,TError}"/>
    /// is <see cref="Prelude.Error{E}"/>.
    /// </summary>
    /// <returns>A wrapped value.</returns>
    /// <exception cref="WrongUnwrapException">If the result is <see cref="Prelude.Error{E}"/>.</exception>
    [Pure]
    public TSuccess Unwrap() => Success;
    /// <summary>
    /// Extracts the inner <see cref="Prelude.Ok{T}"/> value. If <see cref="Result{TSuccess,TError}"/> is
    /// <see cref="Prelude.Error{E}"/> returns <paramref name="other"/>.
    /// </summary>
    /// <param name="other">The alternative value.</param>
    /// <returns>A wrapped value or <paramref name="other"/>.</returns>
    [Pure]
    public TSuccess UnwrapOr(TSuccess other) => TryUnwrapSuccess(out var ok) ? ok : other;
    /// <summary>
    /// Extracts the inner <see cref="Prelude.Ok{T}"/> value. If <see cref="Result{TSuccess,TError}"/> is
    /// <see cref="Prelude.Error{E}"/> returns output of <paramref name="other"/>.
    /// </summary>
    /// <param name="other">The alternative value source.</param>
    /// <returns>A wrapped value or invocation result of <paramref name="other"/>.</returns>
    public TSuccess UnwrapOrElse([InstantHandle] Func<TSuccess> other) => IsSuccess ? _success : other();
    /// <summary>
    /// Extracts the inner <see cref="Prelude.Ok{T}"/> value. If <see cref="Result{TSuccess,TError}"/> is
    /// <see cref="Prelude.Error{E}"/> returns default instance of <typeparamref name="TSuccess"/>.
    /// </summary>
    /// <returns>A wrapped value or <see langword="default"/>.</returns>
    [Pure]
    public TSuccess UnwrapOrDefault() => IsSuccess ? _success : default!;
    /// <summary>
    /// Tries to extract the value inside this <see cref="Result{TSuccess,TError}"/>.
    /// </summary>
    /// <param name="success">When this method returns, contains the inner value, if this <see cref="Result{TSuccess,TError}"/>
    /// is <see cref="Prelude.Ok{T}"/>;
    /// otherwise, the default value for the type of the <paramref name="success"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <returns><see langword="true"/>, if extracted the value from <see cref="Result{TSuccess,TError}"/>;
    /// false, otherwise.</returns>
    [Pure]
    public bool TryUnwrapSuccess([NotNullWhen(true)] out TSuccess? success)
    {
        if (IsSuccess)
        {
            success = _success!;
            return true;
        }

        success = default;
        return false;
    }
    
    /// <summary>
    /// Deconstructs the value in this <see cref="Result{TSuccess, TError}"/> into both <typeparamref name="TSuccess"/> and
    /// <typeparamref name="TError"/> returning true and false depending on what value was in the <see cref="Result{TSuccess, TError}"/>.
    /// </summary>
    /// <param name="success">When this method returns, contains the inner value, if this <see cref="Result{TSuccess,TError}"/>
    /// is <see cref="Prelude.Ok{T}"/>;
    /// otherwise, the default value for the type of the <paramref name="success"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <param name="error">When this method returns, contains the inner value, if this <see cref="Result{TSuccess,TError}"/>
    /// is <see cref="Prelude.Error{E}(E)"/>;
    /// otherwise, the default value for the type of the <paramref name="success"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <returns><see langword="true"/>, if extracted the value <see cref="Prelude.Ok{T}(T)"/> from <see cref="Result{TSuccess,TError}"/>;
    /// false, otherwise.</returns>
    [Pure]
    public bool TryUnwrap2([NotNullWhen(true)] out TSuccess? success, [NotNullWhen(false)] out TError? error)
    {
        if (IsSuccess)
        {
            success = _success!;
            error = default;
            return true;
        }
        else
        {
            success = default;
            error = _error!;
            return false;
        }
    }

    /// <summary>
    /// Extracts the inner <see cref="Prelude.Error{E}"/> value. Throws if <see cref="Result{TSuccess,TError}"/>
    /// is <see cref="Prelude.Ok{T}"/>.
    /// </summary>
    /// <returns>A wrapped error.</returns>
    /// <exception cref="WrongUnwrapException">If the result is <see cref="Prelude.Ok{T}"/>.</exception>
    public TError UnwrapError() => Error;

    /// <summary>
    /// Extracts the inner <see cref="Prelude.Error{E}"/> value. If <see cref="Result{TSuccess,TError}"/> is
    /// <see cref="Prelude.Ok{T}"/> returns <paramref name="other"/>.
    /// </summary>
    /// <param name="other">The alternative error.</param>
    /// <returns>A wrapped error or <paramref name="other"/>.</returns>
    public TError UnwrapErrorOr(TError other) => TryUnwrapError(out var error) ? error : other;
    /// <summary>
    /// Extracts the inner <see cref="Prelude.Error{E}"/> value. If <see cref="Result{TSuccess,TError}"/> is
    /// <see cref="Prelude.Ok{T}"/> returns output of <paramref name="other"/>.
    /// </summary>
    /// <param name="other">The alternative error source.</param>
    /// <returns>A wrapped error or invocation result of <paramref name="other"/>.</returns>
    public TError UnwrapErrorOrElse([InstantHandle] Func<TError> other) =>
        TryUnwrapError(out var error)
            ? error
            : other();
    /// <summary>
    /// Extracts the inner <see cref="Prelude.Error{E}"/> value. If <see cref="Result{TSuccess,TError}"/> is
    /// <see cref="Prelude.Ok{T}"/> returns default instance of <typeparamref name="TError"/>.
    /// </summary>
    /// <returns>A wrapped error or <see langword="default"/>.</returns>
    public TError UnwrapErrorOrDefault() => TryUnwrapError(out var error) ? error : default!;
    /// <summary>
    /// Tries to extract the error value inside this <see cref="Result{TSuccess,TError}"/>.
    /// </summary>
    /// <param name="error">When this method returns, contains the inner value, if this <see cref="Result{TSuccess,TError}"/>
    /// is <see cref="Prelude.Error{E}"/>;
    /// otherwise, the default value for the type of the <paramref name="error"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <returns><see langword="true"/>, if extracted the error from <see cref="Result{TSuccess,TError}"/>;
    /// false, otherwise.</returns>
    [Pure]
    public bool TryUnwrapError([NotNullWhen(true)] out TError? error)
    {
        if (IsError)
        {
            error = _error!;

            return true;
        }

        error = default;
        return false;
    }
}

/// <summary>
/// The <see cref="Prelude.Ok{T}"/> variant.
/// </summary>
/// <param name="Success">The inner value.</param>
/// <typeparam name="TSuccess">The type.</typeparam>
public readonly record struct OkVariant<TSuccess>(TSuccess Success)
{
    /// <inheritdoc cref="object.ToString"/>
    public override string ToString() => $"Ok({Success})";
}
/// <summary>
/// The <see cref="Prelude.Error{E}"/> variant.
/// </summary>
/// <param name="Error">The inner value.</param>
/// <typeparam name="TError">The type.</typeparam>
public readonly record struct FailVariant<TError>(TError Error)
{
    /// <inheritdoc cref="object.ToString"/>
    public override string ToString() => $"Error({Error})";
}
