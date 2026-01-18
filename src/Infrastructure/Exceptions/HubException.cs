namespace Football.Infrastructure.Exceptions;

/// <summary>
/// Represents errors that occur during SignalR hub operations.
/// </summary>
public class HubException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HubException"/> class.
    /// </summary>
    public HubException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HubException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public HubException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HubException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public HubException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
