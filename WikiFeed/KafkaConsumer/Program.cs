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
            var config = KafkaConfigFactory.GetConfluenceConfig();
            //var config = new ProducerConfig { BootstrapServers = BootstrapServers.Cloud };
            WikiConsumer.Consume(KafkaConfig.Topic, config);

            Console.WriteLine("Exiting");
        }
    }
}
