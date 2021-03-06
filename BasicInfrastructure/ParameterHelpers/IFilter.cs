﻿using System.Linq;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.ParameterHelpers
{

    public interface IFilter<T>
    {
        string Field { get; set; }
        string Operation { get; set; }
        string Value { get; set; }
        IQueryable<T> GetQuery(IQueryable<T> query);
    }
}
