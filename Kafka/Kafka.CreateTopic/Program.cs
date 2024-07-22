using Confluent.Kafka;
using Confluent.Kafka.Admin;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new AdminClientConfig
        {
            BootstrapServers = "localhost:9092"  // Replace with your Kafka broker(s)
        };

        using (var adminClient = new AdminClientBuilder(config).Build())
        {
            // Define the list of topics to create
            var topicsToCreate = new List<TopicSpecification>
            {
                new() { Name = "test-topic", NumPartitions = 3, ReplicationFactor = 1 },
                new() { Name = "order-topic", NumPartitions = 2, ReplicationFactor = 1 },
                new() { Name = "leave-applications", ReplicationFactor = 1, NumPartitions = 3}
            };

            foreach (var topicConfig in topicsToCreate)
            {
                try
                {
                    // Check if the topic already exists
                    var existingTopics = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
                    if (existingTopics.Topics.Exists(x => x.Topic == topicConfig.Name))
                    {
                        Console.WriteLine($"Topic '{topicConfig.Name}' already exists.");
                    }
                    else
                    {
                        // Create the topic
                        await adminClient.CreateTopicsAsync([topicConfig]);
                        Console.WriteLine($"Topic '{topicConfig.Name}' created successfully.");
                    }
                }
                catch (CreateTopicsException ex)
                {
                    Console.WriteLine($"An error occurred creating the topic '{topicConfig.Name}': {ex.Error.Reason}");
                }
            }
        }
    }
}
