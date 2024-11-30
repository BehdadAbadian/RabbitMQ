using RabbitMQ.Client;

namespace RabbitMQ.Publisher.Interfaces;

public interface IMessageBrokerConnectionService
{
    Task CreateChannelAsync(string queueName, byte[] body, string exchange);
}
