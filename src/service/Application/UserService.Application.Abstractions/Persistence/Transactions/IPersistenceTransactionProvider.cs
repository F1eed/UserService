using System.Data;

namespace Itmo.HighLoad.Application.Abstractions.Persistence.Transactions;

public interface IPersistenceTransactionProvider
{
    ValueTask<IPersistenceTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken);
}