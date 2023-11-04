using MassTransit;
using Shared.Message;

string rabbitMqUri = "amqps://ejozbhhq:VHp2s9RFbw07iS9uex7nHOyNNiVmZVnk@lionfish.rmq.cloudamqp.com/ejozbhhq";
string queueName = "example-queue";

// 1.Addim
var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);
});

// 2.Addim
var sendEndPoint = await bus.GetSendEndpoint(new($"{rabbitMqUri}/{queueName}"));

Console.Write("Gonderilecek mesaj: ");
string message = Console.ReadLine();

// 3.Addim
await sendEndPoint.Send<IMessage>(new TestMessage
{
    Text = message
});

Console.Read();


//1 Addim: MassTransit'i configure edirik.
//2.Addim: Mesaji hansi adresse gondereciyikse onu yaziriq. BasicPublis kimi dusunmek olar ama ferqleri var.
//3.Addim: Bu methodla mesaji gonderirik, mesajin tipi "IMessage", class ise (bunu dto,model kimi dusune bilerik) "TestMessage". IMessageni istifade etmek ucun Shared Library ref almaq lazimdir.