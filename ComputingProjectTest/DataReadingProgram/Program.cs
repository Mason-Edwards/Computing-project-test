using Confluent.Kafka;

Console.Write("------DataReadingProgram------\n\n");

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "TelemetryReader",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
{
    consumer.Subscribe(new List<string> { "TelemetryData" });


    // Consumer Loop
    while(true)
    {
        var consumeResult = consumer.Consume();
        Console.WriteLine($"{consumeResult.Message.Timestamp.UtcDateTime} : {consumeResult.Message.Value}");
        Console.Out.Flush();

        Task.Delay(5000);
    }
}
