using RabbitMQ.Publisher.Interfaces;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;


namespace RabbitMQ.Publisher.Services;

public class MessageBrokerService : IMessageBrokerService
{
    private readonly IMessageBrokerConnectionService _messageBrokerConnection;

    public MessageBrokerService(IMessageBrokerConnectionService messageBrokerConnection)
    {
        _messageBrokerConnection = messageBrokerConnection;
    }
    public async Task SendMessage<T>(string queueName, T message, string exchange = "")
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        await _messageBrokerConnection.CreateChannelAsync(queueName, body, exchange);

    }
}
