﻿using System.Threading.Tasks;
using NServiceBus.Testing;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

public class MessageHandlerTests :
    VerifyBase
{
    #region HandlerTest

    [Fact]
    public async Task VerifyHandlerResult()
    {
        var handler = new MyHandler();
        var context = new TestableMessageHandlerContext();

        await handler.Handle(new MyRequest(), context);

        await Verify(context);
    }

    #endregion

    public MessageHandlerTests(ITestOutputHelper output) :
        base(output)
    {
    }
}