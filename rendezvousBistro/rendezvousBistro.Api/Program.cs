using Microsoft.AspNetCore.Mvc.Infrastructure;
using rendezvousBistro.Api.Errors;
using rendezvousBistro.Api.Middleware;
using rendezvousBistro.Application;
using rendezvousBistro.Application.Filters;
using rendezvousBistro.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    // builder.Services.AddControllers(
    //     options => options.Filters.Add<ErrorHandlingFilterAttribute>()
    // );

    // Add custom problem details factory
    builder.Services.AddSingleton<ProblemDetailsFactory, RendezvousBistroProblemDetailFactory>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    // app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}