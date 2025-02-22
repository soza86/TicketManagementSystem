using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TicketManagementSystem.API.Middlewares;
using TicketManagementSystem.API.Validators;
using TicketManagementSystem.ApplicationLayer.Common;
using TicketManagementSystem.ApplicationLayer.DTOs;
using TicketManagementSystem.ApplicationLayer.Interfaces;
using TicketManagementSystem.ApplicationLayer.Services;
using TicketManagementSystem.Core.Interfaces;
using TicketManagementSystem.InfrastructureLayer.Persistence;

namespace TicketManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<TicketDbContext>(options =>
                                options.UseSqlServer(builder.Configuration.GetConnectionString("TicketManagementSystemDbConnection"),
                                sqlOptions => sqlOptions.MigrationsAssembly("TicketManagementSystem.InfrastructureLayer")));

            builder.Services.AddScoped<DbContext, TicketDbContext>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddSingleton<ITicketMapper, TicketMapper>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateTicketDtoValidator>();

            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.UseMiddleware<CustomExceptionMiddleware>();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TicketDbContext>();
                dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/tickets/{ticketNumber}", async (int ticketNumber, ITicketService ticketService) =>
            {
                var ticketDetails = await ticketService.GetByNumberAsync(ticketNumber);
                return Results.Ok(ticketDetails);
            }).WithName("GetTicket");

            app.MapPost("/tickets", async (CreateTicketDto request, ITicketService ticketService, IValidator<CreateTicketDto> validator) =>
            {
                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);

                var result = await ticketService.CreateAsync(request);
                return Results.Ok(result);
            }).WithName("CreateTicket");

            app.Run();
        }
    }
}
