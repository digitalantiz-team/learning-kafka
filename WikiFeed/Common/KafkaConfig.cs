namespace Common
{
    public static class KafkaConfig
    {
        public static string BootstrapServers = "!!!pkc-l9wvm.ap-southeast-1.aws.confluent.cloud:9092";
        public static string SaslMechanism = "PLAIN";
        public static string SaslUsername = "!!!QK35E5LOVYETESXF";
        public static string SaslPassword = "!!!onP+kX6uGVlYlMdPMkcAU91VVre2Al0DbQTifZxB60PG/GP7NK0linjd2tE/oWNu";
        public static string SecurityProtocol = "sasl_ssl";
        public static string EnableAutoCommit = "false";
        public static string Topic = "TEST_PLAY_DATA";
        public static string LocalTopic = "orders";
    }
}