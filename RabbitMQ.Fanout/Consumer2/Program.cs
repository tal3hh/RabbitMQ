using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
//Uri appsetting.json'da saxlamaq daha uygundur.
factory.Uri = new Uri("amqps://bheuyprq:o4Ro9yF4IFODJAGCQS-Sctz9LqeKf3hW@lionfish.rmq.cloudamqp.com/bheuyprq");

//Baglanti yaradilir.
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Exchangenin producer terefdeki ile eyni yaziriq.
channel.ExchangeDeclare(exchange: "fanout-test", type: ExchangeType.Fanout);

//Quyruq yaradiriq ki, gelen mesaji bu quyruga ataciyiq.Gelen datalari beraber sekilde bolmek ucun
//diger serverlerde de quyruq adini eyni vermeliyik. Eger diger serverde basqa adla, yeni 'queue1' deyil,
//'queue2' yaxud basqa bir adla quyruq yaratsaydiq onda o quyuga bolunmus(beraber) sekilde deyil, butun datalari gonderecekdi.
channel.QueueDeclare(queue: "queue1",
                     exclusive: false);

//Burda routingKey ne olmasinin bir ferqi yoxdur cunki, Fanout'da exchange adi eyni olan butun quyruqlara o mesaji gonderir.
channel.QueueBind(queue: "queue1",
                  exchange: "fanout-test",
                  routingKey: "dasda");

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "queue1", autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine($"Nomre: {Encoding.UTF8.GetString(e.Body.Span)}");
};

Console.Read();
