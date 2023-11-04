using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
//Uri appsetting.json'da saxlamaq daha uygundur.
factory.Uri = new Uri("amqps://bheuyprq:o4Ro9yF4IFODJAGCQS-Sctz9LqeKf3hW@lionfish.rmq.cloudamqp.com/bheuyprq");

//Baglanti yaradilir.
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Exchangenin adini ve tipini yaziriq.
channel.ExchangeDeclare(exchange: "fanout-test", type: ExchangeType.Fanout);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Sira: {i}");
    
    //RoutingKeyin ehemiyyeti yoxdur,cunki Fanout'da datalari routingKeye baxmadan exchange adi eyni olan butun quyruqlara beraber sekilde gonderir.
    channel.BasicPublish(exchange: "fanout-test",
                         routingKey: "asdasd",
                         body: message);
}

Console.WriteLine("Datalar gonderildi.");
Console.Read();