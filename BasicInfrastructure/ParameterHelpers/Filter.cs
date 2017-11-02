using System;
using System.Linq;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.ParameterHelpers
{
    public class Filter<T> : IFilter<T> where T : Entity
    {
        public string Field { get; set; }
        public string Operation { get; set; }
        public string Value { get; set; }
        public IQueryable<T> GetQuery(IQueryable<T> query)
        {
            if (Field == null)
                throw new ArgumentNullException(nameof(Field));
            if (Operation == null)
                throw new ArgumentNullException(nameof(Operation));

            var prop = typeof(T).GetProperty(Field);
            if (prop == null)
                return query;

            switch (Operation.ToLower())
            {
                case "eq":
                case "equals":
                    query = query.Where(x =>
                        x.GetType().GetProperty(prop.Name).GetValue(x).ToString()
                            .Equals(Value, StringComparison.InvariantCulture));
                    break;
                case "eqi":
                case "equalsInsentitive":
                    query = query.Where(x =>
                        x.GetType().GetProperty(prop.Name).GetValue(x).ToString()
                            .Equals(Value, StringComparison.InvariantCultureIgnoreCase));
                    break;
                case "ct":
                case "contains":
                    query = query.Where(x =>
                        x.GetType().GetProperty(prop.Name).GetValue(x).ToString()
                            .Contains(Value));
                    break;
                case "cti":
                case "containsInsensitive":
                    query = query.Where(x =>
                        x.GetType().GetProperty(prop.Name).GetValue(x).ToString().ToLower()
                            .Contains(Value.ToLower()));
                    break;
                case "sw":
                case "startsWith":
                case "swi":
                case "startsWithInsensitive":
                case "ew":
                case "ewi":
                case "gt":
                case "gte":
                case "lt":
                case "lte":
                case "bf":
                case "bfi":
                case "af":
                case "afi":
                default:
                    break;
            }

            return query;
        }
    }
}
