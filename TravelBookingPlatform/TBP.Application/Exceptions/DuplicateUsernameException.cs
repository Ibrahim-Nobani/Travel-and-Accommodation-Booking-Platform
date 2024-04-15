namespace TravelBookingPlatform.Application.Exceptions;

public class DuplicateUsernameException : Exception
{
    public DuplicateUsernameException()
        : base($"Username already exists. Please choose a different username.") { }

    public DuplicateUsernameException(string message) : base(message) { }

    public DuplicateUsernameException(string message, Exception inner) : base(message, inner) { }

    protected DuplicateUsernameException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
