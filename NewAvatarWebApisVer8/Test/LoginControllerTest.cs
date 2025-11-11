using Xunit;
using System.Net;
using System.Text;
using System.Text.Json;
using NewAvatarWebApis.Core.Domain.Common.NewAvatarWebApis.Models;
using Microsoft.AspNetCore.Mvc.Testing;


namespace NewAvatarWebApis.Test
{
    public class LoginControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        LoginControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_Login_V1_Returns_Success()
        {

            var client = _factory.CreateClient();
            var loginParams = new LoginParams {
                email = "testuser@kisan.com",
                password = "password123"
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(loginParams), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/v1/Login/login", jsonContent);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async Task Post_Login_V1_On_V2_Url_Returns_NotFound()
        {
            var client = _factory.CreateClient();
            var loginParams = new LoginParams {
                email = "itsupport@kisna.com",
                password = "password123"
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(loginParams), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/v2/Login/login", jsonContent);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
