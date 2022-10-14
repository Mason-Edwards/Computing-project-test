using Confluent.Kafka;
using System.Net.Http.Headers;

Console.Write("------DataGeneratingProgram------\n\n");

// Write to kafka topic.
var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

using (var producer = new ProducerBuilder<Null, string>(config).Build())
{
    for (var i = 0; i < 100; i++)
    {
        // Generate some random data
        String data = Convert.ToString(new Random().Next(1, 100));

        var result = await producer.ProduceAsync("TelemetryData", new Message<Null, string> { Value = $"{data}", Timestamp = new Timestamp(DateTimeOffset.Now) });
        Console.WriteLine($"{result.Message.Timestamp.UtcDateTime} : {result.Message.Value}");
        Console.Out.Flush();

        await Task.Delay(5000);
    }
}

// Simple console app to simulate a system generating telemetry data
