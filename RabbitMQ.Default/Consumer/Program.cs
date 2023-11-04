using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
//Uri appsetting.json'da saxlamaq daha uygundur.
factory.Uri = new Uri("amqps://bheuyprq:o4Ro9yF4IFODJAGCQS-Sctz9LqeKf3hW@lionfish.rmq.cloudamqp.com/bheuyprq");

//Baglanti yaradilir.
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue yaradiriq. Producer terefdeki ile eyni olmalidir.
channel.QueueDeclare(queue: "default-queue", exclusive: false);

//Mesaji/Datani almaq
EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queue: "default-queue", autoAck: true, consumer: consumer);


consumer.Received += Consumer_Received;
void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{   
    //Mesaj byte tipinde gelir ve onu stringe stringe yaxud gonderilen mesaja gore dto,modele cevirib uygun isleri gore bilerik. 
    var byteMessage = e.Body.Span;
    string data = Encoding.UTF8.GetString(byteMessage);

    Console.WriteLine("Gelen Mesaj: " + data);
}

Console.Read();