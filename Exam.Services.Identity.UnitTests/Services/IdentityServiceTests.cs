// using Exam.Models.Identity.Requests;
// using Exam.Services.Identity.Services;
// using FluentAssertions;
// using Microsoft.EntityFrameworkCore;
// using Xunit;
//
// namespace Exam.Services.Identity.UnitTests.Services
// {
//     public class IdentityServiceTests : TestBase
//     {
//         private readonly IIdentityService _service;
//
//         public IdentityServiceTests()
//         {
//             _service = new IdentityService(_context);
//         }
//
//         [Fact]
//         public async Task Create_NoCreatedByData_CreatesIdentityAsSystem()
//         {
//             // Arrange
//             var request = new CreateRequest
//             {
//                 Username = "testUser",
//                 Email = "testEmail",
//             };
//
//             // Act
//             var response = await _service.Create(request);
//
//             // Assert
//             response.Should().NotBe(Guid.Empty);
//         }
//
//         [Fact]
//         public async Task Delete_IdentityExists_DeletesIdentity()
//         {
//             // Arrange
//             var identity = new Models.Entities.Identity.Identity
//             {
//                 Id = Guid.NewGuid(),
//                 Username = "test",
//             };
//
//             // Act
//             await _service.Delete(identity.Id);
//
//             // Assert
//             var result = await _context.Identities.FirstOrDefaultAsync(x => x.Username == identity.Username);
//
//             result.Should().BeNull();
//         }
//
//         [Fact]
//         public async Task Get_IdentityExists_ReturnsIdentity()
//         {
//             // Arrange
//             var expected = new Models.Entities.Identity.Identity
//             {
//                 Id = Guid.NewGuid(),
//                 Username = "test",
//             };
//
//             var request = new GetRequest
//             {
//                 Id = expected.Id,
//             };
//
//             // Act
//             var result = await _service.Get(request);
//
//             // Assert
//             result.Username.Should().BeEquivalentTo(expected.Username);
//         }
//
//         [Fact]
//         public async Task Update_IdentityExists_UpdatesIdentity()
//         {
//             // Arrange
//             var identity = new Models.Entities.Identity.Identity
//             {
//                 Id = Guid.NewGuid(),
//                 Username = "test",
//             };
//
//             var expected = new UpdateRequest
//             {
//                 Id = identity.Id,
//                 Username = "updated",
//             };
//
//             // Act
//             await _service.Update(expected);
//
//             // Assert
//             var result = await _context.Identities.FirstOrDefaultAsync(x => x.Id == identity.Id);
//
//             result.Username.Should().BeEquivalentTo(expected.Username);
//         }
//     }
// }