using Confluent.Kafka;

public class Consumer
{
    private readonly string _bootstrapServers;
    private readonly string _groupId;
    private readonly string _topic;

    public Consumer(string bootstrapServers, string groupId, string topic)
    {
        this._bootstrapServers = bootstrapServers;
        this._groupId = groupId;
        this._topic = topic;
    }

    public void Consume(Dictionary<int, long> offsets)
    {
        ConsumerConfig config = new()
        {
            BootstrapServers = _bootstrapServers,
            GroupId = _groupId,
            EnableAutoCommit = false,
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        using IAdminClient? adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = _bootstrapServers }).Build();
        Metadata? meta = adminClient.GetMetadata(_topic, TimeSpan.FromSeconds(5));
        List<PartitionMetadata>? partitions = meta.Topics[0].Partitions;

        Task[] tasks = partitions.Select(partition => Task.Factory.StartNew(() =>
        {
            IConsumer<Null, string>? consumer = new ConsumerBuilder<Null, string>(config).Build();
            TopicPartitionOffset topicPartitionOffset = new(_topic, new Partition(partition.PartitionId), new Offset(offsets[partition.PartitionId]));
            consumer.Assign(topicPartitionOffset);

            while (true)
            {
                try
                {
                    ConsumeResult<Null, string>? consumeResult = consumer.Consume(TimeSpan.FromMilliseconds(100));

                    if (consumeResult is null)
                    {
                        Thread.Sleep(1000);  // Add delay if no message is available
                        continue;
                    }

                    Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");

                    consumer.Commit(consumeResult);
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Consume error: {e.Error.Reason}");
                }
            }
        })).ToArray();

        Task.WaitAll(tasks);
    }
}