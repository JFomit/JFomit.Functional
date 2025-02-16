using System.Diagnostics;

namespace JFomit.Functional.Monads;

// All permutation for OneOf<T1, T2> conversions (except for identity conversion)
public partial record OneOf<T1, T2>
{
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2}"/> to a <see cref="OneOf{T1, T2}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T2, T1>(OneOf<T1, T2> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T2, T1>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T2, T1>)new Variant<T2>(v2.GetValue()),
        _ => throw new UnreachableException(),
    };
}

// All permutation for OneOf<T1, T2, T3> conversions (except for identity conversion)
public partial record OneOf<T1, T2, T3>
{
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3}"/> to a <see cref="OneOf{T1, T2, T3}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T2, T3, T1>(OneOf<T1, T2, T3> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T2, T3, T1>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T2, T3, T1>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T2, T3, T1>)new Variant<T3>(v3.GetValue()),
        _ => throw new UnreachableException(),
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3}"/> to a <see cref="OneOf{T1, T2, T3}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T3, T1, T2>(OneOf<T1, T2, T3> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T3, T1, T2>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T3, T1, T2>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T3, T1, T2>)new Variant<T3>(v3.GetValue()),
        _ => throw new UnreachableException(),
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3}"/> to a <see cref="OneOf{T1, T2, T3}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T2, T1, T3>(OneOf<T1, T2, T3> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T2, T1, T3>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T2, T1, T3>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T2, T1, T3>)new Variant<T3>(v3.GetValue()),
        _ => throw new UnreachableException(),
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3}"/> to a <see cref="OneOf{T1, T2, T3}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T1, T3, T2>(OneOf<T1, T2, T3> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T1, T3, T2>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T1, T3, T2>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T1, T3, T2>)new Variant<T3>(v3.GetValue()),
        _ => throw new UnreachableException(),
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3}"/> to a <see cref="OneOf{T1, T2, T3}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T3, T2, T1>(OneOf<T1, T2, T3> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T3, T2, T1>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T3, T2, T1>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T3, T2, T1>)new Variant<T3>(v3.GetValue()),
        _ => throw new UnreachableException(),
    };
}

// All permutation for OneOf<T1, T2, T3, T4> conversions (except for identity conversion)
public partial record OneOf<T1, T2, T3, T4>
{
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T1, T2, T4, T3>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T1, T2, T4, T3>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T1, T2, T4, T3>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T1, T2, T4, T3>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T1, T2, T4, T3>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T1, T3, T2, T4>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T1, T3, T2, T4>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T1, T3, T2, T4>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T1, T3, T2, T4>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T1, T3, T2, T4>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T1, T3, T4, T2>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T1, T3, T4, T2>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T1, T3, T4, T2>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T1, T3, T4, T2>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T1, T3, T4, T2>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T1, T4, T2, T3>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T1, T4, T2, T3>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T1, T4, T2, T3>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T1, T4, T2, T3>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T1, T4, T2, T3>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T1, T4, T3, T2>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T1, T4, T3, T2>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T1, T4, T3, T2>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T1, T4, T3, T2>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T1, T4, T3, T2>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T2, T1, T3, T4>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T2, T1, T3, T4>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T2, T1, T3, T4>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T2, T1, T3, T4>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T2, T1, T3, T4>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T2, T1, T4, T3>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T2, T1, T4, T3>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T2, T1, T4, T3>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T2, T1, T4, T3>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T2, T1, T4, T3>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T2, T3, T1, T4>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T2, T3, T1, T4>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T2, T3, T1, T4>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T2, T3, T1, T4>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T2, T3, T1, T4>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T2, T3, T4, T1>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T2, T3, T4, T1>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T2, T3, T4, T1>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T2, T3, T4, T1>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T2, T3, T4, T1>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T2, T4, T1, T3>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T2, T4, T1, T3>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T2, T4, T1, T3>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T2, T4, T1, T3>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T2, T4, T1, T3>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T2, T4, T3, T1>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T2, T4, T3, T1>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T2, T4, T3, T1>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T2, T4, T3, T1>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T2, T4, T3, T1>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T3, T1, T2, T4>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T3, T1, T2, T4>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T3, T1, T2, T4>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T3, T1, T2, T4>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T3, T1, T2, T4>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T3, T1, T4, T2>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T3, T1, T4, T2>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T3, T1, T4, T2>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T3, T1, T4, T2>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T3, T1, T4, T2>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T3, T2, T1, T4>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T3, T2, T1, T4>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T3, T2, T1, T4>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T3, T2, T1, T4>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T3, T2, T1, T4>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T3, T2, T4, T1>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T3, T2, T4, T1>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T3, T2, T4, T1>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T3, T2, T4, T1>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T3, T2, T4, T1>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T3, T4, T1, T2>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T3, T4, T1, T2>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T3, T4, T1, T2>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T3, T4, T1, T2>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T3, T4, T1, T2>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T3, T4, T2, T1>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T3, T4, T2, T1>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T3, T4, T2, T1>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T3, T4, T2, T1>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T3, T4, T2, T1>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T4, T1, T2, T3>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T4, T1, T2, T3>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T4, T1, T2, T3>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T4, T1, T2, T3>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T4, T1, T2, T3>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T4, T1, T3, T2>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T4, T1, T3, T2>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T4, T1, T3, T2>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T4, T1, T3, T2>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T4, T1, T3, T2>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T4, T2, T1, T3>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T4, T2, T1, T3>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T4, T2, T1, T3>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T4, T2, T1, T3>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T4, T2, T1, T3>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T4, T2, T3, T1>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T4, T2, T3, T1>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T4, T2, T3, T1>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T4, T2, T3, T1>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T4, T2, T3, T1>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T4, T3, T1, T2>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T4, T3, T1, T2>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T4, T3, T1, T2>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T4, T3, T1, T2>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T4, T3, T1, T2>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
    /// <summary>
    /// Converts this <see cref="OneOf{T1, T2, T3, T4}"/> to a <see cref="OneOf{T1, T2, T3, T4}"/> of different order.
    /// </summary>
    public static explicit operator OneOf<T4, T3, T2, T1>(OneOf<T1, T2, T3, T4> oneOf) => oneOf switch
    {
        GenericVariant<T1> v1 => (OneOf<T4, T3, T2, T1>)new Variant<T1>(v1.GetValue()),
        GenericVariant<T2> v2 => (OneOf<T4, T3, T2, T1>)new Variant<T2>(v2.GetValue()),
        GenericVariant<T3> v3 => (OneOf<T4, T3, T2, T1>)new Variant<T3>(v3.GetValue()),
        GenericVariant<T4> v4 => (OneOf<T4, T3, T2, T1>)new Variant<T4>(v4.GetValue()),
        _ => throw new UnreachableException()
    };
}
