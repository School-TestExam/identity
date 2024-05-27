using Exam.Tools.Tests.Containers;
using Xunit;

namespace Exam.Services.Identity.IntegrationTests.Setup;

[CollectionDefinition("mysql")]
public class ContainerCollection : ICollectionFixture<MySQLContainerFixture>
{
    
}