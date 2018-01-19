using System;
using System.Collections.Generic;
using System.Linq;
using BasicInfrastructure.Extensions;
using BasicInfrastructure.ParameterHelpers;
using BasicInfrastructurePersistence.Tests.TestEntities;
using Xunit;
using Xunit2.Should;

namespace BasicInfrastructurePersistence.Tests.Helpers
{
    /// <summary>
    /// Classe para teste de Filter
    /// </summary>
    public class FilterTests
    {
        private readonly RequestParameters<TestContrato> _req = new RequestParameters<TestContrato>();
        private readonly IQueryable<TestContrato> _testList = new List<TestContrato>()
        {
            new TestContrato(){ TitularId = 01, Contrato_Id = "Contrato 01", DateCreated = DateTime.Today},
            new TestContrato(){ TitularId = 02, Contrato_Id = "Contrato 02", DateCreated = DateTime.Today.AddMinutes(1)},
            new TestContrato(){ TitularId = 03, Contrato_Id = "Contrato 03", DateCreated = DateTime.Today.AddMinutes(2)},
            new TestContrato(){ TitularId = 04, Contrato_Id = "Contrato 04", DateCreated = DateTime.Today.AddMinutes(3)},
            new TestContrato(){ TitularId = 05, Contrato_Id = "Contrato 05", DateCreated = DateTime.Today.AddMinutes(4)},
            new TestContrato(){ TitularId = 06, Contrato_Id = "Contrato 06", DateCreated = DateTime.Today.AddMinutes(5)},
            new TestContrato(){ TitularId = 07, Contrato_Id = "Contrato 07", DateCreated = DateTime.Today.AddMinutes(6)},
            new TestContrato(){ TitularId = 08, Contrato_Id = "Contrato 08", DateCreated = DateTime.Today.AddMinutes(7)},
            new TestContrato(){ TitularId = 09, Contrato_Id = "Contrato 09", DateCreated = DateTime.Today.AddMinutes(8)},
            new TestContrato(){ TitularId = 10, Contrato_Id = "Contrato 10", DateCreated = DateTime.Today.AddMinutes(9)},
            new TestContrato(){ TitularId = 11, Contrato_Id = "Contrato 11", DateCreated = DateTime.Today.AddMinutes(10)},
            new TestContrato(){ TitularId = 12, Contrato_Id = "Contrato 12", DateCreated = DateTime.Today.AddMinutes(11)},
            new TestContrato(){ TitularId = 13, Contrato_Id = "Contrato 13", DateCreated = DateTime.Today.AddMinutes(12)},
            new TestContrato(){ TitularId = 14, Contrato_Id = "Contrato 14", DateCreated = DateTime.Today.AddMinutes(13)},
            new TestContrato(){ TitularId = 15, Contrato_Id = "Contrato 15", DateCreated = DateTime.Today.AddMinutes(14)},
            new TestContrato(){ TitularId = 16, Contrato_Id = "Contrato 16", DateCreated = DateTime.Today.AddMinutes(15)},
            new TestContrato(){ TitularId = 17, Contrato_Id = "Contrato 17", DateCreated = DateTime.Today.AddMinutes(16)},
            new TestContrato(){ TitularId = 18, Contrato_Id = "Contrato 18", DateCreated = DateTime.Today.AddMinutes(17)},
            new TestContrato(){ TitularId = 19, Contrato_Id = "Contrato 19", DateCreated = DateTime.Today.AddMinutes(18)}
        }.AsQueryable();
        
        public FilterTests()
        {
            //Ignorando a paginação
            _req.PerPage = _testList.Count();
        }

        /// <summary>
        /// Teste FilterByGreatherThanOrLessThan
        /// </summary>
        [Fact]
        public void MustFilterByGreatherThanOrLessThan()
        {
            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "TitularId",
                    Operation = "lt",
                    Value = "10"
                },
                new Filter<TestContrato>()
                {
                    Field = "TitularId",
                    Operation = "gt",
                    Value = "5"
                }
            };

