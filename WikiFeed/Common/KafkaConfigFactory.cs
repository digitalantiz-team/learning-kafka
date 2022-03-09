using Confluent.Kafka;

namespace Common
{
    public class KafkaConfigFactory
    {
        public static ClientConfig GetConfluenceConfig()
        {
            var clientConfig = new ClientConfig();
            clientConfig.BootstrapServers = BootstrapServers.Cloud;
            clientConfig.SecurityProtocol = Confluent.Kafka.SecurityProtocol.SaslSsl;
            clientConfig.SaslMechanism = Confluent.Kafka.SaslMechanism.Plain;
            clientConfig.SaslUsername = KafkaConfig.SaslUsername;
            clientConfig.SaslPassword = KafkaConfig.SaslPassword;
            clientConfig.SslCaLocation = "probe"; // /etc/ssl/certs

            return clientConfig;
        }
    }
}
