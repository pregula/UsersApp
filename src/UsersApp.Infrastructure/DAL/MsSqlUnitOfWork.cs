namespace UsersApp.Infrastructure.DAL;

internal sealed class  MsSqlUnitOfWork : IUnitOfWork
{
    private readonly UsersAppDbContext _dbContext;

    public MsSqlUnitOfWork(UsersAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await action();
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}