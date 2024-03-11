using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Application.DTOs;
using AutoMapper;
namespace TravelBookingPlatform.Application.Queries;

public class GetPaymentTransactionQueryHandler : IRequestHandler<GetPaymentTransactionQuery, PaymentTransactionDto>
{
    private readonly IPaymentTransactionRepository _paymentTransactionRepository;
    private readonly IMapper _mapper;

    public GetPaymentTransactionQueryHandler(IPaymentTransactionRepository paymentTransactionRepository, IMapper mapper)
    {
        _paymentTransactionRepository = paymentTransactionRepository;
        _mapper = mapper;
    }

    public async Task<PaymentTransactionDto> Handle(GetPaymentTransactionQuery request, CancellationToken cancellationToken)
    {
        var paymentTransaction = await _paymentTransactionRepository.GetByIdAsync(request.PaymentTransactionId);

        if (paymentTransaction == null)
        {
            throw new EntityNotFoundException(nameof(paymentTransaction));
        }

        var paymentTransactionDto = _mapper.Map<PaymentTransactionDto>(paymentTransaction);
        return paymentTransactionDto;
    }
}