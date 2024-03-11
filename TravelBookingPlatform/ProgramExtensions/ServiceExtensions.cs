using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Application.Services;
using TravelBookingPlatform.Domain.Interfaces;
using TravelBookingPlatform.Domain.Services;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Infrastructure.ExternalServices;
using TravelBookingPlatform.Infrastructure.Interfaces;
using TravelBookingPlatform.Infrastructure.Repositories;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IFeaturedDealRepository, FeaturedDealRepository>();
        services.AddScoped<IUserVisitRepository, UserVisitRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();

        services.AddScoped<IRoomAvailabilityService, RoomAvailabilityService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBraintreeService, BraintreeService>();

        services.AddTransient<IPasswordHashService, PasswordHashBcryptService>();
        services.AddTransient<IPricingService, PricingService>();

        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IPaymentEmailService, PaymentEmailService>();
    }

    public static void AddCustomMediatR(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssemblyContaining<Program>();
            //c.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        });
    }
}
