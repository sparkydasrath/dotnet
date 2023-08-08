// See https://aka.ms/new-console-template for more information

using System.Xml;
using Confluent.Kafka;
using KafkaProducer;

const string bootstrapServers = "localhost:19092";
const string topic = "test";
const string groupId = "test-group";

//Consumer consumer = new(bootstrapServers, groupId, topic);
Producer producer = new(bootstrapServers, topic);

// Start producing messages
await producer.Produce($"Message 1");
/*for (int i = 0; i < 3; i++)
{
    _ = producer.Produce($"Message {i}");
    Thread.Sleep(1000);  // Add delay to simulate periodic production of messages
}*/

// Start consumer in a separate task
//Task? consumerTask = Task.Run(() => consumer.Consume(new Dictionary<int, long> { { 0, 0 }, { 1, 0 } }));


// Wait for consumer to finish processing
//consumerTask.Wait();

Console.WriteLine("done producing");