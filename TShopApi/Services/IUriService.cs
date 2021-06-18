using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TShopApi.Filters;

namespace TShopApi.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter paginationFilter, string route);
    }
}
