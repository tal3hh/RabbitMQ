using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
//Uri appsetting.json'da saxlamaq daha uygundur.
factory.Uri = new Uri("amqps://bheuyprq:o4Ro9yF4IFODJAGCQS-Sctz9LqeKf3hW@lionfish.rmq.cloudamqp.com/bheuyprq");

//Baglanti yaradilir.
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Exchangenin adini ve tipini Producerdeki ile eyni yazririq
channel.ExchangeDeclare(exchange: "header-test", type: ExchangeType.Headers);


//Quyruq yaradib gelen dataalri bu quyruga bind edeciyik.
string queueName = channel.QueueDeclare().QueueName;


//Dictionary'ya uygun olan quyrugu bind edirik."all" publis edilen quyruqdaki datalarin hamsi eyni olmalidir artiq key/value olmamalidir,
//"any"de ise gelen key/value ile eyni olsa kifayet edir, artiq olsada olar. 
channel.QueueBind(exchange: "header-test",
                 queue: queueName,
                 routingKey: string.Empty,
                 arguments: new Dictionary<string, object>
                 {
                     //*Datalar bura gelmiyecek
                     ["x-match"] = "all",
                     ["key1"] = "1",
                     ["key2"] = "2",
                     ["key3"] = "3"
                 });


EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine("Gelen mesaj: " + Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();