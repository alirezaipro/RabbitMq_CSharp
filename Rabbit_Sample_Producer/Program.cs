using RabbitMQ.Client;
using System.Text;

var connectionFactory=new ConnectionFactory()
{
    HostName="localhost"
};

using var connection = connectionFactory.CreateConnection();

using var channel = connection.CreateModel();

string exchangeName = "alirezaei_exchange_direct";
channel.ExchangeDeclare(exchange: exchangeName, ExchangeType.Fanout);

bool isRepeat = true;

while (isRepeat)
{
    Console.WriteLine("please enter command");
    string command = Console.ReadLine();

    switch (command)
    {
        case "p":

            //Console.WriteLine("Please enter routing key");
            //string routingKey = Console.ReadLine();

            Console.WriteLine("Please enter message");
            string message = Console.ReadLine();

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: body);

            Console.WriteLine("-----------------------");

            break;


        default:
        case "e":
            isRepeat = false;
            break;
    }
}

Console.ReadLine();