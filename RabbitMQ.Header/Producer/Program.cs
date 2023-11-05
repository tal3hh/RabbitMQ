using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
//Uri appsetting.json'da saxlamaq daha uygundur.
factory.Uri = new Uri("amqps://bheuyprq:o4Ro9yF4IFODJAGCQS-Sctz9LqeKf3hW@lionfish.rmq.cloudamqp.com/bheuyprq");

//Baglanti yaradilir.
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Exchangenin adini ve tipini yaziriq.
channel.ExchangeDeclare(exchange: "header-test", type: ExchangeType.Headers);

while (true)
{
    //Mesaj gondermek.
    Console.Write("Gonderilecek Mesaj: ");
    string message = Console.ReadLine();
    byte[] data = Encoding.UTF8.GetBytes(message);

    //Header exch. tipinde, Dictionary olaraq data gonderirik.
    IBasicProperties prop = channel.CreateBasicProperties();
    prop.Headers = new Dictionary<string, object>
    {
        ["key1"] = "1",
        ["key2"] = "2"
    };

    //RoutingKey hec bir ehemiyyeti yoxdur deye bos kecsekde olar.
    channel.BasicPublish(exchange: "header-test",
                         routingKey: string.Empty,
                         basicProperties: prop,
                         body: data);
}

Console.Read();