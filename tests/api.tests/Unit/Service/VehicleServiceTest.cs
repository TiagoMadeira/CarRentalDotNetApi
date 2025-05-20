using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Dtos.Rentals;
using api.Dtos.Vehicles;
using api.Helpers;
using api.Interfaces;
using api.Models;
using api.Service;
using api.Shared;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace api.tests.Unit.Service
{
    public class VehicleServiceTest
    {
        private static IVehicleRepository _vehicleRepoMock;
        private static IVehicleService _vehicleService;

        private static string dummyUserId = "DummyUserID";
        public VehicleServiceTest()
        {
            //Initialize mocks
            _vehicleRepoMock = Substitute.For<IVehicleRepository>();
            _vehicleService = new VehicleService(_vehicleRepoMock);


        }
        //Create Vehicle Tests
        [Fact]
        public async Task CreateAsync_should_Return_Error_When_Brand_Is_Not_Provided()
        {
            //Arrange    
            var InvalidCreateDto = new CreateVehicleRequestDto
            {
                Model = "DummyModel",
                Brand = null
            };

            //Act
            var result = await _vehicleService.CreateAsync(dummyUserId, InvalidCreateDto);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Error.Should().ContainKey("Brand").WhoseValue.Should().BeEquivalentTo(["Vehcile must have brand"]);
        }
        [Fact]
        public static async Task CreateAsync_should_Return_Error_When_Model_Is_Not_Provided()
        {
            //Arrange   
            var InvalidCreateDto = new CreateVehicleRequestDto
            {
                Model = null,
                Brand = "Dummy Brand"
            };

            //Act
            var result = await _vehicleService.CreateAsync(dummyUserId, InvalidCreateDto);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Error.Should().ContainKey("Model").WhoseValue.Should().BeEquivalentTo(["Vehcile must have model"]);

        }
        [Fact]
        public static async Task CreateAsync_should_Return_Error_When_UserId_Is_Not_Provided()
        {
            //Arrange
            var ValidCreateDto = new CreateVehicleRequestDto
            {
                Model = "Dummy Model",
                Brand = "Dummy Brand"
            };

            //Act
            var result = await _vehicleService.CreateAsync(null, ValidCreateDto);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Error.Should().ContainKey("AppUserId").WhoseValue.Should().BeEquivalentTo(["Vehicle must have an associated AppUserId"]);

        }
        [Fact]
        public static async Task CreateAsync_should_Return_Success_When_Valid_Dto_Is_Provided()
        {
            //Arrange
            var testVehicle = new Vehicle { Model = "Dummy Model", Brand = "Dummy Brand", AppUserId = dummyUserId };
            var ValidCreateDto = new CreateVehicleRequestDto
            {
                Model = "Dummy Model",
                Brand = "Dummy Brand"
            };

            _vehicleRepoMock.CreateAsync(Arg.Any<Vehicle>()).Returns(testVehicle);

            //Act
            var result = await _vehicleService.CreateAsync(dummyUserId, ValidCreateDto);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().Be(testVehicle);

        }
        //Test GetByIdAsync
        [Fact]
        public async Task GetByIdAsync_should_Return_Error_VehicleDoesNotExist()
        {
            //Arrange
            var dummyId = 2;
            _vehicleRepoMock.GetByIdAsync(Arg.Any<int>()).ReturnsNull();
            //Act
            var result = await _vehicleService.GetByIdAsync(dummyId);
            //Assert
            result.IsSuccess!.Should().BeFalse();
            result.Errors.Should().NotBeNull();
            result.Errors.Should().Be(VehicleErrors.VehicleDoesExistError);
        }

        [Fact]
        public static async Task GetByIdAsync_should_Return_Vehicle_If_It_Exists()
        {
            //Arrange
            var testVehicle = new Vehicle { Model = "Dummy Model", Brand = "Dummy Brand", AppUserId = dummyUserId };
            var dummyId = 2;
            _vehicleRepoMock.GetByIdAsync(Arg.Any<int>()).Returns(testVehicle);

            //Act
            var result = await _vehicleService.GetByIdAsync(dummyId);

            //Assert
            result.IsSuccess!.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().Be(testVehicle);
        }

    }
}
