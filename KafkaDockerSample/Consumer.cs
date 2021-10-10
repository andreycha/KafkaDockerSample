using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace KafkaDockerSample
{
    internal class Consumer
    {
        private readonly IConsumer<Null, string> consumer;

        public Consumer(string connectionString)
        {
            var config = CreateConsumerConfig(connectionString);
            consumer = new ConsumerBuilder<Null, string>(config)
                .SetErrorHandler((c, e) => Console.WriteLine($"Error in consumer: {e}"))
                .Build();
        }

        public void Subscribe(string topic)
        {
            consumer.Subscribe(topic);

            Task.Run(() => ConsumeAsync());
        }

        private static ConsumerConfig CreateConsumerConfig(string connectionString)
            => new ConsumerConfig
            {
                BootstrapServers = connectionString,
                SecurityProtocol = SecurityProtocol.Plaintext,
                GroupId = "KafkaDockerSample",
                EnableAutoCommit = true,
                AutoOffsetReset = AutoOffsetReset.Latest
            };

        private Task ConsumeAsync()
        {
            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume();
                    if (!consumeResult.IsPartitionEOF)
                    {
                        Console.WriteLine($"Received message: '{consumeResult.Message.Value}'");
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error consuming messages: {e.Message}");
                }
            }
        }
    }
}