﻿using Microsoft.Data.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Troubadour.Extensions
{
    public static class Queryable
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
