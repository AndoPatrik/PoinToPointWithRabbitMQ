using Microsoft.AspNetCore.Mvc;
using Publihser.Models;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using System;

namespace Publihser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagePublisherController : ControllerBase
    {
        private const string queueName = "people";

        // POST api/<MessagePublisherController>
        [HttpPost]
        public IActionResult Post([FromBody] Person bodyPayload)
        {
            try
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest",
                    Port = 5672,
                };
                using (var rabbitConnection = connectionFactory.CreateConnection())
                {
                    using (var channel = rabbitConnection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: queueName,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        channel.BasicPublish(
                            exchange: string.Empty,
                            routingKey: queueName,
                            basicProperties: null,
                            body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bodyPayload))
                            );

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }
    }
}
