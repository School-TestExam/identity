// using Exam.Services.Identity.Persistence;
// using Microsoft.EntityFrameworkCore;
//
// namespace Exam.Services.Identity.UnitTests
// {
//     public class TestBase
//     {
//         protected readonly IdentityContext _context;
//
//         public TestBase()
//         {
//
//             var options = new DbContextOptionsBuilder()
//                 .UseInMemoryDatabase(databaseName: $"identities-{Guid.NewGuid()}")
//                 .UseLowerCaseNamingConvention()
//                 .Options;
//
//             _context = new(options);
//         }
//     }
// }