            // Operação lt(lessThan) 
            var result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.TitularId > 5 && x.TitularId < 10));

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "TitularId",
                    Operation = "lte",
                    Value = "10"
                },
                new Filter<TestContrato>()
                {
                    Field = "TitularId",
                    Operation = "gte",
                    Value = "5"
                }
            };

            // Operação lte (lessThanOrEqual)  
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.TitularId >= 5 && x.TitularId <= 10));
        }

        /// <summary>
        /// Teste MustFilterNumberParameterByStartsWith deve filtrar começando por um número
        /// </summary>
        [Fact]
        public void MustFilterNumberParameterByStartsWith()
        {
            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "TitularId",
                    Operation = "sw",
                    Value = "1"
                }
            };

            // Operação sw (startsWith)
            var result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.TitularId.ToString().StartsWith("1")));
            result.Count().ShouldBe(11);
        }

        /// <summary>
        /// Teste MustFilterStringParameterByStartsWith deve filtrar uma string começando por letras
        /// </summary>
        [Fact]
        public void MustFilterStringParameterByStartsWith()
        {
            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "sw",
                    Value = "Cont"
                }
            };

            // Operação sw (startsWith)  Cont(maiúsculo)
            var result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().StartsWith("Cont")));
            result.Count().ShouldBe(19);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "sw",
                    Value = "cont"
                }
            };

            // Operação sw (startsWith) cont(minúsculo)
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().StartsWith("cont")));
            result.Count().ShouldBe(0);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "swi",
                    Value = "Cont"
                }
            };

            // Operação swi (startsWithInsensitive)  
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().StartsWith("Cont")));
            result.Count().ShouldBe(19);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "swi",
                    Value = "cont"
                }
            };
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().StartsWith("cont", StringComparison.InvariantCultureIgnoreCase)));
            result.Count().ShouldBe(19);
        }

        /// <summary>
        /// Teste MustFilterNumberByEndsWith deve filtrar pelo fim com um número 
        /// </summary>
        [Fact]
        public void MustFilterNumberByEndsWith()
        {
            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "TitularId",
                    Operation = "ew",
                    Value = "1"
                }
            };
            // Operação ew (endsWith)
            var result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.TitularId.ToString().EndsWith("1")));
            result.Count().ShouldBe(2);
        }

        /// <summary>
        /// Teste MustFilterStringByEndsWith deve filtrar pelo fim da string com letras
        /// </summary>
        [Fact]
        public void MustFilterStringByEndsWith()
        {
            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "ew",
                    Value = "to 01"
                }
            };
            // Operação ew (endsWith)
            var result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().EndsWith("to 01")));
            result.Count().ShouldBe(1);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "ew",
                    Value = "TO 01"
                }
            };
            // Operação ew (endsWith)
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().EndsWith("TO 01")));
            result.Count().ShouldBe(0);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "ewi",
                    Value = "to 01"
                }
            };
            // Operação ewi (endsWithInsensitive) 
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().EndsWith("to 01", StringComparison.InvariantCultureIgnoreCase)));
            result.Count().ShouldBe(1);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "ewi",
                    Value = "TO 01"
                }
            };
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().EndsWith("TO 01", StringComparison.InvariantCultureIgnoreCase)));
            result.Count().ShouldBe(1);
        }

        /// <summary>
        /// Teste MustFilterNumberByContains deve conter um número dentro da string 
        /// </summary>
        [Fact]
        public void MustFilterNumberByContains()
        {

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "TitularId",
                    Operation = "ct",
                    Value = "2"
                }
            };
            // Operação ct (contains)
            var result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.TitularId.ToString().Contains("2")));
            result.Count().ShouldBe(2);
        }

        /// <summary>
        /// Teste MustFilterStringByContains deve conter uma string especificada 
        /// </summary>
        [Fact]
        public void MustFilterStringByContains()
        {

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "ct",
                    Value = "trato"
                }
            };
            // Operação ct (contains)
            var result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().Contains("trato")));
            result.Count().ShouldBe(19);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "ct",
                    Value = "trato 01"
                }
            };
            // Operação ct (contains)
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().Contains("trato 01")));
            result.Count().ShouldBe(1);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "ct",
                    Value = "Trato 01"
                }
            };
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().Contains("Trato 01")));
            result.Count().ShouldBe(0);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "cti",
                    Value = "trato 01"
                }
            };
            // Operação cti (containsInsensitive)
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().ToLower().Contains("trato 01".ToLower())));
            result.Count().ShouldBe(1);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "cti",
                    Value = "TrATo"
                }
            };
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id.ToString().ToLower().Contains("TrATo".ToLower())));
            result.Count().ShouldBe(19);
        }

        /// <summary>
        /// Teste NumberMustEqual número deve ser igual 
        /// </summary>
        [Fact]
        public void NumberMustEqual()
        {
            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "TitularId",
                    Operation = "eq",
                    Value = "1"
                }
            };
            // Operação eq (equals)
            var result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.TitularId.ToString().Equals("1")));
            result.Count().ShouldBe(1);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "TitularId",
                    Operation = "eqn",
                    Value = "1"
                }
            };
            // Operação eqn (equalsNumber) igual ao um número 
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x =>
                Math.Abs("1".ToDouble(null) - x.TitularId.Value) < 0.0000000000001));
            result.Count().ShouldBe(1);
        }

        /// <summary>
        /// Deve MustFilterByString filtrar por uma string com equals 
        /// </summary>
        [Fact]
        public void MustFilterByString()
        {
            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "eq",
                    Value = "Contrato 10"
                }
            };
            // Operação eq (equals)
            var result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id == "Contrato 10"));
            result.Count().ShouldBe(1);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = "eqi",
                    Value = "contrato 10"
                }
            };
            // Operação eqi (equalsInsentitive)
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.Contrato_Id == "Contrato 10"));
            result.Count().ShouldBe(1);
        }

        /// <summary>
        /// Teste MustThrowArgumentException deve retornar uma exception ArgumentNullException
        /// </summary>
        [Fact]
        public void MustThrowArgumentException()
        {
            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "TitularId",
                    Value = "10"
                }
            };
            Assert.Throws<ArgumentNullException>(() => _req.GetQuery(_testList));
            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Operation = "eq",
                    Value = "10"
                }
            };
            Assert.Throws<ArgumentNullException>(() => _req.GetQuery(_testList));
        }

        /// <summary>
        /// Teste MustThrowArgumentOutOfRangeExceptionOnInvalidOperation deve retornar uma exception de ArgumentOutOfRangeException
        /// </summary>
        /// <param name="operation">tipo de operação do filtro</param>
        [Theory]
        [InlineData("InvalidOperation")]
        [InlineData("")]
        public void MustThrowArgumentOutOfRangeExceptionOnInvalidOperation(string operation)
        {
            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "Contrato_Id",
                    Operation = operation,
                    Value = "Contrato 01"
                }
            };
            Assert.Throws<ArgumentOutOfRangeException>(() => _req.GetQuery(_testList));
        }

        /// <summary>
        /// Teste MustReturnQueryIfPropertyNameIsMistyped teste propriedade incorreta
        /// </summary>
        [Fact]
        public void MustReturnQueryIfPropertyNameIsMistyped()
        {

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "NotAProperty",
                    Operation = "eq",
                    Value = "10"
                }
            };
            // Operação eq (equals)
            _req.GetQuery(_testList).ShouldBe(_testList);
        }

        /// <summary>
        /// Teste MustFilterByBefore filtro before(antes) com data
        /// </summary>
        [Fact]
        public void MustFilterByBefore()
        {
            var target = DateTime.Today.AddMinutes(10);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "DateCreated",
                    Operation = "bf",
                    Value = target.ToString("O")
                }
            };
            // Operação bf (before)
            var result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.DateCreated.CompareTo(target) < 0));
            result.Count().ShouldBe(10);
            
            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "DateCreated",
                    Operation = "bfi",
                    Value = target.ToString("O")
                }
            };
            // Operação bfi (beforeInclusive)
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.DateCreated.CompareTo(target) <= 0));
            result.Count().ShouldBe(11);
        }

        /// <summary>
        /// Teste MustFilterByAfter filtro after(depois)
        /// </summary>
        [Fact]
        public void MustFilterByAfter()
        {
            var target = DateTime.Today.AddMinutes(12);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "DateCreated",
                    Operation = "af",
                    Value = target.ToString("O")
                }
            };
            // Operação af(after)
            var result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.DateCreated.CompareTo(target) > 0));
            result.Count().ShouldBe(6);

            _req.Filters = new List<Filter<TestContrato>>()
            {
                new Filter<TestContrato>()
                {
                    Field = "DateCreated",
                    Operation = "afi",
                    Value = target.ToString("O")
                }
            };
            // Operação afi(afterInclusive)
            result = _req.GetQuery(_testList);
            result.ShouldBe(_testList.Where(x => x.DateCreated.CompareTo(target) >= 0));
            result.Count().ShouldBe(7);
        }
    }
}
