using System;
using System.Threading.Tasks;

namespace KafkaDockerSample
{
    class Program
    {
        private const string KafkaConnectionString = "localhost:29092";
        private const string KafkaTopic = "test_topic";

        private static readonly Consumer consumer = new Consumer(KafkaConnectionString);
        private static readonly Producer producer = new Producer(KafkaConnectionString);

        static async Task Main(string[] args)
        {
            Console.WriteLine("Type text and hit ENTER to send. Type 'q' to quit.");

            consumer.Subscribe(KafkaTopic);

            var input = Console.ReadLine();
            while (!string.Equals(input, "q", StringComparison.InvariantCultureIgnoreCase))
            {
                await producer.SendAsync(KafkaTopic, input);
                input = Console.ReadLine();
            }
        }
    }
}
