using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using api.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace api.tests.Integration
{
    public class RentalControllerTests : IClassFixture<CustomApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomApplicationFactory _factory;

        public RentalControllerTests(CustomApplicationFactory factory)
       
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetById_RentalDoesNotExists_ReturnResultSuccessWithRental()
        {
            var RentalId = 1;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
           
            var response = await _client.GetAsync(string.Format("/api/Rental/{0}", RentalId));
            //var result = await response.Content.ReadFromJsonAsync<RentalDto>();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            //result.Should().NotBeNull();
        }
    }
}
