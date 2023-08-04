// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;

const string bootstrapServers = "localhost:9092";
const string topic = "test-topic1";
const string groupId = "test-group";

Consumer consumer = new(bootstrapServers, groupId, topic);
Producer producer = new(bootstrapServers, topic);

// Start consumer in a separate task
Task? consumerTask = Task.Run(() => consumer.Consume(new Dictionary<int, long> { { 0, 0 }, { 1, 0 } }));

// Start producing messages
for (int i = 0; i < 100; i++)
{
    producer.Produce($"Message {i}");
    Thread.Sleep(1000);  // Add delay to simulate periodic production of messages
}

// Wait for consumer to finish processing
consumerTask.Wait();
