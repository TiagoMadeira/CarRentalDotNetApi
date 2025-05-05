using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Dtos.Rentals;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Service;
using api.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ReturnsExtensions;


namespace api.tests.Unit.Service
{
    public class RentalManagerServiceTest
    {
        private readonly IRentalManagerService _rentalManagerService;
        private readonly IRentalRepository _rentalRepoMock;
        private readonly IBlockedDateRepository _blockedDateRepoMock;
        private readonly IVehicleRepository _vehicleRepoMock;

        //dummmys
        private static readonly string dummmyUserId = "dummyID";
        private static readonly int dummmyRentalId = 9999;
        private static readonly int dummmyVehicleId = 9999;
        private static readonly Rental dummmyRental = new Rental();
        

        //dates
        private static readonly DateOnly pastDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-3));
        private static readonly DateOnly tomorrowDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        private static readonly DateOnly futureDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3));

        //BlockedDates
        private static readonly BlockedDate upCommingBlockedDate = new BlockedDate
        {
            StartDate = tomorrowDate,
            EndDate = futureDate
        };
        private static readonly BlockedDate pastBlockedDate = new BlockedDate
        {
            StartDate = pastDate,
            EndDate = pastDate.AddDays(1)
        };

        //


        //Rentals
        private static readonly Rental concludedRental = new Rental { Id = 1, BlockedDate = pastBlockedDate };
        private static readonly Rental upComingRental = new Rental { Id = 2,  BlockedDate = upCommingBlockedDate };

        private static readonly Vehicle testVehicle = new Vehicle { Rentals = [upComingRental, concludedRental] };
       
           
        

        public RentalManagerServiceTest()
        {   
            //Initialize mocks
            _rentalRepoMock = Substitute.For<IRentalRepository>();
            _blockedDateRepoMock = Substitute.For<IBlockedDateRepository>();
            _vehicleRepoMock = Substitute.For<IVehicleRepository>();
            //Initialize Service
            _rentalManagerService = new RentalManagerService(_rentalRepoMock, _blockedDateRepoMock, _vehicleRepoMock);

            //Set up test data
            concludedRental.Vehicle = testVehicle;
            upComingRental.Vehicle = testVehicle;
        }

        //CreateAsync Tests
        [Fact]
        public async Task CreateAsync_should_Return_StartDateError()
        {
            //Arrange
            var InvalidStartDateDto = new CreateRentalRequestDto
            {
                StartDate = pastDate.ToString(),
                EndDate = futureDate.ToString(),
                VehicleId = dummmyVehicleId
            };
            //Act
            var result = await _rentalManagerService.CreateAsync(dummmyUserId, InvalidStartDateDto);

            //Assert
            KeyValuePair<string, string[]> StarDateError = new KeyValuePair<string, string[]>("StartDate", ["StartDate must be set in the future"]);
            result.Errors.Error.Should().ContainKey("StartDate").WhoseValue.Should().BeEquivalentTo(["StartDate must be set in the future"]);

        }
        [Fact]
        public async Task CreateAsync_should_Return_EndDateError()
        {
            var InvalidEndDateDto = new CreateRentalRequestDto
            {
                StartDate = futureDate.ToString(),
                EndDate = tomorrowDate.ToString(),
                VehicleId = dummmyVehicleId
            };
            //Act
            var result = await _rentalManagerService.CreateAsync(dummmyUserId, InvalidEndDateDto);

            KeyValuePair<string, string[]> EndDateError = new KeyValuePair<string, string[]>("EndDate", ["EndDate must be after StartDate"]);
            result.Errors.Error.Should().ContainKey("EndDate").WhoseValue.Should().BeEquivalentTo(["EndDate must be after StartDate"]);

        }

        [Fact]
        public async Task CreateAsync_should_Return_VehicleError_When_Vehicle_Does_Not_Exist()
        {
            //Arrange
            var InvalidVehicleDtoreateDto = new CreateRentalRequestDto
            {
                StartDate = tomorrowDate.ToString(),
                EndDate = futureDate.ToString(),
                VehicleId = dummmyVehicleId
            };
            _vehicleRepoMock.VehicleExistsAsync(Arg.Any<int>()).Returns(false);

            //Act
            var result = await _rentalManagerService.CreateAsync(dummmyUserId, InvalidVehicleDtoreateDto);

            //Assert
            result.Errors.Should().Be(RentaltErrors.RentalVehicleDoesNotExist);
        }

        [Fact]
        public async Task CreateAsync_should_Return_VehicleError_When_Vehicle_is_Not_Available()
        {
            //Arrange

            var InvalidVehicleDtoreateDto = new CreateRentalRequestDto
            {
                StartDate = tomorrowDate.ToString(),
                EndDate = futureDate.ToString(),
                VehicleId = dummmyVehicleId
            };

            _vehicleRepoMock.VehicleExistsAsync(Arg.Any<int>()).Returns(true);
            _vehicleRepoMock.VehicleIsAvailableAsync(Arg.Any<int>(),
                                                     Arg.Any<DateOnly>(),
                                                     Arg.Any<DateOnly>()).Returns(false);

            //Act
            var result = await _rentalManagerService.CreateAsync(dummmyUserId, InvalidVehicleDtoreateDto);

            //Assert
            result.Errors.Should().Be(RentaltErrors.RentalVehicleIsNotAvailable);
        }


        [Fact]
        public async Task CreateAsync_should_Return_Rental_When_Valid_Data_Is_Inserted()
        {
       
            var validVehicleDtoreateDto = new CreateRentalRequestDto
            {
                StartDate = tomorrowDate.ToString(),
                EndDate = futureDate.ToString(),
                VehicleId = dummmyVehicleId
            };

            _vehicleRepoMock.VehicleExistsAsync(Arg.Any<int>()).Returns(true);
            _vehicleRepoMock.VehicleIsAvailableAsync(Arg.Any<int>(),
                                                     Arg.Any<DateOnly>(),
                                                     Arg.Any<DateOnly>()).Returns(true);

            //Act
            var result = await _rentalManagerService.CreateAsync(dummmyUserId, validVehicleDtoreateDto);

            //Assert
            result.Errors.Should().Be(null);
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<Rental>();
        }

        //GetByIdAsync Tests
        [Fact]
        public async Task GetByIdAsync_should_Return_Rental_Error_When_Rental_Does_Not_Exist()
        {
            //Arrange
            _rentalRepoMock.GetByIdAsync(Arg.Any<int>()).ReturnsNull();
            //Act
            var result = await _rentalManagerService.GetByIdAsync(dummmyRentalId);
            //Assert
            result.Errors.Should().Be(RentaltErrors.RentalDoesNotExist);
            result.Value.Should().Be(null);
        }
       
        //UpdateAsync Tests
        [Fact]
        public async Task UpdateAsync_should_Return_Rental_Error_When_Rental_Does_Not_Exist()
        {
            //Arrange
            var validUpdateRentalDto = new UpdateRentalRequestDto
            {
                StartDate = tomorrowDate.ToString(),
                EndDate = futureDate.ToString(),

            };
            _rentalRepoMock.GetByIdAsync(Arg.Any<int>()).ReturnsNull();

            //Act
            var result = await _rentalManagerService.UpdateAsync(dummmyRentalId, validUpdateRentalDto);

            //Assert
            result.Errors.Should().Be(RentaltErrors.RentalDoesNotExist);
            result.Value.Should().Be(null);
        }
        [Fact]
        public async Task UpdateAsync_should_Return_StartDateError()
        {
            //Arrange
            var invalidStartDateUpdateRentalDto = new UpdateRentalRequestDto
            {
                StartDate = pastDate.ToString(),
                EndDate = futureDate.ToString(),

            };
            _rentalRepoMock.GetByIdAsync(Arg.Any<int>()).Returns(dummmyRental);
            //Act
            var result = await _rentalManagerService.UpdateAsync(dummmyRentalId, invalidStartDateUpdateRentalDto);

            //Assert
            KeyValuePair<string, string[]> StarDateError = new KeyValuePair<string, string[]>("StartDate", ["StartDate must be set in the future"]);
            result.Errors.Error.Should().ContainKey("StartDate").WhoseValue.Should().BeEquivalentTo(["StartDate must be set in the future"]);
        }
        [Fact]
        public async Task UpdateAsync_should_Return_EndDateError()
        {
            var invalidEndDateUpdateRentalDto = new UpdateRentalRequestDto
            {
                StartDate = futureDate.ToString(),
                EndDate = tomorrowDate.ToString(),

            };
            _rentalRepoMock.GetByIdAsync(Arg.Any<int>()).Returns(dummmyRental);
            //Act
            var result = await _rentalManagerService.UpdateAsync(dummmyRentalId, invalidEndDateUpdateRentalDto);

            //Assert
            KeyValuePair<string, string[]> EndDateError = new KeyValuePair<string, string[]>("EndDate", ["EndDate must be after StartDate"]);
            result.Errors.Error.Should().ContainKey("EndDate").WhoseValue.Should().BeEquivalentTo(["EndDate must be after StartDate"]);

        }
        [Fact]
        public async Task UpdateAsync_should_Return_VehicleAvailablilityError_When_Vehicle_Is_Not_Available()
        {
            var validUpdateRentalDto = new UpdateRentalRequestDto
            {
                StartDate = tomorrowDate.ToString(),
                EndDate = futureDate.ToString(),

            };
            _rentalRepoMock.GetByIdAsync(Arg.Any<int>()).Returns(concludedRental);
            //Act
            var result = await _rentalManagerService.UpdateAsync(concludedRental.Id, validUpdateRentalDto);
            //Assert
            result.Errors.Should().NotBeNull();
            result.Errors.Error.Should().ContainKey("").WhoseValue.Should().Contain(["Vehicle is not available for sugested dates"]);

        }
        [Fact]
        public async Task UpdateAsync_should_Return_RentalStateError_When_Rental_Is_Not_Upcoming()
        {
            var validUpdateRentalDto = new UpdateRentalRequestDto
            {
                StartDate = tomorrowDate.ToString(),
                EndDate = futureDate.ToString(),

            };
            _rentalRepoMock.GetByIdAsync(Arg.Any<int>()).Returns(concludedRental);
           
            //Act
            var result = await _rentalManagerService.UpdateAsync(dummmyRentalId, validUpdateRentalDto);

            //Assert
            result.Errors.Should().NotBeNull();
            result.Errors.Error.Should().ContainKey("").WhoseValue.Should().Contain(["Rental must be upcoming"]);

        }
        [Fact]
        public async Task UpdateAsync_should_Return_Rental_When_Inserted_Data_Is_Valid_and_Vehicle_Is_Available()
        {
            var validUpdateRentalDto = new UpdateRentalRequestDto
            {
                StartDate = upComingRental.BlockedDate.EndDate.AddDays(1).ToString(),
                EndDate = upComingRental.BlockedDate.EndDate.AddDays(3).ToString(),

            };
            _rentalRepoMock.GetByIdAsync(Arg.Any<int>()).Returns(upComingRental);
            //Act
            var result = await _rentalManagerService.UpdateAsync(dummmyRentalId, validUpdateRentalDto);
            //Assert
            result.Errors.Should().BeNull();
            result.Value.Should().BeOfType<Rental>();

        }
    }
}
