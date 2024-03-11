namespace TravelBookingPlatform.Application.Exceptions;

[Serializable]
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException() { }

    public EntityNotFoundException(string entityName) : base($"{entityName} could not be found!") { }

    public EntityNotFoundException(string message, Exception inner) : base(message, inner) { }

    protected EntityNotFoundException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}