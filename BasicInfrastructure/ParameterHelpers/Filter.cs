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
            var prop = typeof(T).GetProperty(Field);

            if (prop == null)
                return query;

            switch (Operation.ToLower())
            {
                case "eq":
                    query = query.Where(x =>
                        x.GetType().GetProperty(prop.Name).GetValue(x).ToString()
                            .Equals(Value, StringComparison.InvariantCulture));
                    break;
                case "eqi":
                    query = query.Where(x =>
                        x.GetType().GetProperty(prop.Name).GetValue(x).ToString()
                            .Equals(Value, StringComparison.InvariantCultureIgnoreCase));
                    break;
                case "ct":
                    query = query.Where(x =>
                        x.GetType().GetProperty(prop.Name).GetValue(x).ToString()
                            .Contains(Value));
                    break;
                case "cti":
                    query = query.Where(x =>
                        x.GetType().GetProperty(prop.Name).GetValue(x).ToString().ToLower()
                            .Contains(Value.ToLower()));
                    break;
                case "gt":
                case "gte":
                case "lt":
                case "lte":
                default:
                    break;
            }

            return query;
        }
    }
}
