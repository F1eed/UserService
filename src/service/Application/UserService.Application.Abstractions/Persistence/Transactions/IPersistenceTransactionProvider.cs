using System.Data;

namespace UserService.Application.Abstractions.Persistence.Transactions;

public interface IPersistenceTransactionProvider
{
    ValueTask<IPersistenceTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken);
}