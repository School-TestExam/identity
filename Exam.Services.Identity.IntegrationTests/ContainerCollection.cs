using Exam.Tools.Tests.Containers;
using Xunit;

namespace Exam.Services.Identity.IntegrationTests
{
    [CollectionDefinition("mysql")]
    public class ContainerCollection : ICollectionFixture<MySQLContainerFixture>
    {

    }
}
