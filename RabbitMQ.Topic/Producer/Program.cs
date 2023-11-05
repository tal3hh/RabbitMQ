using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://bheuyprq:o4Ro9yF4IFODJAGCQS-Sctz9LqeKf3hW@lionfish.rmq.cloudamqp.com/bheuyprq");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Exchange tipi ve adi
channel.ExchangeDeclare(exchange: "topic-test", type: ExchangeType.Topic);


//Mesaj gonder.
Console.Write("Mesaj: ");
string message = Console.ReadLine();
byte[] data = Encoding.UTF8.GetBytes(message);


//Publis etdikde, routingKey adina diqqet etmek lazimdir.Cunki ona uygun data alinacaqdir.(* ve #)
channel.BasicPublish(exchange: "topic-test",
                     routingKey: "green.red",
                     body: data);

Console.Read();