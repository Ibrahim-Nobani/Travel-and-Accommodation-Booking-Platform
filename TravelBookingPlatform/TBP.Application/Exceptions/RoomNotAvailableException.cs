namespace TravelBookingPlatform.Application.Exceptions;

public class RoomNotAvailableException : Exception
{
    public RoomNotAvailableException() : base("The room is not available for the requested dates.") { }

    public RoomNotAvailableException(string message) : base(message) { }

    public RoomNotAvailableException(string message, Exception innerException) : base(message, innerException) { }
}

