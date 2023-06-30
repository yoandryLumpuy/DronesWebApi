using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DronesWebApi.Commons.Mappings;
using DronesWebApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DronesWebApi.Persistence.Repositories
{
    public class PaginatedList<T> : IPaginatedList<T>
    {
        public IEnumerable<T> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public int PageSize { get; }

        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize = 10)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            PageSize = pageSize;
            Items = items;
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }

    public static class PaginatedListExtensionMethods
    {
        public static IPaginatedList<T> Map<TS, T>(this IPaginatedList<TS> source, IMapper mapper) where TS : class where T: IMapFrom<TS>
           => new PaginatedList<T>(items: mapper.Map<IEnumerable<T>>(source.Items), source.TotalCount, source.PageIndex, source.PageSize);
    }
}
