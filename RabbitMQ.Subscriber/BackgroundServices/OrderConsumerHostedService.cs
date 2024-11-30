
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Subscriber.BackgroundServices;

public class OrderConsumerHostedService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var orderQueueName = "order-queue";
        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: orderQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            return Task.CompletedTask;
            //call another service
        };

            await channel.BasicConsumeAsync(queue: orderQueueName, autoAck: true, consumer: consumer);

            Console.WriteLine("Waiting for feedback. Press [enter] to exit.");
            Console.ReadLine();

        
    }
}
