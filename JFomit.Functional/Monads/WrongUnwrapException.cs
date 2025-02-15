using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace JFomit.Functional.Monads;

/// <summary>
/// An <see cref="Exception"/> that is thrown when tried to unwrap incorrect variant.
/// </summary>
[Serializable]
[PublicAPI]
public class WrongUnwrapException : Exception
{
    /// <summary>
    /// Initializes an instance of <see cref="WrongUnwrapException"/> with default message.
    /// </summary>
    public WrongUnwrapException() { }
    /// <inheritdoc />
    public WrongUnwrapException(string message) : base(message) { }
    /// <inheritdoc />
    public WrongUnwrapException(string message, Exception inner) : base(message, inner) { }
    /// <inheritdoc />
    [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.")]
    protected WrongUnwrapException(
      SerializationInfo info,
      StreamingContext context) : base(info, context) { }
}
