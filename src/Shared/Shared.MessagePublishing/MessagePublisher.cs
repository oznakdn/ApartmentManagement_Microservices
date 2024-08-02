using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Shared.MessagePublising;

namespace Shared.MessagePublishing;

public class MessagePublisher : IMessagePublisher
{
    private readonly RabbitMqOption _option;
    private readonly ConnectionFactory factory;
    private IModel channel;

    public MessagePublisher(RabbitMqOption option)
    {
        _option = option;

        factory = new ConnectionFactory
        {
            Uri = new Uri(_option.ConnectionString)
        };

    }

    public void Publish<TBody>(string queue, TBody messageBody)
    {
        using var connection = factory.CreateConnection();
        channel = connection.CreateModel();

        channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);


        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        var body = JsonSerializer.Serialize(messageBody);

        channel.BasicPublish(exchange: string.Empty,
            routingKey: queue,
            basicProperties: properties,
            body: Encoding.UTF8.GetBytes(body));

    }

    public async Task PublishAsync<TBody>(string queue, TBody messageBody)
    {
        using var connection = factory.CreateConnection();
        channel = connection.CreateModel();

        channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);


        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        var body = JsonSerializer.Serialize(messageBody);

        channel.BasicPublish(exchange: string.Empty,
            routingKey: queue,
            basicProperties: properties,
            body: Encoding.UTF8.GetBytes(body));

        await Task.CompletedTask;
    }


}
