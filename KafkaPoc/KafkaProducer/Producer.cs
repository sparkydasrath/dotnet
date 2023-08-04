using Confluent.Kafka;

public class Producer
{
    private readonly IProducer<string, string> _producer;
    private readonly string _topic;

    public Producer(string bootstrapServers, string topic)
    {
        this._topic = topic;

        ProducerConfig config = new()
        { BootstrapServers = bootstrapServers };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public void Produce(string message)
    {
        try
        {
            DeliveryResult<string, string>? dr = _producer
                .ProduceAsync(_topic, new Message<string, string> { Value = message })
                .GetAwaiter()
                .GetResult();

            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
        catch (ProduceException<string, string> e)
        {
            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
        }
    }
}