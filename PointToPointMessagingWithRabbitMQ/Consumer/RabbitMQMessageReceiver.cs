using System;
using System.IO;
using System.Text;
using Google.Protobuf;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Consumer
{
    class RabbitMQMessageReceiver : DefaultBasicConsumer
    {
        private readonly IModel _channel;

        public RabbitMQMessageReceiver(IModel channel)
        {
            _channel = channel;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            Console.WriteLine($"Message recieved from {exchange}");
            Console.WriteLine($"Message: {Encoding.UTF8.GetString(body.Span)}");
            WriteSerializedPersonToFile(body);
            _channel.BasicAck(deliveryTag, false);
        }
        private void WriteSerializedPersonToFile(ReadOnlyMemory<byte> data)
        {
            var p = JsonConvert.DeserializeObject<Person>(Encoding.UTF8.GetString(data.Span));
                  
            string path = $"{p.Name}.dat";

            using (var output = File.Create(path))
            {
                p.WriteTo(output);
            }
        }
    }
}
