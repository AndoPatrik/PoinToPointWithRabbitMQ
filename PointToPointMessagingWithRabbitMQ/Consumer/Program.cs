using System;
using RabbitMQ.Client;

namespace Consumer
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("Setting up RabbitMQ connection...");
            Console.WriteLine();
            try
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest",
                    Port = 5672,
                };

                var connection = connectionFactory.CreateConnection();
                var channel = connection.CreateModel();

                channel.BasicQos(0, 1, false);
                RabbitMQMessageReceiver messageReceiver = new RabbitMQMessageReceiver(channel);
                Console.WriteLine("Fetching some undelievered messages...");
                Console.WriteLine();
                channel.BasicConsume("people", false, messageReceiver);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
