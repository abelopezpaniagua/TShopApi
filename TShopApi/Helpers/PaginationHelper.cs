using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TShopApi.Filters;
using TShopApi.Services;
using TShopApi.Wrappers;

namespace TShopApi.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedResponse<T>(List<T> pagedData, PaginationFilter paginator, int totalRecords, IUriService uriService, string route)
        {
            var response = new PagedResponse<List<T>>(pagedData, paginator.PageNumber, paginator.PageSize);
            var totalPages = ((double)totalRecords / (double)paginator.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            
            response.NextPage = paginator.PageNumber >= 1 && paginator.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(paginator.PageNumber + 1, paginator.PageSize), route)
                : null;
            response.PreviousPage = paginator.PageNumber - 1 >= 1 && paginator.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(paginator.PageNumber - 1, paginator.PageSize), route)
                : null;
            response.FirstPage = uriService.GetPageUri(new PaginationFilter(1, paginator.PageSize), route);
            response.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, paginator.PageSize), route);
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;

            return response;
        }
    }
}
