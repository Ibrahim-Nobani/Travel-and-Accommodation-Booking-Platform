using TravelBookingPlatform.Domain.Enums;
namespace TravelBookingPlatform.Application.DTOs;
public class PaginationParameters
{
    public int PageNumber { get; set; } = PageDefaultValues.PageNumber;
    public int PageSize { get; set; } = PageDefaultValues.PageSize;
}
