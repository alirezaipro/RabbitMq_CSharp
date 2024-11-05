using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Consumer_One");
Console.WriteLine("-------------------");

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};

using var connection = connectionFactory.CreateConnection();

using var channel = connection.CreateModel();

string exchangeName = "alirezaei_exchange_direct";
channel.ExchangeDeclare(exchange: exchangeName, ExchangeType.Fanout);


string queueName = channel.QueueDeclare().QueueName;
//string routingKey = "log.info";

channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");

var consumerEvent = new EventingBasicConsumer(channel);

consumerEvent.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"message = {message}");
};

channel.BasicConsume(queueName, true, consumerEvent);

Console.WriteLine("Consumer finish");

Console.ReadLine();