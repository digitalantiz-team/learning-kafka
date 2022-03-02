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
            //var clientConfig = KafkaConfigFactory.GetConfluenceConfig();
            var localConfig = new ProducerConfig { BootstrapServers = "localhost:9092" };
            WikiConsumer.Consume(KafkaConfig.LocalTopic, localConfig);

            Console.WriteLine("Exiting");
        }
    }
}
