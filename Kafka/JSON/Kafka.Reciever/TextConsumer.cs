using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Kafka.Consumer
{
    public class TextConsumer : IDisposable
    {
        private readonly IConsumer<int, string> _consumer;
        private readonly string _topic;
        public TextConsumer(ConsumerConfig config, string topic = "test-topic")
        {
            _consumer = new ConsumerBuilder<int, string>(config).Build();
            _topic = topic;
            _consumer.Subscribe(_topic);
        }
        public void Start()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    try
                    {
                        var cr = _consumer.Consume(cts.Token);
                        Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Consume error: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
            }

        }
        public void Dispose()
        {
            _consumer.Dispose();
        }
    }
}
