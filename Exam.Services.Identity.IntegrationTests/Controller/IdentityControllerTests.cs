using Exam.Models.Identity.Requests;
using Exam.Models.Identity.Responses;
using Exam.Tools.Tests.Extensions;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Exam.Models.Identity.DTO;
using Exam.Services.Identity.IntegrationTests.Setup;
using Xunit;

namespace Exam.Services.Identity.IntegrationTests.Controller;

public class IdentityControllerTests : TestBase
{
    private const string _basePath = $"api/v1/identity";

    public IdentityControllerTests(ApiWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_NoCreatedByData_CreatesIdentityAsSystem()
    {
        // Arrange
        const string username = "Tester";
        const string email = "test@gmail.com";
        const string password = "SuperSecret";
        const string fullName = "Tester";
        
        var request = new CreateRequest
        {
            Username = username,
            FullName = fullName,
            Email = email,
            Password = password,
        };

        var expectation = new IdentityDTO
        {
            Username = username,
            FullName = fullName,
            Email = email
        };

        // Act
        var response = await Client.PostAsJsonAsync(_basePath, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var result = await response.ReadAsAsync<GetResponse>();

        result.Should().BeEquivalentTo(expectation, opt => opt.Excluding(x => x.Id));
    }

    //     [Fact]
    //     public async Task Delete_IdentityExists_DeletesIdentity()
    //     {
    //         // Arrange
    //         var id = Guid.NewGuid();
    //         var url = $"{_basePath}/{id}";
    //
    //         var expected = new Models.Entities.Identity.Identity
    //         {
    //             Id = id,
    //             Username = "Test",
    //         };
    //
    //         _context.Identities.Add(expected);
    //         await _context.SaveChangesAsync();
    //
    //         // Act
    //         var response = await _client.DeleteAsync(url);
    //
    //         // Assert
    //         response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    //
    //         var result = await _context.Identities.FirstOrDefaultAsync(x => x.Id == id);
    //         result.Should().BeNull();
    //     }
    //
    //     [Fact]
    //     public async Task Delete_WrongId_NoChanges()
    //     {
    //         // Arrange
    //         var id = Guid.NewGuid();
    //         var url = $"{_basePath}/{Guid.NewGuid()}";
    //
    //         var expected = new Models.Entities.Identity.Identity
    //         {
    //             Id = id,
    //         };
    //
    //         _context.Identities.Add(expected);
    //         await _context.SaveChangesAsync();
    //
    //         // Act
    //         var response = await _client.DeleteAsync(url);
    //
    //         // Assert
    //         var result = await _context.Identities.FirstOrDefaultAsync(x => x.Id == expected.Id);
    //
    //         response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    //         result.Should().NotBeNull();
    //     }
    //
    //     [Fact]
    //     public async Task Get_HasIdentity_ReturnsIdentity()
    //     {
    //         // Arrange
    //         var id = Guid.NewGuid();
    //         var url = $"{_basePath}/{id}";
    //
    //         const string expectedUsername = "Test";
    //
    //         var expected = new Models.Entities.Identity.Identity
    //         {
    //             Id = id,
    //             Username = expectedUsername,
    //         };
    //
    //         _context.Identities.Add(expected);
    //         await _context.SaveChangesAsync();
    //
    //         // Act
    //         var response = await _client.GetAsync(url);
    //
    //         // Assert
    //         response.StatusCode.Should().Be(HttpStatusCode.OK);
    //
    //         var content = await response.Content.ReadAsStringAsync();
    //         var result = JsonConvert.DeserializeObject<GetResponse>(content);
    //         result.Username.Should().BeEquivalentTo(expectedUsername);
    //     }
    //
    //     [Fact]
    //     public async Task Get_WrongId_ReturnsNotFound()
    //     {
    //         // Arrange
    //         var id = Guid.NewGuid();
    //         var url = $"{_basePath}/{Guid.NewGuid()}";
    //
    //         var expected = new Models.Entities.Identity.Identity
    //         {
    //             Id = id,
    //         };
    //
    //         _context.Identities.Add(expected);
    //         await _context.SaveChangesAsync();
    //
    //         // Act
    //         var response = await _client.GetAsync(url);
    //
    //         // Assert
    //         response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    //
    //         var content = await response.Content.ReadAsStringAsync();
    //         var result = JsonConvert.DeserializeObject<GetResponse>(content);
    //         result.Should().BeNull();
    //     }
    //
    //     [Fact]
    //     public async Task Update_ExistingIdentity_UpdatesIdentity()
    //     {
    //         // Arrange
    //         var id = Guid.NewGuid();
    //         var url = $"{_basePath}/{id}";
    //         var expectedUsername = "updated";
    //
    //         var identity = new Models.Entities.Identity.Identity
    //         {
    //             Id = id,
    //             Username = "test",
    //         };
    //
    //         _context.Identities.Add(identity);
    //         await _context.SaveChangesAsync();
    //
    //         var request = new UpdateRequest
    //         {
    //             Id = id,
    //             Username = expectedUsername
    //         };
    //
    //         // Act
    //         var response = await _client.PutAsJsonAsync(url, request);
    //
    //         // Assert
    //         response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    //
    //         var content = await response.Content.ReadAsStringAsync();
    //         var result = JsonConvert.DeserializeObject<GetResponse>(content);
    //         result.Username.Should().BeEquivalentTo(expectedUsername);
    //     }
    //
    //     [Fact]
    //     public async Task Update_WrondId_NoChanges()
    //     {
    //         // Arrange
    //         var id = Guid.NewGuid();
    //         var wrongId = Guid.NewGuid();
    //         var url = $"{_basePath}/{wrongId}";
    //         var expectedUsername = "test";
    //
    //         var identity = new Models.Entities.Identity.Identity
    //         {
    //             Id = id,
    //             Username = expectedUsername,
    //         };
    //
    //         _context.Identities.Add(identity);
    //         await _context.SaveChangesAsync();
    //
    //         var request = new UpdateRequest
    //         {
    //             Id = wrongId,
    //             Username = "updated"
    //         };
    //
    //         // Act
    //         var response = await _client.PutAsJsonAsync(url, request);
    //
    //         // Assert
    //         response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    //


    //         var databaseResult = await _context.Identities.FirstOrDefaultAsync(x => x.Id == id);

    //         databaseResult.Username.Should().BeEquivalentTo(expectedUsername);

    //     }
}