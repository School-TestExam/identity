using Exam.Models.Identity.DTO;
using Exam.Models.Identity.Requests;
using Exam.Services.Identity.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Exam.Services.Identity.UnitTests.Services;

public class IdentityServiceTests : TestBase
{
    private readonly IIdentityService _service;

    public IdentityServiceTests()
    {
        _service = new IdentityService(Context);
    }

    [Fact]
    public async Task Create_BadRequest_ThrowsException()
    {
        // Arrange
        const string username = "Tester";
        const string password = "SuperSecret";

        var request = new CreateRequest
        {
            Username = username,
            Password = password,
        };

        var expected = new IdentityDTO
        {
            Username = username,
        };

        // Act
        var act = async () => await _service.Create(request);

        // Assert
        await act.Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task Create_NoCreatedByData_SavesEntityAsSystem()
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
            Email = email,
        };

        // Act
        var result = await _service.Create(request);

        // Assert
        result.Should().BeEquivalentTo(expected, opt => opt.Excluding(x => x.Id));

        var createdByResult = await Context.Identities.FirstOrDefaultAsync(x => x.Username == username);
        createdByResult.CreatedBy.Should().Be("SYSTEM");
    }

    [Fact]
    public async Task Create_ValidRequest_SavesEntity()
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
            CreatedBy = username,
        };

        var expected = new IdentityDTO
        {
            Username = username,
            FullName = fullName,
            Email = email,
        };

        // Act
        var result = await _service.Create(request);

        // Assert
        result.Should().BeEquivalentTo(expected, opt => opt.Excluding(x => x.Id));

        var createdByResult = await Context.Identities.FirstOrDefaultAsync(x => x.Username == username);
        createdByResult.CreatedBy.Should().Be(username);
    }

    [Fact]
    public async Task Delete_IdentityExists_DeletesIdentity()
    {
        // Arrange
        var id = Guid.NewGuid();

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
        await _service.Delete(id);

        // Assert
        var result = await Context.Identities.FirstOrDefaultAsync(x => x.Id == id);
        result.Should().BeNull();
    }

    [Fact]
    public async Task Delete_WrongId_ThrowsException()
    {
        // Arrange
        var savedId = Guid.NewGuid();
        var requestId = Guid.NewGuid();

        var expected = new Models.Entities.Identity.Identity
        {
            Id = savedId,
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
        var act = async () => await _service.Delete(requestId);

        // Assert
        await act.Should().ThrowAsync<Exception>($"Couldn't find entity with id: {requestId}");

        var result = await Context.Identities.FirstOrDefaultAsync(x => x.Id == savedId);
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Get_IdentityExists_ReturnsIdentity()
    {
        // Arrange
        var id = Guid.NewGuid();

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
        var result = await _service.Get(id);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Get_WrongId_ThrowsException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var wrongId = Guid.NewGuid();

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
        var act = async () => await _service.Get(wrongId);

        // Assert
        await act.Should().ThrowAsync<Exception>($"Couldn't find entity with id: {wrongId}");
    }

    [Fact]
    public async Task Update_IdentityExists_UpdatesIdentity()
    {
        // Arrange
        var id = Guid.NewGuid();

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
        var result = await _service.Update(id, request);

        // Assert
        result.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
    }

    [Fact]
    public async Task Update_WrongId_ThrowsException()
    {
        // Arrange
        var savedId = Guid.NewGuid();
        var requestId = Guid.NewGuid();

        const string username = "Tester";
        const string email = "test@gmail.com";
        const string password = "SuperSecret";
        const string fullName = "Tester";

        var entity = new Models.Entities.Identity.Identity
        {
            Id = savedId,
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

        Context.Identities.Add(entity);
        await Context.SaveChangesAsync();

        // Act
        var act = async () => await _service.Update(requestId, request);

        // Assert
        await act.Should().ThrowAsync<Exception>($"Couldn't find entity with id: {requestId}");
    }
}