using Exam.Models.Identity.DTO;
using Exam.Models.Identity.Requests;
using Exam.Services.Identity.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Exam.Services.Identity.UnitTests.Services
{
    public class IdentityServiceTests : TestBase
    {
        private readonly IIdentityService _service;

        public IdentityServiceTests()
        {
            _service = new IdentityService(Context);
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
            var result = await _service.Create(request);

            // Assert
            result.Should().BeEquivalentTo(expected, opt => opt.Excluding(x => x.Id));
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
        public async Task Get_HasIdentity_ReturnsIdentity()
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
        public async Task Update_ExistingIdentity_UpdatesIdentity()
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
    }
}