using UsersApp.Application.Queries;

namespace UsersApp.Infrastructure.DAL.Handlers;

internal static class QueryExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, Pagination pagination)
    {
        return query.Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize);
    }
}