using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Publisher.Interfaces;
using RabbitMQ.Publisher.Models;

namespace RabbitMQ.Publisher.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestRabbitMQController : ControllerBase
{
    private readonly IMessageBrokerService _messageBrokerService;

    public TestRabbitMQController(IMessageBrokerService messageBrokerService)
    {
        _messageBrokerService = messageBrokerService;
    }
    [HttpGet("Count")]
    public async Task<IActionResult> SendMessage(int count)
    {
        for (int i = 1; i < count; i++)
        {
            var order = new OrderDto
            {
                OrderDate = DateTime.Now.AddDays(-i).ToShortDateString(),
                OrderNumber = i.ToString(),
                Title = $"Order Number {i}",
            };
            string queueName = "order-queue";
            await _messageBrokerService.SendMessage(queueName, order);

        }
        return Ok();
    } 
}
