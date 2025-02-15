using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using JFomit.Functional.Monads;
using static JFomit.Functional.Prelude;

namespace JFomit.Functional.Extensions;

/// <summary>
/// Provides common operations on OneOf-like types.
/// </summary>
[PublicAPI]
public static class OneOfExtensions
{
    /// <summary>
    /// Matches on an instance of <see cref="OneOf{T1,T2}"/> and invokes an appropriate delegate.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2}"/> to match on.</param>
    /// <param name="t1">The delegate to invoke on <typeparamref name="T1"/>.</param>
    /// <param name="t2">The delegate to invoke on <typeparamref name="T2"/>.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <returns>A <typeparamref name="TResult"/>.</returns>
    public static TResult Match<T1, T2, TResult>(
        this OneOf<T1, T2> oneOf,
        [InstantHandle] Func<T1, TResult> t1,
        [InstantHandle] Func<T2, TResult> t2) => oneOf switch
        {
            OneOf<T1, T2>.GenericVariant<T1> variant => t1(variant.Value),
            OneOf<T1, T2>.GenericVariant<T2> variant => t2(variant.Value),

            _ => throw new UnreachableException()
        };
    /// <summary>
    /// Matches on an instance of <see cref="OneOf{T1,T2}"/> and invokes an appropriate delegate with some
    /// arbitrary context.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2}"/> to match on.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="t1">The delegate to invoke on <typeparamref name="T1"/>.</param>
    /// <param name="t2">The delegate to invoke on <typeparamref name="T2"/>.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    /// <returns>A <typeparamref name="TResult"/>.</returns>
    public static TResult Match<T1, T2, TResult, TContext>(
        this OneOf<T1, T2> oneOf,
        TContext context,
        [InstantHandle] Func<T1, TContext, TResult> t1,
        [InstantHandle] Func<T2, TContext, TResult> t2) => oneOf switch
        {
            OneOf<T1, T2>.GenericVariant<T1> variant => t1(variant.Value, context),
            OneOf<T1, T2>.GenericVariant<T2> variant => t2(variant.Value, context),

            _ => throw new UnreachableException()
        };
    /// <summary>
    /// Switches on an instance of <see cref="OneOf{T1,T2}"/> and invokes an appropriate delegate.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2}"/> to match on.</param>
    /// <param name="t1">The delegate to invoke on <typeparamref name="T1"/>.</param>
    /// <param name="t2">The delegate to invoke on <typeparamref name="T2"/>.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    public static void Switch<T1, T2>(
        this OneOf<T1, T2> oneOf,
        [InstantHandle] Action<T1> t1,
        [InstantHandle] Action<T2> t2)
    {
        switch (oneOf)
        {
            case OneOf<T1, T2>.GenericVariant<T1> variant:
                t1(variant.Value);
                break;
            case OneOf<T1, T2>.GenericVariant<T2> variant:
                t2(variant.Value);
                break;

            default:
                throw new UnreachableException();
        }
    }
    /// <summary>
    /// Switches on an instance of <see cref="OneOf{T1,T2}"/> and invokes an appropriate delegate with some
    /// arbitrary context.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2}"/> to match on.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="t1">The delegate to invoke on <typeparamref name="T1"/>.</param>
    /// <param name="t2">The delegate to invoke on <typeparamref name="T2"/>.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    public static void Switch<T1, T2, TContext>(
        this OneOf<T1, T2> oneOf,
        TContext context,
        [InstantHandle] Action<T1, TContext> t1,
        [InstantHandle] Action<T2, TContext> t2)
    {
        switch (oneOf)
        {
            case OneOf<T1, T2>.GenericVariant<T1> variant:
                t1(variant.Value, context);
                break;
            case OneOf<T1, T2>.GenericVariant<T2> variant:
                t2(variant.Value, context);
                break;

            default:
                throw new UnreachableException();
        }
    }
    /// <summary>
    /// Matches on an instance of <see cref="OneOf{T1,T2,T3}"/> and invokes an appropriate delegate.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3}"/> to match on.</param>
    /// <param name="t1">The delegate to invoke on <typeparamref name="T1"/>.</param>
    /// <param name="t2">The delegate to invoke on <typeparamref name="T2"/>.</param>
    /// <param name="t3">The delegate to invoke on <typeparamref name="T3"/>.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <returns>A <typeparamref name="TResult"/>.</returns>
    public static TResult Match<T1, T2, T3, TResult>(
        this OneOf<T1, T2, T3> oneOf,
        [InstantHandle] Func<T1, TResult> t1,
        [InstantHandle] Func<T2, TResult> t2,
        [InstantHandle] Func<T3, TResult> t3) => oneOf switch
        {
            OneOf<T1, T2, T3>.GenericVariant<T1> variant => t1(variant.Value),
            OneOf<T1, T2, T3>.GenericVariant<T2> variant => t2(variant.Value),
            OneOf<T1, T2, T3>.GenericVariant<T3> variant => t3(variant.Value),

            _ => throw new UnreachableException()
        };
    /// <summary>
    /// Matches on an instance of <see cref="OneOf{T1,T2,T3}"/> and invokes an appropriate delegate with some
    /// arbitrary context.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3}"/> to match on.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="t1">The delegate to invoke on <typeparamref name="T1"/>.</param>
    /// <param name="t2">The delegate to invoke on <typeparamref name="T2"/>.</param>
    /// <param name="t3">The delegate to invoke on <typeparamref name="T3"/>.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    /// <returns>A <typeparamref name="TResult"/>.</returns>
    public static TResult Match<T1, T2, T3, TResult, TContext>(
        this OneOf<T1, T2, T3> oneOf,
        TContext context,
        [InstantHandle] Func<T1, TContext, TResult> t1,
        [InstantHandle] Func<T2, TContext, TResult> t2,
        [InstantHandle] Func<T3, TContext, TResult> t3) => oneOf switch
        {
            OneOf<T1, T2, T3>.GenericVariant<T1> variant => t1(variant.Value, context),
            OneOf<T1, T2, T3>.GenericVariant<T2> variant => t2(variant.Value, context),
            OneOf<T1, T2, T3>.GenericVariant<T3> variant => t3(variant.Value, context),

            _ => throw new UnreachableException()
        };

    /// <summary>
    /// Switches on an instance of <see cref="OneOf{T1,T2,T3}"/> and invokes an appropriate delegate with some
    /// arbitrary context.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3}"/> to switch on.</param>
    /// <param name="t1">The delegate to invoke on <typeparamref name="T1"/>.</param>
    /// <param name="t2">The delegate to invoke on <typeparamref name="T2"/>.</param>
    /// <param name="t3">The delegate to invoke on <typeparamref name="T3"/>.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    public static void Switch<T1, T2, T3>(
        this OneOf<T1, T2, T3> oneOf,
        [InstantHandle] Action<T1> t1,
        [InstantHandle] Action<T2> t2,
        [InstantHandle] Action<T3> t3)
    {
        switch (oneOf)
        {
            case OneOf<T1, T2, T3>.GenericVariant<T1> variant:
                t1(variant.Value);
                break;
            case OneOf<T1, T2, T3>.GenericVariant<T2> variant:
                t2(variant.Value);
                break;
            case OneOf<T1, T2, T3>.GenericVariant<T3> variant:
                t3(variant.Value);
                break;

            default:
                throw new UnreachableException();
        }
    }
    /// <summary>
    /// Switches on an instance of <see cref="OneOf{T1,T2,T3}"/> and invokes an appropriate delegate with some
    /// arbitrary context.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3}"/> to switch on.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="t1">The delegate to invoke on <typeparamref name="T1"/>.</param>
    /// <param name="t2">The delegate to invoke on <typeparamref name="T2"/>.</param>
    /// <param name="t3">The delegate to invoke on <typeparamref name="T3"/>.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    public static void Switch<T1, T2, T3, TContext>(
        this OneOf<T1, T2, T3> oneOf,
        TContext context,
        [InstantHandle] Action<T1, TContext> t1,
        [InstantHandle] Action<T2, TContext> t2,
        [InstantHandle] Action<T3, TContext> t3)
    {
        switch (oneOf)
        {
            case OneOf<T1, T2, T3>.GenericVariant<T1> variant:
                t1(variant.Value, context);
                break;
            case OneOf<T1, T2, T3>.GenericVariant<T2> variant:
                t2(variant.Value, context);
                break;
            case OneOf<T1, T2, T3>.GenericVariant<T3> variant:
                t3(variant.Value, context);
                break;

            default:
                throw new UnreachableException();
        }
    }

    /// <summary>
    /// Matches on an instance of <see cref="OneOf{T1,T2,T3,T4}"/> and invokes an appropriate delegate.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3,T4}"/> to match on.</param>
    /// <param name="t1">The delegate to invoke on <typeparamref name="T1"/>.</param>
    /// <param name="t2">The delegate to invoke on <typeparamref name="T2"/>.</param>
    /// <param name="t3">The delegate to invoke on <typeparamref name="T3"/>.</param>
    /// <param name="t4">The delegate to invoke on <typeparamref name="T4"/>.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <typeparam name="T4">The fourth type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <returns>A <typeparamref name="TResult"/>.</returns>
    public static TResult Match<T1, T2, T3, T4, TResult>(
        this OneOf<T1, T2, T3, T4> oneOf,
        [InstantHandle] Func<T1, TResult> t1,
        [InstantHandle] Func<T2, TResult> t2,
        [InstantHandle] Func<T3, TResult> t3,
        [InstantHandle] Func<T4, TResult> t4) => oneOf switch
        {
            OneOf<T1, T2, T3, T4>.GenericVariant<T1> variant => t1(variant.Value),
            OneOf<T1, T2, T3, T4>.GenericVariant<T2> variant => t2(variant.Value),
            OneOf<T1, T2, T3, T4>.GenericVariant<T3> variant => t3(variant.Value),
            OneOf<T1, T2, T3, T4>.GenericVariant<T4> variant => t4(variant.Value),

            _ => throw new UnreachableException()
        };
    /// <summary>
    /// Matches on an instance of <see cref="OneOf{T1,T2,T3,T4}"/> and invokes an appropriate delegate with some
    /// arbitrary context.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3,T4}"/> to match on.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="t1">The delegate to invoke on <typeparamref name="T1"/>.</param>
    /// <param name="t2">The delegate to invoke on <typeparamref name="T2"/>.</param>
    /// <param name="t3">The delegate to invoke on <typeparamref name="T3"/>.</param>
    /// <param name="t4">The delegate to invoke on <typeparamref name="T4"/>.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <typeparam name="T4">The fourth type.</typeparam>
    /// <typeparam name="TResult">The resulting type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    /// <returns>A <typeparamref name="TResult"/>.</returns>
    public static TResult Match<T1, T2, T3, T4, TResult, TContext>(
        this OneOf<T1, T2, T3, T4> oneOf,
        TContext context,
        [InstantHandle] Func<T1, TContext, TResult> t1,
        [InstantHandle] Func<T2, TContext, TResult> t2,
        [InstantHandle] Func<T3, TContext, TResult> t3,
        [InstantHandle] Func<T4, TContext, TResult> t4) => oneOf switch
        {
            OneOf<T1, T2, T3, T4>.GenericVariant<T1> variant => t1(variant.Value, context),
            OneOf<T1, T2, T3, T4>.GenericVariant<T2> variant => t2(variant.Value, context),
            OneOf<T1, T2, T3, T4>.GenericVariant<T3> variant => t3(variant.Value, context),
            OneOf<T1, T2, T3, T4>.GenericVariant<T4> variant => t4(variant.Value, context),

            _ => throw new UnreachableException()
        };
    /// <summary>
    /// Switches on an instance of <see cref="OneOf{T1,T2,T3,T4}"/> and invokes an appropriate delegate.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3,T4}"/> to switch on.</param>
    /// <param name="t1">The delegate to invoke on <typeparamref name="T1"/>.</param>
    /// <param name="t2">The delegate to invoke on <typeparamref name="T2"/>.</param>
    /// <param name="t3">The delegate to invoke on <typeparamref name="T3"/>.</param>
    /// <param name="t4">The delegate to invoke on <typeparamref name="T4"/>.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <typeparam name="T4">The fourth type.</typeparam>
    public static void Switch<T1, T2, T3, T4>(
        this OneOf<T1, T2, T3, T4> oneOf,
        Action<T1> t1,
        Action<T2> t2,
        Action<T3> t3,
        Action<T4> t4)
    {
        switch (oneOf)
        {
            case OneOf<T1, T2, T3, T4>.GenericVariant<T1> variant:
                t1(variant.Value);
                break;
            case OneOf<T1, T2, T3, T4>.GenericVariant<T2> variant:
                t2(variant.Value);
                break;
            case OneOf<T1, T2, T3, T4>.GenericVariant<T3> variant:
                t3(variant.Value);
                break;
            case OneOf<T1, T2, T3, T4>.GenericVariant<T4> variant:
                t4(variant.Value);
                break;

            default:
                throw new UnreachableException();
        }
    }
    /// <summary>
    /// Switches on an instance of <see cref="OneOf{T1,T2,T3}"/> and invokes an appropriate delegate with some
    /// arbitrary context.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3}"/> to switch on.</param>
    /// <param name="context">The context to pass.</param>
    /// <param name="t1">The delegate to invoke on <typeparamref name="T1"/>.</param>
    /// <param name="t2">The delegate to invoke on <typeparamref name="T2"/>.</param>
    /// <param name="t3">The delegate to invoke on <typeparamref name="T3"/>.</param>
    /// <param name="t4">The delegate to invoke on <typeparamref name="T4"/>.</param>
    /// <typeparam name="T1">The first type.</typeparam>
    /// <typeparam name="T2">The second type.</typeparam>
    /// <typeparam name="T3">The third type.</typeparam>
    /// <typeparam name="T4">The fourth type.</typeparam>
    /// <typeparam name="TContext">The context type.</typeparam>
    public static void Switch<T1, T2, T3, T4, TContext>(
        this OneOf<T1, T2, T3, T4> oneOf,
        TContext context,
        Action<T1, TContext> t1,
        Action<T2, TContext> t2,
        Action<T3, TContext> t3,
        Action<T4, TContext> t4)
    {
        switch (oneOf)
        {
            case OneOf<T1, T2, T3, T4>.GenericVariant<T1> variant:
                t1(variant.Value, context);
                break;
            case OneOf<T1, T2, T3, T4>.GenericVariant<T2> variant:
                t2(variant.Value, context);
                break;
            case OneOf<T1, T2, T3, T4>.GenericVariant<T3> variant:
                t3(variant.Value, context);
                break;
            case OneOf<T1, T2, T3, T4>.GenericVariant<T4> variant:
                t4(variant.Value, context);
                break;

            default:
                throw new UnreachableException();
        }
    }

    /// <summary>
    /// Extracts the value of type <typeparamref name="T"/> from a given <see cref="IOneOf"/>.
    /// Throws, if <paramref name="genericOneOf"/> doesn't store <typeparamref name="T"/>. 
    /// </summary>
    /// <param name="genericOneOf">The <see cref="IOneOf"/>.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>An instance of <typeparamref name="T"/>.</returns>
    /// <exception cref="WrongUnwrapException">If <paramref name="genericOneOf"/> isn't of type <typeparamref name="T"/>.</exception>
    [Pure]
    public static T Unwrap<T>(this IOneOf genericOneOf)
    {
        if (genericOneOf is IGenericVariant<T> genericVariant)
        {
            return genericVariant.GetValue();
        }

        throw new WrongUnwrapException($"Tried to get a value of type {typeof(T).FullName} from {genericOneOf.GetType().FullName}.");
    }
    
    /// <summary>
    /// Extracts the value of type <typeparamref name="T"/> from a given <see cref="IOneOf"/>,
    /// returning <paramref name="other"/> if <paramref name="genericOneOf"/> isn't of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="genericOneOf">The <see cref="IOneOf"/>.</param>
    /// <param name="other">The alternative value.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>An instance of <typeparamref name="T"/>.</returns>
    [Pure]
    public static T UnwrapOr<T>(this IOneOf genericOneOf, T other)
    {
        if (genericOneOf is IGenericVariant<T> genericVariant)
        {
            return genericVariant.GetValue();
        }

        return other;
    }
    /// <summary>
    /// Extracts the value of type <typeparamref name="T"/> from a given <see cref="IOneOf"/>,
    /// returning invocation result of <paramref name="other"/>
    /// if <paramref name="genericOneOf"/> isn't of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="genericOneOf">The <see cref="IOneOf"/>.</param>
    /// <param name="other">The alternative value source.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>An instance of <typeparamref name="T"/>.</returns>
    public static T UnwrapOrElse<T>(this IOneOf genericOneOf, [InstantHandle] Func<T> other)
    {
        if (genericOneOf is IGenericVariant<T> genericVariant)
        {
            return genericVariant.GetValue();
        }

        return other();
    }
    /// <summary>
    /// Extracts the value of type <typeparamref name="T"/> from a given <see cref="IOneOf"/>,
    /// returning default value for type <typeparamref name="T"/> if <paramref name="genericOneOf"/> isn't of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="genericOneOf">The <see cref="IOneOf"/>.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>An instance of <typeparamref name="T"/>.</returns>
    [Pure]
    public static T? UnwrapOrDefault<T>(this IOneOf genericOneOf)
    {
        if (genericOneOf is IGenericVariant<T> genericVariant)
        {
            return genericVariant.GetValue();
        }

        return default;
    }
    /// <summary>
    /// Extracts the value of type <typeparamref name="T"/> from a given <see cref="IOneOf"/>.
    /// Throws a <see cref="WrongUnwrapException"/> with custom message,
    /// if <paramref name="genericOneOf"/> doesn't store <typeparamref name="T"/>. 
    /// </summary>
    /// <param name="genericOneOf">The <see cref="IOneOf"/>.</param>
    /// <param name="message">A custom message to put into thrown exception.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>An instance of <typeparamref name="T"/>.</returns>
    /// <exception cref="WrongUnwrapException">If <paramref name="genericOneOf"/> isn't of type <typeparamref name="T"/>.</exception>
    [Pure]
    public static T Expect<T>(this IOneOf genericOneOf, string message)
    {
        if (genericOneOf is IGenericVariant<T> genericVariant)
        {
            return genericVariant.GetValue();
        }

        throw new WrongUnwrapException(message);
    }

    /// <summary>
    /// Tries to extract the value inside this <see cref="IOneOf"/>.
    /// </summary>
    /// <param name="genericOneOf">The <see cref="IOneOf"/>.</param>
    /// <param name="value">When this method returns, contains the inner value, if this <see cref="IOneOf"/>
    /// is <typeparamref name="T"/>;
    /// otherwise, the default value for the type of the <paramref name="value"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <returns><see langword="true"/>, if extracted the value from <see cref="IOneOf"/>; false, otherwise.</returns>
    [Pure]
    public static bool TryUnwrap<T>(this IOneOf genericOneOf, out T value)
    {
        if (genericOneOf is IGenericVariant<T> genericVariant)
        {
            value = genericVariant.GetValue();
            return true;
        }

        value = default!;
        return false;
    }

    /// <summary>
    /// Tries to extract the value inside this <see cref="OneOf{T1,T2}"/>.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2}"/>.</param>
    /// <param name="value">When this method returns, contains the inner value, if this <see cref="OneOf{T1,T2}"/>
    /// is <typeparamref name="T1"/>;
    /// otherwise, the default value for the type of the <paramref name="value"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <param name="remainder">When this method returns contains the value, that wasn't extracted.
    /// If extraction to <paramref name="value"/> is successful, contains default value instead.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns><see langword="true"/>, if extracted the value from <see cref="OneOf{T1,T2}"/>; false, otherwise.</returns>
    [Pure]
    public static bool TryUnwrap<T1, T2>(this OneOf<T1, T2> oneOf,
        [MaybeNullWhen(false)] out T1 value,
        [MaybeNullWhen(true)] out T2 remainder)
    {
        if (oneOf is OneOf<T1, T2>.GenericVariant<T1> genericVariant)
        {
            value = genericVariant.GetValue();
            remainder = default!;
            return true;
        }

        value = default!;
        remainder = oneOf.Unwrap<T2>();
        return false;
    }
    /// <summary>
    /// Tries to extract the value inside this <see cref="OneOf{T1,T2}"/>.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2}"/>.</param>
    /// <param name="value">When this method returns, contains the inner value, if this <see cref="OneOf{T1,T2}"/>
    /// is <typeparamref name="T2"/>;
    /// otherwise, the default value for the type of the <paramref name="value"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <param name="remainder">When this method returns contains the value, that wasn't extracted.
    /// If extraction to <paramref name="value"/> is successful, contains default value instead.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns><see langword="true"/>, if extracted the value from <see cref="OneOf{T1,T2}"/>; false, otherwise.</returns>
    [Pure]
    public static bool TryUnwrap<T1, T2>(this OneOf<T1, T2> oneOf,
        [MaybeNullWhen(false)] out T2 value,
        [MaybeNullWhen(true)] out T1 remainder)
    {
        if (oneOf is OneOf<T1, T2>.GenericVariant<T2> genericVariant)
        {
            value = genericVariant.GetValue();
            remainder = default!;
            return true;
        }

        value = default!;
        remainder = oneOf.Unwrap<T1>();
        return false;
    }
    /// <summary>
    /// Tries to extract the value inside this <see cref="OneOf{T1,T2,T3}"/>.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3}"/>.</param>
    /// <param name="value">When this method returns, contains the inner value, if this <see cref="OneOf{T1,T2,T3}"/>
    /// is <typeparamref name="T1"/>;
    /// otherwise, the default value for the type of the <paramref name="value"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <param name="remainder">When this method returns contains the value, that wasn't extracted.
    /// If extraction to <paramref name="value"/> is successful, contains default value instead.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns><see langword="true"/>, if extracted the value from <see cref="OneOf{T1,T2,T3}"/>; false, otherwise.</returns>
    [Pure]
    public static bool TryUnwrap<T1, T2, T3>(this OneOf<T1, T2, T3> oneOf,
        [MaybeNullWhen(false)] out T1 value,
        [MaybeNullWhen(true)] out OneOf<T2, T3> remainder)
    {
        if (oneOf is OneOf<T1, T2, T3>.GenericVariant<T1> genericVariant)
        {
            value = genericVariant.GetValue();
            remainder = default!;

            return true;
        }

        remainder = oneOf switch
        {
            IGenericVariant<T2> v => Variant(v.GetValue()),
            IGenericVariant<T3> v => Variant(v.GetValue()),

            _ => throw new UnreachableException()
        };
        value = default!;

        return false;
    }
    /// <summary>
    /// Tries to extract the value inside this <see cref="OneOf{T1,T2,T3}"/>.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3}"/>.</param>
    /// <param name="value">When this method returns, contains the inner value, if this <see cref="OneOf{T1,T2,T3}"/>
    /// is <typeparamref name="T2"/>;
    /// otherwise, the default value for the type of the <paramref name="value"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <param name="remainder">When this method returns contains the value, that wasn't extracted.
    /// If extraction to <paramref name="value"/> is successful, contains default value instead.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns><see langword="true"/>, if extracted the value from <see cref="OneOf{T1,T2,T3}"/>; false, otherwise.</returns>
    [Pure]
    public static bool TryUnwrap<T1, T2, T3>(this OneOf<T1, T2, T3> oneOf,
        [MaybeNullWhen(false)] out T2 value,
        [MaybeNullWhen(true)] out OneOf<T1, T3> remainder)
    {
        if (oneOf is OneOf<T1, T2, T3>.GenericVariant<T2> genericVariant)
        {
            value = genericVariant.GetValue();
            remainder = default!;

            return true;
        }

        remainder = oneOf switch
        {
            IGenericVariant<T1> v => Variant(v.GetValue()),
            IGenericVariant<T3> v => Variant(v.GetValue()),

            _ => throw new UnreachableException()
        };
        value = default!;

        return false;
    }
    /// <summary>
    /// Tries to extract the value inside this <see cref="OneOf{T1,T2,T3}"/>.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3}"/>.</param>
    /// <param name="value">When this method returns, contains the inner value, if this <see cref="OneOf{T1,T2,T3}"/>
    /// is <typeparamref name="T3"/>;
    /// otherwise, the default value for the type of the <paramref name="value"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <param name="remainder">When this method returns contains the value, that wasn't extracted.
    /// If extraction to <paramref name="value"/> is successful, contains default value instead.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns><see langword="true"/>, if extracted the value from <see cref="OneOf{T1,T2,T3}"/>; false, otherwise.</returns>
    [Pure]
    public static bool TryUnwrap<T1, T2, T3>(this OneOf<T1, T2, T3> oneOf,
        [MaybeNullWhen(false)] out T3 value,
        [MaybeNullWhen(true)] out OneOf<T1, T2> remainder)
    {
        if (oneOf is OneOf<T1, T2, T3>.GenericVariant<T3> genericVariant)
        {
            value = genericVariant.GetValue();
            remainder = default!;

            return true;
        }

        remainder = oneOf switch
        {
            IGenericVariant<T1> v => Variant(v.GetValue()),
            IGenericVariant<T2> v => Variant(v.GetValue()),

            _ => throw new UnreachableException()
        };
        value = default!;

        return false;
    }

    /// <summary>
    /// Tries to extract the value inside this <see cref="OneOf{T1,T2,T3,T4}"/>.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3,T4}"/>.</param>
    /// <param name="value">When this method returns, contains the inner value, if this <see cref="OneOf{T1,T2,T3,T4}"/>
    /// is <typeparamref name="T1"/>;
    /// otherwise, the default value for the type of the <paramref name="value"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <param name="remainder">When this method returns contains the value, that wasn't extracted.
    /// If extraction to <paramref name="value"/> is successful, contains default value instead.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns><see langword="true"/>, if extracted the value from <see cref="OneOf{T1,T2,T3,T4}"/>; false, otherwise.</returns>
    [Pure]
    public static bool TryUnwrap<T1, T2, T3, T4>(this OneOf<T1, T2, T3, T4> oneOf,
        [MaybeNullWhen(false)] out T1 value,
        [MaybeNullWhen(true)] out OneOf<T2, T3, T4> remainder)
    {
        if (oneOf is OneOf<T1, T2, T3, T4>.GenericVariant<T1> genericVariant)
        {
            value = genericVariant.GetValue();
            remainder = default!;

            return true;
        }

        remainder = oneOf switch
        {
            IGenericVariant<T2> v => Variant(v.GetValue()),
            IGenericVariant<T3> v => Variant(v.GetValue()),
            IGenericVariant<T4> v => Variant(v.GetValue()),

            _ => throw new UnreachableException()
        };
        value = default!;

        return false;
    }
    /// <summary>
    /// Tries to extract the value inside this <see cref="OneOf{T1,T2,T3,T4}"/>.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3,T4}"/>.</param>
    /// <param name="value">When this method returns, contains the inner value, if this <see cref="OneOf{T1,T2,T3,T4}"/>
    /// is <typeparamref name="T2"/>;
    /// otherwise, the default value for the type of the <paramref name="value"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <param name="remainder">When this method returns contains the value, that wasn't extracted.
    /// If extraction to <paramref name="value"/> is successful, contains default value instead.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns><see langword="true"/>, if extracted the value from <see cref="OneOf{T1,T2,T3,T4}"/>; false, otherwise.</returns>
    [Pure]
    public static bool TryUnwrap<T1, T2, T3, T4>(this OneOf<T1, T2, T3, T4> oneOf,
        [MaybeNullWhen(false)] out T2 value,
        [MaybeNullWhen(true)] out OneOf<T1, T3, T4> remainder)
    {
        if (oneOf is OneOf<T1, T2, T3, T4>.GenericVariant<T2> genericVariant)
        {
            value = genericVariant.GetValue();
            remainder = default!;

            return true;
        }

        remainder = oneOf switch
        {
            IGenericVariant<T1> v => Variant(v.GetValue()),
            IGenericVariant<T3> v => Variant(v.GetValue()),
            IGenericVariant<T4> v => Variant(v.GetValue()),

            _ => throw new UnreachableException()
        };
        value = default!;

        return false;
    }
    /// <summary>
    /// Tries to extract the value inside this <see cref="OneOf{T1,T2,T3,T4}"/>.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3,T4}"/>.</param>
    /// <param name="value">When this method returns, contains the inner value, if this <see cref="OneOf{T1,T2,T3,T4}"/>
    /// is <typeparamref name="T3"/>;
    /// otherwise, the default value for the type of the <paramref name="value"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <param name="remainder">When this method returns contains the value, that wasn't extracted.
    /// If extraction to <paramref name="value"/> is successful, contains default value instead.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns><see langword="true"/>, if extracted the value from <see cref="OneOf{T1,T2,T3,T4}"/>; false, otherwise.</returns>
    [Pure]
    public static bool TryUnwrap<T1, T2, T3, T4>(this OneOf<T1, T2, T3, T4> oneOf,
        [MaybeNullWhen(false)] out T3 value,
        [MaybeNullWhen(true)] out OneOf<T1, T2, T4> remainder)
    {
        if (oneOf is OneOf<T1, T2, T3, T4>.GenericVariant<T3> genericVariant)
        {
            value = genericVariant.GetValue();
            remainder = default!;

            return true;
        }

        remainder = oneOf switch
        {
            IGenericVariant<T1> v => Variant(v.GetValue()),
            IGenericVariant<T2> v => Variant(v.GetValue()),
            IGenericVariant<T4> v => Variant(v.GetValue()),

            _ => throw new UnreachableException()
        };
        value = default!;

        return false;
    }
    /// <summary>
    /// Tries to extract the value inside this <see cref="OneOf{T1,T2,T3,T4}"/>.
    /// </summary>
    /// <param name="oneOf">The <see cref="OneOf{T1,T2,T3,T4}"/>.</param>
    /// <param name="value">When this method returns, contains the inner value, if this <see cref="OneOf{T1,T2,T3,T4}"/>
    /// is <typeparamref name="T4"/>;
    /// otherwise, the default value for the type of the <paramref name="value"/> parameter.
    /// This parameter is passed uninitialized.</param>
    /// <param name="remainder">When this method returns contains the value, that wasn't extracted.
    /// If extraction to <paramref name="value"/> is successful, contains default value instead.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns><see langword="true"/>, if extracted the value from <see cref="OneOf{T1,T2,T3,T4}"/>; false, otherwise.</returns>
    [Pure]
    public static bool TryUnwrap<T1, T2, T3, T4>(this OneOf<T1, T2, T3, T4> oneOf,
        [MaybeNullWhen(false)] out T4 value,
        [MaybeNullWhen(true)] out OneOf<T1, T2, T3> remainder)
    {
        if (oneOf is OneOf<T1, T2, T3, T4>.GenericVariant<T4> genericVariant)
        {
            value = genericVariant.GetValue();
            remainder = default!;

            return true;
        }

        remainder = oneOf switch
        {
            IGenericVariant<T1> v => Variant(v.GetValue()),
            IGenericVariant<T2> v => Variant(v.GetValue()),
            IGenericVariant<T3> v => Variant(v.GetValue()),

            _ => throw new UnreachableException()
        };
        value = default!;

        return false;
    }
}