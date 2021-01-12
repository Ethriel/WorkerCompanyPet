using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Broker.TopicExchange
{
    public class TopicExchange : IDisposable
    {
        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;
        private const string baseUri = "amqp://guest:guest@localhost:5672";
        public TopicExchange()
        {

        }

        public void Publish(string routingKey, string exchange, object obj)
        {
            if (factory == null)
            {
                var uri = new Uri(baseUri);
                factory = new ConnectionFactory() { Uri = uri };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
            }

            var json = JsonConvert.SerializeObject(obj);
            var body = Encoding.UTF8.GetBytes(json);
            channel.ExchangeDeclare(exchange, ExchangeType.Topic);
            channel.BasicPublish(exchange, routingKey, null, body);
            Console.WriteLine("Sent body:");
            Console.WriteLine(json);
        }

        public void Dispose()
        {
            channel.Dispose();
            connection.Dispose();
            factory = null;
        }
    }
}
