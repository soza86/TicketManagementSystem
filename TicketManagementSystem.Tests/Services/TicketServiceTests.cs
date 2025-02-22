using Moq;
using TicketManagementSystem.ApplicationLayer.Common;
using TicketManagementSystem.ApplicationLayer.DTOs;
using TicketManagementSystem.ApplicationLayer.Interfaces;
using TicketManagementSystem.ApplicationLayer.Services;
using TicketManagementSystem.Core.Entities;

namespace TicketManagementSystem.Tests.Services
{
    public class TicketServiceTests
    {
        private readonly Mock<ITicketRepository> _repositoryMock;
        private readonly ITicketMapper _ticketMapper;

        public TicketServiceTests()
        {
            _repositoryMock = new Mock<ITicketRepository>();
            _ticketMapper = new TicketMapper();
        }

        [Fact]
        public async Task Given_IRequestForExistingTicket_When_GetByNumberAsync_Then_ReturnsResult()
        {
            //Arrange
            var ticket = new Ticket
            {
                Id = 1,
                FlightDate = DateTime.UtcNow,
                Number = 123456789,
                DepartureCity = "Athens",
                DepartureCountry = "Greece",
                ArrivalCity = "Brussels",
                ArrivalCountry = "Belgium",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
            };

            _repositoryMock.Setup(a => a.GetByNumberAsync(It.IsAny<long>())).ReturnsAsync(ticket);
            var service = new TicketService(_repositoryMock.Object, _ticketMapper);

            //Act
            var result = await service.GetByNumberAsync(123456789);

            //Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(1, result.Data?.Id);
            Assert.Equal(123456789, result.Data?.Number);
            Assert.Equal("Athens", result.Data?.DepartureCity);
            Assert.Equal("Greece", result.Data?.DepartureCountry);
            Assert.Equal("Brussels", result.Data?.ArrivalCity);
            Assert.Equal("Belgium", result.Data?.ArrivalCountry);
        }

        [Fact]
        public async Task Given_IRequestForNonExistingTicket_When_GetByNumberAsync_Then_ReturnsNoResult()
        {
            //Arrange
            Ticket? ticket = null;

            _repositoryMock.Setup(a => a.GetByNumberAsync(It.IsAny<long>())).ReturnsAsync(ticket);
            var service = new TicketService(_repositoryMock.Object, _ticketMapper);

            //Act
            var result = await service.GetByNumberAsync(123456789);

            //Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Ticket not found.", result.Message);
        }

        [Fact]
        public async Task Given_IRequestToCreateNewTicket_When_CreateAsync_Then_ReturnsResult()
        {
            //Arrange
            var ticketDto = new CreateTicketDto
            {
                FlightDate = DateTime.UtcNow,
                Number = 123456789,
                DepartureCity = "Athens",
                DepartureCountry = "Greece",
                ArrivalCity = "Brussels",
                ArrivalCountry = "Belgium",
            };
            Ticket? nonExistingTicket = null;
            var newTicket = new Ticket
            {
                Id = 1,
                FlightDate = DateTime.UtcNow,
                Number = 123456789,
                DepartureCity = "Athens",
                DepartureCountry = "Greece",
                ArrivalCity = "Brussels",
                ArrivalCountry = "Belgium",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
            };

            _repositoryMock.Setup(a => a.GetByNumberAsync(It.IsAny<long>())).ReturnsAsync(nonExistingTicket);
            _repositoryMock.Setup(a => a.AddAsync(It.IsAny<Ticket>())).ReturnsAsync(newTicket);
            var service = new TicketService(_repositoryMock.Object, _ticketMapper);

            //Act
            var result = await service.CreateAsync(ticketDto);

            //Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(1, result.Data?.Id);
            Assert.Equal(123456789, result.Data?.Number);
            Assert.Equal("Athens", result.Data?.DepartureCity);
            Assert.Equal("Greece", result.Data?.DepartureCountry);
            Assert.Equal("Brussels", result.Data?.ArrivalCity);
            Assert.Equal("Belgium", result.Data?.ArrivalCountry);
        }

        [Fact]
        public async Task Given_IRequestToCreateNewTicketWithExistingNumber_When_CreateAsync_Then_ReturnsNoResult()
        {
            //Arrange
            var ticketDto = new CreateTicketDto
            {
                FlightDate = DateTime.UtcNow,
                Number = 123456789,
                DepartureCity = "Athens",
                DepartureCountry = "Greece",
                ArrivalCity = "Brussels",
                ArrivalCountry = "Belgium",
            };
            var existingTicket = new Ticket
            {
                Id = 1,
                FlightDate = DateTime.UtcNow,
                Number = 123456789,
                DepartureCity = "Athens",
                DepartureCountry = "Greece",
                ArrivalCity = "Brussels",
                ArrivalCountry = "Belgium",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
            };
            var newTicket = new Ticket
            {
                Id = 2,
                FlightDate = DateTime.UtcNow,
                Number = 123456789,
                DepartureCity = "Athens",
                DepartureCountry = "Greece",
                ArrivalCity = "Brussels",
                ArrivalCountry = "Belgium",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
            };

            _repositoryMock.Setup(a => a.GetByNumberAsync(It.IsAny<long>())).ReturnsAsync(existingTicket);
            _repositoryMock.Setup(a => a.AddAsync(It.IsAny<Ticket>())).ReturnsAsync(newTicket);
            var service = new TicketService(_repositoryMock.Object, _ticketMapper);

            //Act
            var result = await service.CreateAsync(ticketDto);

            //Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Ticket already exists.", result.Message);
        }
    }
}
