using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Broker.TopicConsumer
{
    public class TopicConsumer : IDisposable
    {
        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;
        private const string baseUri = "amqp://guest:guest@localhost:5672";
        public TopicConsumer()
        {

        }

        public void Consume(string routingKey, string queue, string exchange)
        {
            if (factory == null)
            {
                var uri = new Uri(baseUri);
                factory = new ConnectionFactory() { Uri = uri };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
            }

            channel.ExchangeDeclare(exchange, ExchangeType.Topic);
            channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queue, exchange, routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Recieved;

            channel.BasicConsume(queue, true, consumer);
        }

        private void Recieved(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine("Message recieved");
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Message:");
            Console.WriteLine(message);
        }

        public void Dispose()
        {
            channel.Dispose();
            connection.Dispose();
            factory = null;
        }
    }
}
