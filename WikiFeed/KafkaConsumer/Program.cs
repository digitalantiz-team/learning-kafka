using Common;
using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsumer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var clientConfig = KafkaConfigFactory.GetConfluenceConfig();
            WikiConsumer.Consume(KafkaConfig.Topic, clientConfig);

            Console.WriteLine("Exiting");
        }
    }
}
