using Robotico.Outbox;
using Xunit;

namespace Robotico.Outbox.Tests;

public sealed class OutboxTests
{
    [Fact]
    public void IOutbox_interface_exists()
    {
        Assert.NotNull(typeof(IOutbox).GetMethod(nameof(IOutbox.EnqueueAsync)));
        Assert.NotNull(typeof(IOutbox).GetMethod(nameof(IOutbox.CommitAsync)));
    }

    [Fact]
    public async Task EnqueueAsync_then_CommitAsync_succeeds_and_persists_messages()
    {
        InMemoryOutbox outbox = new(Robotico.Result.Result.Success());
        object msg = new { Id = 1 };

        Robotico.Result.Result enqueueResult = await outbox.EnqueueAsync(msg);
        Assert.True(enqueueResult.IsSuccess());
        Robotico.Result.Result commitResult = await outbox.CommitAsync();
        Assert.True(commitResult.IsSuccess());
        Assert.Single(outbox.CommittedMessages);
        Assert.Same(msg, outbox.CommittedMessages[0]);
    }

    [Fact]
    public async Task EnqueueAsync_throws_on_null_message()
    {
        InMemoryOutbox outbox = new(Robotico.Result.Result.Success());

        await Assert.ThrowsAsync<ArgumentNullException>(() => outbox.EnqueueAsync(null!));
    }

    [Fact]
    public async Task CommitAsync_returns_configured_failure_when_set()
    {
        Robotico.Result.Result failure = Robotico.Result.Result.Error(new Robotico.Result.Errors.SimpleError("commit failed"));
        InMemoryOutbox outbox = new(failure);
        await outbox.EnqueueAsync(new object());

        Robotico.Result.Result result = await outbox.CommitAsync();

        Assert.True(result.IsError(out _));
        Assert.Empty(outbox.CommittedMessages);
    }

    [Fact]
    public async Task EnqueueAsync_throws_on_cancellation()
    {
        InMemoryOutbox outbox = new(Robotico.Result.Result.Success());
        using CancellationTokenSource cts = new();
        await cts.CancelAsync();

        await Assert.ThrowsAsync<OperationCanceledException>(() => outbox.EnqueueAsync(new object(), cts.Token));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public async Task Outbox_law_enqueue_order_preserved(int count)
    {
        InMemoryOutbox outbox = new(Robotico.Result.Result.Success());
        object[] messages = Enumerable.Range(0, count).Select(i => (object)i).ToArray();
        foreach (object m in messages)
        {
            await outbox.EnqueueAsync(m);
        }

        Robotico.Result.Result commitResult = await outbox.CommitAsync();

        Assert.True(commitResult.IsSuccess());
        Assert.Equal(messages.Length, outbox.CommittedMessages.Length);
        for (int i = 0; i < messages.Length; i++)
        {
            Assert.Same(messages[i], outbox.CommittedMessages[i]);
        }
    }
}
