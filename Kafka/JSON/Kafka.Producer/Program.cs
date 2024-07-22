using Confluent.Kafka;
using Kafka.Producer;
using Kafka.Shared;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

var orderProducer = new OrderProducer(config, "order-topic");

var order = new Order("John Doe", "Laptop", 2);
orderProducer.SendOrder(order);

Thread.Sleep(10000);
orderProducer.Dispose();