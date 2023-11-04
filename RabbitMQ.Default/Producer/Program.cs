using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
//Uri appsetting.json'da saxlamaq daha uygundur.
factory.Uri = new Uri("amqps://bheuyprq:o4Ro9yF4IFODJAGCQS-Sctz9LqeKf3hW@lionfish.rmq.cloudamqp.com/bheuyprq");

//Baglanti yaradilir.
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue(quruq) yaradiriq.
channel.QueueDeclare(queue: "default-queue", exclusive: false);

//Queue(quyruga) mesaj gondermek.
Console.Write("Gonderilen Mesaj: ");
string message = Console.ReadLine();

byte[] data = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "", routingKey: "default-queue", body: data);

Console.Read();


