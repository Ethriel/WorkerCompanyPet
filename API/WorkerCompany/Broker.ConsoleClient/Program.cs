using Broker.TopicConsumer;
using System;

namespace Broker.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var consumer = new Broker.TopicConsumer.TopicConsumer())
            {
                consumer.Consume("demo.*", "demo.queue.log", "demo.exchange");
            }
        }
    }
}
