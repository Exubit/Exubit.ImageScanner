// Copyright 2022 Exubit Ltd.

namespace Exubit.ImageScanner;

/// <summary>
///     WIA exception class to raise exceptions.
/// </summary>
public class WiaException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Exubit.Libs.ImageScanner.WIAException" /> class with a specified
    ///     error message.
    /// </summary>
    public WiaException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:Exubit.Libs.ImageScanner.WIAException" /> class with a specified
    ///     error message.
    /// </summary>
    /// <param name="message">The exception message that describes the error.</param>
    public WiaException(string message)
        : base(message)
    {
    }

    /// <summary></summary>
    /// <param name="message">The exception message that describes the error.</param>
    /// <param name="innerException">The inner exception.</param>
    public WiaException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
