using System.Collections.Generic;

namespace DronesWebApi.Core.Repositories
{
    public interface IPaginatedList<T>
    {
        IEnumerable<T> Items { get; }
        int PageIndex { get; }
        int PageSize { get; }
        int TotalPages { get; }
        int TotalCount { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}