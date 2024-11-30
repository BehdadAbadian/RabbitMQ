namespace RabbitMQ.Publisher.Interfaces;

public interface IMessageBrokerService
{
    Task SendMessage<T>(string queueName, T message, string exchange = "");
}
