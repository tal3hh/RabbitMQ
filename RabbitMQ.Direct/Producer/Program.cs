using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
//Uri appsetting.json'da saxlamaq daha uygundur.
factory.Uri = new Uri("amqps://bheuyprq:o4Ro9yF4IFODJAGCQS-Sctz9LqeKf3hW@lionfish.rmq.cloudamqp.com/bheuyprq");

//Baglanti yaradilir.
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Exchangenin adini ve tipini yaziriq.
channel.ExchangeDeclare(exchange: "direct-test", type: ExchangeType.Direct);

//Mesaj gondermek.
Console.Write("Ad: ");
string name = Console.ReadLine();
Console.Write("Yas: ");
int age = Convert.ToInt32(Console.ReadLine());

var testDto = new TestDto { Name = name, Age = age };
var json = JsonConvert.SerializeObject(testDto);
var byteMessage = Encoding.UTF8.GetBytes(json);

//Burda routingKey sayesinde hansi quyruga gedeceyine qerar verilir.
channel.BasicPublish(exchange: "direct-test", 
                     routingKey: "direct-queue", 
                     body: byteMessage);

Console.Read();
class TestDto
{
    public string? Name { get; set; }
    public int Age { get; set; }
}
