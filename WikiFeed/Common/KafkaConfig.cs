namespace Common
{
    public static class KafkaConfig
    {
        public static string SaslMechanism = "PLAIN";
        public static string SaslUsername = "QK35E5LOVYETESXF";
        public static string SaslPassword = "onP+kX6uGVlYlMdPMkcAU91VVre2Al0DbQTifZxB60PG/GP7NK0linjd2tE/oWNu";
        public static string SecurityProtocol = "sasl_ssl";
        public static string EnableAutoCommit = "false";
        public static string Topic = "TEST_PLAY_DATA";
    }

    public static class BootstrapServers
    {
        public const string Localhost = "locahost:9092";
        public const string Cloud = "pkc-l9wvm.ap-southeast-1.aws.confluent.cloud:9092";
        public const string OnPremises = "20.24.36.110:9092";
    }
}