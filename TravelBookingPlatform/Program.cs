using TravelBookingPlatform.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCustomMvc();
builder.Services.AddCustomDbContext(builder.Configuration);
builder.Services.AddCustomServices();
builder.Services.AddCustomMediatR();
builder.Services.AddCustomJWT(builder.Configuration);
builder.Services.AddSwagger();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelPlatform.API v1");
});

app.Run();
