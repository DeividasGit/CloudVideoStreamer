using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Helpers
{
  public static class SortingHelper
  {
    public static IQueryable<T> Sort<T>(this IQueryable<T> query, string sortingColumns)
    {
      if (string.IsNullOrWhiteSpace(sortingColumns)) return query;

      var sortingColumnsList = sortingColumns.Trim().Split(',').ToList();

      bool firstSort = true;
      foreach (var column in sortingColumnsList)
      {
        bool descending = column.StartsWith("-");

        string columnName = descending ? column.Substring(1) : column;

        var property = typeof(T).GetProperty(columnName, 
          BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (property == null) continue;

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAcess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAcess, parameter);
        string method = descending ? (firstSort ? "OrderByDescending" : "ThenByDescending")
                                       : (firstSort ? "OrderBy" : "ThenBy");

        var resultExp = Expression.Call(typeof(Queryable), method, new Type[] { typeof(T), property.PropertyType },
                                        query.Expression, Expression.Quote(orderByExpression));

        query = query.Provider.CreateQuery<T>(resultExp);
        firstSort = false;
      }
      return query;
    }
  }
}
