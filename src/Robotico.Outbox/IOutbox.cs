namespace Robotico.Outbox;

/// <summary>
/// Transactional outbox: enqueue messages in the same transaction as domain changes; commit persists both. CommitAsync returns <see cref="Robotico.Result.Result"/>.
/// </summary>
/// <remarks>
/// <para>Call EnqueueAsync one or more times, then CommitAsync once. Implementations must persist outbox rows in the same transaction as domain changes. A separate outbox processor publishes committed messages to the message bus.</para>
/// </remarks>
public interface IOutbox
{
    /// <summary>
    /// Enqueues a message to be published after the transaction is committed. Call before CommitAsync.
    /// </summary>
    /// <param name="message">The message to enqueue. Must not be null. Implementations must throw <see cref="ArgumentNullException"/> or return a failed result if null.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>Success when the message was enqueued; otherwise a failed result.</returns>
    Task<Robotico.Result.Result> EnqueueAsync(object message, CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the current transaction (including enqueued outbox messages). Returns a failed result if commit fails.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>Success when the transaction was committed; otherwise a failed result.</returns>
    Task<Robotico.Result.Result> CommitAsync(CancellationToken cancellationToken = default);
}
