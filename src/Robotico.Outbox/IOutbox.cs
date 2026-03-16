namespace Robotico.Outbox;

/// <summary>
/// Transactional outbox: enqueue messages in the same transaction as domain changes; commit persists both. CommitAsync returns Result.
/// </summary>
public interface IOutbox
{
    /// <summary>
    /// Enqueues a message to be published after the transaction is committed. Call before CommitAsync.
    /// </summary>
    Task<Robotico.Result.Result> EnqueueAsync(object message, CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the current transaction (including enqueued outbox messages). Returns a failed result if commit fails.
    /// </summary>
    Task<Robotico.Result.Result> CommitAsync(CancellationToken cancellationToken = default);
}
