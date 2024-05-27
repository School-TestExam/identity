using Exam.Models.Identity.DTO;
using Exam.Models.Identity.Requests;
using Exam.Services.Identity.IntegrationTests.Setup;
using Exam.Tools.Tests.Extensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
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

        var expected = new IdentityDTO
        {
            Username = username,
            FullName = fullName,
            Email = email
        };

        // Act
        var response = await Client.PostAsJsonAsync(_basePath, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var result = await response.ReadAsAsync<IdentityDTO>();

        result.Should().BeEquivalentTo(expected, opt => opt.Excluding(x => x.Id));
    }

    [Fact]
    public async Task Delete_IdentityExists_DeletesIdentity()
    {
        // Arrange
        var id = Guid.NewGuid();
        var url = $"{_basePath}/{id}";

        var expected = new Models.Entities.Identity.Identity
        {
            Id = id,
            Username = "Tester",
            Email = "test@gmail.com",
            Password = "SuperSecret",
            FullName = "Tester",
            CreatedBy = "SYSTEM",
            LastUpdatedBy = "SYSTEM",
        };

        Context.Identities.Add(expected);
        await Context.SaveChangesAsync();

        // Act
        var response = await Client.DeleteAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var result = await Context.Identities.FirstOrDefaultAsync(x => x.Id == id);
        result.Should().BeNull();
    }

    [Fact]
    public async Task Delete_WrongId_NoChanges()
    {
        // Arrange
        var id = Guid.NewGuid();
        var wrongId = Guid.NewGuid();
        var url = $"{_basePath}/{wrongId}";

        const string username = "Tester";
        const string email = "test@gmail.com";
        const string password = "SuperSecret";
        const string fullName = "Tester";

        var entity = new Models.Entities.Identity.Identity
        {
            Id = id,
            Username = username,
            Email = email,
            Password = password,
            FullName = fullName,
            CreatedBy = "SYSTEM",
            LastUpdatedBy = "SYSTEM",
        };

        var expected = new IdentityDTO
        {
            Username = username,
            FullName = fullName,
            Email = email,
            Id = id,
        };

        Context.Identities.Add(entity);
        await Context.SaveChangesAsync();

        // Act
        var response = await Client.DeleteAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var result = await Context.Identities.FirstOrDefaultAsync(x => x.Id == id);
        result.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task Get_HasIdentity_ReturnsIdentity()
    {
        // Arrange
        var id = Guid.NewGuid();
        var url = $"{_basePath}/{id}";

        const string username = "Tester";
        const string email = "test@gmail.com";
        const string password = "SuperSecret";
        const string fullName = "Tester";

        var entity = new Models.Entities.Identity.Identity
        {
            Id = id,
            Username = username,
            Email = email,
            Password = password,
            FullName = fullName,
            CreatedBy = "SYSTEM",
            LastUpdatedBy = "SYSTEM",
        };

        var expected = new IdentityDTO
        {
            Username = username,
            FullName = fullName,
            Email = email,
            Id = id
        };

        Context.Identities.Add(entity);
        await Context.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.ReadAsAsync<IdentityDTO>();
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Get_WrongId_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var wrongId = Guid.NewGuid();
        var url = $"{_basePath}/{wrongId}";

        const string username = "Tester";
        const string email = "test@gmail.com";
        const string password = "SuperSecret";
        const string fullName = "Tester";

        var entity = new Models.Entities.Identity.Identity
        {
            Id = id,
            Username = username,
            Email = email,
            Password = password,
            FullName = fullName,
            CreatedBy = "SYSTEM",
            LastUpdatedBy = "SYSTEM",
        };

        Context.Identities.Add(entity);
        await Context.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Update_ExistingIdentity_UpdatesIdentity()
    {
        // Arrange
        var id = Guid.NewGuid();
        var url = $"{_basePath}/{id}";

        const string username = "Tester";
        const string email = "test@gmail.com";
        const string password = "SuperSecret";
        const string fullName = "Tester";

        var entity = new Models.Entities.Identity.Identity
        {
            Id = id,
            Username = "WrongTester",
            Email = "wrong@gmail.com",
            Password = "NotSoSecret",
            FullName = "WrongTester",
            CreatedBy = "SYSTEM",
            LastUpdatedBy = "SYSTEM",
        };

        var request = new UpdateRequest
        {
            Email = email,
            FullName = fullName,
            Username = username,
            Password = password,
        };

        var expected = new IdentityDTO
        {
            Username = username,
            FullName = fullName,
            Email = email,
            Id = id,
        };

        Context.Identities.Add(entity);
        await Context.SaveChangesAsync();

        // Act
        var response = await Client.PutAsJsonAsync(url, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.ReadAsAsync<IdentityDTO>();
        result.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task Update_WrongId_NoChangesAndReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var wrongId = Guid.NewGuid();
        var url = $"{_basePath}/{wrongId}";

        const string username = "Tester";
        const string email = "test@gmail.com";
        const string password = "SuperSecret";
        const string fullName = "Tester";

        var entity = new Models.Entities.Identity.Identity
        {
            Id = id,
            Email = email,
            FullName = fullName,
            Username = username,
            Password = password,
            CreatedBy = "SYSTEM",
            LastUpdatedBy = "SYSTEM",
        };

        var request = new UpdateRequest
        {
            Username = "WrongTester",
            Email = "wrong@gmail.com",
            Password = "NotSoSecret",
            FullName = "WrongTester",
        };

        var expected = new IdentityDTO
        {
            Username = username,
            FullName = fullName,
            Email = email,
            Id = id,
        };

        Context.Identities.Add(entity);
        await Context.SaveChangesAsync();

        // Act
        var response = await Client.PutAsJsonAsync(url, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var dbResult = await Context.Identities.FirstOrDefaultAsync(x => x.Id == id);
        dbResult.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
    }

    //[Fact]
    //public async Task Get_WrongId_ThrowsException()
    //{
    //    // Arrange
    //    var id = Guid.NewGuid();
    //    var wrongId = Guid.NewGuid();
    //    var url = $"{_basePath}/{wrongId}";

    //    const string username = "Tester";
    //    const string email = "test@gmail.com";
    //    const string password = "SuperSecret";
    //    const string fullName = "Tester";

    //    var entity = new Models.Entities.Identity.Identity
    //    {
    //        Id = id,
    //        Username = username,
    //        Email = email,
    //        Password = password,
    //        FullName = fullName,
    //        CreatedBy = "SYSTEM",
    //        LastUpdatedBy = "SYSTEM",
    //    };

    //    Context.Identities.Add(entity);
    //    await Context.SaveChangesAsync();

    //    // Act
    //    var act = async () => await Client.GetAsync(url);

    //    // Assert
    //    await act.Should().ThrowAsync<Exception>();
    //}
    //[Fact]
    //public async Task Update_WrongId_NoChangesAndThrowsException()
    //{
    //    // Arrange
    //    var id = Guid.NewGuid();
    //    var wrongId = Guid.NewGuid();
    //    var url = $"{_basePath}/{wrongId}";

    //    const string username = "Tester";
    //    const string email = "test@gmail.com";
    //    const string password = "SuperSecret";
    //    const string fullName = "Tester";

    //    var entity = new Models.Entities.Identity.Identity
    //    {
    //        Id = id,
    //        Email = email,
    //        FullName = fullName,
    //        Username = username,
    //        Password = password,
    //        CreatedBy = "SYSTEM",
    //        LastUpdatedBy = "SYSTEM",
    //    };

    //    var request = new UpdateRequest
    //    {
    //        Username = "WrongTester",
    //        Email = "wrong@gmail.com",
    //        Password = "NotSoSecret",
    //        FullName = "WrongTester",
    //    };

    //    var expected = new IdentityDTO
    //    {
    //        Username = username,
    //        FullName = fullName,
    //        Email = email,
    //        Id = id,
    //    };

    //    Context.Identities.Add(entity);
    //    await Context.SaveChangesAsync();

    //    // Act
    //    var act = async () => await Client.PutAsJsonAsync(url, request);

    //    // Assert
    //    await act.Should().ThrowAsync<Exception>();
    //}
}