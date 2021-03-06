﻿using Newtonsoft.Json;
using NServiceBus.Testing;
using Verify;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

class SubscriptionConverter :
    WriteOnlyJsonConverter<Subscription>
{
    public override void WriteJson(JsonWriter writer, Subscription? subscription, JsonSerializer serializer)
    {
        if (subscription == null)
        {
            return;
        }
        writer.WriteStartObject();

        writer.WritePropertyName("MessageType");
        serializer.Serialize(writer, subscription.Message);
        var options = subscription.Options;
        if (options.HasValue())
        {
            writer.WritePropertyName("Options");
            serializer.Serialize(writer, options);
        }

        writer.WriteEndObject();
    }
}