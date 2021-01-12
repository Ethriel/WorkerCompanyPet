using System;

namespace Broker.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var consumer = new TopicConsumer.TopicConsumer())
            {
                consumer.Consume("demo.*", "demo.queue.log", "demo.exchange");
                Console.WriteLine("Consumer has started");
                Console.ReadLine();
            }
        }
    }
}
