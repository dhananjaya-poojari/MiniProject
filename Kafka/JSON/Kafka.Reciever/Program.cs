using Confluent.Kafka;
using Kafka.Consumer;
using Kafka.Shared;
using System.Text.RegularExpressions;

var config = new ConsumerConfig
{
    GroupId = "test-consumer-group",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var cts = new CancellationTokenSource();
var token = cts.Token;

#region Simple text message
Task t1 = Task.Run(() =>
{
    using var consumer = new TextConsumer(config, "test-topic");
    consumer.Start();
    token.ThrowIfCancellationRequested();
}, token);

#endregion

Task t2 = Task.Run(() =>
{
    using var consumer = new OrderConsumer(config, "order-topic");
    consumer.Start();
    token.ThrowIfCancellationRequested();
}, token);

Console.ReadKey();
cts.Cancel();



