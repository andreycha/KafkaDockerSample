using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace KafkaDockerSample
{
    internal class Producer
    {
        private readonly IProducer<Null, string> producer;

        public Producer(string connectionString)
        {
            var config = CreateProducerConfig(connectionString);
            producer = new ProducerBuilder<Null, string>(config).Build();
        }

        private static ProducerConfig CreateProducerConfig(string connectionString)
            => new ProducerConfig
            {
                BootstrapServers = connectionString
            };

        public async Task SendAsync(string topic, string payload)
        {
            var message = new Message<Null, string>()
            {
                Value = payload
            };

            try
            {
                var result = await producer.ProduceAsync(topic, message);
                Console.WriteLine($"Message produced: {result.TopicPartitionOffset}");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Error producing a message: {e.Message}");
            }
        }
    }
}