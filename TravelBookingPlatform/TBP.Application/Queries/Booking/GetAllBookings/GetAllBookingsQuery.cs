using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetAllBookingsQuery : IRequest<IEnumerable<BookingDto>> { }