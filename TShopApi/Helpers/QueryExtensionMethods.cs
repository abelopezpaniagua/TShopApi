using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TShopApi.Helpers
{
    public static class QueryExtensionMethods
    {
        public static IQueryable<T> OrderByPropertyName<T>(this IQueryable<T> query, string sortColumn, bool ascending)
        {
            var parameterExpression = Expression.Parameter(typeof(T), "p");
            var memberExpression = Expression.Property(parameterExpression, sortColumn);
            var lambdaExpression = Expression.Lambda(memberExpression, parameterExpression);
            string methodName = ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { query.ElementType, lambdaExpression.Body.Type };
            var methodCallExpression = Expression.Call(typeof(Queryable), methodName, types, query.Expression, lambdaExpression);
            return query.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
