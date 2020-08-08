using System;
using Confluent.Kafka;

namespace Producer.Sync
{
    internal static class Program
    {
        private static void Main()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            var builder = new ProducerBuilder<Null, string>(config);
            using var producer = builder.Build();
            for (var i = 0; i < 100; i++)
            {
                var message = new Message<Null, string> { Value = $"test{i}" };
                producer.Produce("my-topic", message, HandleDeliveryReport);
            }

            producer.Flush(TimeSpan.FromSeconds(10));
        }

        private static void HandleDeliveryReport(DeliveryReport<Null, string> report)
        {
            if (report.Error.IsError)
            {
                Console.WriteLine($"Delivery failed: {report.Error.Reason}");
            }
            else
            {
                Console.WriteLine($"Delivered '{report.Value}' to '{report.TopicPartitionOffset}'");
            }
        }
    }
}
