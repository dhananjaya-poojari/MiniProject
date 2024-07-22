using Confluent.Kafka;
using Kafka.Shared;
using Newtonsoft.Json;
using System.Text;

namespace Kafka.Consumer
{
    public class OrderConsumer : IDisposable
    {
        private readonly IConsumer<string, Order> _consumer;
        private readonly string _topic;

        public OrderConsumer(ConsumerConfig config, string topic)
        {
            _consumer = new ConsumerBuilder<string, Order>(config)
                .SetValueDeserializer(new JsonDeserializer<Order>())
                .Build();
            _topic = topic;
            _consumer.Subscribe(_topic);
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    var cr = _consumer.Consume();
                    if (cr != null)
                    {
                        var order = cr.Value;
                        Console.WriteLine($"Consumed order with key: {cr.Key}");
                        Console.WriteLine($"Product: {order.product}, Quantity: {order.quantity}");
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occurred: {e.Error.Reason}");
                }
            }
        }

        public void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
        }
    }

    public class JsonDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
