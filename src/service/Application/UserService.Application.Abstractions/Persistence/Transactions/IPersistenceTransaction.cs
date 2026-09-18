namespace UserService.Application.Abstractions.Persistence.Transactions;

public interface IPersistenceTransaction : IAsyncDisposable
{
    ValueTask CommitAsync(CancellationToken cancellationToken);

    ValueTask RollbackAsync(CancellationToken cancellationToken);
}