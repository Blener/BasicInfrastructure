using System.Collections.Generic;
using System.Linq;
using BasicInfrastructure.ParameterHelpers;
using BasicInfrastructurePersistence.Tests.TestEntities;
using Moq;
using Xunit;
using Xunit2.Should;

namespace BasicInfrastructurePersistence.Tests.Helpers
{

    public class RequestParameterTests
    {
        private readonly Mock<Filter<TestContrato>> _mockFilter = new Mock<Filter<TestContrato>>();
        private readonly RequestParameters<TestContrato> _req;
        private readonly IQueryable<TestContrato> _testList = new List<TestContrato>()
        {
            new TestContrato(){ TitularId = 01, Contrato_Id = "01"},
            new TestContrato(){ TitularId = 02, Contrato_Id = "02"},
            new TestContrato(){ TitularId = 03, Contrato_Id = "03"},
            new TestContrato(){ TitularId = 04, Contrato_Id = "04"},
            new TestContrato(){ TitularId = 05, Contrato_Id = "05"},
            new TestContrato(){ TitularId = 06, Contrato_Id = "06"},
            new TestContrato(){ TitularId = 07, Contrato_Id = "07"},
            new TestContrato(){ TitularId = 08, Contrato_Id = "08"},
            new TestContrato(){ TitularId = 09, Contrato_Id = "09"},
            new TestContrato(){ TitularId = 10, Contrato_Id = "10"},
            new TestContrato(){ TitularId = 11, Contrato_Id = "11"},
            new TestContrato(){ TitularId = 12, Contrato_Id = "12"},
            new TestContrato(){ TitularId = 13, Contrato_Id = "13"},
            new TestContrato(){ TitularId = 14, Contrato_Id = "14"},
            new TestContrato(){ TitularId = 15, Contrato_Id = "15"},
            new TestContrato(){ TitularId = 16, Contrato_Id = "16"},
            new TestContrato(){ TitularId = 17, Contrato_Id = "17"},
            new TestContrato(){ TitularId = 18, Contrato_Id = "18"},
            new TestContrato(){ TitularId = 19, Contrato_Id = "19"}
        }.AsQueryable();
        public RequestParameterTests()
        {
            _mockFilter
                .Setup(x => x.GetQuery(It.IsAny<IQueryable<TestContrato>>()))
                .Returns(() => _testList);

            _req = new RequestParameters<TestContrato>()
            {
                Filters = new List<Filter<TestContrato>>() { _mockFilter.Object, }
            };
        }

        [Fact]
        public void MustGetQueryFromRequestParameterWithDefaultPagination()
        {
            var result = _req.GetQuery(_testList);
            _mockFilter.Verify(x => x.GetQuery(_testList), Times.Once);

            result.ShouldBe(_testList.Take(10));
        }

        [Fact]
        public void QueryMustReturnNull()
        {
            var result = _req.GetQuery(null);
            _mockFilter.Verify(x => x.GetQuery(_testList), Times.Never);
            result.ShouldBeNull();
        }

        [Fact]
        public void MustGetQueryFromRequestParameterWithDefaultPaginationAndNullFilter()
        {
            _req.Filters = null;
            var result = _req.GetQuery(_testList);
            _mockFilter.Verify(x => x.GetQuery(_testList), Times.Never);
            result.ShouldBe(_testList.Take(10));


            _req.Filters = new List<Filter<TestContrato>>()
            {
                null
            };
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Take(10));
        }

        [Fact]
        public void MustGetOrderedQuery()
        {
            _req.PageId = 1;
            _req.PerPage = 5;

            _req.SortItems.Add(new SortItem<TestContrato>
            {
                SortDirection = true,
                SortField = "Contrato_Id",
                Priotity = 0
            });

            var result = _req.GetQuery(_testList, true);
            result.ShouldBe(_testList.OrderBy(x=> x.Contrato_Id)
                .Skip( _req.PageId.Value * _req.PerPage.Value)
                .Take(_req.PerPage.Value));
            
            _req.PageCount.ShouldBe(4);

            _req.SortItems.Add(new SortItem<TestContrato>
            {
                SortDirection = false,
                SortField = "Contrato_Id",
                Priotity = 1
        });

            result = _req.GetQuery(_testList, true);
            result.ShouldBe(_testList.OrderByDescending(x=> x.Contrato_Id)
                .Skip(_req.PageId.Value * _req.PerPage.Value)
                .Take(_req.PerPage.Value));

            _req.PageCount.ShouldBe(4);
        }

        [Fact]
        public void MustGetOrderedQueryWithoutCountingItems()
        {
            _req.PageId = 1;
            _req.PerPage = 5;

            _req.SortItems.Add(new SortItem<TestContrato>
            {
                SortDirection = true,
                SortField = "Contrato_Id",
                Priotity = 1
            });
            _req.SortItems.Add(new SortItem<TestContrato>
            {
                SortDirection = true,
                SortField = "NumeroContrato",
                Priotity = 0
            });


            var result = _req.GetQuery(_testList, false);
            result.ShouldBe(_testList.OrderBy(x=>x.NumeroContrato).ThenBy(x=> x.Contrato_Id)
                .Skip( _req.PageId.Value * _req.PerPage.Value)
                .Take(_req.PerPage.Value));
            
            _req.PageCount.ShouldBe(null);


            _req.SortItems.Add(new SortItem<TestContrato>
            {
                SortDirection = false,
                SortField = "Contrato_Id",
                Priotity = 1
            });
            _req.SortItems.Add(new SortItem<TestContrato>
            {
                SortDirection = false,
                SortField = "NumeroContrato",
                Priotity = 0
            });

            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.OrderByDescending(x=> x.NumeroContrato).ThenByDescending(x=> x.Contrato_Id)
                .Skip(_req.PageId.Value * _req.PerPage.Value)
                .Take(_req.PerPage.Value));

            _req.PageCount.ShouldBe(null);
        }
    }
}
