using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Infrastructure.Interfaces;

public interface IMailService
{
    Task<bool> SendMailAsync(MailData mailData);
}