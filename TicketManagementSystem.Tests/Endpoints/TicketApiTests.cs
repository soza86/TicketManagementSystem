using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;
using System.Net.Http.Json;
using TicketManagementSystem.API;
using TicketManagementSystem.ApplicationLayer.DTOs;
using TicketManagementSystem.ApplicationLayer.Interfaces;

namespace TicketManagementSystem.Tests.Endpoints
{
    public class TicketApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<ITicketService> _serviceMock;

        public TicketApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _serviceMock = new Mock<ITicketService>();
        }

        [Fact]
        public async Task Given_IRequestForExistingTicket_When_GetTicket_Then_ReturnsResult()
        {
            //Arrange
            var ticketDto = new TicketDto
            {
                Id = 1,
                FlightDate = DateTime.UtcNow,
                Number = 123456789,
                DepartureCity = "Athens",
                DepartureCountry = "Greece",
                ArrivalCity = "Brussels",
                ArrivalCountry = "Belgium",
            };
            _serviceMock.Setup(s => s.GetByNumberAsync(It.IsAny<long>())).ReturnsAsync(ServiceResponse<TicketDto?>.SuccessResponse(ticketDto));

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => _serviceMock.Object);
                });
            }).CreateClient();

            //Act
            var response = await client.GetAsync("/tickets/123456789");
            var ticket = await response.Content.ReadFromJsonAsync<ServiceResponse<TicketDto>>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(ticket?.Success);
            Assert.Equal(200, ticket?.StatusCode);
            Assert.Equal(1, ticket?.Data?.Id);
            Assert.Equal(123456789, ticket?.Data?.Number);
            Assert.Equal("Athens", ticket?.Data?.DepartureCity);
            Assert.Equal("Greece", ticket?.Data?.DepartureCountry);
            Assert.Equal("Brussels", ticket?.Data?.ArrivalCity);
            Assert.Equal("Belgium", ticket?.Data?.ArrivalCountry);
        }

        [Fact]
        public async Task Given_IRequestForNonExistingTicket_When_GetTicket_Then_ReturnsNoResult()
        {
            //Arrange
            _serviceMock.Setup(s => s.GetByNumberAsync(It.IsAny<long>())).ReturnsAsync(ServiceResponse<TicketDto?>.NotFoundResponse("Ticket not found."));

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => _serviceMock.Object);
                });
            }).CreateClient();

            //Act
            var response = await client.GetAsync("/tickets/123456789");
            var message = await response.Content.ReadFromJsonAsync<string>();

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("Ticket not found.", message);
        }

        [Fact]
        public async Task Given_IRequestForCreatingNewTicket_When_CreateTicket_Then_ReturnsResult()
        {
            //Arrange
            var createTicketDto = new CreateTicketDto
            {
                FlightDate = DateTime.UtcNow.AddDays(2),
                Number = 123456789,
                DepartureCity = "Athens",
                DepartureCountry = "Greece",
                ArrivalCity = "Brussels",
                ArrivalCountry = "Belgium",
            };
            var ticketDto = new TicketDto
            {
                Id = 1,
                FlightDate = DateTime.UtcNow,
                Number = 123456789,
                DepartureCity = "Athens",
                DepartureCountry = "Greece",
                ArrivalCity = "Brussels",
                ArrivalCountry = "Belgium",
            };

            _serviceMock.Setup(s => s.CreateAsync(It.IsAny<CreateTicketDto>())).ReturnsAsync(ServiceResponse<TicketDto?>.SuccessResponse(ticketDto));

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => _serviceMock.Object);
                });
            }).CreateClient();

            //Act
            var response = await client.PostAsJsonAsync("/tickets", createTicketDto);
            var ticket = await response.Content.ReadFromJsonAsync<ServiceResponse<TicketDto>>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(ticket?.Success);
            Assert.Equal(200, ticket?.StatusCode);
            Assert.Equal(1, ticket?.Data?.Id);
            Assert.Equal(123456789, ticket?.Data?.Number);
            Assert.Equal("Athens", ticket?.Data?.DepartureCity);
            Assert.Equal("Greece", ticket?.Data?.DepartureCountry);
            Assert.Equal("Brussels", ticket?.Data?.ArrivalCity);
            Assert.Equal("Belgium", ticket?.Data?.ArrivalCountry);
        }

        [Fact]
        public async Task Given_IRequestForCreatingNewTicketWithExistingNumber_When_CreateTicket_Then_ReturnsNoResult()
        {
            //Arrange
            var createTicketDto = new CreateTicketDto
            {
                FlightDate = DateTime.UtcNow.AddDays(2),
                Number = 123456789,
                DepartureCity = "Athens",
                DepartureCountry = "Greece",
                ArrivalCity = "Brussels",
                ArrivalCountry = "Belgium",
            };

            _serviceMock.Setup(s => s.CreateAsync(It.IsAny<CreateTicketDto>())).ReturnsAsync(ServiceResponse<TicketDto?>.FailureResponse("Ticket already exists."));

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => _serviceMock.Object);
                });
            }).CreateClient();

            //Act
            var response = await client.PostAsJsonAsync("/tickets", createTicketDto);
            var message = await response.Content.ReadFromJsonAsync<string>();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Ticket already exists.", message);
        }

        [Fact]
        public async Task Given_IRequestForCreatingNewTicketWithInvalidRequest_When_CreateTicket_Then_ReturnsNoResult()
        {
            //Arrange
            var createTicketDto = new CreateTicketDto
            {
                FlightDate = DateTime.UtcNow,
                Number = 123456789,
                DepartureCity = "Athens",
                DepartureCountry = "Greece",
                ArrivalCity = "Athens",
                ArrivalCountry = "Belgium",
            };

            _serviceMock.Setup(s => s.CreateAsync(It.IsAny<CreateTicketDto>())).ReturnsAsync(ServiceResponse<TicketDto?>.FailureResponse("Ticket already exists."));

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => _serviceMock.Object);
                });
            }).CreateClient();

            //Act
            var response = await client.PostAsJsonAsync("/tickets", createTicketDto);
            var errors = await response.Content.ReadFromJsonAsync<List<ValidationFailure>>();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Flight date can not be in the past", errors?.ElementAtOrDefault(0)?.ErrorMessage);
            Assert.Equal("Departure city can not be the same as arrival city", errors?.ElementAtOrDefault(1)?.ErrorMessage);
            Assert.Equal("Arrival city can not be the same as departure city", errors?.ElementAtOrDefault(2)?.ErrorMessage);
        }
    }
}
