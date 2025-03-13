using CloudVideoStreamer.Repository.DTOs.Helpers;
using CloudVideoStreamer.Repository.DTOs.Paging;
using CloudVideoStreamer.Repository.Enums;
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
  public static class QueryableHelper
  {
    public static IQueryable<T> Sort<T>(this IQueryable<T> query, List<SortingDto> sorting)
    {
      if (sorting.Count == 0) return query;

      bool firstSort = true;
      foreach (var column in sorting)
      {
        bool descending = column.Order == SortingOrder.Descending ? true : false;
        string columnName = column.ColumnName;

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

    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, PagingDto paging)
    {
      if (paging == null) return query;

      return query.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);
    }
  }
}
