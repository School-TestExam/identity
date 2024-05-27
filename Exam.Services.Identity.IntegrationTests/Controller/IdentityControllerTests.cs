using Exam.Models.Identity.Requests;
using Exam.Models.Identity.Responses;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace Exam.Services.Identity.IntegrationTests.Controller
{
    public class IdentityControllerTests : TestBase
    {
        public IdentityControllerTests(ApiWebApplicationFactory webFactory) : base(webFactory)
        {
        }

        [Fact]
        public async Task Create_NoCreatedByData_CreatesIdentityAsSystem()
        {
            // Arrange
            var expected = new CreateRequest
            {
                Username = "Test",
            };
            var json = JsonConvert.SerializeObject(expected);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestUri = $"/api/v1/Identity/";

            // Act
            var response = await _client.PostAsync(requestUri, content);
            response.EnsureSuccessStatusCode();

            // Assert
            var result = await _context.Identities.FirstOrDefaultAsync(x => x.Username == expected.Username);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            result.Username.Should().BeEquivalentTo(expected.Username);
            result.CreatedBy.Should().BeEquivalentTo("SYSTEM");
        }

        [Fact]
        public async Task Delete_IdentityExists_DeletesIdentity()
        {
            // Arrange
            var expected = new Models.Entities.Identity.Identity
            {
                Id = Guid.NewGuid(),
                Username = "Test",
            };

            _context.Identities.Add(expected);
            await _context.SaveChangesAsync();

            var requestUri = $"/api/v1/Identity/{expected.Id}";

            // Act
            var response = await _client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();

            // Assert
            var result = await _context.Identities.FirstOrDefaultAsync(x => x.Id == expected.Id);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            result.Should().BeNull();
        }

        [Fact]
        public async Task Delete_WrongId_NoChanges()
        {
            // Arrange
            var expected = new Models.Entities.Identity.Identity
            {
                Id = Guid.NewGuid(),
                Username = "Test",
            };

            _context.Identities.Add(expected);
            await _context.SaveChangesAsync();

            var requestUri = $"/api/v1/Identity/{Guid.NewGuid()}";

            // Act
            var response = await _client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();

            // Assert
            var result = await _context.Identities.FirstOrDefaultAsync(x => x.Id == expected.Id);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            result.Username.Should().BeEquivalentTo(expected.Username);
        }

        [Fact]
        public async Task Get_HasIdentity_ReturnsIdentity()
        {
            // Arrange
            var expected = new Models.Entities.Identity.Identity
            {
                Id = Guid.NewGuid(),
                Username = "Test",
            };

            _context.Identities.Add(expected);
            await _context.SaveChangesAsync();

            var requestUri = $"/api/v1/Identity/{expected.Id}";

            // Act
            var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetResponse>(content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Username.Should().BeEquivalentTo(expected.Username);
        }

        [Fact]
        public async Task Get_WrongId_ReturnsNotFound()
        {
            // Arrange
            var identity = new Models.Entities.Identity.Identity
            {
                Id = Guid.NewGuid(),
                Username = "Test",
            };

            _context.Identities.Add(identity);
            await _context.SaveChangesAsync();

            var requestUri = $"/api/v1/Identity/{Guid.NewGuid()}";

            // Act
            var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetResponse>(content);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.Should().BeNull();
        }

        [Fact]
        public async Task Update_ExistingIdentity_UpdatesIdentity()
        {
            // Arrange
            var identity = new Models.Entities.Identity.Identity
            {
                Id = Guid.NewGuid(),
                Username = "Test",
            };

            _context.Identities.Add(identity);
            await _context.SaveChangesAsync();

            var expected = new UpdateRequest
            {
                Id = identity.Id,
                Username = "Updated"
            };

            var json = JsonConvert.SerializeObject(expected);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestUri = $"/api/v1/Identity/{identity.Id}";

            // Act
            var response = await _client.PutAsync(requestUri, content);
            response.EnsureSuccessStatusCode();

            // Assert
            var result = await _context.Identities.FirstOrDefaultAsync(x => x.Username == expected.Username);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            result.Username.Should().BeEquivalentTo(expected.Username);
            result.Username.Should().NotBeEquivalentTo(identity.Username);
        }

        [Fact]
        public async Task Update_WrondId_NoChanges()
        {
            // Arrange
            var expected = new Models.Entities.Identity.Identity
            {
                Id = Guid.NewGuid(),
                Username = "Test",
            };

            _context.Identities.Add(expected);
            await _context.SaveChangesAsync();

            var request = new UpdateRequest
            {
                Id = Guid.NewGuid(),
                Username = "Updated"
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestUri = $"/api/v1/Identity/{expected.Id}";

            // Act
            var response = await _client.PutAsync(requestUri, content);
            response.EnsureSuccessStatusCode();

            // Assert
            var result = await _context.Identities.FirstOrDefaultAsync(x => x.Username == expected.Username);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            result.Username.Should().BeEquivalentTo(expected.Username);
            result.Username.Should().NotBeEquivalentTo(request.Username);
        }
    }
}