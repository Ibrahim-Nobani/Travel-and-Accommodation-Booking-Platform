namespace TravelBookingPlatform.Application.Exceptions;

public class DuplicateEmailException : Exception
{
    public DuplicateEmailException()
        : base($"Email already exists. Please choose a different email.") { }

    public DuplicateEmailException(string message) : base(message) { }

    public DuplicateEmailException(string message, Exception inner) : base(message, inner) { }

    protected DuplicateEmailException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
