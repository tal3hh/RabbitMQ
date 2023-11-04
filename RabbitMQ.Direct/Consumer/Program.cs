using Newtonsoft.Json;
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
channel.ExchangeDeclare(exchange: "direct-test", type: ExchangeType.Direct);

//Producer terefinden gonderilen quyruqdaki datani, oz yaratdigimiz quyruga yonlendirerek istifade edirik.
channel.QueueDeclare(queue:"test1", exclusive:false);

//Bu routingKeydeki  datalari, yuxarida yeni yaratdigimiz quyruga gonderirik.
channel.QueueBind(queue: "test1",
                  exchange: "direct-test",
                  routingKey: "direct-queue");

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queue: "test1", autoAck: true, consumer: consumer);

consumer.Received += (model, ea) =>
{
    var byteData = ea.Body.Span;
    string stringData = Encoding.UTF8.GetString(byteData);
    var dto = JsonConvert.DeserializeObject<TestDto>(stringData);

    Console.WriteLine($"Ad: {dto.Name}, Yas: {dto.Age}");
};

Console.Read();


class TestDto
{
    public string? Name { get; set; }
    public int Age { get; set; }
}
