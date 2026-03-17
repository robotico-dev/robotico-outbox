using System.Collections.Immutable;
using Robotico.Result.Errors;

namespace Robotico.Outbox.Tests;

public sealed class InMemoryOutbox(Robotico.Result.Result commitResultToReturn) : IOutbox
{
    private readonly List<object> _pending = [];

    public ImmutableArray<object> CommittedMessages { get; private set; } = [];

    public Task<Robotico.Result.Result> EnqueueAsync(object message, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);
        cancellationToken.ThrowIfCancellationRequested();
        _pending.Add(message);
        return Task.FromResult(Robotico.Result.Result.Success());
    }

    public Task<Robotico.Result.Result> CommitAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (commitResultToReturn.IsSuccess())
        {
            CommittedMessages = [.. _pending];
            _pending.Clear();
        }

        return Task.FromResult(commitResultToReturn);
    }
}
