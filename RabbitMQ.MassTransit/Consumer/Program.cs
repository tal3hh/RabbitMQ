using MassTransit;
using Shared.Message;

string rabbitMqUri = "amqps://ejozbhhq:VHp2s9RFbw07iS9uex7nHOyNNiVmZVnk@lionfish.rmq.cloudamqp.com/ejozbhhq";
string queueName = "example-queue";

//1.Addim
var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);

    //2.Addim
    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<CustomConsumerMessage>();
    });
});
//3.Addim
await bus.StartAsync();

Console.Read();

// --------------------------------------------------------------------------------------
public class CustomConsumerMessage : IConsumer<IMessage>
{
    public Task Consume(ConsumeContext<IMessage> context)
    {
        Console.WriteLine("Gelen mesaj: " + context.Message.Text);

        return Task.CompletedTask;
    }
}

//1.Addim: Masstransiti configure edirik.
//2.Addim: Bu methodla 'queueName' bu quyruqdan gelen mesaji 'CustomConsumerMessage' bu classda qebul edib uygun isleri gore bilerik.
//3.Addim: bus.StartAsync() etmesek islemeyecekdir.