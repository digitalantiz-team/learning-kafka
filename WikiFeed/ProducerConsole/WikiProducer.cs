using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProducerConsole
{
    public class WikiProducer
    {
        // Produce recent-change messages from Wikipedia to a Kafka topic.
        // The messages are sent from the RCFeed https://www.mediawiki.org/wiki/Manual:RCFeed
        // to the topic with the specified name. 
        public static async Task Produce(string topicName, ClientConfig config)
        {
            Console.WriteLine($"{nameof(Produce)} starting");

            // The URL of the EventStreams service.
            string eventStreamsUrl = "https://stream.wikimedia.org/v2/stream/recentchange";

            // Declare the producer reference here to enable calling the Flush
            // method in the finally block, when the app shuts down.
            IProducer<string, string> producer = null;

            try
            {
                // Build a producer based on the provided configuration.
                // It will be disposed in the finally block.
                producer = new ProducerBuilder<string, string>(config).Build();

                // Create an HTTP client and request the event stream.
                using (var httpClient = new HttpClient())

                // Get the RC stream. 
                using (var stream = await httpClient.GetStreamAsync(eventStreamsUrl))

                // Open a reader to get the events from the service.
                using (var reader = new StreamReader(stream))
                {
                    // Read continuously until interrupted by Ctrl+C.
                    while (!reader.EndOfStream)
                    {
                        // Get the next line from the service.
                        var line = reader.ReadLine();

                        // The Wikimedia service sends a few lines, but the lines
                        // of interest for this demo start with the "data:" prefix. 
                        if (!line.StartsWith("data:"))
                        {
                            continue;
                        }

                        // Extract and deserialize the JSON payload.
                        int openBraceIndex = line.IndexOf('{');
                        string jsonData = line.Substring(openBraceIndex);
                        Console.WriteLine($"Data string: {jsonData}");

                        // Parse the JSON to extract the URI of the edited page.
                        var jsonDoc = JsonDocument.Parse(jsonData);
                        var metaElement = jsonDoc.RootElement.GetProperty("meta");
                        var uriElement = metaElement.GetProperty("uri");
                        var key = uriElement.GetString(); // Use the URI as the message key.

                        // For higher throughput, use the non-blocking Produce call
                        // and handle delivery reports out-of-band, instead of awaiting
                        // the result of a ProduceAsync call.
                        producer.Produce(topicName, new Message<string, string> { Key = key, Value = jsonData },
                            (deliveryReport) =>
                            {
                                if (deliveryReport.Error.Code != ErrorCode.NoError)
                                {
                                    Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                                }
                                else
                                {
                                    Console.WriteLine($"Produced message to: {deliveryReport.TopicPartitionOffset}");
                                }
                            });
                    }
                }
            }
            finally
            {
                var queueSize = producer.Flush(TimeSpan.FromSeconds(5));
                if (queueSize > 0)
                {
                    Console.WriteLine("WARNING: Producer event queue has " + queueSize + " pending events on exit.");
                }
                producer.Dispose();
            }
        }
    }
}
