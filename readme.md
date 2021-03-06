<!--
GENERATED FILE - DO NOT EDIT
This file was generated by [MarkdownSnippets](https://github.com/SimonCropp/MarkdownSnippets).
Source File: /readme.source.md
To change this file edit the source file and then run MarkdownSnippets.
-->

# <img src="/src/icon.png" height="30px"> Verify.NServiceBus

[![Build status](https://ci.appveyor.com/api/projects/status/wwrri8srggv1h56j/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-NServiceBus)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.NServiceBus.svg)](https://www.nuget.org/packages/Verify.NServiceBus/)

Adds [Verify](https://github.com/SimonCropp/Verify) support to verify [NServiceBus Test Contexts](https://docs.particular.net/nservicebus/samples/unit-testing/).

<!--- StartOpenCollectiveBackers -->

[Already a Patron? skip past this section](#endofbacking)


## Community backed

**It is expected that all developers either [become a Patron](https://opencollective.com/nservicebusextensions/contribute/patron-6976) or have a [Tidelift Subscription](#support-via-tidelift) to use NServiceBusExtensions. [Go to licensing FAQ](https://github.com/NServiceBusExtensions/Home/#licensingpatron-faq)**


### Sponsors

Support this project by [becoming a Sponsor](https://opencollective.com/nservicebusextensions/contribute/sponsor-6972). The company avatar will show up here with a website link. The avatar will also be added to all GitHub repositories under the [NServiceBusExtensions organization](https://github.com/NServiceBusExtensions).


### Patrons

Thanks to all the backing developers! Support this project by [becoming a patron](https://opencollective.com/nservicebusextensions/contribute/patron-6976).

<img src="https://opencollective.com/nservicebusextensions/tiers/patron.svg?width=890&avatarHeight=60&button=false">

<a href="#" id="endofbacking"></a>

<!--- EndOpenCollectiveBackers -->


## Support via TideLift

Support is available via a [Tidelift Subscription](https://tidelift.com/subscription/pkg/nuget-verify.nservicebus?utm_source=nuget-verify.nservicebus&utm_medium=referral&utm_campaign=enterprise).


<!-- toc -->
## Contents

  * [Usage](#usage)
    * [Verifying a context](#verifying-a-context)
    * [Example behavior change](#example-behavior-change)
  * [Security contact information](#security-contact-information)<!-- endtoc -->


## NuGet package

https://nuget.org/packages/Verify.NServiceBus/


## Usage

Before any test have run call:

```
VerifyNServiceBus.Enable();
```


### Verifying a context

Given the following handler:

<!-- snippet: SimpleHandler -->
<a id='snippet-simplehandler'/></a>
```cs
public class MyHandler :
    IHandleMessages<MyRequest>
{
    public async Task Handle(MyRequest message, IMessageHandlerContext context)
    {
        await context.Publish(
            new MyPublishMessage
            {
                Property = "Value"
            });

        await context.Reply(
            new MyReplyMessage
            {
                Property = "Value"
            });

        var sendOptions = new SendOptions();
        sendOptions.DelayDeliveryWith(TimeSpan.FromHours(12));
        await context.Send(
            new MySendMessage
            {
                Property = "Value"
            },
            sendOptions);

        await context.ForwardCurrentMessageTo("newDestination");
    }
}
```
<sup><a href='/src/Tests/Snippets/MyHandler.cs#L5-L37' title='File snippet `simplehandler` was extracted from'>snippet source</a> | <a href='#snippet-simplehandler' title='Navigate to start of snippet `simplehandler`'>anchor</a></sup>
<!-- endsnippet -->

The test that verifies the resulting context:

<!-- snippet: HandlerTest -->
<a id='snippet-handlertest'/></a>
```cs
[Fact]
public async Task VerifyHandlerResult()
{
    var handler = new MyHandler();
    var context = new TestableMessageHandlerContext();

    await handler.Handle(new MyRequest(), context);

    await Verify(context);
}
```
<sup><a href='/src/Tests/Snippets/MessageHandlerTests.cs#L10-L23' title='File snippet `handlertest` was extracted from'>snippet source</a> | <a href='#snippet-handlertest' title='Navigate to start of snippet `handlertest`'>anchor</a></sup>
<!-- endsnippet -->

The resulting context verification file is as follows:

<!-- snippet: MessageHandlerTests.VerifyHandlerResult.verified.txt -->
<a id='snippet-MessageHandlerTests.VerifyHandlerResult.verified.txt'/></a>
```txt
{
  RepliedMessages: [
    {
      MyReplyMessage: {
        Property: 'Value'
      }
    }
  ],
  ForwardedMessages: [
    'newDestination'
  ],
  SentMessages: [
    {
      MySendMessage: {
        Property: 'Value'
      },
      Options: {
        DeliveryDelay: '12:00:00'
      }
    }
  ],
  PublishedMessages: [
    {
      MyPublishMessage: {
        Property: 'Value'
      }
    }
  ]
}
```
<sup><a href='/src/Tests/Snippets/MessageHandlerTests.VerifyHandlerResult.verified.txt#L1-L29' title='File snippet `MessageHandlerTests.VerifyHandlerResult.verified.txt` was extracted from'>snippet source</a> | <a href='#snippet-MessageHandlerTests.VerifyHandlerResult.verified.txt' title='Navigate to start of snippet `MessageHandlerTests.VerifyHandlerResult.verified.txt`'>anchor</a></sup>
<!-- endsnippet -->


### Example behavior change

The next time there is a code change, that results in a different resulting interactions with NServiceBus, those changes can be visualized. For example if the `DelayDeliveryWith` is changed from 12 hours to 1 day:

<!-- snippet: SimpleHandlerV2 -->
<a id='snippet-simplehandlerv2'/></a>
```cs
await context.Publish(
    new MyPublishMessage
    {
        Property = "Value"
    });

await context.Reply(
    new MyReplyMessage
    {
        Property = "Value"
    });

var sendOptions = new SendOptions();
sendOptions.DelayDeliveryWith(TimeSpan.FromDays(1));
await context.Send(
    new MySendMessage
    {
        Property = "Value"
    },
    sendOptions);

await context.ForwardCurrentMessageTo("newDestination");
```
<sup><a href='/src/Tests/Snippets/MyHandlerV2.cs#L10-L35' title='File snippet `simplehandlerv2` was extracted from'>snippet source</a> | <a href='#snippet-simplehandlerv2' title='Navigate to start of snippet `simplehandlerv2`'>anchor</a></sup>
<!-- endsnippet -->

Then the resulting visualization diff would look as follows:


![visualization diff](/src/approvaltests-diff.png)


## Security contact information

To report a security vulnerability, use the [Tidelift security contact](https://tidelift.com/security). Tidelift will coordinate the fix and disclosure.


## Icon

[Approval](https://thenounproject.com/term/approval/1759519/) designed by [Mike Zuidgeest](https://thenounproject.com/zuidgeest/) from [The Noun Project](https://thenounproject.com/).
