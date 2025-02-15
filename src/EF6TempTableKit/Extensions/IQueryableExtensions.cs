﻿using System.Data.Entity.Core.Objects;
using System.Linq;
using EF6TempTableKit.SqlCommands;

namespace EF6TempTableKit.Extensions
{
    internal static class IQueryableExtensions
    {
        /// <summary>
        /// For an Entity Framework IQueryable, returns the SQL with inlined Parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static ParameterSqlQuery ToTraceQuery<T>(this IQueryable<T> query)
        {
            var method = typeof(IQueryableExtensions).GetMethod(nameof(GetQueryFromQueryable));
            var genMethod = method.MakeGenericMethod(query.GetType().GenericTypeArguments[0]);

            var objectQuery = (ObjectQuery)genMethod.Invoke(null, new object[] { query });

            var sql = objectQuery.ToTraceString();

            return new ParameterSqlQuery(sql, objectQuery.Parameters);
        }


        public static ObjectQuery<T> GetQueryFromQueryable<T>(IQueryable<T> query)
        {
            var internalQueryField = query.GetType()
                .GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Where(f => f.Name.Equals("_internalQuery"))
                .FirstOrDefault();
            var internalQuery = new object();

            //If query is wrapped with LinqKit extensions we have to get InnerQuery and InternalQuery from it afterwards.
            if (internalQueryField == null)
            {
                var innerQuery = query.GetType()
                    .GetProperties(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .FirstOrDefault(f => f.Name.Equals("InnerQuery"))
                    .GetValue(query);
                internalQuery = innerQuery.GetType()
                    .GetProperties(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .FirstOrDefault(f => f.Name.Equals("InternalQuery"))
                    .GetValue(innerQuery);
            }
            else
            {
                internalQuery = internalQueryField.GetValue(query);
            }

            var objectQueryField = internalQuery.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Where(f => f.Name.Equals("_objectQuery")).FirstOrDefault();
            var objectQueryValue = objectQueryField.GetValue(internalQuery);

            return (dynamic)objectQueryValue;
        }
    }
}
