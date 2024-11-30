using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Publisher.Interfaces;
using RabbitMQ.Publisher.Models;

namespace RabbitMQ.Publisher.Services;

public class MessageBrokerConnectionService : IMessageBrokerConnectionService
{
    private readonly RabbitMqConfiguration _configuration;

    public MessageBrokerConnectionService(IOptions<RabbitMqConfiguration> options)
    {
        _configuration = options.Value;
    }
    public async Task CreateChannelAsync(string queueName,byte[] body,string exchange)
    {
        var factory = new ConnectionFactory()
        {
            UserName = _configuration.Username,
            Password = _configuration.Password,
            HostName = _configuration.HostName
        };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        await channel.BasicPublishAsync(exchange: exchange, routingKey: queueName, mandatory: true, body: body);

    }
}
