﻿using System;
using System.Threading;
using Confluent.Kafka;

namespace Consumer
{
    internal static class Program
    {
        private static void Main()
        {
            var config = new ConsumerConfig
            {
                GroupId = Environment.GetEnvironmentVariable("GROUP_ID") ?? "test-consumer-group",
                BootstrapServers = Environment.GetEnvironmentVariable("BOOTSTRAP_SERVERS") ?? "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var builder = new ConsumerBuilder<Ignore, string>(config);
            using var consumer = builder.Build();
            consumer.Subscribe(Environment.GetEnvironmentVariable("TOPIC") ?? "my-topic");

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    try
                    {
                        var result = consumer.Consume(cts.Token);
                        Console.WriteLine($"Consumed message '{result.Message.Value}' at '{result.TopicPartitionOffset}'");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }
    }
}
