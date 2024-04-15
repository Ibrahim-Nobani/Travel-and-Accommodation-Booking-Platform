namespace TravelBookingPlatform.Application.Exceptions;

public class RoomAlreadyFeaturedException : Exception
{
    public RoomAlreadyFeaturedException() : base("The room is already featured in another deal.") { }

    public RoomAlreadyFeaturedException(string message) : base(message) { }

    public RoomAlreadyFeaturedException(string message, Exception inner) : base(message, inner) { }
}
