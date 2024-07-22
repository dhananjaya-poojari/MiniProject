using Confluent.Kafka;
using Kafka.Shared;
using Newtonsoft.Json;
using System.Text;

namespace Kafka.Producer
{
    public class OrderProducer
    {
        private readonly IProducer<Null, Order> _producer;
        private readonly string _topic;

        public OrderProducer(ProducerConfig config, string topic)
        {
            _producer = new ProducerBuilder<Null, Order>(config).SetValueSerializer(new JsonSerializer<Order>()).Build();
            _topic = topic;
        }

        public async void SendOrder(Order order)
        {
            var message = new Message<Null, Order> { Value = order };
            var deliveryResult = await _producer.ProduceAsync(_topic, message);

            Console.WriteLine($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }

    public class JsonSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            if (data == null)
                return null;

            var json = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
