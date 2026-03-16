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
}
