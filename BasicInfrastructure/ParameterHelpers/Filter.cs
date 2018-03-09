using System;
using System.Linq;
using BasicInfrastructure.Extensions;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.ParameterHelpers
{
    public class Filter<T> : IFilter<T>
    {
        public string Field { get; set; }
        public string Operation { get; set; }
        public string Value { get; set; }
        public virtual IQueryable<T> GetQuery(IQueryable<T> query)
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
                    return query.Where(x =>
                        prop.GetValue(x).ToString()
                            .Equals(Value));
                case "eqi":
                case "equalsInsentitive":
                    return query.Where(x =>
                        prop.GetValue(x).ToString()
                            .EqualsIgnoreCase(Value));
                case "eqn":
                case "equalsNumber":
                    //TODO Tolerance Configuration for double comparison
                    return query.Where(x =>
                        Math.Abs(prop.GetValue(x).ToString().ToDouble(null) - Value.ToDouble(null)) < 0.0000000000001);
                case "ct":
                case "contains":
                    return query.Where(x =>
                        prop.GetValue(x).ToString()
                            .Contains(Value));
                case "cti":
                case "containsInsensitive":
                    return query.Where(x =>
                        prop.GetValue(x).ToString()
                            .ContainsIgnoreCase(Value));
                case "sw":
                case "startsWith":
                    return query.Where(x =>
                        prop.GetValue(x).ToString()
                            .StartsWith(Value));
                case "swi":
                case "startsWithInsensitive":
                    return query.Where(x =>
                        prop.GetValue(x).ToString()
                            .StartsWithIgnoreCase(Value));
                case "ew":
                case "endsWith":
                    return query.Where(x =>
                        prop.GetValue(x).ToString()
                            .EndsWith(Value));
                case "ewi":
                case "endsWithInsensitive":
                    return query.Where(x =>
                        prop.GetValue(x).ToString()
                            .EndsWithIgnoreCase(Value));
                case "gt":
                case "greatherThan":
                    return query.Where(x =>
                        prop.GetValue(x).ToString().ToDouble(null) > Value.ToDouble(null)
                    );
                case "gte":
                case "greatherThanOrEqual":
                    return query.Where(x =>
                        prop.GetValue(x).ToString().ToDouble(null) >= Value.ToDouble(null)
                    );
                case "lt":
                case "lessThan":
                    return query.Where(x =>
                        prop.GetValue(x).ToString().ToDouble(null) < Value.ToDouble(null)
                    );
                case "lte":
                case "lessThanOrEqual":
                    return query.Where(x =>
                        prop.GetValue(x).ToString().ToDouble(null) <= Value.ToDouble(null)
                    );
                case "bf":
                case "before":
                    return query.Where(x =>
                        prop.GetValue(x).ToString().ToDateTimeTicks() < Value.ToDateTimeTicks()
                    );
                case "bfi":
                case "beforeInclusive":
                    return query.Where(x =>
                        prop.GetValue(x).ToString().ToDateTimeTicks() <= Value.ToDateTimeTicks()
                    );
                case "af":
                case "after":
                    return query.Where(x =>
                        prop.GetValue(x).ToString().ToDateTimeTicks() > Value.ToDateTimeTicks()
                    );
                case "afi":
                case "afterInclusive":
                    return query.Where(x =>
                        prop.GetValue(x).ToString().ToDateTimeTicks() >= Value.ToDateTimeTicks()
                    );
                default:
                    throw new ArgumentOutOfRangeException(nameof(Operation));
            }
        }
    }
}
