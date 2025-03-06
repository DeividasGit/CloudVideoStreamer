using CloudVideoStreamer.Repository.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Helpers
{
  public static class PagingHelper
  {
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, PagingDto paging)
    {
      if (paging == null) return query;

      return query.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);
    }
  }
}
