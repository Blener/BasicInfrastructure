using System.Collections.Generic;
using System.Linq;
using BasicInfrastructure.ParameterHelpers;
using BasicInfrastructure.Persistence;
using BasicInfrastructure.Service;
using BasicInfrastructurePersistence.Tests.TestEntities;
using Moq;
using Xunit;

namespace BasicInfrastructurePersistence.Tests.Data
{
    public class EntityRepositoryTests
    {
        //private readonly Mock<DbSet<TestEntity>> _mockSet = new Mock<DbSet<TestEntity>>();
        //private readonly Mock<AppContext> _mockContext = new Mock<AppContext>("DefaultConnection");
        //private readonly Mock<IRequestParameters<TestEntity>> _mockRequest = new Mock<IRequestParameters<TestEntity>>();
        //private readonly IRepository<TestEntity> _repo;

        //public EntityRepositoryTests()
        //{
        //    _mockContext.Setup(x => x.Set<TestEntity>()).Returns(() => _mockSet.Object);
        //    _mockRequest.Setup(x => x.Filters).Returns(() => new List<Filter<TestEntity>>());
        //    _repo = new Repository<TestEntity, AppContext>(_mockContext.Object);
        //}

        //[Fact]
        //public void ShouldAccessDbSet()
        //{
        //    _repo.GetAll(null);
        //    _mockContext.Verify(x => x.Set<TestEntity>(), Times.Once);
        //}

        //[Fact]
        //public void RepositoryMustGetQueryFromRequestParameter()
        //{
        //    _repo.GetAll(_mockRequest.Object);
        //    _mockRequest.Verify(x => x.GetQuery(It.IsAny<IQueryable<TestEntity>>()), Times.Once());
        //    _mockRequest.Verify(x => x.GetQuery(null), Times.Never);
        //}
    }
}
