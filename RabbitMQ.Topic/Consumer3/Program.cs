using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://bheuyprq:o4Ro9yF4IFODJAGCQS-Sctz9LqeKf3hW@lionfish.rmq.cloudamqp.com/bheuyprq");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Exchange tipi ve adini Producerdeki ile eyni olmalidir.
channel.ExchangeDeclare(exchange: "topic-test", type: ExchangeType.Topic);


//RoutingKey'e verilecek adin nece olacagini urda teyin edek.
string topicName = "green.#";
Console.WriteLine(topicName);

//Queue yaradaraq bu quyruga datalari ataciyiq.
string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName,
                  exchange: "topic-test",
                  routingKey: topicName);

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (model, e) =>
{
    Console.WriteLine("Gelen mesaj: " + Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();