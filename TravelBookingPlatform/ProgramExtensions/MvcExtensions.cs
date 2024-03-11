using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TravelBookingPlatform.API.Filters;

public static class MvcExtensions
{
    public static void AddCustomMvc(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));

        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters().AddValidatorsFromAssemblyContaining<Program>();

        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        }).AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
    }
}
