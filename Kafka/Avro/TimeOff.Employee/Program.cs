using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using Confluent.SchemaRegistry;
using System.Globalization;
using TimeOff.Models;
using System.Net;

var schemaRegistryConfig = new SchemaRegistryConfig { Url = "http://127.0.0.1:8081" };
var producerConfig = new ProducerConfig
{
    BootstrapServers = "127.0.0.1:9092",
    // Guarantees delivery of message to topic.
    EnableDeliveryReports = true,
    ClientId = Dns.GetHostName()
};

using var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
using var producer = new ProducerBuilder<string, LeaveApplicationReceived>(producerConfig)
    .SetKeySerializer(new AvroSerializer<string>(schemaRegistry))
    .SetValueSerializer(new AvroSerializer<LeaveApplicationReceived>(schemaRegistry))
    .Build();
while (true)
{
    var empEmail = ReadLine.Read("Enter your employee Email (e.g. none@example-company.com): ",
        "none@example.com").ToLowerInvariant();
    var empDepartment = ReadLine.Read("Enter your department code (HR, IT, OPS): ").ToUpperInvariant();
    var leaveDurationInHours =
        int.Parse(ReadLine.Read("Enter number of hours of leave requested (e.g. 8): ", "8"));
    var leaveStartDate = DateTime.ParseExact(ReadLine.Read("Enter vacation start date (dd-mm-yy): ",
        $"{DateTime.Today:dd-MM-yy}"), "dd-mm-yy", CultureInfo.InvariantCulture);

    var leaveApplication = new LeaveApplicationReceived
    {
        EmpDepartment = empDepartment,
        EmpEmail = empEmail,
        LeaveDurationInHours = leaveDurationInHours,
        LeaveStartDateTicks = leaveStartDate.Ticks
    };
    var partition = new TopicPartition(
        "leave-applications",
        new Partition((int)Enum.Parse<Departments>(empDepartment)));
    var result = await producer.ProduceAsync(partition,
        new Message<string, LeaveApplicationReceived>
        {
            Key = $"{empEmail}-{DateTime.UtcNow.Ticks}",
            Value = leaveApplication
        });
    Console.WriteLine(
        $"\nMsg: Your leave request is queued at offset {result.Offset.Value} in the Topic {result.Topic}:{result.Partition.Value}\n\n");
}
