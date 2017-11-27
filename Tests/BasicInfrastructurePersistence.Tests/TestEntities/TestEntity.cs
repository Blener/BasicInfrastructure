using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructurePersistence.Tests.TestEntities
{
    public class TestEntity : Entity
    {
    }

    public class TestContrato : TestEntity
    {
        public int? TitularId { get; set; }
        public string Contrato_Id{ get; set; }
        public string NumeroContrato { get; set; }
        public string NumeroProposta { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
