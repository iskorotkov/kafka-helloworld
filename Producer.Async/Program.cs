using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Producer.Async
{
    internal static class Program
    {
        private static async Task Main()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            var builder = new ProducerBuilder<Null, string>(config);
            using var producer = builder.Build();
            try
            {
                var message = new Message<Null, string> { Value = "test" };
                var result = await producer.ProduceAsync("test-topic", message).ConfigureAwait(false);
                Console.WriteLine($"Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
