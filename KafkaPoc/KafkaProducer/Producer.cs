using Confluent.Kafka;

namespace KafkaProducer;

public class Producer
{
    private readonly ProducerConfig _config;
    private readonly string _topic;

    public Producer(string bootstrapServers, string topic)
    {
        _topic = topic;
        _config = new()
        {
            BootstrapServers = bootstrapServers,
            Acks = Acks.Leader
        };
    }

    public async Task Produce(string message)
    {
        using IProducer<Null, string>? p = new ProducerBuilder<Null, string>(_config).Build();
        try
        {

            //p.Produce(_topic, new Message<Null, string> { Value = message });
            DeliveryResult<Null, string>? dr = await p.ProduceAsync(_topic, new Message<Null, string> { Value = message });
            p.Flush();
            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
        catch (ProduceException<Null, string> e)
        {
            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
        }

        /*
        DeliveryResult<Null, string>? dr = await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        */

        /*try
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
    }*/
    }
}