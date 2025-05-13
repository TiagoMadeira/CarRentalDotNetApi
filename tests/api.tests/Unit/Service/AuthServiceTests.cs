using api.Dtos.Account;
using api.Dtos.Rentals;
using api.Interfaces;
using api.Models;
using api.Service;
using api.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace api.tests.Unit.Service
{
    public class AuthServiceTests
    {
        private readonly IAuthService _authService;

       
        private readonly UserManager<AppUser> _userManagerMock;
        private readonly IUserRepository _userRepoMock;
        private readonly ITokenService _tokenServiceMock;
        private readonly SignInManager<AppUser> _signInManagerMock;

        //dummmys
        private static readonly string dummmyName = "Zé Teste";
        private static readonly string dummmyEmail = "dummy@Email";
        private static readonly string dummmyPassword = "dummyPassword";
        private static readonly string dummmyToken = "dummyToken";

        //AppUsers
        private static readonly AppUser dummyAppUser = new AppUser { Name = dummmyName, Email = dummmyEmail };

        public AuthServiceTests()
        {
            var _userStoreMock = Substitute.For<IUserStore<AppUser>>();
            var _contextAccessorMock = Substitute.For<IHttpContextAccessor>();
            var _claimFactoryMock = Substitute.For<IUserClaimsPrincipalFactory<AppUser>>();

            _userManagerMock = Substitute.For<UserManager<AppUser>>(_userStoreMock, null, null, null, null, null, null, null, null);
            _tokenServiceMock = Substitute.For<ITokenService>();
            _signInManagerMock = Substitute.For<SignInManager<AppUser>>(_userManagerMock, _contextAccessorMock, _claimFactoryMock, null, null, null, null);
            _userRepoMock = Substitute.For<IUserRepository>();

            _authService = new AuthService(_userManagerMock, _tokenServiceMock, _signInManagerMock, _userRepoMock);
        }

        //LoginAsync Tests
        [Fact]
        public async Task LoginAsync_should_Return_LoginError_When_Email_Is_Not_Found()
        {
            //Arrange
            var defaultLoginRequestDto = new LoginRequestDto
            {
                Email = dummmyEmail,
                Password = dummmyPassword
            };

            _userRepoMock.GetByEmailAsync(Arg.Any<string>()).ReturnsNull();
            //Act
            var result = await _authService.LoginAsync(defaultLoginRequestDto);

            //Assert
            result.Errors.Should().NotBeNull();
            result.Errors.Should().Be(AuthErrors.LoginError);
        }

        [Fact]
        public async Task LoginAsync_should_Return_LoginError_When_Password_Is_Incorrect()
        {
            var defaultLoginRequestDto = new LoginRequestDto
            {
                Email = dummmyEmail,
                Password = dummmyPassword
            };

            //Arrange
            _userRepoMock.GetByEmailAsync(Arg.Any<string>()).Returns(dummyAppUser);
            _signInManagerMock.CheckPasswordSignInAsync(Arg.Any<AppUser>(), Arg.Any<string>(), false).Returns(SignInResult.Failed);

            //Act
            var result = await _authService.LoginAsync(defaultLoginRequestDto);

            //Assert
            result.Errors.Should().NotBeNull();
            result.Errors.Should().Be(AuthErrors.LoginError);
        }

        [Fact]
        public async Task LoginAsync_should_Return_ResultSuccess_With_Successful_Login()
        {
            var defaultLoginRequestDto = new LoginRequestDto
            {
                Email = dummmyEmail,
                Password = dummmyPassword
            };

            //Arrange
            _userRepoMock.GetByEmailAsync(Arg.Any<string>()).Returns(dummyAppUser);
            _signInManagerMock.CheckPasswordSignInAsync(Arg.Any<AppUser>(), Arg.Any<string>(), false).Returns(SignInResult.Success);
            _tokenServiceMock.CreateToken(Arg.Any<AppUser>()).Returns(dummmyToken);

            //Act
            var result = await _authService.LoginAsync(defaultLoginRequestDto);

            //Assert
            result.Errors.Should().BeNull();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<LoginDto>();
            result.Value.Email.Should().Be(dummmyEmail);
            result.Value.Token.Should().Be(dummmyToken);
        }

        //RegisterAsync Tests
        [Fact]
        public async Task RegisterAsync_should_Return_EmailError_When_User_Already_Exists()
        {

            var defaultRegisterRequestDto = new RegisterRequestDto
            {
                Username = dummmyName,
                Email = dummmyEmail,
                Password = dummmyPassword
            }; 
            //Arrange
            _userRepoMock.UserExistsAsync(Arg.Any<string>()).Returns(true);
            //Act
            var result = await _authService.RegisterAsync(defaultRegisterRequestDto);
            //Assert
            result.Errors.Should().NotBeNull();
            result.Errors.Should().Be(AuthErrors.EmailAlreadyExistsrError);
        }
        [Fact]
        public async Task RegisterAsync_should_ReturnSuccess_When_Successfully_Creates_User()
        {
            var defaultRegisterRequestDto = new RegisterRequestDto
            {
                Username = dummmyName,
                Email = dummmyEmail,
                Password = dummmyPassword
            };
            //Arrange
            _userRepoMock.UserExistsAsync(Arg.Any<string>()).Returns(false);
            //Need to create expections for this methods calls
            _userManagerMock.CreateAsync(Arg.Any<AppUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);
            _userManagerMock.AddToRoleAsync(Arg.Any<AppUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

            //Act
            var result = await _authService.RegisterAsync(defaultRegisterRequestDto);
            //Assert
            result.Errors.Should().BeNull();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<AppUserDto>();
            result.Value.Email.Should().Be(dummmyEmail);
            result.Value.UserName.Should().Be(dummmyName);
        }
    }
}
