using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Kafka.Producer
{
    public class TextProducer
    {
        private readonly IProducer<int, string> _producer;
        private readonly string _topic;
        public TextProducer(ProducerConfig config, string topic = "test-topic")
        {
            _producer = new ProducerBuilder<int, string>(config).Build();
            _topic = topic;
        }

        public async Task Send()
        {
            try
            {
                var dr = await _producer.ProduceAsync(_topic, new Message<int, string> { Key = 1, Value = "Hello Kafka!" });
                Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
