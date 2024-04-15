using AutoMapper;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelPaymentTransactionPlatform.Application.MappingProfiles;

public class PaymentTransactionMappingProfile : Profile
{
    public PaymentTransactionMappingProfile()
    {
        PaymentTransactionMapping();
    }

    private void PaymentTransactionMapping()
    {
        CreateMap<PaymentTransactionDto, PaymentTransaction>();
        CreateMap<PaymentTransaction, PaymentTransactionDto>();
    }
}