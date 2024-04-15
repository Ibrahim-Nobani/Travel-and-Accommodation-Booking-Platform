using System.Transactions;
using MediatR;
using TravelBookingPlatform.Infrastructure.Database;

namespace TravelBookingPlatform.Application.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (IsRequestNotCommand())
        {
            return await next();
        }

        var response = await next();

        await _unitOfWork.SaveChangesAsync();

        return response;
    }

    private static bool IsRequestNotCommand()
    {
        return !typeof(TRequest).Name.EndsWith("Command");
    }
}