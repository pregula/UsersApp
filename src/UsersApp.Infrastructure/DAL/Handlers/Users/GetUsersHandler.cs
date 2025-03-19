using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UsersApp.Application.DTO.Users;
using UsersApp.Application.Queries.Users;
using UsersApp.Core.Entities;

namespace UsersApp.Infrastructure.DAL.Handlers.Users;

internal class GetUsersHandler : IRequestHandler<GetUsers, IEnumerable<UserDto>>
{
    private readonly UsersAppDbContext _dbContext;
    
    public GetUsersHandler(UsersAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<UserDto>> Handle(GetUsers request, CancellationToken cancellationToken)
    {
        var queryDB = FilterUsers(_dbContext, request);
        queryDB = queryDB
            .OrderBy(u => u.CreatedAt)
            .Paginate(request);
        
        return (await queryDB
                .AsNoTracking()
                .ToListAsync(cancellationToken))
            .Select(x => x.AsDto());
    }
    
    private static IQueryable<User> FilterUsers(UsersAppDbContext dbContext, GetUsers getUsersDto)
    {
        var predicate = PredicateBuilder.New<User>(true);

        if (!string.IsNullOrWhiteSpace(getUsersDto.Name))
        {
            predicate = predicate.And(u => u.UserName.Value.Contains(getUsersDto.Name));
        }

        if (!string.IsNullOrWhiteSpace(getUsersDto.Email))
        {
            predicate = predicate.And(u => u.Email.Value.Contains(getUsersDto.Email));
        }

        var query = dbContext.Users.AsExpandable().Where(predicate);
        return query;
    }
}